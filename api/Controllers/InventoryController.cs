using System.Data.Common;
using api.Data;
using api.DTO;
using api.Models;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Data.SqlClient;
using Microsoft.EntityFrameworkCore;

namespace api.Controllers
{
    [Authorize(Policy = "ViewInventory")]
    [Route("api/[controller]")]
    [ApiController]
    public class InventoryController : ControllerBase
    {
        private readonly ApplicationDbContext _context;

        public InventoryController(ApplicationDbContext context)
        {
            _context = context;
        }

        [HttpGet]
        [Route("GetItemSummaries")]
        public async Task<IActionResult> GetItemSummaries()
        {
            try
            {
                var itemSummaries = await _context.Items
                .OrderBy(e => e.SerialNumber)
                .Select(e => new ItemSummaryDto(
                    e.Id,
                    e.Active,
                    e.SerialNumber,
                    e.ItemType.Name,
                    e.Model
                )
                {
                    AssignedToFullName = e.AssignedTo != null ? e.AssignedTo.LastName + ", " + e.AssignedTo.FirstName : null
                })
                .ToListAsync();

                return Ok(itemSummaries);
            }
            catch (SqlException sqlEx)
            {
                return StatusCode(503, new
                {
                    message = "The database is currently unavailable. Please try again later.",
                    error = sqlEx.Message
                });
            }
            catch (DbException dbEx)
            {
                return StatusCode(503, new { message = "Database failure.", error = dbEx.Message });
            }
            catch (TimeoutException)
            {
                return StatusCode(504, new { message = "Database request timed out." });
            }
            catch (Exception ex)
            {
                // Fallback for any other unknown issues
                return StatusCode(500, new { message = "An unexpected error occurred.", error = ex.Message });
            }
        }

        [HttpGet]
        [Route("GetDetailedItem")]
        public async Task<IActionResult> GetDetailedItem(Guid id)
        {
            try
            {
                var detailedItem = await _context.Items
                    .Where(e => e.Id == id)
                    .Select(e => new DetailedItemDto(
                        e.Id,
                        e.Active,
                        e.SerialNumber,
                        e.ItemType.Name,
                        e.Model
                    )
                    {
                        AssignedToFullName = e.AssignedTo != null ? e.AssignedTo.FirstName + " " + e.AssignedTo.LastName : null,
                        Notes = e.Notes
                    })
                    .FirstOrDefaultAsync();

                // If item was not found
                if (detailedItem == null)
                {
                    return NotFound($"Cannot get item. Item (id:{id}) was not found. Please check id sent and try again");
                }

                return Ok(detailedItem);
            }
            catch (SqlException sqlEx)
            {
                return StatusCode(503, new
                {
                    message = "The database is currently unavailable. Please try again later.",
                    error = sqlEx.Message
                });
            }
            catch (DbException dbEx)
            {
                return StatusCode(503, new { message = "Database failure.", error = dbEx.Message });
            }
            catch (TimeoutException)
            {
                return StatusCode(504, new { message = "Database request timed out." });
            }
            catch (Exception ex)
            {
                // Fallback for any other unknown issues
                return StatusCode(500, new { message = "An unexpected error occurred.", error = ex.Message });
            }
        }

        [Authorize(Policy = "EditInventory")]
        [HttpGet]
        [Route("GetItemTypes")]
        public async Task<IActionResult> GetItemTypes()
        {
            try
            {
                var itemTypes = await _context.ItemTypes
                    .OrderBy(e => e.Name)
                    .Select(e => new ItemTypeNameDto(
                        e.Id,
                        e.Name
                    ))
                    .ToListAsync();

                return Ok(itemTypes);
            }
            catch (SqlException sqlEx)
            {
                return StatusCode(503, new
                {
                    message = "The database is currently unavailable. Please try again later.",
                    error = sqlEx.Message
                });
            }
            catch (DbException dbEx)
            {
                return StatusCode(503, new { message = "Database failure.", error = dbEx.Message });
            }
            catch (TimeoutException)
            {
                return StatusCode(504, new { message = "Database request timed out." });
            }
            catch (Exception ex)
            {
                // Fallback for any other unknown issues
                return StatusCode(500, new { message = "An unexpected error occurred.", error = ex.Message });
            }
        }
        
        [Authorize(Policy = "EditInventory")]
        [HttpPost]
        [Route("AddItem")]
        public async Task<IActionResult> AddItem([FromBody] NewItemDto newItem)
        {
            var item = new Item
            {
                Id = Guid.NewGuid(),
                Active = newItem.Active,
                SerialNumber = newItem.SerialNumber,
                ItemTypeId = newItem.ItemTypeId,
                Model = newItem.Model,
                Notes = newItem.Notes
            };

            // Add item
            _context.Items.Add(item);

            try
            {
                // Save changes
                await _context.SaveChangesAsync();
                return CreatedAtAction(nameof(GetDetailedItem), item.Id, item);
            }
            catch (DbUpdateException ex) when (ex.InnerException is SqlException sqlEx)
            {
                if (sqlEx.Number == 2627 || sqlEx.Number == 2601)
                {
                    // 2627: Violation of PRIMARY KEY or UNIQUE constraint
                    // 2601: Cannot insert duplicate key row in object
                    return Conflict(new { message = "An item with the same serial number already exists. Please enter a different serial number and try again." });
                }

                return StatusCode(500, "Database error occurred when trying to add item that is not accounted for. Please contact system administrator if the problem continues.");
            }
            catch (SqlException sqlEx)
            {
                return StatusCode(503, new
                {
                    message = "The database is currently unavailable. Please try again later.",
                    error = sqlEx.Message
                });
            }
            catch (DbException dbEx)
            {
                return StatusCode(503, new { message = "Database failure.", error = dbEx.Message });
            }
            catch (TimeoutException)
            {
                return StatusCode(504, new { message = "Database request timed out." });
            }
            catch (Exception ex)
            {
                // Fallback for any other unknown issues
                return StatusCode(500, new { message = "An unexpected error occurred.", error = ex.Message });
            }
        }

        [Authorize(Policy = "EditInventory")]
        [HttpPut]
        [Route("UpdateItem")]
        public async Task<IActionResult> UpdateItem([FromBody] EditItemDto updatedItem)
        {
            try
            {
                // Find item by id
                var item = await _context.Items.FirstOrDefaultAsync(e => e.Id == updatedItem.Id);

                // If item was not found
                if (item == null)
                {
                    return NotFound($"Cannot update item. Item (id:{updatedItem.Id}) was not found. Please check id sent and try again.");
                }

                // Update item
                item.Active = updatedItem.Active;
                item.SerialNumber = updatedItem.SerialNumber;
                item.ItemTypeId = updatedItem.ItemTypeId;
                item.Model = updatedItem.Model;
                item.AssignedToId = updatedItem.AssignedToId;
                item.Notes = updatedItem.Notes;

                // Save changes
                await _context.SaveChangesAsync();
                return NoContent();
            }
            catch (DbUpdateException ex) when (ex.InnerException is SqlException sqlEx)
            {
                if (sqlEx.Number == 2627 || sqlEx.Number == 2601)
                {
                    // 2627: Violation of PRIMARY KEY or UNIQUE constraint
                    // 2601: Cannot insert duplicate key row in object
                    return Conflict(new { message = "An item with the same serial number already exists. Please enter a different serial number and try again." });
                }

                return StatusCode(500, "Database error occurred when trying to update the item that is not accounted for. Please contact system administrator if the problem continues.");
            }
            catch (SqlException sqlEx)
            {
                return StatusCode(503, new
                {
                    message = "The database is currently unavailable. Please try again later.",
                    error = sqlEx.Message
                });
            }
            catch (DbException dbEx)
            {
                return StatusCode(503, new { message = "Database failure.", error = dbEx.Message });
            }
            catch (TimeoutException)
            {
                return StatusCode(504, new { message = "Database request timed out." });
            }
            catch (Exception ex)
            {
                // Fallback for any other unknown issues
                return StatusCode(500, new { message = "An unexpected error occurred.", error = ex.Message });
            }
        }

        [Authorize(Policy = "EditInventory")]
        [HttpPost]
        [Route("AddItemEvent")]
        public async Task<IActionResult> AddItemEvent([FromBody] NewItemEventDto newItemEvent)
        {
            var itemEvent = new ItemEvent
            {
                Id = Guid.NewGuid(),
                ItemId = newItemEvent.ItemId,
                EmployeeId = newItemEvent.EmployeeId,
                EventTypeId = newItemEvent.EventTypeId,
                DateTime = newItemEvent.DateTime,
                Reason = newItemEvent.Reason
            };

            // Add item event
            _context.ItemEvents.Add(itemEvent);

            try
            {
                // Save changes
                await _context.SaveChangesAsync();
                return CreatedAtAction(nameof(GetDetailedItem), itemEvent.ItemId, itemEvent);
            }
            catch (DbUpdateException ex) when (ex.InnerException is SqlException sqlEx)
            {
                return StatusCode(500, "Database error occurred when trying to add item event that is not accounted for. Please contact system administrator if the problem continues.");
            }
            catch (SqlException sqlEx)
            {
                return StatusCode(503, new
                {
                    message = "The database is currently unavailable. Please try again later.",
                    error = sqlEx.Message
                });
            }
            catch (DbException dbEx)
            {
                return StatusCode(503, new { message = "Database failure.", error = dbEx.Message });
            }
            catch (TimeoutException)
            {
                return StatusCode(504, new { message = "Database request timed out." });
            }
            catch (Exception ex)
            {
                // Fallback for any other unknown issues
                return StatusCode(500, new { message = "An unexpected error occurred.", error = ex.Message });
            }
        }
    }
}

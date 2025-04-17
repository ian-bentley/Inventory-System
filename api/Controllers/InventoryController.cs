using api.Data;
using api.Models;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
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
        [Route("GetItems")]
        public async Task<IActionResult> GetItems()
        {
            // Get all items, their type data, their assigned data, and their events
            return Ok( await _context.Items
                .Include(item => item.ItemType)
                .Include(item => item.AssignedTo)
                .Include(item => item.ItemEvents)
                .ThenInclude(itemEvent => itemEvent.Employee)
                .Include(item => item.ItemEvents)
                .ThenInclude(itemEvent => itemEvent.EventType)
                .ToListAsync());
        }

        [HttpGet]
        [Route("GetItem")]
        public async Task<IActionResult> GetItem(int id)
        {
            // Get item by id, its type data, its assigned data, and its events
            var item = await _context.Items.Include(item => item.ItemType)
                .Include(item => item.AssignedTo)
                .Include(item => item.ItemEvents)
                .ThenInclude(itemEvent => itemEvent.Employee)
                .Include(item => item.ItemEvents)
                .ThenInclude(itemEvent => itemEvent.EventType).FirstOrDefaultAsync(item => item.Id == id);

            // If item was not found
            if (item == null)
            {
                return NotFound($"Cannot get item. Item (id:{id}) was not found. Please check id sent and try again");
            }

            return Ok(item);
        }
        
        [Authorize(Policy = "EditInventory")]
        [HttpPost]
        [Route("AddItem")]
        public async Task<IActionResult> AddItem([FromBody] Item item)
        {
            // Add item
            _context.Items.Add(item);

            // Save changes
            await _context.SaveChangesAsync();

            return Ok("Item added.");
        }

        [Authorize(Policy = "EditInventory")]
        [HttpPut]
        [Route("UpdateItem")]
        public async Task<IActionResult> UpdateItem([FromBody] Item updatedItem)
        {
            // Find item by id
            var item = await _context.Items.FindAsync(updatedItem.Id);

            // If item was not found
            if (item == null)
            {
                return NotFound($"Cannot update item. Item (id:{updatedItem.Id}) was not found. Please check id sent and try again.");
            }

            // Update item
            _context.Entry(item).CurrentValues.SetValues(updatedItem);

            // Save changes
            await _context.SaveChangesAsync();

            return Ok("Item updated.");
        }

        [Authorize(Policy = "EditInventory")]
        [HttpPost]
        [Route("AddItemEvent")]
        public async Task<IActionResult> AddItemEvent([FromBody] ItemEvent itemEvent)
        {
            // Add item event
            _context.Add(itemEvent);

            // Save changes
            await _context.SaveChangesAsync();

            return Ok("Item event added.");
        }
    }
}

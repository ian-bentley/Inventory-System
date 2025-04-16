using System.Data;
using api.Data;
using api.Models;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Data.SqlClient;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;

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
            return Ok( await _context.Items.Include(e => e.ItemType).ToListAsync());
        }

        [HttpGet]
        [Route("GetItem")]
        public async Task<IActionResult> GetItem(int id)
        {
            var item = await _context.Items.FindAsync(id);
            if (item == null)
            {
                return NotFound();
            }
            return Ok(item);
        }

        [Authorize(Policy = "EditInventory")]
        [HttpPost]
        [Route("AddItem")]
        public async Task<IActionResult> AddItem([FromBody] Item item)
        {
            _context.Items.Add(item);
            await _context.SaveChangesAsync();
            return Ok();
        }

        [Authorize(Policy = "EditInventory")]
        [HttpPut]
        [Route("UpdateItem")]
        public async Task<IActionResult> UpdateItem([FromBody] Item updatedItem)
        {
            var item = await _context.Items.FindAsync(updatedItem.Id);
            if (item == null)
            {
                return NotFound();
            }
            _context.Entry(item).CurrentValues.SetValues(updatedItem);
            await _context.SaveChangesAsync();
            return Ok();
        }
    }
}

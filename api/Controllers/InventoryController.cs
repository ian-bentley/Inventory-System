using System.Data;
using api.Models;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Data.SqlClient;
using Microsoft.Extensions.Configuration;

namespace api.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class InventoryController : ControllerBase
    {
        [HttpGet]
        [Route("GetItems")]
        public IActionResult GetItems()
        {
            return Ok();
        }

        [HttpGet]
        [Route("GetItem")]
        public IActionResult GetItem(int id)
        {
            return Ok();
        }

        [HttpPost]
        [Route("AddItem")]
        public IActionResult AddItem([FromBody] Item item)
        {
            return Ok();
        }

        [HttpPut]
        [Route("UpdateItem")]
        public IActionResult UpdateItem([FromBody] Item item)
        {
            return Ok();
        }
    }
}

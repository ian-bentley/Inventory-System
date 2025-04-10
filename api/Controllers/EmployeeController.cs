using System;
using System.Data;
using api.Models;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Data.SqlClient;

namespace api.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class EmployeeController : ControllerBase
    {
        [HttpGet]
        [Route("GetEmployees")]
        public IActionResult GetEmployees()
        {
            return Ok();
        }

        [HttpGet]
        [Route("GetEmployee")]
        public IActionResult GetEmployee(int id)
        {
            return Ok();
        }

        [HttpPost]
        [Route("AddEmployee")]
        public IActionResult AddEmployee([FromBody] Employee employee)
        {
            return Ok();
        }

        [HttpPut]
        [Route("UpdateEmployee")]
        public IActionResult UpdateEmployee([FromBody] Employee employee)
        {
            return Ok();
        }
    }
}

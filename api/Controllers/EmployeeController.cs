using api.Data;
using api.Models;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace api.Controllers
{
    [Authorize(Policy = "ViewEmployees")]
    [Route("api/[controller]")]
    [ApiController]
    public class EmployeeController : ControllerBase
    {
        private readonly ApplicationDbContext _context;

        public EmployeeController(ApplicationDbContext context)
        {
            _context = context;
        }

        [HttpGet]
        [Route("GetEmployees")]
        public async Task<IActionResult> GetEmployees()
        {
            // Get all employees, their department data, home address data, and assigned items
            //return Ok(await _context.Employees
            //    .Include(employee => employee.Department)
            //    .Include(employee => employee.Manager)
            //    .Include(employee => employee.HomeAddress)
            //    .ThenInclude(homeAddress => homeAddress.UsState)
            //    .Include(employee => employee.Items)
            //    .ThenInclude(item => item.ItemType)
            //    .ToListAsync());
            return StatusCode(500, "This endpoint is not set up yet.");
        }

        [HttpGet]
        [Route("GetEmployee")]
        public async Task<IActionResult> GetEmployee(Guid id)
        {
            //// Get employee by id, its department data, home address data, and assigned items
            //var employee = await _context.Employees
            //    .Include(employee => employee.Department)
            //    .Include(employee => employee.Manager)
            //    .Include(employee => employee.HomeAddress)
            //    .ThenInclude(homeAddress => homeAddress.UsState)
            //    .Include(employee => employee.Items)
            //    .ThenInclude(item => item.ItemType)
            //    .FirstOrDefaultAsync(employee => employee.Id == id);

            //// If employee was not found
            //if (employee == null)
            //{
            //    return NotFound($"Cannot get employee. Employee (id: {id}) was not found. Please check id sent and try again.");
            //}

            //return Ok(employee);
            return StatusCode(500, "This endpoint is not set up yet.");
        }

        [Authorize(Policy = "EditEmployees")]
        [HttpGet]
        [Route("GetDepartments")]
        public async Task<IActionResult> GetDepartments()
        {
            return Ok(await _context.Departments.ToListAsync());
        }

        [Authorize(Policy = "EditEmployees")]
        [HttpGet]
        [Route("GetUsStates")]
        public async Task<IActionResult> GetUsStates()
        {
            return Ok(await _context.UsStates.ToListAsync());
        }

        [Authorize(Policy = "EditEmployees")]
        [HttpPost]
        [Route("AddEmployee")]
        public async Task<IActionResult> AddEmployee([FromBody] Employee employee)
        {
            // Add employee
            _context.Employees.Add(employee);

            // Save changes
            await _context.SaveChangesAsync();

            return Ok("Employee added.");
        }

        [Authorize(Policy = "EditEmployees")]
        [HttpPut]
        [Route("UpdateEmployee")]
        public async Task<IActionResult> UpdateEmployee([FromBody] Employee updatedEmployee)
        {
            //// Find employee by id
            //var employee = await _context.Employees
            //    .Include(e => e.HomeAddress)
            //    .FirstOrDefaultAsync(e => e.Id == updatedEmployee.Id);

            //// If employee was not found
            //if (employee == null)
            //{
            //    return NotFound($"Cannot update employee. Employee (id:{updatedEmployee.Id}) was not found. Please check id sent and try again.");
            //}

            //// Update employee
            //_context.Entry(employee).CurrentValues.SetValues(updatedEmployee);

            //// If the HomeAddress exists in both DB and input
            //if (employee.HomeAddress != null && updatedEmployee.HomeAddress != null)
            //{
            //    _context.Entry(employee.HomeAddress).CurrentValues.SetValues(updatedEmployee.HomeAddress);
            //}
            //else if (employee.HomeAddress == null && updatedEmployee.HomeAddress != null)
            //{
            //    // If employee has no address yet, add the new one
            //    employee.HomeAddress = updatedEmployee.HomeAddress;
            //}

            //// Save changes
            //await _context.SaveChangesAsync();

            //return Ok("Employee updated.");
            return StatusCode(500, "This endpoint is not set up yet.");
        }
    }
}

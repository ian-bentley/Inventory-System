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
        [Route("GetEmployeeNames")]
        public async Task<IActionResult> GetEmployeeNames()
        {
            try
            {
                var employeeNames = await _context.Employees
                .OrderBy(e => e.LastName)
                .ThenBy(e => e.FirstName)
                .Select(e => new EmployeeNameDto(
                    e.Id,
                    e.FirstName,
                    e.LastName
                ))
                .ToListAsync();

                return Ok(employeeNames);
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
        [Route("GetEmployeeSummaries")]
        public async Task<IActionResult> GetEmployeeSummaries()
        {
            try
            {
                var employeeSummaries = await _context.Employees
                .OrderBy(e => e.LastName)
                .ThenBy(e => e.FirstName)
                .Select(e => new EmployeeSummaryDto(
                    e.Id,
                    e.Active,
                    e.EmployeeNumber,
                    e.FirstName,
                    e.LastName
                ))
                .ToListAsync();

                return Ok(employeeSummaries);
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
        [Route("GetDetailedEmployee")]
        public async Task<IActionResult> GetDetailedEmployee(Guid id)
        {
            try
            {
                var detailedEmployee = await _context.Employees
                .Where(e => e.Id == id)
                .Select(e => new DetailedEmployeeDto(
                    e.Id,
                    e.Active,
                    e.EmployeeNumber,
                    e.FirstName,
                    e.LastName,
                    e.Department.Name,
                    e.Title,
                    e.EmployeeAddressLink.HomeAddress.Street1,
                    e.EmployeeAddressLink.HomeAddress.City,
                    e.EmployeeAddressLink.HomeAddress.Zip,
                    e.EmployeeAddressLink.HomeAddress.UsState.Initials
                )
                {
                    Notes = e.Notes,
                    ManagerFullName = e.Manager != null ? e.Manager.FirstName + " " + e.Manager.LastName : null,
                    AddressStreet2 = e.EmployeeAddressLink.HomeAddress.Street2,
                    AssignedItems = e.Items
                        .Where(e => e.AssignedToId == e.Id)
                        .Select(e => new AssignedItemDto(
                            e.SerialNumber,
                            e.ItemType.Name,
                            e.Model
                        ))
                        .ToList()
                })
                .FirstOrDefaultAsync();

                // If employee was not found
                if (detailedEmployee == null)
                {
                    return NotFound(new { message = $"Cannot get employee. Employee (id: {id}) was not found. Please send a valid id and try again." });
                }

                return Ok(detailedEmployee);
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

        [Authorize(Policy = "EditEmployees")]
        [HttpGet]
        [Route("GetDepartments")]
        public async Task<IActionResult> GetDepartments()
        {
            try
            {
                var departments = await _context.Departments
                .OrderBy(e => e.Name)
                .Select(e => new DepartmentNameDto(
                    e.Id,
                    e.Name
                ))
                .ToListAsync();

                return Ok(departments);
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

        [Authorize(Policy = "EditEmployees")]
        [HttpGet]
        [Route("GetUsStates")]
        public async Task<IActionResult> GetUsStates()
        {
            try
            {
                var usStates = await _context.UsStates
                .OrderBy(e => e.Initials)
                .Select(e => new UsStateInitialsDto(
                    e.Id,
                    e.Initials
                ))
                .ToListAsync();
                return Ok(usStates);
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

        [Authorize(Policy = "EditEmployees")]
        [HttpPost]
        [Route("AddEmployee")]
        public async Task<IActionResult> AddEmployee([FromBody] NewEmployeeDto newEmployee)
        {
            var employee = new Employee
            {
                Id = Guid.NewGuid(),
                Active = newEmployee.Active,
                EmployeeNumber = newEmployee.EmployeeNumber,
                FirstName = newEmployee.FirstName,
                LastName = newEmployee.LastName,
                Title = newEmployee.Title,
                DepartmentId = newEmployee.DepartmentId,
                ManagerId = newEmployee.ManagerId,
                Notes = newEmployee.Notes
            };

            var address = new HomeAddress
            {
                Id = Guid.NewGuid(),
                Street1 = newEmployee.AddressStreet1,
                Street2 = newEmployee.AddressStreet2,
                City = newEmployee.AddressCity,
                Zip = newEmployee.AddressZip,
                UsStateId = newEmployee.UsStateId
            };

            var employeeAddressLink = new EmployeeAddressLink
            {
                Id = Guid.NewGuid(),
                EmployeeId = employee.Id,
                AddressId = address.Id
            };

            _context.Employees.Add(employee);
            _context.HomeAddresses.Add(address);
            _context.EmployeeAddressLinks.Add(employeeAddressLink);

            try
            {
                await _context.SaveChangesAsync();
                return CreatedAtAction(nameof(GetDetailedEmployee), employee.Id, employee);
            }
            catch (DbUpdateException ex) when (ex.InnerException is SqlException sqlEx)
            {
                if (sqlEx.Number == 2627 || sqlEx.Number == 2601)
                {
                    // 2627: Violation of PRIMARY KEY or UNIQUE constraint
                    // 2601: Cannot insert duplicate key row in object
                    return Conflict(new { message = "An employee with the same employee number already exists. Please enter a different employee number and try again." });
                }

                return StatusCode(500, "Database error occurred when trying to add employee that is not accounted for. Please contact system administrator if the problem continues.");
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

        [Authorize(Policy = "EditEmployees")]
        [HttpPut]
        [Route("UpdateEmployee")]
        public async Task<IActionResult> UpdateEmployee([FromBody] EditEmployeeDto updatedEmployee)
        {
            try
            {
                var employee = await _context.Employees
                .Include(e => e.EmployeeAddressLink)
                .ThenInclude(e => e.HomeAddress)
                .FirstOrDefaultAsync(e => e.Id == updatedEmployee.Id);

                // If employee was not found
                if (updatedEmployee == null)
                {
                    return NotFound(new { message = $"Cannot update employee. Employee (id: {updatedEmployee.Id}) was not found. Please send a valid id and try again." });
                }

                employee.Active = updatedEmployee.Active;
                employee.EmployeeNumber = updatedEmployee.EmployeeNumber;
                employee.FirstName = updatedEmployee.FirstName;
                employee.LastName = updatedEmployee.LastName;
                employee.DepartmentId = updatedEmployee.DepartmentId;
                employee.Title = updatedEmployee.Title;
                employee.ManagerId = updatedEmployee.ManagerId;
                employee.Notes = updatedEmployee.Notes;

                var address = employee.EmployeeAddressLink.HomeAddress;

                if (address == null)
                {
                    return NotFound($"Cannot update employee. Address was not found. Please try again or contact the system administrator if the problem persists.");
                }

                address.Street1 = updatedEmployee.AddressStreet1;
                address.Street2 = updatedEmployee.AddressStreet2;
                address.City = updatedEmployee.AddressCity;
                address.Zip = updatedEmployee.AddressZip;
                address.UsStateId = updatedEmployee.UsStateId;

                await _context.SaveChangesAsync();
                return NoContent();
            }
            catch (DbUpdateException ex) when (ex.InnerException is SqlException sqlEx)
            {
                if (sqlEx.Number == 2627 || sqlEx.Number == 2601)
                {
                    // 2627: Violation of PRIMARY KEY or UNIQUE constraint
                    // 2601: Cannot insert duplicate key row in object
                    return Conflict(new { message = "An employee with the same employee number already exists. Please enter a different employee number and try again." });
                }

                return StatusCode(500, "Database error occurred when trying to update the employee that is not accounted for. Please contact system administrator if the problem continues.");
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

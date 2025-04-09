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
        [Route("GetAllEmployees")]
        public IActionResult GetAllEmployees()
        {
            string query = "select employee.*, department.department_name, manager.first_name as manager_first_name, manager.last_name as manager_last_name, home_address.street_1, home_address.street_2, home_address.city, home_address.state_id, us_state.state_initial, home_address.zip from employee inner join department on employee.department_id = department.id left join employee as manager on employee.manager_id = manager.id inner join home_address on employee.home_address_id = home_address.id inner join us_state on home_address.state_id = us_state.id";
            string sqlDatasource = "Server=tcp:portfolio-ian-bentley.database.windows.net,1433;Initial Catalog=inventoryDB;Persist Security Info=False;User ID=portfolioadmin;Password=Pasw*750243*;MultipleActiveResultSets=False;Encrypt=True;TrustServerCertificate=False;Connection Timeout=30;";
            DataTable dataTable = new DataTable();
            SqlDataReader sqlDataReader;
            using (SqlConnection sqlConnection = new SqlConnection(sqlDatasource))
            {
                sqlConnection.Open();
                using (SqlCommand sqlCommand = new SqlCommand(query, sqlConnection))
                {
                    sqlDataReader = sqlCommand.ExecuteReader();
                    dataTable.Load(sqlDataReader);
                    sqlDataReader.Close();
                    sqlConnection.Close();
                }
            }

            return new JsonResult(dataTable);
        }

        [HttpGet]
        [Route("GetEmployee")]
        public IActionResult GetEmployee(int id)
        {
            string query = "select employee.*, department.department_name, manager.first_name as manager_first_name, manager.last_name as manager_last_name, home_address.street_1, home_address.street_2, home_address.city, home_address.state_id, us_state.state_initial, home_address.zip from employee inner join department on employee.department_id = department.id left join employee as manager on employee.manager_id = manager.id inner join home_address on employee.home_address_id = home_address.id inner join us_state on home_address.state_id = us_state.id where id=@id";
            string sqlDatasource = "Server=tcp:portfolio-ian-bentley.database.windows.net,1433;Initial Catalog=inventoryDB;Persist Security Info=False;User ID=portfolioadmin;Password=Pasw*750243*;MultipleActiveResultSets=False;Encrypt=True;TrustServerCertificate=False;Connection Timeout=30;";
            DataTable dataTable = new DataTable();
            SqlDataReader sqlDataReader;
            using (SqlConnection sqlConnection = new SqlConnection(sqlDatasource))
            {
                sqlConnection.Open();
                using (SqlCommand sqlCommand = new SqlCommand(query, sqlConnection))
                {
                    sqlCommand.Parameters.AddWithValue("@id", id);
                    sqlDataReader = sqlCommand.ExecuteReader();
                    dataTable.Load(sqlDataReader);
                    sqlDataReader.Close();
                    sqlConnection.Close();
                }
            }

            return new JsonResult(dataTable);
        }

        [HttpPost]
        [Route("AddEmployee")]
        public IActionResult AddEmployee([FromBody] Employee employee)
        {
            string query = "insert into employee values(@active, @employee_number, @first_name, @last_name, @department_id, @title, @manager_id, @home_address_id)";
            string sqlDatasource = "Server=tcp:portfolio-ian-bentley.database.windows.net,1433;Initial Catalog=inventoryDB;Persist Security Info=False;User ID=portfolioadmin;Password=Pasw*750243*;MultipleActiveResultSets=False;Encrypt=True;TrustServerCertificate=False;Connection Timeout=30;";
            DataTable dataTable = new DataTable();
            SqlDataReader sqlDataReader;
            using (SqlConnection sqlConnection = new SqlConnection(sqlDatasource))
            {
                sqlConnection.Open();
                using (SqlCommand sqlCommand = new SqlCommand(query, sqlConnection))
                {
                    sqlCommand.Parameters.AddWithValue("@active", employee.Active);
                    sqlCommand.Parameters.AddWithValue("@employee_number", employee.EmployeeNumber);
                    sqlCommand.Parameters.AddWithValue("@first_name", employee.FirstName);
                    sqlCommand.Parameters.AddWithValue("@last_name", employee.LastName);
                    sqlCommand.Parameters.AddWithValue("@department_id", employee.DepartmentId);
                    sqlCommand.Parameters.AddWithValue("@title", employee.Title);
                    sqlCommand.Parameters.AddWithValue("@manager_id", employee.ManagerId);
                    sqlCommand.Parameters.AddWithValue("@home_address_id", employee.HomeAddressId);
                    sqlDataReader = sqlCommand.ExecuteReader();
                    dataTable.Load(sqlDataReader);
                    sqlDataReader.Close();
                    sqlConnection.Close();
                }
            }

            return new JsonResult("Added Successfully");
        }

        [HttpPost]
        [Route("UpdateEmployee")]
        public IActionResult UpdateEmployee([FromBody] Employee employee)
        {
            string query = "update item set active=@active, employee_number=@employee_number, first_name=@first_name, last_name=@last_name, department_id=@department_id, title=@title, manager_id=@manager_id, home_address_id=@home_address_id where id=@id";
            string sqlDatasource = "Server=tcp:portfolio-ian-bentley.database.windows.net,1433;Initial Catalog=inventoryDB;Persist Security Info=False;User ID=portfolioadmin;Password=Pasw*750243*;MultipleActiveResultSets=False;Encrypt=True;TrustServerCertificate=False;Connection Timeout=30;";
            DataTable dataTable = new DataTable();
            SqlDataReader sqlDataReader;
            using (SqlConnection sqlConnection = new SqlConnection(sqlDatasource))
            {
                sqlConnection.Open();
                using (SqlCommand sqlCommand = new SqlCommand(query, sqlConnection))
                {
                    sqlCommand.Parameters.AddWithValue("@id", employee.Id);
                    sqlCommand.Parameters.AddWithValue("@active", employee.Active);
                    sqlCommand.Parameters.AddWithValue("@employee_number", employee.EmployeeNumber);
                    sqlCommand.Parameters.AddWithValue("@first_name", employee.FirstName);
                    sqlCommand.Parameters.AddWithValue("@last_name", employee.LastName);
                    sqlCommand.Parameters.AddWithValue("@department_id", employee.DepartmentId);
                    sqlCommand.Parameters.AddWithValue("@title", employee.Title);
                    sqlCommand.Parameters.AddWithValue("@manager_id", employee.ManagerId);
                    sqlCommand.Parameters.AddWithValue("@home_address_id", employee.HomeAddressId);
                    sqlDataReader = sqlCommand.ExecuteReader();
                    dataTable.Load(sqlDataReader);
                    sqlDataReader.Close();
                    sqlConnection.Close();
                }
            }

            return new JsonResult("Updated Successfully");
        }

        [HttpDelete]
        [Route("DeleteEmployee")]
        public IActionResult DeleteEmployee(int id)
        {
            string query = "delete from employee where id=@id";
            string sqlDatasource = "Server=tcp:portfolio-ian-bentley.database.windows.net,1433;Initial Catalog=inventoryDB;Persist Security Info=False;User ID=portfolioadmin;Password=Pasw*750243*;MultipleActiveResultSets=False;Encrypt=True;TrustServerCertificate=False;Connection Timeout=30;";
            DataTable dataTable = new DataTable();
            SqlDataReader sqlDataReader;
            using (SqlConnection sqlConnection = new SqlConnection(sqlDatasource))
            {
                sqlConnection.Open();
                using (SqlCommand sqlCommand = new SqlCommand(query, sqlConnection))
                {
                    sqlCommand.Parameters.AddWithValue("@id", id);
                    sqlDataReader = sqlCommand.ExecuteReader();
                    dataTable.Load(sqlDataReader);
                    sqlDataReader.Close();
                    sqlConnection.Close();
                }
            }

            return new JsonResult("Deleted Successfully");
        }
    }
}

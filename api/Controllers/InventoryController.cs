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
        [Route("GetAllItems")]
        public IActionResult GetAllItems()
        {
            string query = "select * from dbo.item";
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
        [Route("GetItem")]
        public IActionResult GetItem(int id)
        {
            string query = "select * from dbo.item where id=@id";
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
        [Route("AddItem")]
        public IActionResult AddItem([FromBody] Item item)
        {
            string query = "insert into dbo.item values(@active, @snst, @itemTypeId, @model, @notes)";
            string sqlDatasource = "Server=tcp:portfolio-ian-bentley.database.windows.net,1433;Initial Catalog=inventoryDB;Persist Security Info=False;User ID=portfolioadmin;Password=Pasw*750243*;MultipleActiveResultSets=False;Encrypt=True;TrustServerCertificate=False;Connection Timeout=30;";
            DataTable dataTable = new DataTable();
            SqlDataReader sqlDataReader;
            using (SqlConnection sqlConnection = new SqlConnection(sqlDatasource))
            {
                sqlConnection.Open();
                using (SqlCommand sqlCommand = new SqlCommand(query, sqlConnection))
                {
                    sqlCommand.Parameters.AddWithValue("@active", item.Active);
                    sqlCommand.Parameters.AddWithValue("@snst", item.SnSt);
                    sqlCommand.Parameters.AddWithValue("@itemTypeId", item.ItemTypeId);
                    sqlCommand.Parameters.AddWithValue("@model", item.Model);
                    sqlCommand.Parameters.AddWithValue("@notes", item.Notes);
                    sqlDataReader = sqlCommand.ExecuteReader();
                    dataTable.Load(sqlDataReader);
                    sqlDataReader.Close();
                    sqlConnection.Close();
                }
            }

            return new JsonResult("Added Successfully");
        }

        [HttpPost]
        [Route("UpdateItem")]
        public IActionResult UpdateItem([FromBody] Item item)
        {

            string query = "update table dbo.item set active = @active, snst = @snst, item_type_id = @itemTypeId, model = @model, notes = @notes where id = @id";
            string sqlDatasource = "Server=tcp:portfolio-ian-bentley.database.windows.net,1433;Initial Catalog=inventoryDB;Persist Security Info=False;User ID=portfolioadmin;Password=Pasw*750243*;MultipleActiveResultSets=False;Encrypt=True;TrustServerCertificate=False;Connection Timeout=30;";
            DataTable dataTable = new DataTable();
            SqlDataReader sqlDataReader;
            using (SqlConnection sqlConnection = new SqlConnection(sqlDatasource))
            {
                sqlConnection.Open();
                using (SqlCommand sqlCommand = new SqlCommand(query, sqlConnection))
                {
                    sqlCommand.Parameters.AddWithValue("@id", item.Id);
                    sqlCommand.Parameters.AddWithValue("@active", item.Active);
                    sqlCommand.Parameters.AddWithValue("@snst", item.SnSt);
                    sqlCommand.Parameters.AddWithValue("@itemTypeId", item.ItemTypeId);
                    sqlCommand.Parameters.AddWithValue("@model", item.Model);
                    sqlCommand.Parameters.AddWithValue("@notes", item.Notes);
                    sqlDataReader = sqlCommand.ExecuteReader();
                    dataTable.Load(sqlDataReader);
                    sqlDataReader.Close();
                    sqlConnection.Close();
                }
            }

            return new JsonResult("Updated Successfully");
        }

        [HttpDelete]
        [Route("DeleteItem")]
        public IActionResult DeleteItem([FromBody] Item item)
        {
            string query = "delete from dbo.item where id=@id";
            string sqlDatasource = "Server=tcp:portfolio-ian-bentley.database.windows.net,1433;Initial Catalog=inventoryDB;Persist Security Info=False;User ID=portfolioadmin;Password=Pasw*750243*;MultipleActiveResultSets=False;Encrypt=True;TrustServerCertificate=False;Connection Timeout=30;";
            DataTable dataTable = new DataTable();
            SqlDataReader sqlDataReader;
            using (SqlConnection sqlConnection = new SqlConnection(sqlDatasource))
            {
                sqlConnection.Open();
                using (SqlCommand sqlCommand = new SqlCommand(query, sqlConnection))
                {
                    sqlCommand.Parameters.AddWithValue("@id", item.Id);
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

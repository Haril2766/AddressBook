using System.Data.SqlClient;
using System.Data;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Configuration;

namespace Address_Book.Controllers
{
    public class UserController : Controller
    {
        private IConfiguration configuration;

        public UserController(IConfiguration _configuration)
        {
            configuration = _configuration;
        }
        public IActionResult UserAdd()
        {
            return View();
        }
        public IActionResult UserList()
        {
            string connectionString = configuration.GetConnectionString("ConnectionString");

            using (SqlConnection connection = new SqlConnection(connectionString))
            {
                connection.Open();
                using (SqlCommand command = connection.CreateCommand())
                {
                    command.CommandType = CommandType.StoredProcedure;
                    command.CommandText = "[dbo].[PR_GetAllUsers]";

                    using (SqlDataReader reader = command.ExecuteReader())
                    {
                        DataTable table = new DataTable();
                        table.Load(reader);
                        return View(table);
                    }
                }
            }
        }
        public IActionResult UserDelete(int UserID)
        {
            try
            {
                string connectionString = this.configuration.GetConnectionString("ConnectionString");
                SqlConnection connection = new SqlConnection(connectionString);
                connection.Open();
                SqlCommand command = connection.CreateCommand();
                command.CommandType = CommandType.StoredProcedure;
                command.CommandText = "[dbo].[PR_DeleteUser]";
                command.Parameters.Add("@UserID", SqlDbType.Int).Value = UserID;
                command.ExecuteNonQuery();
            }
            catch (Exception ex)
            {
                TempData["ErrorMessage"] = ex.Message;
                Console.WriteLine(ex.ToString());
            }
            return RedirectToAction("UserList");
        }
    }
}

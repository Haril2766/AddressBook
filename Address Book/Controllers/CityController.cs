using Microsoft.AspNetCore.Mvc;
using System.Data.SqlClient;
using System.Data;
using Microsoft.Extensions.Configuration;

namespace Address_Book.Controllers
{
    public class CityController : Controller
    {
        private IConfiguration configuration;

        public CityController(IConfiguration _configuration)
        {
            configuration = _configuration;
        }
        public IActionResult CityAdd()
        {
            return View();
        }
        public IActionResult CityList()
        {
            string connectionString = configuration.GetConnectionString("ConnectionString");

            using (SqlConnection connection = new SqlConnection(connectionString))
            {
                connection.Open();
                using (SqlCommand command = connection.CreateCommand())
                {
                    command.CommandType = CommandType.StoredProcedure;
                    command.CommandText = "[dbo].[PR_GetAllCities]";

                    using (SqlDataReader reader = command.ExecuteReader())
                    {
                        DataTable table = new DataTable();
                        table.Load(reader);
                        return View(table);
                    }
                }
            }
        }
        public IActionResult CityDelete(int CityID)
        {
            try
            {
                string connectionString = this.configuration.GetConnectionString("ConnectionString");
                SqlConnection connection = new SqlConnection(connectionString);
                connection.Open();
                SqlCommand command = connection.CreateCommand();
                command.CommandType = CommandType.StoredProcedure;
                command.CommandText = "[dbo].[PR_DeleteCity]";
                command.Parameters.Add("@CityID", SqlDbType.Int).Value = CityID;
                command.ExecuteNonQuery();
            }
            catch (Exception ex)
            {
                TempData["ErrorMessage"] = ex.Message;
                Console.WriteLine(ex.ToString());
            }
            return RedirectToAction("CityList");
        }
    }
}

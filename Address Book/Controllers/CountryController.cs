using System.Data;
using Microsoft.AspNetCore.Mvc;
using System.Data.SqlClient;
using Address_Book.Models;

namespace Address_Book.Controllers
{
    public class CountryController : Controller
    {
        private IConfiguration configuration;

        public CountryController(IConfiguration _configuration)
        {
            configuration = _configuration;
        }
        public IActionResult CountryAdd()
        {
            return View();
        }
        public IActionResult CountryList()
        {
            string connectionString = configuration.GetConnectionString("ConnectionString");

            using (SqlConnection connection = new SqlConnection(connectionString))
            {
                connection.Open();
                using (SqlCommand command = connection.CreateCommand())
                {
                    command.CommandType = CommandType.StoredProcedure;
                    command.CommandText = "[dbo].[PR_GetAllCountries]";

                    using (SqlDataReader reader = command.ExecuteReader())
                    {
                        DataTable table = new DataTable();
                        table.Load(reader);
                        return View(table);
                    }
                }
            }
        }
        public IActionResult CountryDelete(int CountryID)
        {
            try
            {
                string connectionString = this.configuration.GetConnectionString("ConnectionString");
                SqlConnection connection = new SqlConnection(connectionString);
                connection.Open();
                SqlCommand command = connection.CreateCommand();
                command.CommandType = CommandType.StoredProcedure;
                command.CommandText = "[dbo].[PR_DeleteCountry]";
                command.Parameters.Add("@CountryID", SqlDbType.Int).Value = CountryID;
                command.ExecuteNonQuery();
            }
            catch (Exception ex)
            {
                TempData["ErrorMessage"] = ex.Message;
                Console.WriteLine(ex.ToString());
            }
            return RedirectToAction("CountryList");
        }

    }
}
using System.Data.SqlClient;
using System.Data;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Configuration;
using static AddressBook._2.Models.CityModel;
using System.Reflection;
using AddressBook._2.Models;
using OfficeOpenXml;

namespace AddressBook._2.Controllers
{
    public class CityController : Controller
    {
        public IConfiguration configuration;
        public CityController(IConfiguration _configuration)
        {
            configuration = _configuration;
        }

        #region City List
        public IActionResult CityList()
        {
            string connectionString = configuration.GetConnectionString("ConnectionString");
            SqlConnection connection = new SqlConnection(connectionString);
            connection.Open();
            SqlCommand command = connection.CreateCommand();
            command.CommandType = CommandType.StoredProcedure;
            command.CommandText = "PR_City_SelectAll";
            SqlDataReader reader = command.ExecuteReader();
            DataTable table = new DataTable();
            table.Load(reader);
            return View(table);
        }
        #endregion City List

        #region City Add
        public IActionResult CitySave(CityModel model)
        {
            if (ModelState.IsValid)
            {
                string connectionString = configuration.GetConnectionString("ConnectionString");
                SqlConnection sqlConnection = new SqlConnection(connectionString);
                sqlConnection.Open();
                SqlCommand command = sqlConnection.CreateCommand();
                command.CommandType = CommandType.StoredProcedure;
                if (model.CityId == 0)
                {
                    command.CommandText = "PR_City_Insert";
                }
                else
                {
                    command.CommandText = "PR_City_Update";
                    command.Parameters.Add("@CityId", SqlDbType.Int).Value = model.CityId;
                }
                command.Parameters.Add("@CityName", SqlDbType.VarChar).Value = model.CityName;
                command.Parameters.Add("@STDCode", SqlDbType.VarChar).Value = model.STDCode;
                command.Parameters.Add("@PinCode", SqlDbType.VarChar).Value = model.PinCode;
                command.Parameters.Add("@UserId", SqlDbType.Int).Value = model.UserId;
                command.Parameters.Add("@StateId", SqlDbType.Int).Value = model.StateId;
                command.Parameters.Add("@CountryId", SqlDbType.Int).Value = model.CountryId;
                command.ExecuteNonQuery();
                return RedirectToAction("CityList");
            }
            return View("CityAddEdit", model);
        }
        #endregion City Add

        #region City Edit
        public IActionResult CityAddEdit(int CityId)
        {
            string connectionString = configuration.GetConnectionString("ConnectionString");
            SqlConnection Connection = new SqlConnection(connectionString);
            Connection.Open();
            SqlCommand command = Connection.CreateCommand();
            command.CommandType = CommandType.StoredProcedure;
            command.CommandText = "PR_City_SelectById";
            command.Parameters.AddWithValue("@CityId", CityId);
            SqlDataReader reader = command.ExecuteReader();
            DataTable datatable = new DataTable();
            datatable.Load(reader);
            CityModel model = new CityModel();
            foreach (DataRow row in datatable.Rows)
            {
                model.CityName = @row["CityName"].ToString();
                model.STDCode = @row["STDCode"].ToString();
                model.PinCode = @row["PinCode"].ToString();
                model.UserId = Convert.ToInt32(@row["UserId"]);
                model.StateId = Convert.ToInt32(@row["StateId"]);
                model.CountryId = Convert.ToInt32(@row["CountryId"]);
            }
            return View("CityAddEdit", model);
        }
        #endregion City Edit

        #region City Delete
        public IActionResult CityDelete(int CityId)
        {
            try
            {
                string connectionString = configuration.GetConnectionString("ConnectionString");
                SqlConnection Connection = new SqlConnection(connectionString);
                Connection.Open();
                SqlCommand command = Connection.CreateCommand();
                command.CommandType = CommandType.StoredProcedure;
                command.CommandText = "PR_City_Delete";
                command.Parameters.AddWithValue("@CityId", CityId);
                command.ExecuteNonQuery();
            }
            catch (Exception ex)
            {
                TempData["ErrorMessage"] = ex.Message;
                Console.WriteLine(ex.ToString());
            }
            return RedirectToAction("CityList");
        }
        #endregion City Delete

        #region Execl Export


        public IActionResult ExportToExcel()
        {
            try
            {
                ExcelPackage.LicenseContext = LicenseContext.NonCommercial; // Required for EPPlus

                string connectionString = configuration.GetConnectionString("ConnectionString");

                using (SqlConnection sqlConnection = new SqlConnection(connectionString))
                {
                    sqlConnection.Open();

                    using (SqlCommand sqlCommand = sqlConnection.CreateCommand())
                    {
                        sqlCommand.CommandType = CommandType.StoredProcedure;
                        sqlCommand.CommandText = "PR_City_SelectAll"; // No parameters needed

                        using (SqlDataReader sqlDataReader = sqlCommand.ExecuteReader())
                        {
                            DataTable data = new DataTable();
                            data.Load(sqlDataReader);

                            using (var package = new ExcelPackage())
                            {
                                var worksheet = package.Workbook.Worksheets.Add("CityData");

                                // Add headers
                                worksheet.Cells[1, 1].Value = "CityID";
                                worksheet.Cells[1, 2].Value = "CityName";
                                worksheet.Cells[1, 3].Value = "STDCode";
                                //worksheet.Cells[1, 4].Value = "StateName";
                                worksheet.Cells[1, 5].Value = "CreationDate";

                                // Add data
                                int row = 2;
                                foreach (DataRow item in data.Rows)
                                {
                                    worksheet.Cells[row, 1].Value = item["CityID"];
                                    worksheet.Cells[row, 2].Value = item["CityName"];
                                    worksheet.Cells[row, 3].Value = item["STDCode"];
                                    //worksheet.Cells[row, 4].Value = item["StateName"];
                                    worksheet.Cells[row, 5].Value = Convert.ToDateTime(item["CreationDate"]).ToString("yyyy-MM-dd");
                                    row++;
                                }

                                // Convert package to stream
                                var stream = new MemoryStream();
                                package.SaveAs(stream);
                                stream.Position = 0;

                                string excelName = $"CityData-{DateTime.Now:yyyyMMddHHmmss}.xlsx";
                                return File(stream, "application/vnd.openxmlformats-officedocument.spreadsheetml.sheet", excelName);
                            }
                        }
                    }
                }
            }
            catch (Exception ex)
            {
                TempData["ErrorMessage"] = "Error exporting data: " + ex.Message;
                return RedirectToAction("CityList");
            }
        }


        #endregion Execl Export
    }
}

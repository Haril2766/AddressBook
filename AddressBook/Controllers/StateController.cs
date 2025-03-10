using System.Data.SqlClient;
using System.Data;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Configuration;
using static AddressBook._2.Models.StateModel;
using System.Reflection;
using AddressBook._2.Models;

namespace AddressBook._2.Controllers
{
    public class StateController : Controller
    {
        public IConfiguration configuration;
        public StateController(IConfiguration _configuration)
        {
            configuration = _configuration;
        }

        #region State List
        public IActionResult StateList()
        {
            string connectionString = configuration.GetConnectionString("ConnectionString");
            SqlConnection connection = new SqlConnection(connectionString);
            connection.Open();
            SqlCommand command = connection.CreateCommand();
            command.CommandType = CommandType.StoredProcedure;
            command.CommandText = "PR_State_SelectAll";
            SqlDataReader reader = command.ExecuteReader();
            DataTable table = new DataTable();
            table.Load(reader);
            return View(table);
        }
        #endregion State List

        #region State Add
        public IActionResult StateSave(StateModel model)
        {
            if (ModelState.IsValid)
            {
                string connectionString = configuration.GetConnectionString("ConnectionString");
                SqlConnection sqlConnection = new SqlConnection(connectionString);
                sqlConnection.Open();
                SqlCommand command = sqlConnection.CreateCommand();
                command.CommandType = CommandType.StoredProcedure;
                if (model.StateId == 0)
                {
                    command.CommandText = "PR_State_Insert";
                }
                else
                {
                    command.CommandText = "PR_State_Update";
                    command.Parameters.Add("@StateId", SqlDbType.Int).Value = model.StateId;
                }
                command.Parameters.Add("@StateName", SqlDbType.VarChar).Value = model.StateName;
                command.Parameters.Add("@StateCode", SqlDbType.VarChar).Value = model.StateCode;
                command.Parameters.Add("@UserId", SqlDbType.Int).Value = model.UserId;
                command.Parameters.Add("@CountryId", SqlDbType.Int).Value = model.CountryId;
                command.ExecuteNonQuery();
                return RedirectToAction("StateList");
            }
            return View("StateAddEdit", model);
        }
        #endregion State Add

        #region State Edit
        public IActionResult StateAddEdit(int StateId)
        {
            string connectionString = configuration.GetConnectionString("ConnectionString");
            SqlConnection Connection = new SqlConnection(connectionString);
            Connection.Open();
            SqlCommand command = Connection.CreateCommand();
            command.CommandType = CommandType.StoredProcedure;
            command.CommandText = "PR_State_SelectById";
            command.Parameters.AddWithValue("@StateId", StateId);
            SqlDataReader reader = command.ExecuteReader();
            DataTable datatable = new DataTable();
            datatable.Load(reader);
            StateModel model = new StateModel();
            foreach (DataRow row in datatable.Rows)
            {
                model.StateName = @row["StateName"].ToString();
                model.StateCode = @row["StateCode"].ToString();
                model.UserId = Convert.ToInt32(@row["UserId"]);
            }
            return View("StateAddEdit", model);
        }
        #endregion State Edit

        #region State Delete
        public IActionResult StateDelete(int StateId)
        {
            try
            {
                string connectionString = configuration.GetConnectionString("ConnectionString");
                SqlConnection Connection = new SqlConnection(connectionString);
                Connection.Open();
                SqlCommand command = Connection.CreateCommand();
                command.CommandType = CommandType.StoredProcedure;
                command.CommandText = "PR_State_Delete";
                command.Parameters.AddWithValue("@StateId", StateId);
                command.ExecuteNonQuery();
            }
            catch (Exception ex)
            {
                TempData["ErrorMessage"] = ex.Message;
                Console.WriteLine(ex.ToString());
            }
            return RedirectToAction("StateList");
            }
        #endregion Country Delete
    }
}

using System.Data;
using System.Net;
using Azure;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Data.SqlClient;
using WebApplication1.Models;

namespace WebApplication1.Controllers
{
    [Route("[controller]")]
    public class testController : Controller
    {
        string _connectionString = "Server=DESKTOP-DLIH9Q9;Database=UserManagement;Integrated Security=True;TrustServerCertificate=True;";
        public IActionResult Index()
        {
            return View();
        }

        [HttpPost("SaveUserInfo")]
        public IActionResult SaveUserInfo([FromBody] UserInfo info)
        {
           

            SqlConnection conn = new SqlConnection(_connectionString);

            SqlCommand cmd = new SqlCommand("PRC_USERINFO_CREATE", conn);

            cmd.CommandType = CommandType.StoredProcedure;

            // Add parameters
            cmd.Parameters.AddWithValue("@UserName", info.UserName);
            cmd.Parameters.AddWithValue("@Password", info.Password);
            cmd.Parameters.AddWithValue("@Address", info.Address);
            cmd.Parameters.AddWithValue("@DateOfBirth", info.DateofBirth);
            cmd.Parameters.AddWithValue("@Department", info.Department);

            // Open connection and execute
            conn.Open();
            int response = cmd.ExecuteNonQuery();


            return Ok(response);
        }

        [HttpGet("getuserinfo")]
        public IActionResult GetUserInfo()
        {

            Sample sm = new Sample(_connectionString);
            Sample sms = new Sample(_connectionString, "asdasdada");

            int a = sm.add();

            List<UserInfo> users = new List<UserInfo>();

            SqlConnection connection = new SqlConnection(_connectionString);

            connection.Open();

            SqlDataAdapter dataAdapter = new SqlDataAdapter("PRC_USERINFO_GET", connection);


            DataTable dataTable = new DataTable();
            dataAdapter.Fill(dataTable);

            for (int i = 0; i < dataTable.Rows.Count; i++)
            {
                DataRow row = dataTable.Rows[i];

                UserInfo user = new UserInfo
                {
                    UserName = row["Username"].ToString(),
                    Password = row["Password"].ToString(),
                    Address = row["Address"].ToString(),
                    DateofBirth = (DateTime)row["DateOfBirth"],
                    Department = Convert.ToInt32(row["Department"])
                };

                users.Add(user);
            }

            return Ok(users);
        }
    }
}

using System;
using System.Collections.Generic;
using System.Diagnostics;
using MySql.Data.MySqlClient;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using dungeons_and_dragons_app.Models;
using Microsoft.Extensions.Options;
using Microsoft.AspNetCore.Mvc.Rendering;
using System.Data;
using System.Data.SqlClient;
using Microsoft.AspNetCore.Http;

namespace dungeons_and_dragons_app.Controllers
{
    public class AccountController : Controller
    {
        private readonly string connectionString;
        private readonly AppSetting appSetting;

        public AccountController(AppSetting appSettingIn)
        {
            connectionString = appSettingIn.ConnectionString;
            appSetting = appSettingIn;
        }

        [HttpPost]
        public IActionResult Login(string username, string hashPass)
        {
            if (username != null && hashPass != null)
            {
                using (MySqlConnection connection = new MySqlConnection(connectionString))
                {
                    try
                    {
                        connection.Open();
                        string query = "SELECT `UserID`, `Username` FROM `User` WHERE username = @username and hashPass=@hashPass";
                        MySqlCommand command = new MySqlCommand(query, connection);
                        command.Parameters.AddWithValue("@username", username);
                        command.Parameters.AddWithValue("@hashPass", hashPass);
                        MySqlDataReader dr = command.ExecuteReader();
                        while(dr.Read())
                        {
                            
                            HttpContext.Session.SetString("UserID", dr["UserID"].ToString());
                            HttpContext.Session.SetString("Username", dr["Username"].ToString());
                        }
                        connection.Close();
                        return RedirectToAction("Dashboard", "Home");
  
                    }
                    catch (MySqlException e)
                    {
                        Console.Write(e);
                    }
                    
                }
            }
                ViewBag.error = "Invalid Login Credentials";
                return View("~/Views/Home/Login.cshtml");
        }



        [Route("logout")]
        [HttpGet]
        public IActionResult Logout()
        {
            return RedirectToAction("Index");
        }


    }
}

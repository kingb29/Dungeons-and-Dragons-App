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

namespace dungeons_and_dragons_app.Controllers
{
    public class AccountController : Controller
    {
        private readonly string connectionString;

        public AccountController(AppSetting appSetting)
        {
            connectionString = appSetting.ConnectionString;
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
                        string query = "SELECT `HashPass`, `Username` FROM `User` WHERE username = @username and hashPass=@hashPass";
                        MySqlCommand command = new MySqlCommand(query, connection);
                        command.Parameters.AddWithValue("@username", username);
                        command.Parameters.AddWithValue("@hashPass", hashPass);
                        var count = command.ExecuteScalar();

                        if (count != null) // test if correct login details
                        {
                            // do stuff to make the user "signed in"
                            return View("~/Views/Home/Index.cshtml");
                        }

                        connection.Close();
                    }
                    catch (MySqlException e)
                    {
                        Console.Write(e);
                    }
                    
                }
            }
                ViewBag.error = "Invalid Account";
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

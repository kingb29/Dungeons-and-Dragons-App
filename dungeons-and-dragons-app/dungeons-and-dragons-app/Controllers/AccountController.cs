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
using System.Security.Cryptography;
using System.Text;
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
                            HttpContext.Session.SetString("Username", dr["UserName"].ToString());
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


        [HttpPost]
        public IActionResult Register(string username, string password, string email)
        {
            String salt = "";

            try
            {
                if (username.Length > 9 && password.Length > 9 && email.Length > 9) // check to make sure the information is long enough
                {
                    MySqlConnection connection = new MySqlConnection(connectionString); // check to see if username/email is taken yet

                    connection.Open();
                    string query = "SELECT `Email`, `Username` FROM `User` WHERE username = @username and email=@email";
                    MySqlCommand command = new MySqlCommand(query, connection);
                    command.Parameters.AddWithValue("@username", username);
                    command.Parameters.AddWithValue("@email", email);
                    var count = command.ExecuteScalar();

                    if (count != null) // if true the username/email exists in the database and they need to re-enter information
                    {
                        ViewBag.error = "This information is already in use.";
                        connection.Close();
                        return View("~/Views/Home/createAccount.cshtml");
                    }
                    else // the information has not been taken yet and an account can be made on the database
                    {
                        connection.Close(); // close previous connection

                        // open new connection for inserting data into the database
                        MySqlConnection connection2 = new MySqlConnection(connectionString);
                        salt = getSalt();
                        connection2.Open();
                        query = "INSERT INTO `User`(`Salt`, `HashPass`, `Username`, `Email`) VALUES(@salt,@hashPass,@username,@email)";
                        MySqlCommand command2 = new MySqlCommand(query, connection2);
                        command2.Parameters.AddWithValue("@salt", salt);
                        command2.Parameters.AddWithValue("@hashPass", password);
                        command2.Parameters.AddWithValue("@username", username);
                        command2.Parameters.AddWithValue("@email", email);
                        command2.ExecuteNonQuery();
                        ViewBag.error = "Successfully created your account " + username + "!";
                        return View("~/Views/Home/createAccount.cshtml");
                    }
                }
                else
                {
                    ViewBag.error = "Invalid Form, please check the requirements below.";
                    return View("~/Views/Home/createAccount.cshtml");
                }
            }catch(Exception e)
            {
                ViewBag.error = "Invalid Form, please check the requirements below.";
                return View("~/Views/Home/createAccount.cshtml");
            }
        }

        private static String getSalt()
        {
            var random = new RNGCryptoServiceProvider();

            int max_length = 32;

            byte[] salt = new byte[max_length];

            random.GetNonZeroBytes(salt);

            return Convert.ToBase64String(salt);
        }

        [Route("logout")]
        [HttpGet]
        public IActionResult Logout()
        {
            return RedirectToAction("Index");
        }


    }
}

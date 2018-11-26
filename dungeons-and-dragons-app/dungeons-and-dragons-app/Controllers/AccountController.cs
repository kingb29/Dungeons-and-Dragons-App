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
            String hashedPass = "";
            String salt = "";
            int userID = 0;

            if (username != null && hashPass != null)
            {
                using (MySqlConnection connection = new MySqlConnection(connectionString))
                {
                    try // first connection checks to see if the username exists inside of the database
                    {
                        
                        connection.Open();
                        string query = "SELECT 'Username' FROM `User` WHERE Username = @Username";
                        MySqlCommand command = new MySqlCommand(query, connection);
                        command.Parameters.AddWithValue("@Username", username);
                        var count = command.ExecuteScalar();
                        if (count != null) // if true the username exists in the database and we can continue to check if the password matches
                        {
                            connection.Close();
                            connection.Open();
                            query = "SELECT * FROM `User` WHERE  Username = '" + username + "'";
                            MySqlCommand command2 = new MySqlCommand(query, connection);
                            command2.Parameters.AddWithValue("@Username", username);
                            MySqlDataReader reader = command2.ExecuteReader();

                            while (reader.Read())
                            {
                                salt = reader["Salt"].ToString();
                                hashedPass = reader["HashPass"].ToString();
                                userID = Convert.ToInt32(reader["UserID"]);
                            }

                            bool access = areEqual(hashPass, hashedPass, salt);

                            if (access) // if the user information is correct
                            {
                                // do things to log in user
                                HttpContext.Session.SetString("UserID", userID.ToString());
                                HttpContext.Session.SetString("Username", username);
                            
                                connection.Close();
                                return RedirectToAction("Dashboard", "Home");
                        }
                            else
                            {
                                connection.Close();
                                ViewBag.error = "Invalid Login Credentials";
                                return View("~/Views/Home/Login.cshtml");
                            }
                        }
                        else // the username doesn't exists within the database and the user with have to re-enter information
                        {
                            connection.Close();
                            ViewBag.error = "Invalid Login Credentials";
                            return View("~/Views/Home/Login.cshtml");

                        }
                    }
                    catch (MySqlException e)
                    {
                        ViewBag.error = "Error connecting to server";
                        return View("~/Views/Home/Login.cshtml");
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
                        password = generateHash(password,salt);
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

        private static String getSalt() // creates the original salt for the account when it is registered
        {
            var random = new RNGCryptoServiceProvider();

            int max_length = 32;

            byte[] salt = new byte[max_length];

            random.GetNonZeroBytes(salt);

            return Convert.ToBase64String(salt);
        }

        public string generateHash(string password, string salt) // generates a hashed password for the user with the randomly generated salt
        {
            byte[] bytes = Encoding.UTF8.GetBytes(password + salt);
            SHA256Managed sHA256ManagedString = new SHA256Managed();
            byte[] hash = sHA256ManagedString.ComputeHash(bytes);
            return Convert.ToBase64String(hash);
        }

        public bool areEqual(string password, string hashedPass, string salt) // compares the users input hashed with the hash already inside the database
        {
            string newHashedPin = generateHash(password, salt);
            return newHashedPin.Equals(hashedPass);
        }


        [Route("logout")]
        [HttpGet]
        public IActionResult Logout()
        {
            return RedirectToAction("Index");
        }


    }
}


/*

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

    */
using System;
using System.Collections.Generic;
using System.Diagnostics;
using MySql.Data.MySqlClient;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using dungeons_and_dragons_app.Models;
using Microsoft.Extensions.Options;

namespace dungeons_and_dragons_app.Controllers
{
    public class HomeController : Controller
    {
        private readonly string connectionString;

        public HomeController(AppSetting appSetting)
        {
            connectionString = appSetting.ConnectionString;
        }

        public IActionResult Index()
        {
            return View();
        }

        public IActionResult About()
        {
            ViewData["Message"] = "Your application description page.";

            return View();
        }

        public IActionResult CharacterCreation()
        {
            ViewData["Message"] = "Create a character";



            return View();
        }

        [HttpPost]
        public ActionResult CharacterCreation(Character character)
        {
            using (MySqlConnection con = new MySqlConnection(connectionString))
            {
                try
                {
                    con.Open();
                    string query = "INSERT INTO CharacterTable(CharacterName,CharacterLevel,Char) VALUES(person,address)";
                }
                catch (MySqlException e) {
                    
                }
            }
            return null;
        }

        public IActionResult Privacy()
        {
            return View();
        }

        [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
        public IActionResult Error()
        {
            return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
        }
    }
}

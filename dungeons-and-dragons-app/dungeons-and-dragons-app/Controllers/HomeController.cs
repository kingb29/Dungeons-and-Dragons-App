using System;
using System.Collections.Generic;
using System.Diagnostics;
using MySql.Data.MySqlClient;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Http;
using dungeons_and_dragons_app.Models;
using Microsoft.Extensions.Options;
using Microsoft.AspNetCore.Mvc.Rendering;

namespace dungeons_and_dragons_app.Controllers
{
    public class HomeController : Controller
    {
        private readonly string connectionString;
        private readonly AppSetting appSetting;
        private readonly SessionUtility sessionUtility;

        public HomeController(AppSetting appSetting, SessionUtility sessionUtility)
        {
            connectionString = appSetting.ConnectionString;
            this.appSetting = appSetting;
            this.sessionUtility = sessionUtility;

        }

        public IActionResult Dashboard()
        {
            if (HttpContext.Session.GetString("UserID") == null)
            {
                return RedirectToAction("Login", "Home");
            }
            else
            {
                DataObj dbo = new DataObj(appSetting, sessionUtility);
                List<Character> characters = dbo.getCharactersFromUserId();
                if (characters.Count == 0)
                {
                    return RedirectToAction("CharacterCreation", "Home");
                }
                return View(characters);
            } 
        }

        public IActionResult Index(int id)
        {
            if (HttpContext.Session.GetString("UserID") == null)
            {
                return RedirectToAction("Login", "Home");
            }
            else
            {
                return View();
            }
        }

        public IActionResult About()
        {
            ViewData["Message"] = "Your application description page.";

            return View();
        }

        public IActionResult CharacterCreation()
        {
            if (HttpContext.Session.GetString("UserID") == null)
            {
                return RedirectToAction("Login", "Home");
            }
            else
            {
                ViewData["Message"] = "Create a character";

                DataObj dbo = new DataObj(appSetting, sessionUtility);

                // RACE LIST
                IEnumerable<SelectListItem> raceList = new SelectList(dbo.getAllRaces(), "id", "name");

                // CLASS LIST
                IEnumerable<SelectListItem> classList = new SelectList(dbo.getAllClasses(), "id", "name");

                // ALIGNMENT LIST
                IEnumerable<SelectListItem> alignmentList = new SelectList(dbo.getAllAlignments(), "id", "name");

                // WEAPON LIST
                IEnumerable<SelectListItem> weaponList = new SelectList(dbo.getAllWeapons(), "id", "name");

                // SPELL LIST
                IEnumerable<SelectListItem> spellList = new SelectList(dbo.getAllSpells(), "id", "name");

                CharacterViewModel characterViewModel = new CharacterViewModel(raceList, classList, alignmentList, weaponList, spellList);
                return View(characterViewModel);
            }
        }

        [HttpPost]
        public ActionResult CharacterCreation(CharacterViewModel model)
        {
            using (MySqlConnection connection = new MySqlConnection(connectionString))
            {
                try
                {
                    connection.Open();
                    string query = "INSERT INTO CharacterTable(CharacterName,CharacterLevel,RaceID,ClassID,AlignmentID,Strength,Dexterity,Constitution,Intelligence,Wisdom,Charisma,UserID) VALUES(@name,@level,@race,@class,@alignment,@str,@dex,@con,@inte,@wis,@cha,@user)";
                    MySqlCommand command = new MySqlCommand(query, connection);
                    command.Parameters.AddWithValue("@name", model.name);
                    command.Parameters.AddWithValue("@race", Convert.ToInt32(model.race));
                    command.Parameters.AddWithValue("@level", Convert.ToInt32(model.level));
                    command.Parameters.AddWithValue("@class", Convert.ToInt32(model.charClass));
                    command.Parameters.AddWithValue("@alignment", Convert.ToInt32(model.alignment));
                    command.Parameters.AddWithValue("@str", Convert.ToInt32(model.str));
                    command.Parameters.AddWithValue("@dex", Convert.ToInt32(model.dex));
                    command.Parameters.AddWithValue("@con", Convert.ToInt32(model.con));
                    command.Parameters.AddWithValue("@inte", Convert.ToInt32(model.inte));
                    command.Parameters.AddWithValue("@wis", Convert.ToInt32(model.wis));
                    command.Parameters.AddWithValue("@cha", Convert.ToInt32(model.cha));
                    command.Parameters.AddWithValue("@user", HttpContext.Session.GetString("UserID"));
                    command.ExecuteNonQuery();
                    connection.Close();
                }
                catch (MySqlException e) {
                    Console.Write(e);
                }
            }
            return RedirectToAction("Index");
        }

        public IActionResult Privacy()
        {
            return View();
        }

        public IActionResult Login()
        {
            return View();
        }

        public IActionResult Failure()
        {
            return View();
        }

        public IActionResult CreateAccount()
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

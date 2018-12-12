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

        public HomeController(AppSetting appSetting)
        {
            connectionString = appSetting.ConnectionString;
            this.appSetting = appSetting;
        }

        public AppSetting getAppSetting()
        {
            return appSetting;
        }

        public IActionResult Dashboard()
        {
            if (HttpContext.Session.GetString("UserID") == null)
            {
                return RedirectToAction("Login", "Home");
            }
            else
            {
                DataObj dbo = new DataObj(appSetting);
                int userId = Int32.Parse(HttpContext.Session.GetString("UserID"));
                List<Character> characters = dbo.getCharactersFromUserId(userId);
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
                Character curChar= new Character(id, appSetting);
                return View(curChar);
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

                CharacterViewModel characterViewModel = new CharacterViewModel(appSetting);
                return View(characterViewModel);
            }
        }

        public IActionResult CharacterEdit(int id)
        {
            if (HttpContext.Session.GetString("UserID") == null)
            {
                return RedirectToAction("Login", "Home");
            }
            else
            {
                ViewData["Message"] = "Edit a character";

                CharacterViewModel characterViewModel = new CharacterViewModel(id, appSetting);
                return View(characterViewModel);
            }
        }

        [HttpPost]
        public ActionResult CharacterCreation(CharacterViewModel model)
        {
            if (!ModelState.IsValid)
            {
                DataObj dbo = new DataObj(appSetting);

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

                model.raceList = raceList;
                model.classList = classList;
                model.alignmentList = alignmentList;
                model.weaponList = weaponList;
                model.spellList = spellList;

                return View("CharacterCreation", model);
            }
            else
            {
                DataObj dbo = new DataObj(appSetting);
                int userId = Int32.Parse(HttpContext.Session.GetString("UserID"));
                long charId = dbo.createCharacter(model, userId);
                if (charId != -1)
                {
                    return RedirectToAction("Dashboard", "Home");
                }
                else
                {
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

                    model.raceList = raceList;
                    model.classList = classList;
                    model.alignmentList = alignmentList;
                    model.weaponList = weaponList;
                    model.spellList = spellList;

                    return View("CharacterCreation", model);
                }
            }
                
        }

        [HttpPost]
        public ActionResult CharacterEdit(CharacterViewModel model)
        {
            if (!ModelState.IsValid)
            {
                DataObj dbo = new DataObj(appSetting);

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

                model.raceList = raceList;
                model.classList = classList;
                model.alignmentList = alignmentList;
                model.weaponList = weaponList;
                model.spellList = spellList;

                return View("CharacterEdit", model);
            }
            else
            {
                DataObj dbo = new DataObj(appSetting);
                int userId = Int32.Parse(HttpContext.Session.GetString("UserID"));
                long charId = dbo.editCharacter(model, userId);
                if (charId != -1)
                {
                    return RedirectToAction("Index", new { id = charId });
                }
                else
                {
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

                    model.raceList = raceList;
                    model.classList = classList;
                    model.alignmentList = alignmentList;
                    model.weaponList = weaponList;
                    model.spellList = spellList;

                    return View("CharacterEdit", model);
                }
            }

        }

        public ActionResult CharacterDelete(int id)
        {
            DataObj dbo = new DataObj(appSetting);
            dbo.deleteCharacterById(id);
            return RedirectToAction("Dashboard", "Home");

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

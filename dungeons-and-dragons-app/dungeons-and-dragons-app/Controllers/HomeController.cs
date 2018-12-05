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

                ViewData["race"] = raceList;
                ViewData["charClass"] = classList;
                ViewData["alignment"] = alignmentList;
                ViewData["weapon"] = weaponList;
                ViewData["spell"] = spellList;

                return View("CharacterCreation", model);
            }
            else
            {
                DataObj dbo = new DataObj(appSetting, sessionUtility);
                long charId = dbo.createCharacter(model);
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

                    ViewData["race"] = raceList;
                    ViewData["charClass"] = classList;
                    ViewData["alignment"] = alignmentList;
                    ViewData["weapon"] = weaponList;
                    ViewData["spell"] = spellList;

                    return View("CharacterCreation", model);
                }
            }
                
        }

        [HttpPost]
        public ActionResult CharacterEdit(CharacterViewModel model)
        {
            if (!ModelState.IsValid)
            {
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

                ViewData["race"] = raceList;
                ViewData["charClass"] = classList;
                ViewData["alignment"] = alignmentList;
                ViewData["weapon"] = weaponList;
                ViewData["spell"] = spellList;

                return View("CharacterEdit", model);
            }
            else
            {
                DataObj dbo = new DataObj(appSetting, sessionUtility);

                long charId = dbo.editCharacter(model);
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

                    ViewData["race"] = raceList;
                    ViewData["charClass"] = classList;
                    ViewData["alignment"] = alignmentList;
                    ViewData["weapon"] = weaponList;
                    ViewData["spell"] = spellList;

                    return View("CharacterEdit", model);
                }
            }

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

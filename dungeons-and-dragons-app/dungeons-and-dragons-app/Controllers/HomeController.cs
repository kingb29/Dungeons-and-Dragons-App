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

            // RACE LIST
            List<Race> races = new List<Race>();
            using (MySqlConnection connection = new MySqlConnection(connectionString))
            {
                try
                {
                    connection.Open();
                    string query = "SELECT * FROM Race";
                    MySqlCommand cmd = new MySqlCommand(query, connection);
                    MySqlDataReader dr = cmd.ExecuteReader();
                    while (dr.Read())
                    {
                        Race race = new Race();
                        race.id = Int32.Parse(dr["RaceID"].ToString());
                        race.name = dr["RaceName"].ToString();
                        races.Add(race);
                    }
                    connection.Close();
                }
                catch (MySqlException e)
                {
                    Console.Write(e);
                }
            }
            IEnumerable<SelectListItem> raceList = new SelectList(races, "id", "name");

            // CLASS LIST
            List<CharacterClass> classes = new List<CharacterClass>();
            using (MySqlConnection connection = new MySqlConnection(connectionString))
            {
                try
                {
                    connection.Open();
                    string query = "SELECT * FROM Class";
                    MySqlCommand cmd = new MySqlCommand(query, connection);
                    MySqlDataReader dr = cmd.ExecuteReader();
                    while (dr.Read())
                    {
                        CharacterClass charClass = new CharacterClass();
                        charClass.id = Int32.Parse(dr["ClassID"].ToString());
                        charClass.name = dr["ClassName"].ToString();
                        classes.Add(charClass);
                    }
                    connection.Close();
                }
                catch (MySqlException e)
                {
                    Console.Write(e);
                }
            }
            IEnumerable<SelectListItem> classList = new SelectList(classes, "id", "name");

            // ALIGNMENT LIST
            List<Alignment> alignments = new List<Alignment>();
            using (MySqlConnection connection = new MySqlConnection(connectionString))
            {
                try
                {
                    connection.Open();
                    string query = "SELECT * FROM Alignment";
                    MySqlCommand cmd = new MySqlCommand(query, connection);
                    MySqlDataReader dr = cmd.ExecuteReader();
                    while (dr.Read())
                    {
                        Alignment alignment = new Alignment();
                        alignment.id = Int32.Parse(dr["AlignmentID"].ToString());
                        alignment.name = dr["AlignmentName"].ToString();
                        alignments.Add(alignment);
                    }
                    connection.Close();
                }
                catch (MySqlException e)
                {
                    Console.Write(e);
                }
            }
            IEnumerable<SelectListItem> alignmentList = new SelectList(alignments, "id", "name");

            // weapons
            List<Weapon> weapons = new List<Weapon>();
            using (MySqlConnection connection = new MySqlConnection(connectionString))
            {
                try
                {
                    connection.Open();
                    string query = "SELECT * FROM Weapons";
                    MySqlCommand cmd = new MySqlCommand(query, connection);
                    MySqlDataReader dr = cmd.ExecuteReader();
                    while (dr.Read())
                    {
                        Weapon weapon = new Weapon();
                        weapon.id = Int32.Parse(dr["WeaponID"].ToString());
                        weapon.name = dr["WeaponName"].ToString();
                        weapons.Add(weapon);
                    }
                    connection.Close();
                }
                catch (MySqlException e)
                {
                    Console.Write(e);
                }
            }
            IEnumerable<SelectListItem> weaponList = new SelectList(weapons, "id", "name");

            List<Spell> spells = new List<Spell>();
            using (MySqlConnection connection = new MySqlConnection(connectionString))
            {
                try
                {
                    connection.Open();
                    string query = "SELECT * FROM Spells";
                    MySqlCommand cmd = new MySqlCommand(query, connection);
                    MySqlDataReader dr = cmd.ExecuteReader();
                    while (dr.Read())
                    {
                        Spell spell = new Spell();
                        spell.id = Int32.Parse(dr["SpellID"].ToString());
                        spell.name = dr["SpellName"].ToString();
                        spells.Add(spell);
                    }
                    connection.Close();
                }
                catch (MySqlException e)
                {
                    Console.Write(e);
                }
            }
            IEnumerable<SelectListItem> spellList = new SelectList(spells, "id", "name");

            CharacterViewModel characterViewModel = new CharacterViewModel(raceList, classList, alignmentList, weaponList, spellList);
            return View(characterViewModel);
        }

        [HttpPost]
        public ActionResult CharacterCreation(CharacterViewModel model)
        {
            using (MySqlConnection connection = new MySqlConnection(connectionString))
            {
                try
                {
                    connection.Open();
                    string query = "INSERT INTO CharacterTable(CharacterName,CharacterLevel,RaceID,ClassID,AlignmentID,Strength,Dexterity,Constitution,Intelligence,Wisdom,Charisma) VALUES(@name,@level,@race,@class,@alignment,@str,@dex,@con,@inte,@wis,@cha)";
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

        [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
        public IActionResult Error()
        {
            return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
        }
    }
}

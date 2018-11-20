using Microsoft.AspNetCore.Http;
using MySql.Data.MySqlClient;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.Web;

public class DataObj
{
    private SessionUtility _sessionUtility;
    private readonly string connectionString;

    public DataObj(AppSetting appSetting, SessionUtility sessionUtility)
    {
        connectionString = appSetting.ConnectionString;
        _sessionUtility = sessionUtility;
    }


    // Gets list of characters' name/level/class/race to display on dashboard

    public List<Character> getCharactersFromUserId()
    {
        using (MySqlConnection connection = new MySqlConnection(connectionString))
        {
            try
            {
                string userId = _sessionUtility.GetSession("UserID");
                List<Character> characters = new List<Character>();
                connection.Open();
                string query = "SELECT `CharacterID`, `CharacterName`,`CharacterLevel`, `ClassName`, `RaceName` FROM `CharacterTable` " +
                    "INNER JOIN `Race` ON Race.RaceID = CharacterTable.RaceID INNER JOIN `Class` ON Class.ClassID = " +
                    "CharacterTable.ClassID WHERE UserID = @id";
                MySqlCommand cmd = new MySqlCommand(query, connection);
                cmd.Parameters.Add(new MySqlParameter("@id", userId));
                MySqlDataReader dr = cmd.ExecuteReader();
                while (dr.Read())
                {
                    Character character = new Character();
                    character.id = Int32.Parse(dr["CharacterID"].ToString());
                    character.name = dr["CharacterName"].ToString();
                    character.level = Int32.Parse(dr["CharacterLevel"].ToString());
                    character.race = dr["RaceName"].ToString();
                    character.charClass = dr["ClassName"].ToString();
                    characters.Add(character);
                }
                connection.Close();
                return characters;
            }
            catch (MySqlException e)
            {
                Console.Write(e);
                return null;
            }
        }
    }

    public List<Race> getAllRaces()
    {
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
                return races;
            }
            catch (MySqlException e)
            {
                Console.Write(e);
                return null;
            }
        }
    }

    public List<CharacterClass> getAllClasses()
    {
        List<CharacterClass> charClasses = new List<CharacterClass>();
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
                    charClasses.Add(charClass);
                }
                connection.Close();
                return charClasses;
            }
            catch (MySqlException e)
            {
                Console.Write(e);
                return null;
            }
        }
    }

    public List<Alignment> getAllAlignments()
    {
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
                return alignments;
            }
            catch (MySqlException e)
            {
                Console.Write(e);
                return null;
            }
        }
    }

    public List<Weapon> getAllWeapons()
    {
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
                return weapons;
            }
            catch (MySqlException e)
            {
                Console.Write(e);
                return null;
            }
        }
    }

    public List<Spell> getAllSpells()
    {
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
                return spells;
            }
            catch (MySqlException e)
            {
                Console.Write(e);
                return null;
            }
        }
    }
}

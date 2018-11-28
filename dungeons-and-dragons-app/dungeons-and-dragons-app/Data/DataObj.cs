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

    public DataObj(AppSetting appSetting)
    {
        connectionString = appSetting.ConnectionString;
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

    public Character pullCharInfoById(int id) // creates and returns a character from the database based on character ID
    {
        Character temp = new Character(); // the character to return 

        using (MySqlConnection connection = new MySqlConnection(connectionString))
        {
            try
            {
                connection.Open();
                string query = "SELECT * FROM `CharacterTable` " +
                    "INNER JOIN `Race` ON Race.RaceID = CharacterTable.RaceID INNER JOIN `Class` ON Class.ClassID = " +
                    "CharacterTable.ClassID INNER JOIN `Alignment` ON Alignment.AlignmentID = CharacterTable.AlignmentID WHERE CharacterID = @id";


                MySqlCommand cmd = new MySqlCommand(query, connection);
                cmd.Parameters.Add(new MySqlParameter("@id", id));
                MySqlDataReader dr = cmd.ExecuteReader();
                while (dr.Read())
                {
                    temp.name = dr["CharacterName"].ToString();
                    temp.level = Int32.Parse(dr["CharacterLevel"].ToString());
                    temp.race = dr["RaceName"].ToString();
                    temp.charClass = dr["ClassName"].ToString();
                    temp.str = Int32.Parse(dr["Strength"].ToString());
                    temp.dex = Int32.Parse(dr["Dexterity"].ToString());
                    temp.con = Int32.Parse(dr["Constitution"].ToString());
                    temp.inte = Int32.Parse(dr["Intelligence"].ToString());
                    temp.wis = Int32.Parse(dr["Wisdom"].ToString());
                    temp.cha = Int32.Parse(dr["Charisma"].ToString());
                    temp.alignment = dr["AlignmentName"].ToString();
                    temp.gender = dr["Gender"].ToString();
                }
               
                // code to set gender to actual gender
                if (temp.gender.Equals("0"))
                {
                    temp.gender = "Male";
                }
                else
                {
                    temp.gender = "Female";
                }

                connection.Close();
                connection.Open();
                // code to grab the character weapons from the database with the characterID
                query = "SELECT `SpellName`, `SpellDamage` FROM `CharacterSpells` " +
                    "INNER JOIN `Spells` ON Spells.SpellID = CharacterSpells.SpellID WHERE CharacterID = @id";
                cmd = new MySqlCommand(query, connection);
                cmd.Parameters.Add(new MySqlParameter("@id", id));
                dr = cmd.ExecuteReader();
                List<Spell> spells = new List<Spell>();
                while (dr.Read())
                {
                    Spell spell = new Spell();
                    spell.damage = dr["SpellDamage"].ToString();
                    spell.name = dr["SpellName"].ToString();
                    spells.Add(spell);
                }

                temp.spells = spells;

                connection.Close();
                connection.Open();
                // code to grab the character weapons from the database with the characterID
                query = "SELECT `WeaponName`, `WeaponDamage` FROM `CharacterWeapons` " +
                    "INNER JOIN `Weapons` ON Weapons.WeaponID = CharacterWeapons.WeaponID WHERE CharacterID = @id";
                cmd = new MySqlCommand(query, connection);
                cmd.Parameters.Add(new MySqlParameter("@id", id));
                dr = cmd.ExecuteReader();
                List<Weapon> weapons = new List<Weapon>();
                while (dr.Read())
                {
                    Weapon weapon = new Weapon();
                    weapon.name = dr["WeaponName"].ToString();
                    weapon.damage = Int32.Parse(dr["WeaponDamage"].ToString());
                    weapons.Add(weapon);
                }

                temp.weapons = weapons;


                connection.Close();

                return temp;
            }
            catch (MySqlException e)
            {
                Console.Write(e);
                return null;
            }
        }
    }
}

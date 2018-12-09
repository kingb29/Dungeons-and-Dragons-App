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
                string query = "SELECT `CharacterID`, `CharacterName`,`CharacterLevel`, Class.`ClassID`, `ClassName`, Race.`RaceID`, `RaceName` FROM `CharacterTable` " +
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
                    character.race.id = Int32.Parse(dr["RaceID"].ToString());
                    character.race.name = dr["RaceName"].ToString();
                    character.charClass.id = Int32.Parse(dr["ClassID"].ToString());
                    character.charClass.name = dr["ClassName"].ToString();
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
                string query = "SELECT * FROM Weapons ORDER BY WeaponName ASC";
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
                string query = "SELECT * FROM Spells ORDER BY SpellName ASC";
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

    public Character pullCharInfoById(int id) // returns a character from the database based on character ID
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
                    temp.race.id = Int32.Parse(dr["RaceID"].ToString());
                    temp.race.name = dr["RaceName"].ToString();
                    temp.charClass.id = Int32.Parse(dr["ClassID"].ToString());
                    temp.charClass.name = dr["ClassName"].ToString();
                    temp.str = Int32.Parse(dr["Strength"].ToString());
                    temp.dex = Int32.Parse(dr["Dexterity"].ToString());
                    temp.con = Int32.Parse(dr["Constitution"].ToString());
                    temp.inte = Int32.Parse(dr["Intelligence"].ToString());
                    temp.wis = Int32.Parse(dr["Wisdom"].ToString());
                    temp.cha = Int32.Parse(dr["Charisma"].ToString());
                    temp.alignment.id = Int32.Parse(dr["AlignmentID"].ToString());
                    temp.alignment.name = dr["AlignmentName"].ToString();
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
                query = "SELECT CharacterSpells.`SpellID`, `SpellName`, `SpellDamage` FROM `CharacterSpells` " +
                    "INNER JOIN `Spells` ON Spells.SpellID = CharacterSpells.SpellID WHERE CharacterID = @id";
                cmd = new MySqlCommand(query, connection);
                cmd.Parameters.Add(new MySqlParameter("@id", id));
                dr = cmd.ExecuteReader();
                List<Spell> spells = new List<Spell>();
                while (dr.Read())
                {
                    Spell spell = new Spell();
                    spell.id = Int32.Parse(dr["SpellID"].ToString());
                    spell.damage = dr["SpellDamage"].ToString();
                    spell.name = dr["SpellName"].ToString();
                    spells.Add(spell);
                }

                temp.spells = spells;

                connection.Close();
                connection.Open();
                // code to grab the character weapons from the database with the characterID
                query = "SELECT CharacterWeapons.`WeaponID`, `WeaponName`, `WeaponDamage` FROM `CharacterWeapons` " +
                    "INNER JOIN `Weapons` ON Weapons.WeaponID = CharacterWeapons.WeaponID WHERE CharacterID = @id";
                cmd = new MySqlCommand(query, connection);
                cmd.Parameters.Add(new MySqlParameter("@id", id));
                dr = cmd.ExecuteReader();
                List<Weapon> weapons = new List<Weapon>();
                while (dr.Read())
                {
                    Weapon weapon = new Weapon();
                    weapon.id = Int32.Parse(dr["WeaponID"].ToString());
                    weapon.name = dr["WeaponName"].ToString();
                    weapon.damage = dr["WeaponDamage"].ToString();
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

    public long createCharacter(CharacterViewModel model)
    {
        using (MySqlConnection connection = new MySqlConnection(connectionString))
        {
            try
            {

                int gender = -1;

                if (model.gender == "Male")
                {
                    gender = 0;
                }
                else
                {
                    gender = 1;
                }

                // inserting into character table
                connection.Open();
                string query = "INSERT INTO CharacterTable(CharacterName,CharacterLevel,Gender,RaceID,ClassID,AlignmentID,Strength,Dexterity,Constitution,Intelligence,Wisdom,Charisma,UserID) VALUES(@name,@level,@gender,@race,@class,@alignment,@str,@dex,@con,@inte,@wis,@cha,@user)";
                MySqlCommand command = new MySqlCommand(query, connection);
                command.Parameters.AddWithValue("@name", model.name);
                command.Parameters.AddWithValue("@gender", gender);
                command.Parameters.AddWithValue("@race", Convert.ToInt32(model.race.id));
                command.Parameters.AddWithValue("@level", Convert.ToInt32(model.level));
                command.Parameters.AddWithValue("@class", Convert.ToInt32(model.charClass.id));
                command.Parameters.AddWithValue("@alignment", Convert.ToInt32(model.alignment.id));
                command.Parameters.AddWithValue("@str", Convert.ToInt32(model.str));
                command.Parameters.AddWithValue("@dex", Convert.ToInt32(model.dex));
                command.Parameters.AddWithValue("@con", Convert.ToInt32(model.con));
                command.Parameters.AddWithValue("@inte", Convert.ToInt32(model.inte));
                command.Parameters.AddWithValue("@wis", Convert.ToInt32(model.wis));
                command.Parameters.AddWithValue("@cha", Convert.ToInt32(model.cha));
                command.Parameters.AddWithValue("@user", _sessionUtility.GetSession("UserID"));
                command.ExecuteNonQuery();

                long charId = command.LastInsertedId;

                // inserting spells 
                List<Spell> spells = model.spells;

                foreach (var spell in spells)
                {
                    query = "INSERT INTO CharacterSpells(CharacterID,SpellId) VALUES(@charId,@spellId)";
                    command = new MySqlCommand(query, connection);
                    command.Parameters.AddWithValue("@charId", charId);
                    command.Parameters.AddWithValue("@spellId", Convert.ToInt32(spell.id));
                    command.ExecuteNonQuery();
                }

                // inserting weapons

                List<Weapon> weapons = model.weapons;

                foreach (var weapon in weapons)
                {
                    query = "INSERT INTO CharacterWeapons(CharacterID,WeaponId) VALUES(@charId,@weaponId)";
                    command = new MySqlCommand(query, connection);
                    command.Parameters.AddWithValue("@charId", charId);
                    command.Parameters.AddWithValue("@spellId", Convert.ToInt32(weapon.id));
                    command.ExecuteNonQuery();
                }

                connection.Close();
                return charId;
                
            }
            catch (MySqlException e)
            {
                Console.Write(e);
                return -1;
            }
    }
}

    public int editCharacter(CharacterViewModel model)
    {
        using (MySqlConnection connection = new MySqlConnection(connectionString))
        {
            try
            {

                int gender = -1;

                if (model.gender == "Male")
                {
                    gender = 0;
                }
                else
                {
                    gender = 1;
                }

                // updating into character table
                connection.Open();
                string query = "UPDATE CharacterTable SET CharacterName=@name,CharacterLevel=@level,Gender=@gender,RaceID=@race," +
                    "ClassID=@class,AlignmentID=@alignment,Strength=@str,Dexterity=@dex,Constitution=@con,Intelligence=@inte," +
                    "Wisdom=@wis,Charisma=@cha,UserID=@user WHERE CharacterID=@id";
                MySqlCommand command = new MySqlCommand(query, connection);
                command.Parameters.AddWithValue("@id", model.id);
                command.Parameters.AddWithValue("@name", model.name);
                command.Parameters.AddWithValue("@gender", gender);
                command.Parameters.AddWithValue("@race", Convert.ToInt32(model.race.id));
                command.Parameters.AddWithValue("@level", Convert.ToInt32(model.level));
                command.Parameters.AddWithValue("@class", Convert.ToInt32(model.charClass.id));
                command.Parameters.AddWithValue("@alignment", Convert.ToInt32(model.alignment.id));
                command.Parameters.AddWithValue("@str", Convert.ToInt32(model.str));
                command.Parameters.AddWithValue("@dex", Convert.ToInt32(model.dex));
                command.Parameters.AddWithValue("@con", Convert.ToInt32(model.con));
                command.Parameters.AddWithValue("@inte", Convert.ToInt32(model.inte));
                command.Parameters.AddWithValue("@wis", Convert.ToInt32(model.wis));
                command.Parameters.AddWithValue("@cha", Convert.ToInt32(model.cha));
                command.Parameters.AddWithValue("@user", _sessionUtility.GetSession("UserID"));
                command.ExecuteNonQuery();

                // dropping old spells

                query = "DELETE FROM CharacterSpells WHERE CharacterID = @charId";
                command = new MySqlCommand(query, connection);
                command.Parameters.AddWithValue("@charId", model.id);
                command.ExecuteNonQuery();

                // inserting spells 

                for (int i = 0; i < model.spellInputs.Length; i++)
                {
                    query = "INSERT INTO CharacterSpells(CharacterID,SpellId) VALUES(@charId,@spellId)";
                    command = new MySqlCommand(query, connection);
                    command.Parameters.AddWithValue("@charId", model.id);
                    command.Parameters.AddWithValue("@spellId", Convert.ToInt32(model.spellInputs[i]));
                    command.ExecuteNonQuery();
                }

                // dropping old weapons

                query = "DELETE FROM CharacterWeapons WHERE CharacterID = @charId";
                command = new MySqlCommand(query, connection);
                command.Parameters.AddWithValue("@charId", model.id);
                command.ExecuteNonQuery();

                // inserting weapons

                List<Weapon> weapons = model.weapons;

                for (int i = 0; i < model.weaponInputs.Length; i++)
                {
                    query = "INSERT INTO CharacterWeapons(CharacterID,WeaponId) VALUES(@charId,@weaponId)";
                    command = new MySqlCommand(query, connection);
                    command.Parameters.AddWithValue("@charId", model.id);
                    command.Parameters.AddWithValue("@weaponId", Convert.ToInt32(model.weaponInputs[i]));
                    command.ExecuteNonQuery();
                }

                connection.Close();
                return model.id;

            }
            catch (MySqlException e)
            {
                Console.Write(e);
                return -1;
            }
        }
    }
}

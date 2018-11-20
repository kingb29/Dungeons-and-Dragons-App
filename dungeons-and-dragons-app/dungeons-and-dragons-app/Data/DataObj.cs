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
                    "CharacterTable.ClassID INNER JOIN `Alignment` ON Alignment.AlignmentID = CharacterTable.AlignmentID WHERE UserID = @id";
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
}

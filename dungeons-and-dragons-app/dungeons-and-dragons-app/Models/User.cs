using System;

public class User
{
    public string username { get; set; }
    public int databaseID { get; set; }
    public int[] characterSheetIDs { get; set; }

    public User()
	{
        username = "";
        databaseID = 0;
        characterSheetIDs = new int[0];
	}
    public User(string usernameIn, int databaseIDIn, int[] characterSheetIDsIn)
    {
        this.username = usernameIn;
        this.databaseID = databaseIDIn;
        this.characterSheetIDs = characterSheetIDsIn;
    }
    
}

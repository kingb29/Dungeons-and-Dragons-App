using System;

public class User
{
    private string username;
    private int databaseID;
    private int[] characterSheetIDs;

	public User()
	{
        username = "";
        databaseID = 0;
        characterSheetIDs = new int[0];
	}
    public User(string usernameIn, int databaseIDIn, int[] characetSheetIDsIn)
    {
        this.username = usernameIn;
        this.databaseID = databaseIDIn;
        this.characterSheetIDs = characterSheetIDsIn;
    }
    public string username { get; set; }
    public int databaseID { get; set; }
    public int[] characterSheetIDs { get; set; }
}

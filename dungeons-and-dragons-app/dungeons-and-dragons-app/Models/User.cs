using System;
using System.Collections.Generic;

public class User
{
    public string username { get; set; }
    public int databaseID { get; set; }
    public List<Character> characters { get; set; }

    public User()
	{
        username = "";
        databaseID = 0;
	}
    public User(AppSetting appSetting, SessionUtility sessionUtility)
    {
    }
    
}

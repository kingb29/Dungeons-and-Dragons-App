using Microsoft.AspNetCore.Mvc.Rendering;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

public class LoginViewModel
{
    public string username { get; set; }
    public int databaseID { get; set; }
    public int[] characterSheetIDs { get; set; }
    public string hashPass { get; set; }
    public int salt { get; set; }

    public LoginViewModel()
    {

    }

    public LoginViewModel(int databaseID, int salt, string hashPass, string username, int[] characterSheetIDs)
    {
        this.databaseID = databaseID;
        this.salt = salt;
        this.hashPass = hashPass;
        this.username = username;
        this.characterSheetIDs = characterSheetIDs;
    }

}


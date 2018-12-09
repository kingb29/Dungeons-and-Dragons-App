using System;
using System.Collections.Generic;

public class Character
{
    public string name { get; set; }
    public int id { get; set; }
    public int str { get; set; }
    public int dex { get; set; }
    public int con { get; set; }
    public int inte { get; set; }
    public int wis { get; set; }
    public int cha { get; set; }
    public Race race { get; set; }
    public string armor { get; set; }
    public string speed { get; set; }
    public string hitpoints { get; set; }
    public string gender { get; set; }
    public int level { get; set; }
    public CharacterClass charClass { get; set; }
    public Alignment alignment { get; set; }
    public List<Weapon> weapons { get; set; }
    public List<Spell> spells { get; set; }

    public Character()
	{
        name = "";
        id = 0;
        gender = "";
        alignment = new Alignment();
        race = new Race();
        charClass = new CharacterClass();
        weapons = new List<Weapon>();
        spells = new List<Spell>();
	}
    public Character(string nameIn, string genderIn, CharacterAttribute[] charAttributesIn,
                      List<Weapon> weaponsIn, List<Spell> spellsIn, string armorIn, string speedIn, string hitpointsIn)
    {
        this.name = nameIn;
        this.weapons = weaponsIn;
        this.spells = spellsIn;
        this.gender = genderIn;
        this.hitpoints = hitpointsIn;
        this.speed = speedIn;
        this.armor = armorIn;
    }

    public Character(int id, AppSetting appSetting)
    {
        DataObj data = new DataObj(appSetting);
        Character temp = data.pullCharInfoById(id);
        this.name = temp.name;
        this.id = id;
        this.str = temp.str;
        this.dex = temp.dex;
        this.con = temp.con;
        this.inte = temp.inte;
        this.wis = temp.wis;
        this.cha = temp.cha;
        this.race = temp.race;
        this.gender = temp.gender;
        this.level = temp.level;
        this.armor = temp.armor;
        this.speed = temp.speed;
        this.hitpoints = temp.hitpoints;
        this.charClass = temp.charClass;
        this.alignment = temp.alignment;
        this.spells = temp.spells;
        this.weapons = temp.weapons;
    }

    public void setGender(bool genderIn)
    {
        if (genderIn)
            this.gender = "Male";
        else
            this.gender = "Female";
    }
}

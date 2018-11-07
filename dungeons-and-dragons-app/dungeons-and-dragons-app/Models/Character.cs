using System;

public class Character
{
    public string name { get; set; }
    public int charSheetID { get; set; }
    public string str { get; set; }
    public string dex { get; set; }
    public string con { get; set; }
    public string inte { get; set; }
    public string wis { get; set; }
    public string cha { get; set; }
    public Race race { get; set; }
    public string gender { get; set; }
    public int level { get; set; }
    public string charClass { get; set; }
    public Weapon[] weapons { get; set; }
    public Spell[] spells { get; set; }

    public Character()
	{
        name = "";
        charSheetID = 0;
        //charAttributes = new CharacterAttribute[0];
        race = new Race();
        gender = "";
        weapons = new Weapon[0];
        spells = new Spell[0];
	}
    public Character(string nameIn, string genderIn, CharacterAttribute[] charAttributesIn,
                     Race raceIn, Weapon[] weaponsIn, Spell[] spellsIn)
    {
        this.name = nameIn;
        //this.charAttributes = charAttributesIn;
        this.race = raceIn;
        this.weapons = weaponsIn;
        this.spells = spellsIn;
        this.gender = genderIn;
    }

    public CharacterAttribute[] addCharAttribute(CharacterAttribute[] currAttributes)
    {
        return null;
    }
    public Weapon[] addWeapon(Weapon[] currWeapons)
    {
        return null;
    }
    public Spell[] addSpell(Spell[] currSpells)
    {
        return null;
    }
}

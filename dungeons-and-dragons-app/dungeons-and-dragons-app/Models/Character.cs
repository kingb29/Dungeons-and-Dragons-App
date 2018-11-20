using System;

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
    public string race { get; set; }
    public string gender { get; set; }
    public int level { get; set; }
    public string charClass { get; set; }
    public string alignment { get; set; }
    public Weapon[] weapons { get; set; }
    public Spell[] spells { get; set; }

    public Character()
	{
        name = "";
        id = 0;
        //charAttributes = new CharacterAttribute[0];
        gender = "";
        weapons = new Weapon[0];
        spells = new Spell[0];
	}
    public Character(string nameIn, string genderIn, CharacterAttribute[] charAttributesIn,
                      Weapon[] weaponsIn, Spell[] spellsIn)
    {
        this.name = nameIn;
        //this.charAttributes = charAttributesIn;
        this.weapons = weaponsIn;
        this.spells = spellsIn;
        this.gender = genderIn;
    }

    public void setGender(bool genderIn)
    {
        if (genderIn)
            this.gender = "Male";
        else
            this.gender = "Female";
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

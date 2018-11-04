using System;

public class Character
{

    public string name { get; set; }
    public int charSheetID { get; set; }
    public CharacterAttribute[] charAttributes { get; set; }
    public Race race { get; set; }
    public Weapon[] weapons { get; set; }
    public Spell[] spells { get; set; }

    public Character()
	{
        name = "";
        charSheetID = 0;
        charAttributes = new CharacterAttribute[0];
        race = new Race();
        weapons = new Weapon[0];
        spells = new Spell[0];
	}
    public Character(string nameIn, int charSheetIDIn, CharacterAttribute[] charAttributesIn,
                     Race raceIn, Weapon[] weaponsIn, Spell[] spellsIn)
    {
        this.name = nameIn;
        this.charSheetID = charSheetIDIn;
        this.charAttributes = charAttributesIn;
        this.race = raceIn;
        this.weapons = weaponsIn;
        this.spells = spellsIn;
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

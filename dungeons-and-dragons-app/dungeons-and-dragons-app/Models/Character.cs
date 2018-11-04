using System;

public class Character
{
    private string name;
    private int charSheetID;
    private CharacterAttribute[] charAttributes;
    private Race race;
    private Weapon[] weapons;
    private Spell[] spells;

	public Character()
	{
        name = "";
        charSheetID = 0;
        charAttributes = new CharacterAttribute[0];
        race = new Race();
        weapons = new Weapon[0];
        spells = new Spell[0];
	}
    public Character(string nameIn, int charSheetIDIn, charAttribute[] charAttributesIn,
                     Race raceIn, Weapon[] weaponsIn, Spell[] spellsIn)
    {
        this.name = nameIn;
        this.charSheetID = charSheetIDIn;
        this.charAttributes = charAttributesIn;
        this.race = raceIn;
        this.weapons = weaponsIn;
        this.spells = spellsIn;
    }
    public string name { get; set; }
    public int charSheetID { get; set; }
    public characterAttribute[] charAttributes { get; set; }
    public Race race { get; set; }
    public Weapon[] weapons { get; set; }
    public Spell[] spells { get; set; }

    public CharacterAttribute[] addCharAttribute(CharacterAttribute[] currAttributes)
    {
    }
    public Weapon[] addWeapon(Weapon[] currWeapons)
    {
    }
    public Spell[] addSpell(Spell[] currSpells)
    {
    }
}

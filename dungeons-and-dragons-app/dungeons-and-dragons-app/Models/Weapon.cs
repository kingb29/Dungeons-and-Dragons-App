using System;

public class Weapon
{
    public int id { get; set; }
    public string damage { get; set; }
    public string name { get; set; }
    public string[] properties { get; set; }
    public bool proficiency { get; set; }

    public Weapon()
	{
        properties = new string[0];
        proficiency = false;
	}
    public Weapon(string[] propertiesIn, bool proficiencyIn)
    {
        this.properties = propertiesIn;
        this.proficiency = proficiencyIn;
    }
    
}

using System;

public class Weapon
{
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

using System;

public class Weapon
{
    private string[] properties;
    private bool proficiency;

	public Weapon()
	{
        properties = new string[0];
        proficiency = false;
	}
    public Weapon(string[] propertiesIn, bool proficiencyIn)
    {
        this.properties = propertiesIn;
        this.proficeincy = proficiencyIn;
    }
    public string[] properties { get; set; }
    public bool proficiency { get; set; }
}

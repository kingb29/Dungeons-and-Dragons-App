using System;

public class Spell
{
    public int id { get; set; }
    public string name { get; set; }
    public string description { get; set; }
    public bool isDCSave { get; set; }
    public bool isConcentration { get; set; }

    public Spell()
	{
        description = "";
        isDCSave = false;
        isConcentration = false;
	}
    public Spell(string descriptionIn, bool isDCSaveIn, bool isConcentrationIn)
    {
        this.description = descriptionIn;
        this.isDCSave = isDCSaveIn;
        this.isConcentration = isConcentrationIn;
    }
}

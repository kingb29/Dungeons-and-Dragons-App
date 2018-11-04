using System;

public class Spell
{
    private string description;
    private bool isDCSave;
    private bool isConcentration;

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
        this.isConcentration = isConcnetrationIn;
    }
    public string description { get; set; }
    public bool isDCSave { get; set; }
    public bool isConcentration { get; set; }
}

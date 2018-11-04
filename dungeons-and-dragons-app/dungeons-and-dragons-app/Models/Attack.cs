using System;

public class Attack
{
    public string name { get; set; }
    public string damage { get; set; }
    public int shortRange { get; set; }
    public int longRange { get; set; }

    public Attack()
	{
        name = "";
        damage = "";
        shortRange = 0;
        longRange = 0;
	}
    public Attack(string nameIn, string damageIn, int shortRangeIn, int longRangeIn)
    {
        this.name = nameIn;
        this.damage = damageIn;
        this.shortRange = shortRangeIn;
        this.longRange = longRangeIn;
    }


    public string attackSR()
    {
        return "";
    }
    public string attackLR()
    {
        return "";
    }
}

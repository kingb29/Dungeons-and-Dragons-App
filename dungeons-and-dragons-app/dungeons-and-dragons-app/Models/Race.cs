using System;

public class Race
{
    public int id { get; set; }
    public string name { get; set; }
    public int[] features { get; set; }

    public Race()
	{
        name = "";
        features = new int[0];
	}
    public Race(string nameIn, int[] featuresIn)
    {
        this.name = nameIn;
        this.features = featuresIn;
    }
}

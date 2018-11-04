using System;

public class Race
{
    private string name;
    private int[] features;

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
    public string username { get; set; }
    public int[] features { get; set; }
}

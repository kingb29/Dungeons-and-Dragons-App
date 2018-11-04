using System;

public class CharacterAttribute
{
    private string name;
    private int attributeValue;
    private int attributeModifier;

	public CharacterAttribute()
	{
        name = "";
        attributeValue = 0;
        attributeModifier = 0;
	}
    public User(string nameIn, int attributeValueIn, int attributeModifierIn)
    {
        this.name = nameIn;
        this.attributeValue = attributeValueIn;
        this.attributeModifier = attributeModifierIn;
    }
    public string name { get; set; }
    public int attributeValue { get; set; }
    public int[] attributeModifier { get; set; }
}

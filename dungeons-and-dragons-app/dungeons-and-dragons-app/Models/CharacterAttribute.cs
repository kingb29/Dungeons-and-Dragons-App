using System;

public class CharacterAttribute
{
    public string name { get; set; }
    public int attributeValue { get; set; }
    public int attributeModifier { get; set; }

    public CharacterAttribute()
	{
        name = "";
        attributeValue = 0;
        attributeModifier = 0;
	}
    public CharacterAttribute(string nameIn, int attributeValueIn, int attributeModifierIn)
    {
        this.name = nameIn;
        this.attributeValue = attributeValueIn;
        this.attributeModifier = attributeModifierIn;
    }
}

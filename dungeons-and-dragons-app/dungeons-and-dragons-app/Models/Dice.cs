﻿using System;

public class Dice
{
    private Random rand;
    private string diceString;
    private int statModifier;
    private int proficiencyBonus;
    private int numRolls;
    private int numSides;

    public Dice()
    {
        this.rand = new Random();
        this.diceString = "";
        this.statModifier = 0;
        this.proficiencyBonus = 0;
        this.numRolls = 0;
        this.numSides = 0;
    }
    public Dice(string inDiceString, int inStatMod, int inProfBonus)
    {
        this.rand = new Random();
        this.diceString = inDiceString;
        this.statModifier = inStatMod;
        this.proficiencyBonus = inProfBonus;
        this.numRolls = 0;
        this.numSides = 0;
        this.parseDiceString();
    }
    private void parseDiceString()
    {
        string rollString = diceString.Substring(0, diceString.IndexOf("D"));
        string sideString = diceString.Substring(diceString.IndexOf("D") + 1);
        numRolls = int.Parse(rollString);
        numSides = int.Parse(sideString);
    }
    public int roll()
    {
        int total = 0;
        for (int i = 0; i < numRolls; i++)
        {
            total += this.rollDie(numSides);
        }
        return total + statModifier + proficiencyBonus;
    }
    private int rollDie(int sides)
    {
        int rollResult = 1 + rand.Next(sides);
        return rollResult;
    }
}
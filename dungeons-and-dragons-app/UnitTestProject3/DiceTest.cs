using System;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using dungeons_and_dragons_app.Models;

namespace DiceTest
{
    // Utilizes modified internal Dice class to get around visibility constraints
    [TestClass]
    public class DiceTest
    {
        [TestMethod]
        public void parseDiceStringTestSingleDigitRollsSides()
        {
            DiceForTesting tester = new DiceForTesting("6D8", 0, 0);

            int numRollsExpected = 6;
            int numSidesExpected = 8;

            Assert.AreEqual(numRollsExpected, tester.numRolls, 0, "DiceTest.parseDiceStringTestSingleDigitRollsSides():" +
                            "diceString was not parsed correctly - expected " + numRollsExpected + ", got " +
                            tester.numRolls + ".");
            Assert.AreEqual(numSidesExpected, tester.numSides, 0, "DiceTest.parseDiceStringTestSingleDigitRollsSides():" +
                            "diceString was not parsed correctly - expected " + numSidesExpected + ", got " +
                            tester.numSides + ".");
        }
        [TestMethod]
        public void parseDiceStringTestDoubleDigitRolls()
        {
            DiceForTesting tester = new DiceForTesting("10D8", 0, 0);

            int numRollsExpected = 10;
            int numSidesExpected = 8;

            Assert.AreEqual(numRollsExpected, tester.numRolls, 0, "DiceTest.parseDiceStringTestDoubleDigitRolls():" +
                            "diceString was not parsed correctly - expected " + numRollsExpected + ", got " +
                            tester.numRolls + ".");
            Assert.AreEqual(numSidesExpected, tester.numSides, 0, "DiceTest.parseDiceStringTestDoubleDigitRolls():" +
                            "diceString was not parsed correctly - expected " + numSidesExpected + ", got " +
                            tester.numSides + ".");
        }
        [TestMethod]
        public void parseDiceStringTestDoubleDigitSides()
        {
            DiceForTesting tester = new DiceForTesting("8D12", 0, 0);

            int numRollsExpected = 8;
            int numSidesExpected = 12;

            Assert.AreEqual(numRollsExpected, tester.numRolls, 0, "DiceTest.parseDiceStringTestDoubleDigitSides():" +
                            "diceString was not parsed correctly - expected " + numRollsExpected + ", got " +
                            tester.numRolls + ".");
            Assert.AreEqual(numSidesExpected, tester.numSides, 0, "DiceTest.parseDiceStringTestDoubleDigitSides():" +
                            "diceString was not parsed correctly - expected " + numSidesExpected + ", got " +
                            tester.numSides + ".");
        }
        [TestMethod]
        public void parseDiceStringTestDoubleDigitRollsSides()
        {
            DiceForTesting tester = new DiceForTesting("10D12", 0, 0);

            int numRollsExpected = 10;
            int numSidesExpected = 12;

            Assert.AreEqual(numRollsExpected, tester.numRolls, 0, "DiceTest.parseDiceStringTestDoubleDigitRollsSides():" +
                            "diceString was not parsed correctly - expected " + numRollsExpected + ", got " +
                            tester.numRolls + ".");
            Assert.AreEqual(numSidesExpected, tester.numSides, 0, "DiceTest.parseDiceStringTestDoubleDigitRollsSides():" +
                            "diceString was not parsed correctly - expected " + numSidesExpected + ", got " +
                            tester.numSides + ".");
        }
        [TestMethod]
        public void rollTestWithinCorrectRNGRange()
        {
            DiceForTesting tester = new DiceForTesting("2D4", 10, 10);

            int rollResultActual = tester.roll();

            // (2 * 4) + 20 = 28
            int rollResultExpectedMax = 28;
            // (2 * 1) + 20 = 22
            int rollResultExpectedMin = 22;

            Assert.IsTrue(rollResultActual <= rollResultExpectedMax && rollResultActual >= rollResultExpectedMin,
                          "DiceTest.rollTestWithinCorrectRNGRange():" + "rollResult out of range - expected 22 <= " +
                          "rollResult <= 28, got " + rollResultActual + ".");
        }
        [TestMethod]
        // "Fake" RNG for addition of stat modifier and proficiency bonus
        public void rollTestStatProficiencyAdditionCorrect()
        {
            DiceForTesting tester = new DiceForTesting("4D6", 13, 17);

            int rollResultExpected = 54;

            int rollResultActual = tester.rollFixedAdditionTest();

            Assert.AreEqual(rollResultExpected, rollResultActual, 0,
                          "DiceTest.rollTestStatProficiencyAdditionCorrect():" + "rollResult not computed " +
                          "correctly - expected " +
                          rollResultExpected + ", got " + rollResultActual + ".");
        }
    }
    // Modified Dice class for used testing purposes
    public class DiceForTesting
    {
        public Random rand;
        public string diceString;
        public int statModifier;
        public int proficiencyBonus;
        public int numRolls;
        public int numSides;

        public DiceForTesting()
        {
            this.rand = new Random();
            this.diceString = "";
            this.statModifier = 0;
            this.proficiencyBonus = 0;
            this.numRolls = 0;
            this.numSides = 0;
        }
        public DiceForTesting(string inDiceString, int inStatMod, int inProfBonus)
        {
            this.rand = new Random();
            this.diceString = inDiceString;
            this.statModifier = inStatMod;
            this.proficiencyBonus = inProfBonus;
            this.numRolls = 0;
            this.numSides = 0;
            this.parseDiceString();
        }
        public void parseDiceString()
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
        // Fixed roll method for testing addition
        public int rollFixedAdditionTest()
        {
            int total = 0;
            for (int i = 0; i < numRolls; i++)
            {
                total += numSides;
            }
            return total + statModifier + proficiencyBonus;
        }
        public int rollDie(int sides)
        {
            int rollResult = 1 + rand.Next(sides);
            return rollResult;
        }
    }
}
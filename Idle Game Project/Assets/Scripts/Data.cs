using System.Collections;
using System;
using System.Collections.Generic;
using BreakInfinity;
using System.Linq;


[Serializable]

public class Data 
{
    public BigDouble Coins;
    public List<int> ClickUpgradeLevel;
    public List<BigDouble> ProductionUpgradeLevel;
    public List<BigDouble> ProductionUpgradeGenerated;
    public List<int> GeneratorUpgradeLevel;

    public int notation;
    public Data()
    {
        Coins = 0;

        ClickUpgradeLevel = new int[4].ToList();
        ProductionUpgradeLevel = new BigDouble[4].ToList();
        ProductionUpgradeGenerated = new BigDouble[4].ToList();
        GeneratorUpgradeLevel = new int[4].ToList();

        notation = 0;
    }
}

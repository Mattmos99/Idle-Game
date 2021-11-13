using System.Collections;
using System;
using System.Collections.Generic;
using BreakInfinity;
using System.Linq;

public class Data 
{
    public BigDouble Coins;
    public List <int> ClickUpgradeLevel;
    public Data()
    {
        Coins = 0;

        ClickUpgradeLevel = new int[4].ToList();
    }
}

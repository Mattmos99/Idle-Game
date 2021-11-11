using System.Collections;
using System.Collections.Generic;
using BreakInfinity;
using UnityEngine;

public class UpgradesManager : MonoBehaviour
{
    public Controller Controller;
    public Upgrades clickUpgrade;
    public string ClickUpgradeName;
    public BigDouble ClickUpgradeBaseCost;
    public BigDouble ClickUpgradeCostMult;


    public void StartUpgradeManager()
    {
        ClickUpgradeName = " Coins Per Click";
        ClickUpgradeBaseCost = 10;
        ClickUpgradeCostMult = 1.5;
        UpdateClickUpgradeUI();
    }


    public void UpdateClickUpgradeUI()
    {
        clickUpgrade.LevelText.text = Controller.Data.ClickUpgradeLevel.ToString();
        clickUpgrade.CostText.text = "Cost :" + Cost().ToString("F2")+ "Coins";
        clickUpgrade.NameText.text = "+1" + ClickUpgradeName;

    }

    public BigDouble Cost() => ClickUpgradeBaseCost * BigDouble.Pow(ClickUpgradeCostMult, Controller.Data.ClickUpgradeLevel);
    
    public void BuyUpgrade()
    {
        if (Controller.Data.Coins >= Cost())
        {

            Controller.Data.Coins -= Cost();
            Controller.Data.ClickUpgradeLevel += 1;

        }

        UpdateClickUpgradeUI();


    }
}

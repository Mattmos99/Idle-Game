using System.Collections;
using System.Collections.Generic;
using BreakInfinity;
using UnityEngine;
using UnityEngine.UI;

public class UpgradesManager : MonoBehaviour
{
    public static UpgradesManager instance;
    public void Awake() => instance = this;

    public List<Upgrades> clickUpgrades;
    public Upgrades clickUpgradePrefab;

    public List<Upgrades> ProductionUpgrades;
    public Upgrades ProductionUpgradePrefab;

    public ScrollRect clickUpgradeScroll;
    public Transform clickUpgradesPanel;

    public ScrollRect ProductionUpgradeScroll;
    public Transform ProductionUpgradesPanel;

    public string[] ClickUpgradeNames;
    public string[] ProductionUpgradeNames;

    public BigDouble[] ClickUpgradeBaseCost;
    public BigDouble[] ClickUpgradeCostMult;
    public BigDouble[] ClickUpgradesBasePower;

    public BigDouble[] ProductionUpgradeBaseCost;
    public BigDouble[] ProductionUpgradeCostMult;
    public BigDouble[] ProductionUpgradesBasePower;



    public void StartUpgradeManager()
    {
        Methods.UpgradeCheck(Controller.instance.Data.ClickUpgradeLevel, 4);

        ClickUpgradeNames = new[] { "Click Power +1", "Click Power +5", "Click Power +10", "Click Power +25" };
        ProductionUpgradeNames = new[]
        {
            "+1 Coin/s",
            "+2 Coins/s",
            "+10 Coins/s",
            "+100 Coins/s"
        };

        ClickUpgradeBaseCost = new BigDouble[] { 10, 50, 100, 1000 };
        ClickUpgradeCostMult = new BigDouble[] { 1.25, 1.35, 1.55, 2 };
        ClickUpgradesBasePower = new BigDouble[] { 1, 5, 10, 25 };

        ProductionUpgradeBaseCost = new BigDouble[] { 25, 100, 1000, 10000 };
        ProductionUpgradeCostMult = new BigDouble[] { 1.5, 1.75, 2, 3 };
        ProductionUpgradesBasePower = new BigDouble[] { 1, 2, 10, 100 };

        for (int i = 0; i < Controller.instance.Data.ClickUpgradeLevel.Count; i++ )
        {
            Upgrades upgrade = Instantiate(clickUpgradePrefab, clickUpgradesPanel);
            upgrade.UpgradeID = i;
            clickUpgrades.Add(upgrade);
        }

        for (int i = 0; i < Controller.instance.Data.ProductionUpgradeLevel.Count; i++)
        {
            Upgrades upgrade = Instantiate(ProductionUpgradePrefab, ProductionUpgradesPanel);
            upgrade.UpgradeID = i;
            ProductionUpgrades.Add(upgrade);
        }


        clickUpgradeScroll.normalizedPosition = new Vector2(0, 0);
        ProductionUpgradeScroll.normalizedPosition = new Vector2(0, 0);

        UpdateUpgradeUI("click");
        UpdateUpgradeUI("production");


    }


    public void UpdateUpgradeUI(string type, int UpgradeID = -1 )
    {
        var Data = Controller.instance.Data;

        switch (type)
        {
            case "click":
            if (UpgradeID == -1)
            for (int i = 0; i < clickUpgrades.Count; i++) UpdateUI(clickUpgrades,Data.ClickUpgradeLevel,ClickUpgradeNames, i);
        else UpdateUI(clickUpgrades, Data.ClickUpgradeLevel, ClickUpgradeNames, UpgradeID);
                break;
            case "production":
                if (UpgradeID == -1)
                    for (int i = 0; i < ProductionUpgrades.Count; i++) UpdateUI(ProductionUpgrades,Data.ProductionUpgradeLevel,ProductionUpgradeNames, i);
                else UpdateUI(ProductionUpgrades, Data.ProductionUpgradeLevel, ProductionUpgradeNames, UpgradeID);
                break;
        }

        void UpdateUI(List<Upgrades> upgrades, List<int> upgradeLevels,string[] upgradeNames, int ID)
        {
            upgrades[ID].LevelText.text = upgradeLevels[ID].ToString();
            upgrades[ID].CostText.text = $"Cost : { UpgradeCost(type, ID):F2} Coins";
            upgrades[ID].NameText.text = upgradeNames[ID];
        }
    }

    public BigDouble UpgradeCost(string type, int UpgradeID)
    {
        var Data = Controller.instance.Data;
        switch (type)
        {
            case "click":
                return ClickUpgradeBaseCost[UpgradeID]
           * BigDouble.Pow(ClickUpgradeCostMult[UpgradeID], (BigDouble)Data.ClickUpgradeLevel[UpgradeID]);
            case "production":
                return ProductionUpgradeBaseCost[UpgradeID]
           * BigDouble.Pow(ProductionUpgradeCostMult[UpgradeID], (BigDouble)Data.ProductionUpgradeLevel[UpgradeID]);
        }

        return 0;
       
    }
    
    public void BuyUpgrade(string type,int UpgradeID)
    {
        var Data = Controller.instance.Data;

        switch (type)
        {
            case "click": Buy(Data.ClickUpgradeLevel);
                break;
            case "production": Buy(Data.ProductionUpgradeLevel);
                break;
        }

        void Buy(List<int> upgradeLevels)
        {
            if(Data.Coins >= UpgradeCost(type, UpgradeID))
            {
                Data.Coins -= UpgradeCost(type, UpgradeID);
                upgradeLevels[UpgradeID] += 1;
            }


            UpdateUpgradeUI(type, UpgradeID);


        }


    }
}

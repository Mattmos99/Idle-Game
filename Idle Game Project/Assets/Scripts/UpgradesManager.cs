using System.Collections;
using System.Collections.Generic;
using BreakInfinity;
using UnityEngine;
using UnityEngine.UI;

public class UpgradesManager : MonoBehaviour
{
    public static UpgradesManager instance;
    public void Awake() => instance = this;

    public UpgradesHandler[] UpgradeHandlers;

    public void StartUpgradeManager()
    {
        Methods.UpgradeCheck(Controller.instance.Data.ClickUpgradeLevel, 4);
        Methods.UpgradeCheck(Controller.instance.Data.ProductionUpgradeLevel, 4);
        Methods.UpgradeCheck(Controller.instance.Data.ProductionUpgradeGenerated, 4);
        Methods.UpgradeCheck(Controller.instance.Data.GeneratorUpgradeLevel, 4);

    

        UpgradeHandlers[0].UpgradeNames = new[] { "Click Power +1", "Click Power +5", "Click Power +10", "Click Power +25" };
        UpgradeHandlers[1].UpgradeNames = new[]
        {
            "+1 Coin/s",
            "+2 Coins/s",
            "+10 Coins/s",
            "+100 Coins/s"
        };
        UpgradeHandlers[2].UpgradeNames = new[]
        {
            $"Produces +0.1 \"{UpgradeHandlers[1].UpgradeNames[0]}\" Upgrades/s",
            $"Produces +0.05 \"{UpgradeHandlers[1].UpgradeNames[1]}\" Upgrades/s",
            $"Produces +0.02 \"{UpgradeHandlers[1].UpgradeNames[2]}\" Upgrades/s",
            $"Produces +0.01 \"{UpgradeHandlers[1].UpgradeNames[3]}\" Upgrades/s"
        };

        // Click Upgrades
        UpgradeHandlers[0].UpgradeBaseCost= new BigDouble[] { 10, 50, 100, 1000 };
        UpgradeHandlers[0].UpgradeCostMult= new BigDouble[] { 1.25, 1.35, 1.55, 2 };
        UpgradeHandlers[0].UpgradesBasePower= new BigDouble[] { 1, 5, 10, 25 };
        UpgradeHandlers[0].UpgradesUnlock= new BigDouble[] { 0, 25, 50, 500 };

        // Production Upgrades
        UpgradeHandlers[1].UpgradeBaseCost = new BigDouble[] { 25, 100, 1000, 10000 };
        UpgradeHandlers[1].UpgradeCostMult = new BigDouble[] { 1.5, 1.75, 2, 3 };
        UpgradeHandlers[1].UpgradesBasePower = new BigDouble[] { 1, 2, 10, 100 };
        UpgradeHandlers[1].UpgradesUnlock = new BigDouble[] { 0, 50, 500, 5000 };

        // Generator Upgrades
        UpgradeHandlers[2].UpgradeBaseCost = new BigDouble[] { 5000, 1e4, 1e5, 1e6 };
        UpgradeHandlers[2].UpgradeCostMult = new BigDouble[] { 1.25, 1.5, 2, 2.5 };
        UpgradeHandlers[2].UpgradesBasePower = new BigDouble[] { 0.1, 0.05, 0.02, 0.01 };
        UpgradeHandlers[2].UpgradesUnlock = new BigDouble[] { 2500, 5e3, 5e4, 5e5 };

        CreateUpgrades(Controller.instance.Data.ClickUpgradeLevel, 0);
        CreateUpgrades(Controller.instance.Data.ProductionUpgradeLevel, 1);
        CreateUpgrades(Controller.instance.Data.GeneratorUpgradeLevel, 2);

        void CreateUpgrades<T>(List<T> level, int index)
        {
            for (int i = 0; i < level.Count; i++)
            {
                Upgrades upgrade = Instantiate(UpgradeHandlers[index].UpgradePrefab, UpgradeHandlers[index].UpgradesPanel);
                upgrade.UpgradeID = i;
                upgrade.gameObject.SetActive(false);
                UpgradeHandlers[index].Upgrades.Add(upgrade);
            }

            UpgradeHandlers[index].UpgradeScroll.normalizedPosition = new Vector2(0, 0);

        }

        UpdateUpgradeUI("click");
        UpdateUpgradeUI("production");
        UpdateUpgradeUI("generator");


    }

    public void Update()
    {
        UpgradeUnlockSystem(Controller.instance.Data.Coins, UpgradeHandlers[0].UpgradesUnlock, 0);
        UpgradeUnlockSystem(Controller.instance.Data.Coins, UpgradeHandlers[1].UpgradesUnlock, 1);
        UpgradeUnlockSystem(Controller.instance.Data.Coins, UpgradeHandlers[2].UpgradesUnlock, 2);

        void UpgradeUnlockSystem(BigDouble currency, BigDouble[] unlock, int index)
        {
            for (var i = 0; i < UpgradeHandlers[index].Upgrades.Count; i++)
                if (!UpgradeHandlers[index].Upgrades[i].gameObject.activeSelf)
                    UpgradeHandlers[index].Upgrades[i].gameObject.SetActive(currency >= unlock[i]);
        }


        if(UpgradeHandlers[1].UpgradeScroll.gameObject.activeSelf)
        {
            UpdateUpgradeUI("production");
        }


    }
    public void UpdateUpgradeUI(string type, int UpgradeID = -1 )
    {
        var Data = Controller.instance.Data;

        switch (type)
        {
            case "click":
                UpdateAllUI(UpgradeHandlers[0].Upgrades, Data.ClickUpgradeLevel, UpgradeHandlers[0].UpgradeNames, 0, UpgradeID, type);
                break;

            case "production":
                UpdateAllUI(UpgradeHandlers[1].Upgrades, Data.ProductionUpgradeLevel, UpgradeHandlers[1].UpgradeNames, 1, UpgradeID, type, Data.ProductionUpgradeGenerated);
                break;

            case "generator":
                UpdateAllUI(UpgradeHandlers[2].Upgrades, Data.GeneratorUpgradeLevel, UpgradeHandlers[2].UpgradeNames, 2, UpgradeID, type);
                break;
        }

    }

    private void UpdateAllUI(List<Upgrades> upgrades, List<int> upgradeLevels, string[] upgradeNames, int index, int UpgradeID,string type)
    {
        if (UpgradeID == -1)
            for (int i = 0; i < UpgradeHandlers[index].Upgrades.Count; i++)
                UpdateUI(i);
        else UpdateUI(UpgradeID);

        void UpdateUI(int ID)
        {
            upgrades[ID].LevelText.text = upgradeLevels[ID].ToString("F0");
            upgrades[ID].CostText.text = $"Cost : { UpgradeCost(type, ID):F2} Coins";
            upgrades[ID].NameText.text = upgradeNames[ID];
        }

    }
    private void UpdateAllUI(List<Upgrades> upgrades, List<BigDouble> upgradeLevels, string[] upgradeNames, int index, int UpgradeID, string type, List<BigDouble> upgradesGenerated = null)
    {
        if (UpgradeID == -1)
            for (int i = 0; i < UpgradeHandlers[index].Upgrades.Count; i++)
                UpdateUI(i);
        else UpdateUI(UpgradeID);

        void UpdateUI(int ID)
        {
            BigDouble generated = upgradesGenerated == null ? 0 : upgradesGenerated[ID];
            upgrades[ID].LevelText.text = (upgradeLevels[ID] + generated).ToString("F0");
            upgrades[ID].CostText.text = $"Cost : { UpgradeCost(type, ID).Notate()} Coins";
            upgrades[ID].NameText.text = upgradeNames[ID];
        }

    }

    public BigDouble UpgradeCost(string type, int UpgradeID)
    {
        var Data = Controller.instance.Data;
        switch (type)
        {
            case "click":
                return UpgradeCost_Int(0, Data.ClickUpgradeLevel, UpgradeID);
            case "production":
                return UpgradeCost_BigDouble(1, Data.ProductionUpgradeLevel, UpgradeID);
            case "generator":
                return UpgradeCost_Int(2, Data.GeneratorUpgradeLevel, UpgradeID);
        }

        return 0;
       
    }

    private BigDouble UpgradeCost_BigDouble(int index, List<BigDouble> levels,int UpgradeID)
    {
        return UpgradeHandlers[index].UpgradeBaseCost[UpgradeID]
          * BigDouble.Pow(UpgradeHandlers[index].UpgradeCostMult[UpgradeID],levels[UpgradeID]);
    }
    private BigDouble UpgradeCost_Int(int index, List<int> levels,int UpgradeID)
    {
        return UpgradeHandlers[index].UpgradeBaseCost[UpgradeID]
                 * BigDouble.Pow(UpgradeHandlers[index].UpgradeCostMult[UpgradeID], (BigDouble)levels[UpgradeID]);
    }

    public void BuyUpgrade(string type,int UpgradeID)
    {
        var Data = Controller.instance.Data;

        switch (type)
        {
            case "click": Buy(Data.ClickUpgradeLevel,type,UpgradeID);
                break;
            case "production": Buy(Data.ProductionUpgradeLevel,type,UpgradeID);
                break;
            case "generator": Buy(Data.GeneratorUpgradeLevel, type, UpgradeID);
                break;
        }

       
    }
    private void Buy(List<int> upgradeLevels, string type, int UpgradeID)
    {
        var Data = Controller.instance.Data;
        if (Data.Coins >= UpgradeCost(type, UpgradeID))
        {
            Data.Coins -= UpgradeCost(type, UpgradeID);
            upgradeLevels[UpgradeID] += 1;
        }


        UpdateUpgradeUI(type, UpgradeID);


    }

    private void Buy(List<BigDouble> upgradeLevels,string type, int UpgradeID)
    {
        var Data = Controller.instance.Data;
        if (Data.Coins >= UpgradeCost(type, UpgradeID))
        {
            Data.Coins -= UpgradeCost(type, UpgradeID);
            upgradeLevels[UpgradeID] += 1;
        }


        UpdateUpgradeUI(type, UpgradeID);


    }
}

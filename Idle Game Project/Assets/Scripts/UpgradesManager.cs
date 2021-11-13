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
    public ScrollRect clickUpgradeScroll;
    public Transform clickUpgradesPanel;
    public string[] ClickUpgradeNames;
    public BigDouble[] ClickUpgradeBaseCost;
    public BigDouble[] ClickUpgradeCostMult;
    public BigDouble[] ClickUpgradesBasePower;


    public void StartUpgradeManager()
    {
        Methods.UpgradeCheck(Controller.instance.Data.ClickUpgradeLevel, 4);
        ClickUpgradeNames = new[] { "Click Power +1", "Click Power +5", "Click Power +10", "Click Power +25" };
        ClickUpgradeBaseCost = new BigDouble[] { 10, 50, 100, 1000 };
        ClickUpgradeCostMult = new BigDouble[] { 1.25, 1.35, 1.55, 2 };
        ClickUpgradesBasePower = new BigDouble[] { 1, 5, 10, 25 };

        for (int i = 0; i < Controller.instance.Data.ClickUpgradeLevel.Count; i++ )
        {
            Upgrades upgrade = Instantiate(clickUpgradePrefab, clickUpgradesPanel);
            upgrade.UpgradeID = i;
            clickUpgrades.Add(upgrade);
        }

        clickUpgradeScroll.normalizedPosition = new Vector2(0, 0);
        UpdateClickUpgradeUI();

        
    }


    public void UpdateClickUpgradeUI(int UpgradeID = -1 )
    {
        var Data = Controller.instance.Data;
        if(UpgradeID == -1)
            for (int i = 0; i < clickUpgrades.Count; i++) UpdateUI(i);
        else UpdateUI(UpgradeID);

        void UpdateUI(int ID)
        {
            clickUpgrades[ID].LevelText.text = Data.ClickUpgradeLevel[ID].ToString();
            clickUpgrades[ID].CostText.text = $"Cost : { clickUpgradeCost(ID):F2} Coins";
            clickUpgrades[ID].NameText.text = ClickUpgradeNames[ID];
        }
    }

    public BigDouble clickUpgradeCost(int UpgradeID) => ClickUpgradeBaseCost[UpgradeID] * BigDouble.Pow(ClickUpgradeCostMult[UpgradeID], Controller.instance.Data.ClickUpgradeLevel[UpgradeID]);
    
    public void BuyUpgrade(int UpgradeID)
    {
        var Data = Controller.instance.Data;
        if (Data.Coins >= clickUpgradeCost(UpgradeID))
        {

            Data.Coins -= clickUpgradeCost(UpgradeID);
            Data.ClickUpgradeLevel[UpgradeID] += 1;

        }

        UpdateClickUpgradeUI(UpgradeID);


    }
}

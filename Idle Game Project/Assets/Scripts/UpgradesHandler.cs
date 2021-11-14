using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using BreakInfinity;
using UnityEngine.UI;
using System;

public class UpgradesHandler : MonoBehaviour
{
    public List<Upgrades> Upgrades;
    public Upgrades UpgradePrefab;
    public ScrollRect UpgradeScroll;
    public Transform UpgradesPanel;
    public string[] UpgradeNames;
    public BigDouble[] UpgradeBaseCost;
    public BigDouble[] UpgradeCostMult;
    public BigDouble[] UpgradesBasePower;
    public BigDouble[] UpgradesUnlock;


}



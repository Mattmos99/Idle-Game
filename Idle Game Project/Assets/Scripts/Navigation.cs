using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class Navigation : MonoBehaviour
{
    public GameObject ClickUpgradesSelected;
    public GameObject ProductionUpgradesSelected;

    public TMP_Text ClickUpgradesTitleText;
    public TMP_Text ProductionUpgradesTitleText;

    public void SwitchUpgrades(string location)

    {

        UpgradesManager.instance.clickUpgradeScroll.gameObject.SetActive(false);
        UpgradesManager.instance.ProductionUpgradeScroll.gameObject.SetActive(false);

        ClickUpgradesSelected.SetActive(false);
        ProductionUpgradesSelected.SetActive(false);
        ClickUpgradesTitleText.color = Color.gray;
        ProductionUpgradesTitleText.color = Color.gray;

        switch (location)
        {
            case "Click":
                UpgradesManager.instance.clickUpgradeScroll.gameObject.SetActive(true);
                ClickUpgradesSelected.SetActive(true);
                ClickUpgradesTitleText.color = Color.white;
                break;

            case "Production":
                UpgradesManager.instance.ProductionUpgradeScroll.gameObject.SetActive(true);
                ProductionUpgradesSelected.SetActive(true);
                ProductionUpgradesTitleText.color = Color.white;
                break;
        }   
    }

}

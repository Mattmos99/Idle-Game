using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class Navigation : MonoBehaviour
{
    public GameObject ClickUpgradesSelected;
    public GameObject ProductionUpgradesSelected;
    public GameObject GeneratorUpgradesSelected;


    public TMP_Text ClickUpgradesTitleText;
    public TMP_Text ProductionUpgradesTitleText;
    public TMP_Text GeneratorUpgradesText;

    public void SwitchUpgrades(string location)

    {

        UpgradesManager.instance.UpgradeHandlers[0].UpgradeScroll.gameObject.SetActive(false);
        UpgradesManager.instance.UpgradeHandlers[1].UpgradeScroll.gameObject.SetActive(false);
        UpgradesManager.instance.UpgradeHandlers[2].UpgradeScroll.gameObject.SetActive(false);

        ClickUpgradesSelected.SetActive(false);
        ProductionUpgradesSelected.SetActive(false);
        GeneratorUpgradesSelected.SetActive(false);

        ClickUpgradesTitleText.color = Color.gray;
        ProductionUpgradesTitleText.color = Color.gray;
        GeneratorUpgradesText.color = Color.gray;

        switch (location)
        {
            case "Click":
                UpgradesManager.instance.UpgradeHandlers[0].UpgradeScroll.gameObject.SetActive(true);
                ClickUpgradesSelected.SetActive(true);
                ClickUpgradesTitleText.color = Color.white;
                break;

            case "Production":
                UpgradesManager.instance.UpgradeHandlers[1].UpgradeScroll.gameObject.SetActive(true);
                ProductionUpgradesSelected.SetActive(true);
                ProductionUpgradesTitleText.color = Color.white;
                break;

            case "Generator":
                UpgradesManager.instance.UpgradeHandlers[2].UpgradeScroll.gameObject.SetActive(true);
                GeneratorUpgradesSelected.SetActive(true);
                GeneratorUpgradesText.color = Color.white;
                break;
        }   
    }

}

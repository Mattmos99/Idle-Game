using UnityEngine;
using TMPro;
using BreakInfinity;

public class Controller : MonoBehaviour
{
    [SerializeField] private TMP_Text CoinText;
    [SerializeField] private TMP_Text CoinPowerText;
    public Data Data;
    public UpgradesManager UpgradesManager;
    public BigDouble ClickPower() => 1 + Data.ClickUpgradeLevel;


    private void Start()
    {
        Data = new Data();
        UpgradesManager.StartUpgradeManager();
    }
    private void Update()
    {
        CoinText.text = Data.Coins.ToString("F2") + " Coins";
        CoinPowerText.text = "+" + ClickPower() + "Coins";

    }

    public void GenerateCoins()
    {
        Data.Coins += ClickPower();
    }
}

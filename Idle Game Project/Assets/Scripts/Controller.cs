using UnityEngine;
using TMPro;
using BreakInfinity;

public class Controller : MonoBehaviour
{
    public static Controller instance;

    public void Awake() => instance = this;

    

    [SerializeField] private TMP_Text CoinText;
    [SerializeField] private TMP_Text CoinPowerText;

    public Data Data;

    public BigDouble ClickPower()
    {
        BigDouble total = 1;
        for(int i = 0; i < Data.ClickUpgradeLevel.Count; i++)
        {
            total += UpgradesManager.instance.ClickUpgradesBasePower[i] * Data.ClickUpgradeLevel[i];
        }

        return total;

    }
    private void Start()
    {
        Data = new Data();
        UpgradesManager.instance.StartUpgradeManager();
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

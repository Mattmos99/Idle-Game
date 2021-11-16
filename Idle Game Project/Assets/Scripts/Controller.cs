using UnityEngine;
using TMPro;
using BreakInfinity;

public class Controller : MonoBehaviour
{
    public static Controller instance;

    public void Awake() => instance = this;

    

    [SerializeField] private TMP_Text CoinText;
    [SerializeField] private TMP_Text CoinPerSecondText;
    [SerializeField] private TMP_Text CoinPowerText;

    public Data Data;

    public BigDouble ClickPower()
    {
        BigDouble total = 1;
        for(int i = 0; i < Data.ClickUpgradeLevel.Count; i++)
        {
            total += UpgradesManager.instance.UpgradeHandlers[0].UpgradesBasePower[i] * Data.ClickUpgradeLevel[i];
        }

        return total;

    }

    public BigDouble CoinsPerSecond()
    {

        BigDouble total = 0;
        for (int i = 0; i < Data.ProductionUpgradeLevel.Count; i++)
        {
            total += UpgradesManager.instance.UpgradeHandlers[1].UpgradesBasePower[i] * (Data.ProductionUpgradeLevel[i] + Data.ProductionUpgradeGenerated[i]);
        }

        return total;
    }


    public BigDouble UpgradesPerSecond(int index)
    {
        return UpgradesManager.instance.UpgradeHandlers[2].UpgradesBasePower[index] * Data.GeneratorUpgradeLevel[index];


    }
    private const string dataFileName = "PlayerData";
    public void Start()
    {
        Data = SaveSystem.SaveExists(dataFileName) ? SaveSystem.LoadData<Data>(dataFileName) : new Data();
        UpgradesManager.instance.StartUpgradeManager();
        Settings.instance.StartSettings();
    }

    public float SaveTime;
    private void Update()
    {
        CoinText.text = $"{Data.Coins.Notate()} Coins";
        CoinPerSecondText.text = $"{CoinsPerSecond():F2}/s";
        CoinPowerText.text = "+" + ClickPower() + "Coins";

        Data.Coins += CoinsPerSecond() * Time.deltaTime;

        for (var i = 0; i < Data.ProductionUpgradeLevel.Count; i++)
            Data.ProductionUpgradeGenerated[i] += UpgradesPerSecond(i) * Time.deltaTime;

        SaveTime += Time.deltaTime * (1 / Time.timeScale);
        if (SaveTime >= 15)
        {
            Save();
            SaveTime = 0;
        }

    }

    public void Save()
    {
        SaveSystem.SaveData(Data, dataFileName);
    }

    public void GenerateCoins()
    {
        Data.Coins += ClickPower();
    }
}

using UnityEngine;
using TMPro;

public class Controller : MonoBehaviour
{
    [SerializeField] private TMP_Text CoinText;
    public Data Data;

    private void Start()
    {
        Data = new Data();

    }
    private void Update()
    {
        CoinText.text = Data.Coins + " Coins";

    }

    public void GenerateCoins()
    {
        Data.Coins += 1;
    }
}

using UnityEngine;
using TMPro;

public class Controller : MonoBehaviour
{
    public TMP_Text CoinText;
    public double Coins;
    public void Update()
    {
        CoinText.text = Coins+" Coins";

    }

    public void GenerateCoins()
    {

        Coins += 1;



    }
}

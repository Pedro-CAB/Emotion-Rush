using UnityEngine;
using TMPro;

public class CoinSystem : MonoBehaviour
{
    public TextMeshProUGUI currentCoinText;
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        updateCurrentCoinUI();
    }

    void updateCurrentCoinUI()
    {
        if (currentCoinText != null)
        {
            currentCoinText.text = PlayerPrefs.GetInt("coins").ToString();
        }
    }

    bool purchase(int cost)
    {
        if (isPurchasePossible(cost))
        {
            changeCoinAmount(-cost);
            return true;
        }
        else
        {
            return false;
        }
    }

    bool isPurchasePossible(int cost)
    { 
        if (cost > PlayerPrefs.GetInt("coins"))
        {
            return false;
        }
        else
        {
            return true;
        }
    }
    
    void changeCoinAmount(int amount)
    {
        int currentCoins = PlayerPrefs.GetInt("coins");
        currentCoins += amount;
        PlayerPrefs.SetInt("coins", currentCoins);
        updateCurrentCoinUI();
    }
}

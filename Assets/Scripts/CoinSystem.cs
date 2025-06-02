using UnityEngine;
using TMPro;

public class CoinSystem : MonoBehaviour
{
    public TextMeshProUGUI currentCoinText;
    void Start()
    {
        updateCurrentCoinUI();
    }
    /// <summary>
    /// Updates the current coin UI to reflect the number of coins the player has.
    /// </summary>
    void updateCurrentCoinUI()
    {
        if (currentCoinText != null)
        {
            currentCoinText.text = PlayerPrefs.GetInt("coins").ToString();
        }
    }

    /// <summary>
    /// Purchase an item with the given cost. 
    /// Returns True if the purchase was possible, otherwise returns False.
    /// </summary>
    /// <param name="cost">Cost of the Item to Purchase.</param>
    public bool purchase(int cost)
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

    /// <summary>
    /// Checks if purchase is possible with the given cost.
    /// Returns True if the purchase is possible, otherwise returns False.
    /// </summary>
    /// <param name="cost">Cost of the Item to Purchase.</param>
    public bool isPurchasePossible(int cost)
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

    /// <summary>
    /// Changes the amount of coins the player has by the given amount.
    /// If the amount is negative, it will deduct coins; if positive, it will add coins.
    /// </summary>
    /// <param name="amount">Amount of coins to deduct or add to the player.</param>
    void changeCoinAmount(int amount)
    {
        int currentCoins = PlayerPrefs.GetInt("coins");
        currentCoins += amount;
        PlayerPrefs.SetInt("coins", currentCoins);
        updateCurrentCoinUI();
    }
}

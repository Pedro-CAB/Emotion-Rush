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
            currentCoinText.text = (PlayerPrefs.GetInt("coins") - PlayerPrefs.GetInt("unsavedCoinsSpent")).ToString();
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
            deductCoinAmount(cost);
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
        if (cost > (PlayerPrefs.GetInt("coins") - PlayerPrefs.GetInt("unsavedCoinsSpent")))
        {
            return false;
        }
        else
        {
            return true;
        }
    }

    /// <summary>
    /// Adjusts the Saved Coins according to what the player earned and spent during the day.
    /// Saves the amount after the adjustment and resets unsaved coins spent.
    /// </summary>
    /// <param name="dailyCoins">Coins won in current in-game day.</param>
    public void saveDailyCoins(int dailyCoins)
    {
        int unsavedCoinsSpent = PlayerPrefs.GetInt("unsavedCoinsSpent");
        int currentCoins = PlayerPrefs.GetInt("coins");

        // Update the coins with the daily earnings and unsaved spent coins
        PlayerPrefs.SetInt("coins", currentCoins + dailyCoins - unsavedCoinsSpent);
        PlayerPrefs.SetInt("unsavedCoinsSpent", 0);

        // Update the UI to reflect the new coin amount
        updateCurrentCoinUI();
    }

    /// <summary>
    /// Deducts the amount of coins the player has by the given amount.
    /// Money spent will only be saved at the end of in-game day.
    /// </summary>
    /// <param name="amount">Amount of coins to deduct from the player.</param>
    void deductCoinAmount(int amount)
    {
        PlayerPrefs.SetInt("unsavedCoinsSpent", PlayerPrefs.GetInt("unsavedCoinsSpent") + amount);
        updateCurrentCoinUI();
    }

    /// <summary>
    /// Increases the amount of coins the player has by the given amount.
    /// Money gained is instantly saved, as it is always gained at the end of in-game day.
    /// </summary>
    /// <param name="amount">Amount of coins to deduct from the player.</param>
    void increaseCoinAmount(int amount)
    {
        PlayerPrefs.SetInt("coins", PlayerPrefs.GetInt("coins") + amount);
        updateCurrentCoinUI();
    }
}

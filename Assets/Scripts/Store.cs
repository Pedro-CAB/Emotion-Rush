using UnityEngine;
using TMPro;
using UnityEngine.SceneManagement;

public class Store : MonoBehaviour
{
    public GameObject storeFrontUI;
    public GameObject upgradeStoreUI;

    public GameObject extrasStoreUI;

    public CoinSystem coinSystem;

    public TextMeshProUGUI timeUpgradeCostText;
    public TextMeshProUGUI interactionUpgradeCostText;
    public TextMeshProUGUI runningUpgradeCostText;
    public TextMeshProUGUI coinsUpgradeCostText;

    public TextMeshProUGUI timeUpgradeTitleText;
    public TextMeshProUGUI interactionUpgradeTitleText;
    public TextMeshProUGUI runningUpgradeTitleText;
    public TextMeshProUGUI coinsUpgradeTitleText;

    public AudioSource buttonPushSound;

    /// <summary>
    /// Base Cost Multiplier for Upgrades.
    /// </summary>
    private int upgradeCostMultiplier = 50;

    void Start()
    {
        if (extrasStoreUI == null)
        {
            storeFrontUI.SetActive(false);
        }
        else
        {
            upgradeStoreUI.SetActive(false);
        }
        updatePrices();
        updateLevels();
    }

    void updatePrices()
    {
        int timeUpgradeLevel = PlayerPrefs.GetInt("timeUpgradeLevel");
        int interactionUpgradeLevel = PlayerPrefs.GetInt("interactionUpgradeLevel");
        int runningUpgradeLevel = PlayerPrefs.GetInt("runningUpgradeLevel");
        int coinsUpgradeLevel = PlayerPrefs.GetInt("coinsUpgradeLevel");

        int unsavedTimeUpgradeLevel = PlayerPrefs.GetInt("unsavedTimeUpgradeLevel");
        int unsavedInteractionUpgradeLevel = PlayerPrefs.GetInt("unsavedInteractionUpgradeLevel");
        int unsavedRunningUpgradeLevel = PlayerPrefs.GetInt("unsavedRunningUpgradeLevel");
        int unsavedCoinsUpgradeLevel = PlayerPrefs.GetInt("unsavedCoinsUpgradeLevel");

        int timeUpgradeCost = upgradeCostMultiplier * (timeUpgradeLevel + unsavedTimeUpgradeLevel + 1);
        int interactionUpgradeCost = upgradeCostMultiplier * (interactionUpgradeLevel + unsavedInteractionUpgradeLevel + 1);
        int runningUpgradeCost = upgradeCostMultiplier * (runningUpgradeLevel + unsavedRunningUpgradeLevel + 1);
        int coinsUpgradeCost = upgradeCostMultiplier * (coinsUpgradeLevel + unsavedCoinsUpgradeLevel + 1);

        if (timeUpgradeLevel + unsavedTimeUpgradeLevel < 10)
        {
            timeUpgradeCostText.text = timeUpgradeCost.ToString();
            if (coinSystem.isPurchasePossible(timeUpgradeCost))
            {
                timeUpgradeCostText.color = Color.white;
            }
            else
            {
                timeUpgradeCostText.color = Color.red;
            }
        }
        else
        {
            timeUpgradeCostText.text = "-";
        }

        if (interactionUpgradeLevel + unsavedInteractionUpgradeLevel < 10)
        {
            interactionUpgradeCostText.text = interactionUpgradeCost.ToString();
            if (coinSystem.isPurchasePossible(interactionUpgradeCost))
            {
                interactionUpgradeCostText.color = Color.white;
            }
            else
            {
                interactionUpgradeCostText.color = Color.red;
            }
        }
        else
        {
            interactionUpgradeCostText.text = "-";
        }

        if (runningUpgradeLevel + unsavedRunningUpgradeLevel< 10)
        {
            runningUpgradeCostText.text = runningUpgradeCost.ToString();
            if (coinSystem.isPurchasePossible(runningUpgradeCost))
            {
                runningUpgradeCostText.color = Color.white;
            }
            else
            {
                runningUpgradeCostText.color = Color.red;
            }
        }
        else
        {
            runningUpgradeCostText.text = "-";
        }

        if (coinsUpgradeLevel + unsavedCoinsUpgradeLevel < 10)
        {
            coinsUpgradeCostText.text = coinsUpgradeCost.ToString();
            if (coinSystem.isPurchasePossible(coinsUpgradeCost))
            {
                coinsUpgradeCostText.color = Color.white;
            }
            else
            {
                coinsUpgradeCostText.color = Color.red;
            }
        }
        else
        {
            coinsUpgradeCostText.text = "-";
        }
    }

    void updateLevels()
    {
        int timeUpgradeLevel = PlayerPrefs.GetInt("timeUpgradeLevel");
        int interactionUpgradeLevel = PlayerPrefs.GetInt("interactionUpgradeLevel");
        int runningUpgradeLevel = PlayerPrefs.GetInt("runningUpgradeLevel");
        int coinsUpgradeLevel = PlayerPrefs.GetInt("coinsUpgradeLevel");

        int unsavedTimeUpgradeLevel = PlayerPrefs.GetInt("unsavedTimeUpgradeLevel");
        int unsavedInteractionUpgradeLevel = PlayerPrefs.GetInt("unsavedInteractionUpgradeLevel");
        int unsavedRunningUpgradeLevel = PlayerPrefs.GetInt("unsavedRunningUpgradeLevel");
        int unsavedCoinsUpgradeLevel = PlayerPrefs.GetInt("unsavedCoinsUpgradeLevel");

        if (timeUpgradeLevel + unsavedTimeUpgradeLevel < 1)
        {
            timeUpgradeTitleText.text = "Aumentar Intervalo  ";
        }
        else if (timeUpgradeLevel + unsavedTimeUpgradeLevel < 10)
        {
            timeUpgradeTitleText.text = "Aumentar Intervalo " + (timeUpgradeLevel + unsavedTimeUpgradeLevel).ToString();
        }
        else
        {
            timeUpgradeTitleText.text = "Aumentar Intervalo MAX";
        }

        if (interactionUpgradeLevel + unsavedInteractionUpgradeLevel < 1)
        {
            interactionUpgradeTitleText.text = "Acelerar Interação  ";
        }
        else if (interactionUpgradeLevel + unsavedInteractionUpgradeLevel < 10)
        {
            interactionUpgradeTitleText.text = "Acelerar Interação " + (interactionUpgradeLevel + unsavedInteractionUpgradeLevel).ToString();
        }
        else
        {
            interactionUpgradeTitleText.text = "Acelerar Interação MAX";
        }

        if (runningUpgradeLevel + unsavedRunningUpgradeLevel < 1)
        {
            runningUpgradeTitleText.text = "Corredor Veloz  ";
        }
        else if (runningUpgradeLevel + unsavedRunningUpgradeLevel < 10)
        {
            runningUpgradeTitleText.text = "Corredor Veloz " + (runningUpgradeLevel + unsavedRunningUpgradeLevel).ToString();
        }
        else
        {
            runningUpgradeTitleText.text = "Corredor Veloz MAX";
        }

        if (coinsUpgradeLevel + unsavedCoinsUpgradeLevel < 1)
        {
            coinsUpgradeTitleText.text = "Melhorar Prémio  ";
        }
        else if (coinsUpgradeLevel + unsavedCoinsUpgradeLevel < 10)
        {
            coinsUpgradeTitleText.text = "Melhorar Prémio " + (coinsUpgradeLevel + unsavedCoinsUpgradeLevel).ToString();
        }
        else
        {
            coinsUpgradeTitleText.text = "Melhorar Prémio MAX";
        }
    }

    public void upgradeTime()
    {
        buttonPushSound.Play();
        int timeUpgradeLevel = PlayerPrefs.GetInt("timeUpgradeLevel");
        int unsavedTimeUpgradeLevel = PlayerPrefs.GetInt("unsavedTimeUpgradeLevel");
        int timeUpgradeCost = upgradeCostMultiplier * (timeUpgradeLevel + unsavedTimeUpgradeLevel + 1);
        if (coinSystem.purchase(timeUpgradeCost) && timeUpgradeLevel < 10)
        {
            Debug.Log("Time Upgrade Bought!");
            PlayerPrefs.SetInt("unsavedTimeUpgradeLevel", timeUpgradeLevel + unsavedTimeUpgradeLevel + 1);
            updatePrices();
            updateLevels();
        }
    }

    public void upgradeInteraction()
    {
        buttonPushSound.Play();
        int interactionUpgradeLevel = PlayerPrefs.GetInt("interactionUpgradeLevel");
        int unsavedInteractionUpgradeLevel = PlayerPrefs.GetInt("unsavedInteractionUpgradeLevel");
        int interactionUpgradeCost = upgradeCostMultiplier * (interactionUpgradeLevel + unsavedInteractionUpgradeLevel + 1);
        if (coinSystem.purchase(interactionUpgradeCost) && interactionUpgradeLevel < 10)
        {
            Debug.Log("Interaction Upgrade Bought!");
            PlayerPrefs.SetInt("unsavedInteractionUpgradeLevel", interactionUpgradeLevel + unsavedInteractionUpgradeLevel + 1);
            updatePrices();
            updateLevels();
        }
    }

    public void upgradeRunning()
    {
        buttonPushSound.Play();
        int runningUpgradeLevel = PlayerPrefs.GetInt("runningUpgradeLevel");
        int unsavedRunningUpgradeLevel = PlayerPrefs.GetInt("unsavedRunningUpgradeLevel");
        int runningUpgradeCost = upgradeCostMultiplier * (runningUpgradeLevel + unsavedRunningUpgradeLevel + 1);
        if (coinSystem.purchase(runningUpgradeCost) && runningUpgradeLevel < 10)
        {
            Debug.Log("Running Upgrade Bought!");
            PlayerPrefs.SetInt("unsavedRunningUpgradeLevel", runningUpgradeLevel + unsavedRunningUpgradeLevel + 1);
            updatePrices();
            updateLevels();
        }
    }

    public void upgradeCoins()
    {
        buttonPushSound.Play();
        int coinsUpgradeLevel = PlayerPrefs.GetInt("coinsUpgradeLevel");
        int unsavedCoinsUpgradeLevel = PlayerPrefs.GetInt("unsavedCoinsUpgradeLevel");
        int coinsUpgradeCost = upgradeCostMultiplier * (coinsUpgradeLevel + unsavedCoinsUpgradeLevel + 1);
        if (coinSystem.purchase(coinsUpgradeCost) && coinsUpgradeLevel < 10)
        {
            Debug.Log("Coins Upgrade Bought!");
            PlayerPrefs.SetInt("unsavedCoinsUpgradeLevel", coinsUpgradeLevel + unsavedCoinsUpgradeLevel + 1);
            updatePrices();
            updateLevels();
        }
    }
    
    public void leaveStore()
    {
        if (extrasStoreUI == null) //if there is no extras store, upgrade store immediately closes when leaving
        {
            float breakTimeLeft = PlayerPrefs.GetFloat("breakTimeLeft") - (300.0f - (PlayerPrefs.GetInt("unsavedInteractionUpgradeLevel") * 30.0f));
            if (breakTimeLeft <= 0.0f)
            {
                //TODO: Ring School Bell
                //TODO: Load Next Static Scene, as Break is over
                //TODO: Delete the two lines below
                PlayerPrefs.SetFloat("breakTimeLeft", breakTimeLeft);
                SceneManager.LoadScene("BreakScene");
            }
            else
            {
                PlayerPrefs.SetFloat("breakTimeLeft", breakTimeLeft);
                SceneManager.LoadScene("BreakScene");
            }
        }
        else //if there is an extras store, go to store front
        {
            if (upgradeStoreUI.activeSelf)
            {
                upgradeStoreUI.SetActive(false);
                storeFrontUI.SetActive(true);
            }
            else if (extrasStoreUI.activeSelf)
            {
                extrasStoreUI.SetActive(false);
                storeFrontUI.SetActive(true);
            }
            else
            {
                PlayerPrefs.SetFloat("breakTimeLeft", PlayerPrefs.GetFloat("breakTimeLeft") - (300.0f - (PlayerPrefs.GetInt("unsavedInteractionUpgradeLevel") * 30.0f)));
                SceneManager.LoadScene("BreakScene");
            }
        }
    }
}

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
    public Schedule schedule;
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

        int timeUpgradeCost = 50 * (timeUpgradeLevel + 1);
        int interactionUpgradeCost = 50 * (interactionUpgradeLevel + 1);
        int runningUpgradeCost = 50 * (runningUpgradeLevel + 1);
        int coinsUpgradeCost = 50 * (coinsUpgradeLevel + 1);

        if (timeUpgradeLevel < 10)
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

        if (interactionUpgradeLevel < 10)
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

        if (runningUpgradeLevel < 10)
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

        if (coinsUpgradeLevel < 10)
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

        if (timeUpgradeLevel < 1)
        {
            timeUpgradeTitleText.text = "Aumentar Intervalo  ";
        }
        else if (timeUpgradeLevel < 10)
        {
            timeUpgradeTitleText.text = "Aumentar Intervalo " + timeUpgradeLevel.ToString();
        }
        else
        {
            timeUpgradeTitleText.text = "Aumentar Intervalo MAX";
        }

        if (interactionUpgradeLevel < 1)
        {
            interactionUpgradeTitleText.text = "Acelerar Interação  ";
        }
        else if (interactionUpgradeLevel < 10)
        {
            interactionUpgradeTitleText.text = "Acelerar Interação " + interactionUpgradeLevel.ToString();
        }
        else
        {
            interactionUpgradeTitleText.text = "Acelerar Interação MAX";
        }

        if (runningUpgradeLevel < 1)
        {
            runningUpgradeTitleText.text = "Corredor Veloz  ";
        }
        else if (runningUpgradeLevel < 10)
        {
            runningUpgradeTitleText.text = "Corredor Veloz " + runningUpgradeLevel.ToString();
        }
        else
        {
            runningUpgradeTitleText.text = "Corredor Veloz MAX";
        }

        if (coinsUpgradeLevel < 1)
        {
            coinsUpgradeTitleText.text = "Melhorar Prémio  ";
        }
        else if (coinsUpgradeLevel < 10)
        {
            coinsUpgradeTitleText.text = "Melhorar Prémio " + coinsUpgradeLevel.ToString();
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
        int timeUpgradeCost = 100 * (timeUpgradeLevel + 1);
        if (coinSystem.purchase(timeUpgradeCost) && timeUpgradeLevel < 10)
        {
            PlayerPrefs.SetInt("unsavedUpgradeLevel", timeUpgradeLevel + 1);
            updatePrices();
            updateLevels();
        }
    }

    public void upgradeInteraction()
    {
        buttonPushSound.Play();
        int interactionUpgradeLevel = PlayerPrefs.GetInt("interactionUpgradeLevel");
        int interactionUpgradeCost = 100 * (interactionUpgradeLevel + 1);
        if (coinSystem.purchase(interactionUpgradeCost) && interactionUpgradeLevel < 10)
        {
            PlayerPrefs.SetInt("unsavedInteractionUpgradeLevel", interactionUpgradeLevel + 1);
            updatePrices();
            updateLevels();
        }
    }

    public void upgradeRunning()
    {
        buttonPushSound.Play();
        int runningUpgradeLevel = PlayerPrefs.GetInt("runningUpgradeLevel");
        int runningUpgradeCost = 100 * (runningUpgradeLevel + 1);
        if (coinSystem.purchase(runningUpgradeCost) && runningUpgradeLevel < 10)
        {
            PlayerPrefs.SetInt("unsavedRunningUpgradeLevel", runningUpgradeLevel + 1);
            updatePrices();
            updateLevels();
        }
    }

    public void upgradeCoins()
    {
        buttonPushSound.Play();
        int coinsUpgradeLevel = PlayerPrefs.GetInt("coinsUpgradeLevel");
        int coinsUpgradeCost = 100 * (coinsUpgradeLevel + 1);
        if (coinSystem.purchase(coinsUpgradeCost) && coinsUpgradeLevel < 10)
        {
            PlayerPrefs.SetInt("unsavedCoinsUpgradeLevel", coinsUpgradeLevel + 1);
            updatePrices();
            updateLevels();
        }
    }
    
    public void leaveStore()
    {
        if (extrasStoreUI == null) //if there is no extras store, upgrade store immediately closes when leaving
        {
            PlayerPrefs.SetFloat("breakTimeLeft", PlayerPrefs.GetFloat("breakTimeLeft") - (300.0f - (PlayerPrefs.GetInt("unsavedInteractionUpgradeLevel") * 30.0f)));
            SceneManager.LoadScene("BreakScene");
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

using UnityEngine;
using UnityEngine.SceneManagement;
using TMPro;
using UnityEngine.UI;
using System;
using System.Linq;
public class ScoreManager : MonoBehaviour
{
    public SequenceManager sequenceManager;
    public bool isOutcomeMenu; //Set as true in the outcome menu scene

    public CoinSystem coinSystem;

    // Scores and Increments for Each Class
    int classAScore;
    int playerScoreIncrement; // Daily Score Increment based on player choices
    int classBScore;
    int classBIncrement;
    int classCScore;
    int classCIncrement;
    int classDScore;
    int classDIncrement;

    int classAPosition;

    public Text classAIncText;
    public Text classBIncText;
    public Text classCIncText;
    public Text classDIncText;

    public TextMeshProUGUI feedbackText;

    public Image classABar;
    public Image classBBar;
    public Image classCBar;
    public Image classDBar;

    public Schedule schedule;

    int coinGain;
    public TextMeshProUGUI coinGainText;

    public AudioPlayer audioPlayer;

    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        coinGain = 0;
        classAScore = PlayerPrefs.GetInt("classAScore");
        classBScore = PlayerPrefs.GetInt("classBScore");
        classCScore = PlayerPrefs.GetInt("classCScore");
        classDScore = PlayerPrefs.GetInt("classDScore");
        playerScoreIncrement = PlayerPrefs.GetInt("playerScoreIncrement");
        updateDisplayedResults();
    }

    void updateDisplayedResults()
    {
        randomizeOtherClassIncrements();
        updateClassAPoints();
        updateClassAPosition();
        updateFeedback();
        giveCoins();
        updateIncs();
        updateBars();
        playerScoreIncrement = 0;
    }

    void updateClassAPoints()
    {
        classAScore = classAScore + playerScoreIncrement;
        PlayerPrefs.SetInt("classAScore", classAScore);
        PlayerPrefs.SetInt("playerScoreIncrement", 0);
    }

    void updateClassAPosition()
    {
        int position = 1;
        if (classAScore < classBScore)
        {
            position++;
        }
        if (classAScore < classCScore)
        {
            position++;
        }
        if (classAScore < classDScore)
        {
            position++;
        }
        classAPosition = position;
    }


    void updateBars()
    {
        float total = classAScore + classBScore + classCScore + classDScore;
        classABar.fillAmount = (float)classAScore / total;
        classBBar.fillAmount = (float)classBScore / total;
        classCBar.fillAmount = (float)classCScore / total;
        classDBar.fillAmount = (float)classDScore / total;
    }
    void updateIncs()
    {
        if (PlayerPrefs.GetString("currentWeekDay") == "Friday")
        {
            classAIncText.text = classAScore.ToString();
            classBIncText.text = classBScore.ToString();
            classCIncText.text = classCScore.ToString();
            classDIncText.text = classDScore.ToString();
        }
        else
        {
            classAIncText.text = displayIncAsString(playerScoreIncrement);
            classBIncText.text = displayIncAsString(classBIncrement);
            classCIncText.text = displayIncAsString(classCIncrement);
            classDIncText.text = displayIncAsString(classDIncrement);
        }
    }

    void updateFeedback()
    {
        string feedback = PlayerPrefs.GetString("feedback");
        string[] feedbackLines = feedback.Split(new[] { '\n' }, System.StringSplitOptions.RemoveEmptyEntries);
        System.Random rnd = new System.Random();
        string[] selectedLines = feedbackLines.OrderBy(x => rnd.Next()).Take(Mathf.Min(5, feedbackLines.Length)).ToArray();
        string randomFeedback = string.Join("\n", selectedLines);
        feedbackText.text = "- Emoções Identificadas: " + PlayerPrefs.GetString("identifiedEmotions") + "\n" + randomFeedback;
    }

    string displayIncAsString(int inc)
    {
        if (inc > 0)
        {
            return "+" + inc.ToString();
        }
        else if (inc < 0)
        {
            return inc.ToString();
        }
        else
        {
            return "0";
        }
    }

    void incrementPlayerScore(int score)
    {
        playerScoreIncrement += score;
        PlayerPrefs.SetInt("playerScoreIncrement", playerScoreIncrement);
    }

    void resetPlayerIncrement()
    {
        playerScoreIncrement = 0;
    }

    void randomizeOtherClassIncrements()
    {
        classBIncrement = UnityEngine.Random.Range(-3, 6);
        classCIncrement = UnityEngine.Random.Range(-3, 6);
        classDIncrement = UnityEngine.Random.Range(-3, 6);

        classBScore = Mathf.Clamp(classBScore + classBIncrement, 0, 70);
        classCScore = Mathf.Clamp(classCScore + classCIncrement, 0, 70);
        classDScore = Mathf.Clamp(classDScore + classDIncrement, 0, 70);
    }

    void saveScores()
    {
        PlayerPrefs.SetInt("classAScore", classAScore);
        PlayerPrefs.SetInt("classBScore", classBScore);
        PlayerPrefs.SetInt("classCScore", classCScore);
        PlayerPrefs.SetInt("classDScore", classDScore);
        PlayerPrefs.SetInt("playerScoreIncrement", playerScoreIncrement);
        if (PlayerPrefs.GetString("currentWeekDay") == "Friday")
        {
            resetScores();
        }
    }

    void saveUpgrades()
    { 
        PlayerPrefs.SetInt("timeUpgradeLevel", PlayerPrefs.GetInt("unsavedTimeUpgradeLevel"));
        PlayerPrefs.SetInt("interactionUpgradeLevel", PlayerPrefs.GetInt("unsavedInteractionUpgradeLevel"));
        PlayerPrefs.SetInt("runningUpgradeLevel", PlayerPrefs.GetInt("unsavedRunningUpgradeLevel"));
        PlayerPrefs.SetInt("coinsUpgradeLevel", PlayerPrefs.GetInt("unsavedCoinsUpgradeLevel"));
    }

    void resetScores()
    {
        PlayerPrefs.SetInt("classAScore", 0);
        PlayerPrefs.SetInt("classBScore", 0);
        PlayerPrefs.SetInt("classCScore", 0);
        PlayerPrefs.SetInt("classDScore", 0);
        playerScoreIncrement = 0;
        PlayerPrefs.SetInt("playerScoreIncrement", playerScoreIncrement);
    }

    void resetUpgrades()
    {
        PlayerPrefs.SetInt("unsavedTimeUpgradeLevel", 0);
        PlayerPrefs.SetInt("unsavedInteractionUpgradeLevel", 0);
        PlayerPrefs.SetInt("unsavedRunningUpgradeLevel", 0);
        PlayerPrefs.SetInt("unsavedCoinsUpgradeLevel", 0);
    }

    public void saveAndMenu()
    {
        audioPlayer.playButtonPushSound();
        PlayerPrefs.SetInt("savedGameExists", 0);
        saveScores();
        saveUpgrades();
        resetPlayerIncrement();
        coinSystem.saveDailyCoins(coinGain);
        sequenceManager.toMainMenu();
    }

    public void saveAndContinue()
    {
        Debug.Log("Current Week Day: " + PlayerPrefs.GetString("currentWeekDay"));
        audioPlayer.playButtonPushSound();
        PlayerPrefs.SetInt("savedGameExists", 0);
        saveScores();
        saveUpgrades();
        resetPlayerIncrement();
        Debug.Log("Current Week Day: " + PlayerPrefs.GetString("currentWeekDay"));
        coinSystem.saveDailyCoins(coinGain);
        Debug.Log("Current Week Day: " + PlayerPrefs.GetString("currentWeekDay"));
        sequenceManager.startDay();
    }

    void giveCoins()
    {
        // Calculate coin gain based on player score increment and class A position
        if (playerScoreIncrement >= 0)
        {
            coinGain = playerScoreIncrement * (5 - classAPosition) + PlayerPrefs.GetInt("coinsUpgradeLevel") * 10;
        }
        else if (playerScoreIncrement < 0)
        {
            coinGain = 0 + PlayerPrefs.GetInt("coinsUpgradeLevel") * 2;
        }

        //Calculate coin gain based on class A position when in the end of the week
        if (PlayerPrefs.GetString("currentWeekDay") == "Friday")
        {
            if (classAPosition == 1)
            {
                coinGain += 30 + PlayerPrefs.GetInt("coinsUpgradeLevel") * 10;
            }
            else if (classAPosition == 2)
            {
                coinGain += 20 + PlayerPrefs.GetInt("coinsUpgradeLevel") * 10;
            }
            else if (classAPosition == 3)
            {
                coinGain += 10 + PlayerPrefs.GetInt("coinsUpgradeLevel") * 10;
            }
            else if (classAPosition == 4)
            {
                coinGain += 0;
            }
        }
        coinGainText.text = "+" + coinGain.ToString();
        Debug.Log("Coin Gain: " + coinGain);
    }
}

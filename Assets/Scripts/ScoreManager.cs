using UnityEngine;
using TMPro;
using UnityEngine.UI;
public class ScoreManager : MonoBehaviour
{
    public bool isOutcomeMenu; //Set as true in the outcome menu scene

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

    // Values for UI Positioting
    //int firstPositionY = 140;
    //int secondPositionY = 70;
    //int thirdPositionY = 0;
    //int fourthPositionY = -70;

    public Text classAIncText;
    public Text classBIncText;
    public Text classCIncText;
    public Text classDIncText;

    public Image classABar;
    public Image classBBar;
    public Image classCBar;
    public Image classDBar;

    public Schedule schedule;

    int coinGain;
    public TextMeshProUGUI coinGainText;

    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        coinGain = 0;
        PlayerPrefs.SetString("gameState", "dayOutcome");
        PlayerPrefs.SetString("currentPhase", "dayOutcome");
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
        giveCoins();
        updateIncs();
        updateBars();
    }

    void updateClassAPoints()
    {
        PlayerPrefs.SetInt("classAScore", classAScore + playerScoreIncrement);
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
        classABar.fillAmount = (float)classAScore / 70f; //70 Points is the max score possible per week
        classBBar.fillAmount = (float)classBScore / 70f;
        classCBar.fillAmount = (float)classCScore / 70f;
        classDBar.fillAmount = (float)classDScore / 70f;
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
        classBIncrement = Random.Range(-3, 6);
        classCIncrement = Random.Range(-3, 6);
        classDIncrement = Random.Range(-3, 6);

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
        PlayerPrefs.SetInt("timeUpgradeLevel", PlayerPrefs.GetInt("unsavedUpgradeLevel"));
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
        PlayerPrefs.SetInt("unsavedUpgradeLevel", 0);
        PlayerPrefs.SetInt("unsavedInteractionUpgradeLevel", 0);
        PlayerPrefs.SetInt("unsavedRunningUpgradeLevel", 0);
        PlayerPrefs.SetInt("unsavedCoinsUpgradeLevel", 0);
    }

    public void saveAndMenu()
    {
        PlayerPrefs.SetInt("savedGameExists", 0);
        saveScores();
        saveUpgrades();
        resetPlayerIncrement();
        schedule.nextPhase();
        schedule.nextDay();
        PlayerPrefs.SetInt("coins", PlayerPrefs.GetInt("coins") + coinGain);
        UnityEngine.SceneManagement.SceneManager.LoadScene("MainMenu");
    }

    public void saveAndContinue()
    {
        PlayerPrefs.SetInt("savedGameExists", 0);
        saveScores();
        saveUpgrades();
        resetPlayerIncrement();
        PlayerPrefs.SetString("gameState", "staticSceneOutsideBreak");
        schedule.nextPhase();
        schedule.nextDay();
        PlayerPrefs.SetInt("coins", PlayerPrefs.GetInt("coins") + coinGain);
        UnityEngine.SceneManagement.SceneManager.LoadScene("Classroom");
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
    }
}

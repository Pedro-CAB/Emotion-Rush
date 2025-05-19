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

    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        PlayerPrefs.SetString("gameState", "dayOutcome");
        PlayerPrefs.SetString("currentPhase", "dayOutcome");
        classAScore = PlayerPrefs.GetInt("classAScore");
        classBScore = PlayerPrefs.GetInt("classBScore");
        classCScore = PlayerPrefs.GetInt("classCScore");
        classDScore = PlayerPrefs.GetInt("classDScore");
        updateDisplayedResults();
    }

    void updateDisplayedResults()
    {
        randomizeOtherClassIncrements();
        updateIncs();
        updateBars();
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
        classAIncText.text = displayIncAsString(playerScoreIncrement);
        classBIncText.text = displayIncAsString(classBIncrement);
        classCIncText.text = displayIncAsString(classCIncrement);
        classDIncText.text = displayIncAsString(classDIncrement);
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
        Debug.Log("Class B Increment: " + classBIncrement);
        Debug.Log("Class C Increment: " + classCIncrement);
        Debug.Log("Class D Increment: " + classDIncrement);
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
        playerScoreIncrement = 0;
        PlayerPrefs.SetInt("playerScoreIncrement", playerScoreIncrement);
    }

    public void saveAndMenu()
    {
        saveScores();
        resetPlayerIncrement();
        schedule.nextPhase();
        schedule.nextDay();
        UnityEngine.SceneManagement.SceneManager.LoadScene("MainMenu");
    }

    public void saveAndContinue()
    {
        saveScores();
        resetPlayerIncrement();
        PlayerPrefs.SetString("gameState", "staticSceneOutsideBreak");
        schedule.nextPhase();
        schedule.nextDay();
        UnityEngine.SceneManagement.SceneManager.LoadScene("Classroom");
    }
}

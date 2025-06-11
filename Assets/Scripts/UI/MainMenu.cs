using UnityEngine;
using UnityEngine.SceneManagement;
using System.Collections;

public class MainMenu : MonoBehaviour
{
    public SequenceController sequenceController;
    public AudioPlayer audioPlayer;

    public GameObject mainMenuUI;
    public GameObject areYouSureUI;
    public GameObject newGameButton;
    public GameObject loadGameButton;
    public void Start()
    {
        sequenceController = GetComponent<SequenceController>();
        areYouSureUI.SetActive(false);
        cleanPrefs();
        if (PlayerPrefs.GetInt("savedGameExists") == -1) //if there is no saved game, there is no option to load game
        {
            loadGameButton.SetActive(false);
        }
        else
        {
            loadGameButton.SetActive(true);
        }
    }

    public void AreYouSure()
    {
        audioPlayer.playButtonPushSound();
        if (PlayerPrefs.GetInt("savedGameExists") == -1) //if there is no saved game, there is no option to load game
        {
            NewGame();
        }
        else
        {
            areYouSureUI.SetActive(true);
            mainMenuUI.SetActive(false);
        }
    }

    public void NotSure()
    {
        audioPlayer.playButtonPushSound();
        areYouSureUI.SetActive(false);
        mainMenuUI.SetActive(true);
    }

    public void NewGame()
    {
        audioPlayer.playButtonPushSound();
        resetPrefsForNewGame();
        StartCoroutine(WaitAndContinue(0.47f));
        sequenceController.startDay();

        IEnumerator WaitAndContinue(float time)
        {
            yield return new WaitForSeconds(time);
        }
    }

    public void LoadGame()
    {
        audioPlayer.playButtonPushSound();
        StartCoroutine(WaitAndContinue(0.47f));
        sequenceController.startDay();

        IEnumerator WaitAndContinue(float time)
        {
            yield return new WaitForSeconds(time);
        }
    }

    /// <summary>
    /// Cleans Contents of PlayerPrefs that should not be saved between game sessions.
    /// </summary>
    public void cleanPrefs()
    {
        PlayerPrefs.SetString("gameState", "mainMenu");
        PlayerPrefs.SetString("currentPhase", "MorningClass1");
        PlayerPrefs.SetString("feedback", "");
        PlayerPrefs.SetString("identifiedEmotions", "");
        PlayerPrefs.SetInt("unsavedCoinsSpent", 0);
        PlayerPrefs.SetInt("unsavedTimeUpgradeLevel", 0);
        PlayerPrefs.SetInt("unsavedInteractionUpgradeLevel", 0);
        PlayerPrefs.SetInt("unsavedRunningUpgradeLevel", 0);
        PlayerPrefs.SetInt("unsavedCoinsUpgradeLevel", 0);
        PlayerPrefs.SetInt("playerScoreIncrement", 0);
    }

    public void resetPrefsForNewGame()
    {
        PlayerPrefs.SetFloat("breakTimeLeft", 300.0f);
        PlayerPrefs.SetString("gameState", "mainMenu");
        PlayerPrefs.SetString("currentPhase", "MorningClass1");
        PlayerPrefs.SetInt("currentDay", 0);
        PlayerPrefs.SetInt("currentWeek", 0);
        Debug.Log("resetPrefsForNewGame :: Current Week Day: Monday ");
        PlayerPrefs.SetString("currentWeekDay", "Monday");
        PlayerPrefs.SetString("feedback", "");
        PlayerPrefs.SetString("identifiedEmotions", "");
        PlayerPrefs.SetInt("coins", 0);
        PlayerPrefs.SetInt("unsavedCoinsSpent", 0);
        PlayerPrefs.SetInt("classAScore", 0);
        PlayerPrefs.SetInt("classBScore", 0);
        PlayerPrefs.SetInt("classCScore", 0);
        PlayerPrefs.SetInt("classDScore", 0);
        PlayerPrefs.SetInt("unsavedTimeUpgradeLevel", 0);
        PlayerPrefs.SetInt("unsavedInteractionUpgradeLevel", 0);
        PlayerPrefs.SetInt("unsavedRunningUpgradeLevel", 0);
        PlayerPrefs.SetInt("unsavedCoinsUpgradeLevel", 0);
        PlayerPrefs.SetInt("timeUpgradeLevel", 0);
        PlayerPrefs.SetInt("interactionUpgradeLevel", 0);
        PlayerPrefs.SetInt("runningUpgradeLevel", 0);
        PlayerPrefs.SetInt("coinsUpgradeLevel", 0);
        PlayerPrefs.SetInt("playerScoreIncrement", 0);
        PlayerPrefs.SetInt("savedGameExists", -1);
    }

    public void QuitGame()
    {
        audioPlayer.playButtonPushSound();
        //Debug.Log("Game Quit");
        Application.Quit();
    }

    public void Options()
    {
        audioPlayer.playButtonPushSound();
        //Debug.Log("Options Menu Opened");
    }
}

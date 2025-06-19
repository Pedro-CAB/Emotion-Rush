using UnityEngine;
using UnityEngine.SceneManagement;
using System.Collections;

public class MainMenu : MonoBehaviour
{
    public SequenceController sequenceController;
    public AudioPlayer audioPlayer;

    public GameObject mainMenuUI;
    public GameObject areYouSureUI;
    public GameObject helpUI;
    public GameObject newGameButton;
    public GameObject loadGameButton;
    public GameObject helpButton;

    public GameObject[] helpPages;
    public GameObject helpNextButton;
    public GameObject helpPreviousButton;
    private int currentPageIndex = 0;
    public void Start()
    {
        sequenceController = GetComponent<SequenceController>();
        areYouSureUI.SetActive(false);
        helpUI.SetActive(false);
        cleanPrefs();
        if (PlayerPrefs.GetInt("savedGameExists") == -1) //if there is no saved game, there is no option to load game
        {
            loadGameButton.SetActive(false);
            Vector3 loadGamePos = loadGameButton.transform.position;
            Vector3 helpButtonPos = helpButton.transform.position;
            helpButton.transform.position = new Vector3(helpButtonPos.x, loadGamePos.y, helpButtonPos.z);
        }
        else
        {
            loadGameButton.SetActive(true);
            //Vector3 helpButtonPos = helpButton.transform.position;
            //helpButton.transform.position = new Vector3(helpButtonPos.x, -528, helpButtonPos.z);
        }
        helpButton.SetActive(true);
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

    public void Help()
    {
        audioPlayer.playButtonPushSound();
        helpPreviousButton.SetActive(false);
        helpUI.SetActive(true);
        mainMenuUI.SetActive(false);
        currentPageIndex = 0;
        for (int i = 0; i < helpPages.Length; i++)
        {
            helpPages[i].SetActive(i == currentPageIndex);
        }
    }

    public void NextHelpPage()
    {
        audioPlayer.playButtonPushSound();
        if (currentPageIndex < helpPages.Length - 1)
        {
            helpPages[currentPageIndex].SetActive(false);
            currentPageIndex++;
            helpPages[currentPageIndex].SetActive(true);
            if (currentPageIndex == helpPages.Length - 1)
            {
                helpNextButton.SetActive(false);
            }
            helpPreviousButton.SetActive(true);
        }
    }

    public void PreviousHelpPage()
    {
        audioPlayer.playButtonPushSound();
        if (currentPageIndex > 0)
        {
            helpPages[currentPageIndex].SetActive(false);
            currentPageIndex--;
            helpPages[currentPageIndex].SetActive(true);
            if (currentPageIndex == 0)
            {
                helpPreviousButton.SetActive(false);
            }
            helpNextButton.SetActive(true);
        }
    }

    public void BackToMainMenu()
    {
        audioPlayer.playButtonPushSound();
        helpUI.SetActive(false);
        mainMenuUI.SetActive(true);
        currentPageIndex = 0;
        for (int i = 0; i < helpPages.Length; i++)
        {
            helpPages[i].SetActive(false);
        }
        helpPreviousButton.SetActive(false);
        helpNextButton.SetActive(true);
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

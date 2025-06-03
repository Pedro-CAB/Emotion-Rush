using UnityEngine;
using UnityEngine.SceneManagement;
using System.Collections;

public class MainMenu : MonoBehaviour
{

    public AudioSource buttonPushSound;

    public GameObject mainMenuUI;
    public GameObject areYouSureUI;
    public GameObject newGameButton;
    public GameObject loadGameButton;

    GameStateMachine stateMachine;
    public void Start()
    {
        stateMachine.toMainMenu();
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
        buttonPushSound.Play();
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
        buttonPushSound.Play();
        areYouSureUI.SetActive(false);
        mainMenuUI.SetActive(true);
    }

    public void NewGame()
    {
        buttonPushSound.Play();
        resetPrefsForNewGame();
        StartCoroutine(WaitAndContinue(0.47f));

        IEnumerator WaitAndContinue(float time)
        {
            yield return new WaitForSeconds(time);
        }
        randomizeDayBeginning();
    }

    public void LoadGame()
    {
        buttonPushSound.Play();
        randomizeDayBeginning();
    }

    /// <summary>
    /// Cleans Contents of PlayerPrefs that should not be saved between game sessions.
    /// </summary>
    public void cleanPrefs()
    {
        stateMachine.toMainMenu();
        PlayerPrefs.SetString("currentPhase", "MorningClass1");
        PlayerPrefs.SetString("feedback", "");
        PlayerPrefs.SetInt("unsavedTimeUpgradeLevel", 0);
        PlayerPrefs.SetInt("unsavedInteractionUpgradeLevel", 0);
        PlayerPrefs.SetInt("unsavedRunningUpgradeLevel", 0);
        PlayerPrefs.SetInt("unsavedCoinsUpgradeLevel", 0);
        PlayerPrefs.SetInt("playerScoreIncrement", 0);
    }

    public void resetPrefsForNewGame()
    {
        PlayerPrefs.SetFloat("breakTimeLeft", 300.0f);
        PlayerPrefs.SetString("currentPhase", "MorningClass1");
        PlayerPrefs.SetInt("currentDay", 0);
        PlayerPrefs.SetInt("currentWeek", 0);
        PlayerPrefs.SetString("currentWeekDay", "Monday");
        PlayerPrefs.SetString("feedback", "");
        PlayerPrefs.SetInt("coins", 0);
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

        stateMachine.startDay();
    }

    public void randomizeDayBeginning()
    {
        string[] classScenes = { "Classroom", "Library", "Auditorium", "Lab", "Gym" };
        System.Random random = new System.Random();
        int index = random.Next(classScenes.Length);
        string randomScene = classScenes[index];
        SceneManager.LoadScene(randomScene);
    }

    public void QuitGame()
    {
        buttonPushSound.Play();
        //Debug.Log("Game Quit");
        Application.Quit();
    }

    public void Options()
    {
        buttonPushSound.Play();
        //Debug.Log("Options Menu Opened");
    }
}

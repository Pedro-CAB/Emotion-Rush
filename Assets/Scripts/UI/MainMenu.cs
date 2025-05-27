using UnityEngine;
using UnityEngine.SceneManagement;

public class MainMenu : MonoBehaviour
{
    public GameObject newGameButton;
    public GameObject loadGameButton;
    public void Start()
    {
        if (PlayerPrefs.GetInt("savedGameExists") == -1) //if there is no saved game, there is no option to load game
        {
            loadGameButton.SetActive(false);
        }
        else
        {
            loadGameButton.SetActive(true);
        }
    }
    public void NewGame()
    {
        //Debug.Log("New Game Started");
        resetSave();
        //string[] classScenes = { "Classroom", "Library", "Auditorium", "Lab", "Playground" };
        randomizeDayBeginning();
    }

    public void LoadGame()
    {
        randomizeDayBeginning();
    }

    public void resetSave()
    {
        PlayerPrefs.SetFloat("breakTimeLeft", 300.0f);
        PlayerPrefs.SetString("gameState", "staticSceneOutsideBreak");
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
        //Debug.Log("Game Quit");
        Application.Quit();
    }

    public void Options(){
        //Debug.Log("Options Menu Opened");
    }
}

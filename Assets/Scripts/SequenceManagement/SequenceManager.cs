using UnityEngine;
using UnityEngine.SceneManagement;
using System.Collections;

public class SequenceManager : MonoBehaviour
{
    /// <summary>
    /// Schedule component that manages the game phases.
    /// This is used to transition to the next phase after the break ends.
    /// </summary>
    public Schedule schedule;

    /// <summary>
    /// Stores the Current Game State.
    /// </summary>
    [HideInInspector] public string gameState; // "mainMenu", "dayOutcome", "staticSceneOutsideBreak", "startingBreakScene", "ongoingBreakScene", "staticSceneDuringBreak"

    public AudioPlayer audioPlayer; // Reference to the AudioPlayer for playing scene music and sounds
    public void loadState()
    {
        gameState = PlayerPrefs.GetString("gameState");
    }

    public void saveState()
    {
        Debug.Log("Saving game state: " + gameState);
        PlayerPrefs.SetString("gameState", gameState);
    }

    public bool toMainMenu()
    {
        Debug.Log("Going to Main Menu...");
        loadState();
        gameState = "mainMenu";
        SceneManager.LoadScene("MainMenu");
        saveState();
        return true;
    }

    public void startDay()
    {
        Debug.Log("startDay :: Current Week Day: " + PlayerPrefs.GetString("currentWeekDay"));
        Debug.Log("Starting Day");
        loadState();
        Debug.Log("GameState: " + gameState);
        Debug.Log("Current Week: " + schedule.currentWeek);
        Debug.Log("startDay :: Current Week Day: " + PlayerPrefs.GetString("currentWeekDay"));
        if (gameState == "mainMenu")
        {
            gameState = "staticSceneOutsideBreak";
            if (PlayerPrefs.GetString("currentWeekDay") == "Monday" && schedule.currentWeek == 0 && PlayerPrefs.GetInt("savedGameExists") == -1)
            {
                SceneManager.LoadScene("Tutorial");
                saveState();
            }
            else
            {
                string randomScene = randomizeStaticScene();
                SceneManager.LoadScene(randomScene);
                saveState();
                schedule.nextDay();
            }
        }
        else if (gameState == "dayOutcome")
        {
            gameState = "staticSceneOutsideBreak";
            string randomScene = randomizeStaticScene();
            SceneManager.LoadScene(randomScene);
            saveState();
            schedule.nextPhase();
            schedule.nextDay();
        }
    }

    public void initiateBreak()
    {
        Debug.Log("Starting Break Scene");
        loadState();
        if (gameState == "staticSceneOutsideBreak")
        {
            gameState = "startingBreakScene";
            SceneManager.LoadScene("BreakScene");
            saveState();
            schedule.nextPhase();
        }
    }

    public void endBreak()
    {
        Debug.Log("Ending Break Scene");
        loadState();
        if (gameState == "startingBreakScene" || gameState == "ongoingBreakScene")
        {
            gameState = "staticSceneOutsideBreak";
            string randomScene = randomizeStaticScene();
            SceneManager.LoadScene(randomScene);
            saveState();
            schedule.nextPhase();
        }
    }

    public void initiateStaticSceneDuringBreak(string sceneName)
    {
        Debug.Log("Initiating static scene during break: " + sceneName);
        loadState();
        if (gameState == "startingBreakScene" || gameState == "ongoingBreakScene")
        {
            gameState = "staticSceneDuringBreak";
            SceneManager.LoadScene(sceneName);
            Debug.Log("GameState: " + gameState);
            saveState();
        }
    }

    public void endStaticSceneDuringBreak()
    {
            //audioPlayer.playSchoolBellSound();
            Debug.Log("Ending Static Scene During Break");
            loadState();
            if (gameState == "staticSceneDuringBreak")
            {
                gameState = "ongoingBreakScene";
                SceneManager.LoadScene("BreakScene");
                saveState();
        }
    }

    public void endStaticSceneDuringBreakAndBreak()
    {
            //audioPlayer.playSchoolBellSound();
            Debug.Log("Ending Static Scene During Break and Ending Break Scene");
            loadState();
            if (gameState == "staticSceneDuringBreak")
            {
                string randomScene = randomizeStaticScene();
                gameState = "staticSceneOutsideBreak";
                SceneManager.LoadScene(randomScene);
                saveState();
                schedule.nextPhase();
            }
    }

    public void endDay()
    {
        Debug.Log("Ending Day");
        loadState();
        if (gameState == "staticSceneOutsideBreak")
        {
            gameState = "dayOutcome";
            SceneManager.LoadScene("DayOutcome");
            saveState();
            schedule.nextPhase();
        }
    }

    public string randomizeStaticScene()
    {
        string[] classScenes = { "Classroom", "Auditorium", "Lab", "Gym" };
        System.Random random = new System.Random();
        int index = random.Next(classScenes.Length);
        string randomScene = classScenes[index];
        return randomScene;
    }


}

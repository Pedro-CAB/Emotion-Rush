using UnityEngine;
using UnityEngine.SceneManagement;

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

    public bool startDay()
    {
        Debug.Log("Current Week Day: " + PlayerPrefs.GetString("currentWeekDay"));
        Debug.Log("Starting Day");
        loadState();
        bool condition = gameState == "mainMenu" || gameState == "dayOutcome";
        if (condition)
        {
            gameState = "staticSceneOutsideBreak";
            string randomScene = randomizeStaticScene();
            SceneManager.LoadScene(randomScene);
            saveState();
            schedule.nextPhase();
            schedule.nextDay();
        }
        return condition;
    }

    public bool initiateBreak()
    {
        Debug.Log("Starting Break Scene");
        loadState();
        bool condition = gameState == "staticSceneOutsideBreak";
        if (condition)
        {
            gameState = "startingBreakScene";
            SceneManager.LoadScene("BreakScene");
            saveState();
            schedule.nextPhase();
        }
        return condition;
    }

    public bool endBreak()
    {
        Debug.Log("Ending Break Scene");
        loadState();
        bool condition = gameState == "startingBreakScene" || gameState == "ongoingBreakScene";
        if (gameState == "startingBreakScene" || gameState == "ongoingBreakScene")
        {
            gameState = "staticSceneOutsideBreak";
            string randomScene = randomizeStaticScene();
            SceneManager.LoadScene(randomScene);
            saveState();
            schedule.nextPhase();
        }
        return condition;
    }

    public bool initiateStaticSceneDuringBreak(string sceneName)
    {
        Debug.Log("Initiating static scene during break: " + sceneName);
        loadState();
        bool condition = gameState == "startingBreakScene" || gameState == "ongoingBreakScene";
        if (condition)
        {
            gameState = "staticSceneDuringBreak";
            SceneManager.LoadScene(sceneName);
            Debug.Log("GameState: " + gameState);
            saveState();
        }
        return condition;
    }

    public bool endStaticSceneDuringBreak()
    {
        Debug.Log("Ending Static Scene During Break");
        loadState();
        bool condition = gameState == "staticSceneDuringBreak";
        if (condition)
        {
            gameState = "ongoingBreakScene";
            SceneManager.LoadScene("BreakScene");
            saveState();
        }
        return condition;
    }

    public bool endStaticSceneDuringBreakAndBreak()
    {
        Debug.Log("Ending Static Scene During Break and Ending Break Scene");
        loadState();
        bool condition = gameState == "staticSceneDuringBreak";
        if (condition)
        {
            string randomScene = randomizeStaticScene();
            gameState = "staticSceneOutsideBreak";
            SceneManager.LoadScene(randomScene);
            saveState();
            schedule.nextPhase();
        }
        return condition;
    }

    public bool endDay()
    {
        Debug.Log("Ending Day");
        loadState();
        bool condition = gameState == "staticSceneOutsideBreak";
        if (condition)
        {
            gameState = "dayOutcome";
            SceneManager.LoadScene("DayOutcome");
            saveState();
            schedule.nextPhase();
        }
        return condition;
    }

    public string randomizeStaticScene()
    {
        string[] classScenes = { "Classroom", "Library", "Auditorium", "Lab", "Gym" };
        System.Random random = new System.Random();
        int index = random.Next(classScenes.Length);
        string randomScene = classScenes[index];
        return randomScene;
    }


}

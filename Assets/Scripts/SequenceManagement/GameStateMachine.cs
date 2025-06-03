using UnityEngine;
/// <summary>
/// Saves current game state and manages transitions between different game states.
/// </summary>
public class GameStateMachine : MonoBehaviour
{
    string gameState; //Possible States: "mainMenu", "dayOutcome", "staticSceneOutsideBreak", "staticSceneDuringBreak", "breakScene"

    private void saveState()
    {
        // Save the current game state to PlayerPrefs
        PlayerPrefs.SetString("gameState", gameState);
        PlayerPrefs.Save();
    }

    private void loadState()
    {
        gameState = PlayerPrefs.GetString("gameState");
    }

    public bool toMainMenu()
    {
        loadState();
        gameState = "mainMenu";
        saveState();
        return true;
    }

    public bool startDay()
    {
        loadState();
        bool condition = gameState == "mainMenu" || gameState == "dayOutcome";
        if (condition)
        {
            gameState = "staticSceneOutsideBreak";
            saveState();
        }
        return condition;
    }

    public bool initiateBreak()
    {
        loadState();
        string gameState = PlayerPrefs.GetString("gameState");
        bool condition = gameState == "staticSceneOutsideBreak";
        if (condition)
        {
            gameState = "breakScene";
            saveState();
        }
        return condition;
    }

    public bool endBreak()
    {
        loadState();
        string gameState = PlayerPrefs.GetString("gameState");
        bool condition = gameState == "breakScene";
        if (gameState == "breakScene")
        {
            gameState = "staticSceneOutsideBreak";
            saveState();
        }
        return condition;
    }

    public bool initiateStaticSceneDuringBreak()
    {
        loadState();
        string gameState = PlayerPrefs.GetString("gameState");
        bool condition = gameState == "breakScene";
        if (condition)
        {
            gameState = "staticSceneDuringBreak";
            saveState();
        }
        return condition;
    }

    public bool endStaticSceneDuringBreak()
    {
        loadState();
        string gameState = PlayerPrefs.GetString("gameState");
        bool condition = gameState == "staticSceneDuringBreak";
        if (condition)
        {
            gameState = "breakScene";
            saveState();
        }
        return condition;
    }

    public bool endStaticSceneDuringBreakAndBreak()
    {
        loadState();
        string gameState = PlayerPrefs.GetString("gameState");
        bool condition = gameState == "staticSceneDuringBreak";
        if (condition)
        {
            gameState = "staticSceneOutsideBreak";
            saveState();
        }
        return condition;
    }

    public bool endDay()
    {
        loadState();
        string gameState = PlayerPrefs.GetString("gameState");
        bool condition = gameState == "staticSceneOutsideBreak";
        if (condition)
        {
            gameState = "dayOutcome";
            saveState();
        }
        return condition;
    }
}

using UnityEngine;

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
    private string gameState;

    public void loadState()
    {
        gameState = PlayerPrefs.GetString("gameState");
    }

    public void saveState()
    {
        PlayerPrefs.SetString("gameState", gameState);
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

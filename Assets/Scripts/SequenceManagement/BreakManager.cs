using UnityEngine;
using TMPro;
using UnityEngine.SceneManagement;

public class BreakManager : SequenceManager
{
    /// <summary>
    /// TextMeshProUGUI component where timer is displayed.
    /// </summary>
    public TextMeshProUGUI timerText;

    /// <summary>
    /// Time that Break Scene Lasts
    /// </summary>
    private float standardBreakTime;

    /// <summary>
    /// Time Remaining in Break Scene
    /// </summary>
    private float timeLeft;

    /// <summary>
    /// Schedule component that manages the game phases.
    /// This is used to transition to the next phase after the break ends.
    /// </summary>
    //public Schedule schedule;

    /// <summary>
    /// Defines whether the timer is running or not.
    /// </summary>
    private bool isTimerRunning = false;

    void Start()
    {
        loadState();
        isTimerRunning = false;
        updateTimerText();
        StartCoroutine(WaitBeforeStartingTimer(2f)); // Wait 2 seconds before starting the timer
        //Debug.Log("standardBreakTime: " + standardBreakTime);
        //Debug.Log("timeLeft: " + timeLeft);
        standardBreakTime = 300.0f + 30.0f * PlayerPrefs.GetInt("timeUpgradeLevel");
        //Debug.Log("Game State is " + gameState);
        if (gameState == "ongoingBreakScene")
        {
            //Debug.Log("Loading Break Scene from Static Scene During Break");
            timeLeft = PlayerPrefs.GetFloat("breakTimeLeft");
        }
        else if (gameState == "startingBreakScene")
        {
            //Debug.Log("Loading Break Scene from Static Scene Outside Break");
            timeLeft = standardBreakTime; // Reset Timer
        }
    }

    void Update()
    {
        if (timeLeft <= 0.0f)
        {
            PlayerPrefs.SetFloat("breakTimeLeft", standardBreakTime); // Restart Timer
            endBreak();
        }
        else{
            updateTimerText();
        }
    }

    private System.Collections.IEnumerator WaitBeforeStartingTimer(float delay)
    {
        yield return new WaitForSeconds(delay);
        isTimerRunning = true;
    }

    /// <summary>
    /// Gets the time left in the break scene.
    /// </summary>
    /// <returns>Time left in the break scene.</returns>
    public float getTimeLeft()
    {
        return timeLeft;
    }

    /// <summary>
    /// Updates the value displayed in the timer and decreases the time left, if the timer is running.
    /// </summary>
    void updateTimerText(){
        if (isTimerRunning)
        {
            timeLeft -= Time.deltaTime;
            if (timeLeft < 0.0f)
            {
                timeLeft = 0.0f;
            }
        }
        int minutesLeft = Mathf.FloorToInt(timeLeft / 60.0f);
        int secondsLeft = Mathf.FloorToInt(timeLeft % 60.0f);
        timerText.text = string.Format("{0:00}:{1:00}", minutesLeft, secondsLeft);
    }
}

using UnityEngine;
using TMPro;
using UnityEngine.SceneManagement;

public class BreakManager : MonoBehaviour
{
    public TextMeshProUGUI timerText; // Reference to the TextMeshProUGUI component for displaying the timer

    public float timeLeft = 600.0f;

    void Start(){
        string gameState = PlayerPrefs.GetString("gameState");
        if (gameState == "staticSceneDuringBreak")
        {
            timeLeft = PlayerPrefs.GetFloat("breakTimeLeft");
            PlayerPrefs.SetString("gameState", "breakScene"); // Save Current Game State
        }
        else{
            timeLeft = 600.0f; // Set the initial time left to 10 minutes (600 seconds)
            PlayerPrefs.SetString("gameState", "breakScene"); // Save Current Game State
        }
    }

    // Update is called once per frame
    void Update()
    {
        if (timeLeft <= 0.0f)
        {
            PlayerPrefs.SetString("gameState", "staticSceneOutsideBreak"); // Save Current Game State
            PlayerPrefs.SetFloat("breakTimeLeft", 600.0f); // Restart Timer
            SceneManager.LoadScene("Classroom");
        }
        else{
            updateTimerText();
        }
    }

    void updateTimerText(){
        timeLeft -= Time.deltaTime;
        if (timeLeft < 0.0f)
        {
            timeLeft = 0.0f;
        }
        int minutesLeft = Mathf.FloorToInt(timeLeft / 60.0f);
        int secondsLeft = Mathf.FloorToInt(timeLeft % 60.0f);
        timerText.text = string.Format("{0:00}:{1:00}", minutesLeft, secondsLeft);
    }
}

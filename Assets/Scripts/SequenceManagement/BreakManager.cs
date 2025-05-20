using UnityEngine;
using TMPro;
using UnityEngine.SceneManagement;

public class BreakManager : MonoBehaviour
{
    public TextMeshProUGUI timerText; // Reference to the TextMeshProUGUI component for displaying the timer

    public float standardBreakTime = 100.0f; //Time that Break Scene Lasts

    public float timeLeft; //Time Remaining in Break Scene

    public Schedule schedule;

    void Start(){
        string gameState = PlayerPrefs.GetString("gameState");
        if (gameState == "staticSceneDuringBreak")
        {
            timeLeft = PlayerPrefs.GetFloat("breakTimeLeft");
            PlayerPrefs.SetString("gameState", "breakScene"); // Save Current Game State
        }
        else if (gameState == "staticSceneOutsideBreak"){
            timeLeft = standardBreakTime; // Reset Timer
            PlayerPrefs.SetString("gameState", "breakScene"); // Save Current Game State
        }
    }

    // Update is called once per frame
    void Update()
    {
        if (timeLeft <= 0.0f)
        {
            PlayerPrefs.SetString("gameState", "staticSceneOutsideBreak"); // Save Current Game State
            PlayerPrefs.SetFloat("breakTimeLeft", standardBreakTime); // Restart Timer
            SceneManager.LoadScene("Classroom");
            schedule.nextPhase();
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

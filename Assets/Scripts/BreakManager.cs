using UnityEngine;
using TMPro;
using UnityEngine.SceneManagement;

public class BreakManager : MonoBehaviour
{
    public TextMeshProUGUI timerText; // Reference to the TextMeshProUGUI component for displaying the timer

    float timeLeft = 10.0f;

    // Update is called once per frame
    void Update()
    {
        if (timeLeft <= 0.0f)
        {
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

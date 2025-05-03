using UnityEngine;
using UnityEngine.UI;

public class Pause : MonoBehaviour
{
    public GameObject pausedUI;
    public GameObject unpausedUI;
    public Button pauseButton;
    public Button resumeButton;
    public Button quitButton;
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        pausedUI.SetActive(false);
        unpausedUI.SetActive(true);
    }

    public void ResumeGame(){
        Debug.Log("Game Resumed");
    }

    public void PauseGame(){
        Debug.Log("Game Paused");
    }

    public void QuitGame(){
        Debug.Log("Back to Main Menu");
    }
}

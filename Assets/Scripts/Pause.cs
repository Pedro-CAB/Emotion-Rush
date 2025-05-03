using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class Pause : MonoBehaviour
{
    public GameObject pausedUI;
    public GameObject unpausedUI;

    void Start()
    {
        pausedUI.SetActive(false);
        unpausedUI.SetActive(true);
    }

    public void ResumeGame(){
        Debug.Log("Game Resumed");
        pausedUI.SetActive(false);
        unpausedUI.SetActive(true);
    }

    public void PauseGame(){
        Debug.Log("Game Paused");
        pausedUI.SetActive(true);
        unpausedUI.SetActive(false);
    }

    public void QuitGame(){
        Debug.Log("Back to Main Menu");
        SceneManager.LoadScene("MainMenu");
    }
}

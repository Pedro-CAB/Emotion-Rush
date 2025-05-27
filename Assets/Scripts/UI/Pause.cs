using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class Pause : MonoBehaviour
{
    public GameObject pausedUI;
    public GameObject unpausedUI;

    public GameObject areYouSureUI;

    void Start()
    {
        pausedUI.SetActive(false);
        unpausedUI.SetActive(true);
        areYouSureUI.SetActive(false);
    }

    public void ResumeGame(){
        //Debug.Log("Game Resumed");
        Time.timeScale = 1;
        pausedUI.SetActive(false);
        unpausedUI.SetActive(true);
    }

    public void PauseGame(){
        Time.timeScale = 0;
        //Debug.Log("Game Paused");
        pausedUI.SetActive(true);
        unpausedUI.SetActive(false);
    }

    public void AreYouSure()
    {
        areYouSureUI.SetActive(true);
        pausedUI.SetActive(false);
    }

    public void NotSure()
    {
        areYouSureUI.SetActive(false);
        pausedUI.SetActive(true);
    }

    public void QuitGame()
    {
        //Debug.Log("Back to Main Menu");
        SceneManager.LoadScene("MainMenu");
        Time.timeScale = 1;

        //Delete Unsaved Data if Player Leaves in the Middle of a Day
        PlayerPrefs.SetFloat("breakTimeLeft", 300.0f);
        PlayerPrefs.SetString("currentPhase", "MorningClass1");
        PlayerPrefs.SetString("gameState", "staticSceneOutsideBreak");
        PlayerPrefs.SetInt("playerScoreIncrement", 0);
        PlayerPrefs.SetInt("unsavedTimeUpgradeLevel", 0);
        PlayerPrefs.SetInt("unsavedInteractionUpgradeLevel", 0);
        PlayerPrefs.SetInt("unsavedRunningUpgradeLevel", 0);
        PlayerPrefs.SetInt("unsavedCoinsUpgradeLevel", 0);
        PlayerPrefs.SetString("feedback", "");
    }
}

using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class Pause : MonoBehaviour
{
    public SequenceController sequenceController; // Reference to the SequenceController for managing game state
    public GameObject pausedUI;
    public GameObject unpausedUI;

    public GameObject areYouSureUI;

    public GameObject helpUI;
    public AudioPlayer audioPlayer; // Reference to the AudioPlayer for playing scene music and sounds

    void Start()
    {
        pausedUI.SetActive(false);
        unpausedUI.SetActive(true);
        areYouSureUI.SetActive(false);
        helpUI.SetActive(false);
    }

    public void ResumeGame(){
        audioPlayer.playButtonPushSound();
        //Debug.Log("Game Resumed");
        Time.timeScale = 1;
        pausedUI.SetActive(false);
        unpausedUI.SetActive(true);
    }

    public void PauseGame(){
        audioPlayer.playButtonPushSound();
        Time.timeScale = 0;
        pausedUI.SetActive(true);
        unpausedUI.SetActive(false);
    }

    public void ShowHelp()
    {
        audioPlayer.playButtonPushSound();
        helpUI.SetActive(true);
        pausedUI.SetActive(false);
    }

    public void HideHelp()
    {
        audioPlayer.playButtonPushSound();
        helpUI.SetActive(false);
        pausedUI.SetActive(true);
    }


    public void AreYouSure()
    {
        audioPlayer.playButtonPushSound();
        areYouSureUI.SetActive(true);
        pausedUI.SetActive(false);
    }

    public void NotSure()
    {
        audioPlayer.playButtonPushSound();
        areYouSureUI.SetActive(false);
        pausedUI.SetActive(true);
    }

    public void QuitGame()
    {
        audioPlayer.playButtonPushSound();
        sequenceController.toMainMenu();
        Time.timeScale = 1;
    }
}

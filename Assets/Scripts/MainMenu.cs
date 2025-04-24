using UnityEngine;
using UnityEngine.SceneManagement;

public class MainMenu : MonoBehaviour
{
    public void NewGame(){
        Debug.Log("New Game Started");
        SceneManager.LoadScene("TestMap"); // Replace with your game scene name
    }

    public void LoadGame(){
        Debug.Log("Load Game Started");
    }

    public void QuitGame(){
        Debug.Log("Game Quit");
        Application.Quit();
    }

    public void Options(){
        Debug.Log("Options Menu Opened");
    }
}

using UnityEngine;
using UnityEngine.SceneManagement;
using System.Collections.Generic;
public class StaticSceneManager : MonoBehaviour
{
    public Player player;
    public DialogueManager dialogueManager;

    public Schedule schedule;

    private string currentSceneName;

    private DialogueSequence dialogueSequence;

    List<DialogueLine> interactions = new List<DialogueLine> { };

    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        System.Random random = new System.Random(); // Create a new instance of Random
        currentSceneName = SceneManager.GetActiveScene().name; // Get the name of the current scene
        createInteractions(); // Create the interactions for the static scene
        int index = random.Next(interactions.Count);
        DialogueLine line = interactions[index]; // Get the first interaction line
        player.setStaticScene();
        dialogueManager.setCurrentLine(line); // Set the DialogueManager to start the scene with the first line of dialogue
    }

    private float dialogueInactiveTime = 0f;

    void Update()
    {
        if (!dialogueManager.isDialogueActive())
        {
            SceneManager.LoadScene("BreakScene");
            if (PlayerPrefs.GetString("gameState") == "staticSceneOutsideBreak")
            {
                if (PlayerPrefs.GetString("currentPhase") == "AfternoonClass")
                {
                    SceneManager.LoadScene("Classroom");
                }
                else
                {
                    SceneManager.LoadScene("BreakScene");
                }
                schedule.nextPhase();
            }
            else if (PlayerPrefs.GetString("gameState") == "staticSceneDuringBreak")
            {
                PlayerPrefs.SetFloat("breakTimeLeft", PlayerPrefs.GetFloat("breakTimeLeft") - (300.0f - (PlayerPrefs.GetInt("unsavedInteractionUpgradeLevel") * 30.0f)));
            }
            dialogueInactiveTime = 0f; // Reset timer after action
        }
        else
        {
            dialogueInactiveTime = 0f; // Reset timer if dialogue is active
        }
    }

    public void createInteractions()
    {
        if (currentSceneName == "Classroom")
        {
            createClassroomInteractions();
        }
        else if (currentSceneName == "Library")
        {
            //createLibraryInteractions();
        }
        else if (currentSceneName == "Bar")
        {
            //createBarInteractions();
        }
        else if (currentSceneName == "Auditorium")
        {
            //createAuditoriumInteractions();
        }
        else if (currentSceneName == "Lab")
        {
            //createLabInteractions();
        }
        else if (currentSceneName == "Playground")
        {
            //createPlaygroundInteractions();
        }
    }

    public void createClassroomInteractions()
    {
        dialogueSequence = new DialogueSequence("Assets/Interactions/Classroom/bad_haircut.json");
        interactions.Add(dialogueSequence.RootLine);
    }
}

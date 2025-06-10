using UnityEngine;
using UnityEngine.SceneManagement;
using System.Collections;
using System.Collections.Generic;
using System.IO;
public class StaticSceneManager : SequenceManager
{
    public DialogueManager dialogueManager;

    private string currentSceneName;

    private DialogueSequence dialogueSequence;

    List<DialogueSequence> interactions = new List<DialogueSequence> { };

    public InteractionLoader interactionLoader;

    // Start is called once before the first execution of Update after the MonoBehaviour is created
    IEnumerator Start()
    {
        System.Random random = new System.Random(); // Create a new instance of Random
        currentSceneName = SceneManager.GetActiveScene().name; // Get the name of the current scene
        yield return createSpecificInteractions(currentSceneName);

        int index = random.Next(interactions.Count);
        Debug.Log("Selected interaction index: " + index);
        Debug.Log("Interaction count: " + interactions.Count);
        DialogueLine line = interactions[index].RootLine; // Get the first interaction line
        //player.setStaticScene();
        dialogueManager.setCurrentLine(line); // Set the DialogueManager to start the scene with the first line of dialogue
    }

    void Update()
    {
        if (!dialogueManager.isDialogueActive()) //if dialogue is over
        {
            if (PlayerPrefs.GetString("gameState") == "staticSceneOutsideBreak") //if it was a static scene outside a break
            {
                if (PlayerPrefs.GetString("currentPhase") == "AfternoonClass") //if it was the last class of the day
                {
                    endDay();
                }
                else
                {
                    initiateBreak();
                }
            }
            else if (PlayerPrefs.GetString("gameState") == "staticSceneDuringBreak")
            {
                PlayerPrefs.SetFloat("breakTimeLeft", PlayerPrefs.GetFloat("breakTimeLeft") - (300.0f - (PlayerPrefs.GetInt("unsavedInteractionUpgradeLevel") * 10.0f)));
                float breakTimeLeft = PlayerPrefs.GetFloat("breakTimeLeft");
                if (breakTimeLeft <= 0.0f) //if the break is over
                {
                    endStaticSceneDuringBreakAndBreak();
                }
                else
                {
                    endStaticSceneDuringBreak();
                }
            }
        }
    }

    public void createInteractions()
    {
        createSpecificInteractions(currentSceneName);
    }


    public IEnumerator createSpecificInteractions(string sceneName)
    {
        yield return interactionLoader.LoadInteractionFiles(sceneName);
        interactions = interactionLoader.GetInteractions(); // Get the interactions from the InteractionLoader
        Debug.Log("Loaded interactions for " + sceneName + " scene:" + interactions.Count);
    }
}

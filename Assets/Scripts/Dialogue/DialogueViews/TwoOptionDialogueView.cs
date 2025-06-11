using UnityEngine;
using UnityEngine.UI;
using TMPro;

/// <summary>
/// TwoDialogueView is a class that serves as the View for dialogue boxes in the game.
/// </summary>
public class TwoOptionDialogueView : DialogueView
{

    /// <summary>
    /// TextMeshProUGUI component for the text of option A in dialogue options.
    /// </summary>
    public TextMeshProUGUI optionAText;

    /// <summary>
    /// TextMeshProUGUI component for the text of option B in dialogue options.
    /// </summary>
    public TextMeshProUGUI optionBText;

    /// <summary>
    /// Starts displaying a Two Option Dialogue Box and typing a Two Option Dialogue Prompt for it.
    /// </summary>
    /// <param name="l">DialogueLine struct containing information on the line to be displayed.</param>
    /// <param name="triggeredBy">Name of the object that triggered the current dialogue. Null, if no object triggered the dialogue.</param>
    public override void handleDialogue(DialogueLine l, string triggeredBy = null)
    {
        //DisplayDialogueView();
        trigger = triggeredBy; // Store the object that triggered the dialogue
        enabled = true; // Enable the dialogue box component to show the dialogue box
        currentLine = l;

        //index = 0;
        StartCoroutine(TypeLine());
        optionAText.text = l.dialogueOptions[0].content; // Set the text for option A button
        optionBText.text = l.dialogueOptions[1].content; // Set the text for option B button
    }
    
    /// <summary>
    /// Processes the player's choice picked from the available buttons.
    /// </summary>
    /// <param name="option">String containing the option the player picked between A, B or C.</param>
    public void PickOption(string option)
    {
        StopAllCoroutines();
        audioPlayer.playButtonPushSound(); // Play the button sound when picking an option
        int optionIndex = 0;
        if (option == "A")
        {
            optionIndex = 0; // Option A corresponds to index 0
        }
        else if (option == "B")
        {
            optionIndex = 1; // Option B corresponds to index 1
        }

        DialogueLine chosenLine = currentLine.dialogueOptions[optionIndex];
        PlayerPrefs.SetInt("playerScoreIncrement", PlayerPrefs.GetInt("playerScoreIncrement") + chosenLine.score); // Increment the player's score based on the chosen line's score
        Debug.Log("Player Score Increment: " + PlayerPrefs.GetInt("playerScoreIncrement"));
        DialogueLine nextLine = chosenLine.nextLine;
        if (nextLine != null)
        {
            dialogueController.setCurrentLine(nextLine);
        }
        if (trigger != null && trigger.Contains("Door"))
        {
            if (option == "A")
            {
                string roomName = trigger.Substring(0, trigger.Length - 4); // Remove "Door" from the trigger name
                //PlayerPrefs.SetString("gameState", "staticSceneDuringBreak"); // Save Current Game State
                breakController.saveTimeLeft(); // Save the current break time left
                breakController.initiateStaticSceneDuringBreak(roomName);
            }
            else if (option == "B")
            {
                //Player answered "No" to the door prompt
                //Do nothing and close the dialogue box.
                HideDialogueView();
            }
        }
        else if (chosenLine.feedback != "None")
        {
            if (PlayerPrefs.GetString("feedback") == "")
            {
                PlayerPrefs.SetString("feedback", chosenLine.feedback); // Save the feedback for the chosen line
            }
            else
            {
                PlayerPrefs.SetString("feedback", PlayerPrefs.GetString("feedback") + "\n" + chosenLine.feedback); // Append the feedback for the chosen line
            }
        }
        enabled = false; // Disable the dialogue box component after picking an option
    }
}

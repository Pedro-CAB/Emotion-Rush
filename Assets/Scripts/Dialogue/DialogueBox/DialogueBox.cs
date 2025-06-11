using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;
using TMPro;
using UnityEngine.SceneManagement;
using System;
using System.Linq;

/// <summary>
/// DialogueBox is a MonoBehaviour that manages the display and interaction of dialogue boxes in the game.
/// It handles both linear dialogue and option-based dialogue, allowing players to interact with characters and make choices that affect the game state.
/// It includes methods for starting dialogue lines, typing them out, picking options, and hiding the dialogue box.
/// </summary>
public abstract class DialogueBox : MonoBehaviour
{
    /// <summary>
    /// TextMeshProUGUI component that contains the Dialogue Prompt presented to the player.
    /// </summary>
    public TextMeshProUGUI textComponent;

    /// <summary>
    /// DialogueManager component that manages the dialogue logic in current scene.
    /// </summary>
    public DialogueManager dialogueManager;

    /// <summary>
    /// BreakManager component that manages the break scene logic in the game.
    /// </summary>
    public BreakManager breakManager;

    /// <summary>
    /// DialogueLine struct where the current line to be displayed is stored.
    /// </summary>
    public DialogueLine currentLine;

    /// <summary>
    /// Speed at which the text is typed in the dialogue box.
    /// </summary>
    public float textSpeed;

    /// <summary>
    /// Stores the name of the object that triggered the current dialogue, if there is one.
    /// Used for Door interactions.
    /// </summary>
    [HideInInspector] public string trigger;

    /// <summary>
    /// Component for generating random numbers used for randomizing options in Emotion Identification segments.
    /// </summary>
    public System.Random random;

    /// <summary>
    /// AudioPlayer component that manages audio playback in the game.
    /// </summary>
    public AudioPlayer audioPlayer;

    void Start()
    {
        textComponent.text = string.Empty;
        random = new System.Random(); // Initialize the random number generator
    }

    //Handling Linear Dialogue --------------------------------------------------------------------------------------------

    /// <summary>
    /// Start DIsplaying a Linear Dialogue Box and Typing a Linear Dialogue Line on it.
    /// </summary>
    /// <param name="l">Linear Line to be typed and displayed.</param>
    /// <param name="triggeredBy">Name of the object that triggered the current dialogue. Null, if no object triggered the dialogue.</param>
    public abstract void handleDialogue(DialogueLine l, string triggeredBy = null);

    /// <summary>
    /// Coroutine that types out lines of dialogue one character at a time.
    /// </summary>
    /// <returns> IEnumerator that types out the line.</returns>
    public IEnumerator TypeLine()
    {
        textComponent.text = string.Empty; // Clear the text component before typing the new line
        foreach (char letter in currentLine.content.ToCharArray())
        {
            textComponent.text += letter; // Display each letter one by one
            yield return new WaitForSeconds(textSpeed); // Wait for the specified text speed before displaying the next letter
        }
    }

    /// <summary>
    /// Shows whole line if the line wasn't fully typed yet, or skips to next line when line is linear and fully displayed.
    /// If line is complete but not linear, it does nothing.
    /// </summary>
    public void SkipDialog()
    {
        audioPlayer.playButtonPushSound(); // Play the button sound when skipping dialogue
        if (textComponent.text == currentLine.content)
        { //If previous line is already fully displayed
            if (currentLine.type == DialogueLine.LineType.Linear)
            { //Only for linear lines
                if (currentLine.nextLine != null)
                {
                    textComponent.text = string.Empty; // Clear the text component before displaying the next line
                    //HideDialogueBox(); // Hide the dialogue box before displaying the next line
                    dialogueManager.setCurrentLine(currentLine.nextLine); // Set the next line as the current line
                }
                else
                {
                    HideDialogueBox(); // Hide the dialogue box after picking an option
                }
            }
        }
        else
        {
            StopAllCoroutines(); // Stop the typing coroutine if the button is pressed before the line is fully displayed
            textComponent.text = currentLine.content; // Display the full line immediately
        }


    }

    public void HideDialogueBox()
    {
        Debug.Log("Hiding Dialogue Box");
        textComponent.text = string.Empty; // Clear the text component when hiding the dialogue box
        gameObject.SetActive(false); // Disable the dialogue box component
    }
}

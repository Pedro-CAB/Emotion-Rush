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
public class LinearDialogueBox : DialogueBox
{
    void Start()
    {
        textComponent.text = string.Empty;
        random = new System.Random(); // Initialize the random number generator
    }

    /// <summary>
    /// Start Displaying a Linear Dialogue Box and Typing a Linear Dialogue Line on it.
    /// </summary>
    /// <param name="l">Linear Line to be typed and displayed.</param>
    /// <param name="triggeredBy">Name of the object that triggered the current dialogue. Null, if no object triggered the dialogue.</param>
    public override void handleDialogue(DialogueLine l, string triggeredBy = null)
    {
        trigger = triggeredBy; // Store the object that triggered the dialogue
        enabled = true; // Show the dialogue box when starting the dialogue
        currentLine = l;
        StartCoroutine(TypeLine()); // Start the coroutine to type out the first line of dialogue
    }

    /// <summary>
    /// Coroutine that types out lines of dialogue one character at a time.
    /// </summary>
    /// <returns> IEnumerator that types out the line.</returns>
    IEnumerator TypeLine()
    {
        textComponent.text = string.Empty; // Clear the text component before typing the new line
        foreach (char letter in currentLine.content.ToCharArray())
        {
            textComponent.text += letter; // Display each letter one by one
            yield return new WaitForSeconds(textSpeed); // Wait for the specified text speed before displaying the next letter
        }
    }
}

using UnityEngine;

/// <summary>
/// Handles dialogue logic.
/// </summary>
public class DialogueManager : MonoBehaviour
{
    /// <summary>
    /// Stores the current dialogue line being processed.
    /// </summary>
    private DialogueLine currentLine;

    /// <summary>
    /// The DialogueBox object for Linear dialogue lines.
    /// </summary>
    public LinearDialogueBox linearDialogueBox;

    /// <summary>
    /// The DialogueBox object for TwoOption dialogue lines.
    /// </summary>
    public TwoOptionDialogueBox twoOptionDialogueBox;

    /// <summary>
    /// The DialogueBox object for ThreeOption and EmotionOption dialogue lines.
    /// </summary>
    public ThreeOptionDialogueBox threeOptionDialogueBox;

    /// <summary>
    /// GameObject that triggered the dialogue.
    /// </summary>
    private string trigger;

    /// <summary>
    /// Sets the Current Line of dialogue to be displayed in the appropriate DialogueBox.
    /// Updates the GUI accordingly.
    /// </summary>
    /// <param name="line"></param>
    /// <param name="triggeredBy"></param>
    public void setCurrentLine(DialogueLine line, string triggeredBy = null)
    {
        currentLine = line;
        trigger = triggeredBy;
        currentLine.content = " " + currentLine.content;
        //Debug.Log("DialogueManager: Setting current line to: " + currentLine.content);
        //Debug.Log("DialogueManager: Type: " + currentLine.type);
        //Handle linear sequences with the correct GUI interface
        if (currentLine.type == DialogueLine.LineType.Linear)
        {
            //Debug.Log("DialogueManager: Starting linear dialogue:" + currentLine.content);
            linearDialogueBox.gameObject.SetActive(true); // Show the linear dialogue box
            twoOptionDialogueBox.gameObject.SetActive(false); // Hide the two option dialogue box
            threeOptionDialogueBox.gameObject.SetActive(false); // Hide the three option dialogue box
            linearDialogueBox.handleDialogue(currentLine, trigger);
        }
        //Handle two option sequences with the correct GUI interface
        else if (currentLine.type == DialogueLine.LineType.TwoOption)
        {
            //Debug.Log("DialogueManager: Starting two option dialogue:" + currentLine.content);
            twoOptionDialogueBox.gameObject.SetActive(true); // Show the two option dialogue box
            linearDialogueBox.gameObject.SetActive(false); // Hide the linear dialogue box
            threeOptionDialogueBox.gameObject.SetActive(false); // Hide the three option dialogue box
            twoOptionDialogueBox.handleDialogue(currentLine, trigger);
        }
        //Handle three option sequences with the correct GUI interface
        else if (currentLine.type == DialogueLine.LineType.ThreeOption || currentLine.type == DialogueLine.LineType.EmotionOption)
        {
            threeOptionDialogueBox.gameObject.SetActive(true); // Show the three option dialogue box
            linearDialogueBox.gameObject.SetActive(false); // Hide the linear dialogue box
            twoOptionDialogueBox.gameObject.SetActive(false); // Hide the two option dialogue box
            threeOptionDialogueBox.handleDialogue(currentLine, trigger);
        }
    }

    /// <summary>
    /// Checks if dialogue is currently happening.
    /// </summary>
    /// <returns>True if dialogue is happening. False otherwise.</returns>
    public bool isDialogueActive()
    {
        return linearDialogueBox.gameObject.activeSelf || twoOptionDialogueBox.gameObject.activeSelf || threeOptionDialogueBox.gameObject.activeSelf;
    }
}

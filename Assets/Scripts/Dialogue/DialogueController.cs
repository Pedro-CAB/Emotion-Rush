using UnityEngine;

/// <summary>
/// Controller that handles dialogue lines and manages the appropriate DialogueView based on the type of dialogue line.
/// </summary>
public class DialogueController : MonoBehaviour
{
    /// <summary>
    /// Stores the current dialogue line being processed.
    /// </summary>
    private DialogueLine currentLine;

    /// <summary>
    /// The DialogueView object for Linear dialogue lines.
    /// </summary>
    public LinearDialogueView linearDialogueView;

    /// <summary>
    /// The DialogueView object for TwoOption dialogue lines.
    /// </summary>
    public TwoOptionDialogueView twoOptionDialogueView;

    /// <summary>
    /// The DialogueView object for ThreeOption and EmotionOption dialogue lines.
    /// </summary>
    public ThreeOptionDialogueView threeOptionDialogueView;

    /// <summary>
    /// GameObject that triggered the dialogue.
    /// </summary>
    private string trigger;

    /// <summary>
    /// Sets the Current Line of dialogue to be displayed in the appropriate DialogueView.
    /// Updates the GUI accordingly.
    /// </summary>
    /// <param name="line"></param>
    /// <param name="triggeredBy"></param>
    public void setCurrentLine(DialogueLine line, string triggeredBy = null)
    {
        currentLine = line;
        trigger = triggeredBy;
        currentLine.content = " " + currentLine.content;
        //Debug.Log("DialogueController: Setting current line to: " + currentLine.content);
        //Debug.Log("DialogueController: Type: " + currentLine.type);
        //Handle linear sequences with the correct GUI interface
        if (currentLine.type == DialogueLine.LineType.Linear)
        {
            //Debug.Log("DialogueController: Starting linear dialogue:" + currentLine.content);
            linearDialogueView.gameObject.SetActive(true); // Show the linear dialogue box
            twoOptionDialogueView.gameObject.SetActive(false); // Hide the two option dialogue box
            threeOptionDialogueView.gameObject.SetActive(false); // Hide the three option dialogue box
            linearDialogueView.handleDialogue(currentLine, trigger);
        }
        //Handle two option sequences with the correct GUI interface
        else if (currentLine.type == DialogueLine.LineType.TwoOption)
        {
            //Debug.Log("DialogueController: Starting two option dialogue:" + currentLine.content);
            twoOptionDialogueView.gameObject.SetActive(true); // Show the two option dialogue box
            linearDialogueView.gameObject.SetActive(false); // Hide the linear dialogue box
            threeOptionDialogueView.gameObject.SetActive(false); // Hide the three option dialogue box
            twoOptionDialogueView.handleDialogue(currentLine, trigger);
        }
        //Handle three option sequences with the correct GUI interface
        else if (currentLine.type == DialogueLine.LineType.ThreeOption || currentLine.type == DialogueLine.LineType.EmotionOption)
        {
            threeOptionDialogueView.gameObject.SetActive(true); // Show the three option dialogue box
            linearDialogueView.gameObject.SetActive(false); // Hide the linear dialogue box
            twoOptionDialogueView.gameObject.SetActive(false); // Hide the two option dialogue box
            threeOptionDialogueView.handleDialogue(currentLine, trigger);
        }
    }

    /// <summary>
    /// Checks if dialogue is currently happening.
    /// </summary>
    /// <returns>True if dialogue is happening. False otherwise.</returns>
    public bool isDialogueActive()
    {
        return linearDialogueView.gameObject.activeSelf || twoOptionDialogueView.gameObject.activeSelf || threeOptionDialogueView.gameObject.activeSelf;
    }
}

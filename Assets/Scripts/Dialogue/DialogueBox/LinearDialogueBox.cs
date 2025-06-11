/// <summary>
/// LinearDialogueBox is a class that serves as the View for dialogue boxes in the game, in the Model-View-Controller (MVC) architecture.
/// </summary>
public class LinearDialogueBox : DialogueBox
{

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
}

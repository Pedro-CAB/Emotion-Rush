using UnityEngine;

public class DialogueManager : MonoBehaviour
{
    private DialogueLine currentLine;

    public DialogueBox linearDialogueBox;
    public DialogueBox twoOptionDialogueBox;
    public DialogueBox threeOptionDialogueBox;


    private string trigger; //The object that triggered the dialogue

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
            linearDialogueBox.StartLinearDialogue(currentLine, trigger);
        }
        //Handle two option sequences with the correct GUI interface
        else if (currentLine.type == DialogueLine.LineType.TwoOption)
        {
            //Debug.Log("DialogueManager: Starting two option dialogue:" + currentLine.content);
            twoOptionDialogueBox.gameObject.SetActive(true); // Show the two option dialogue box
            linearDialogueBox.gameObject.SetActive(false); // Hide the linear dialogue box
            threeOptionDialogueBox.gameObject.SetActive(false); // Hide the three option dialogue box
            twoOptionDialogueBox.StartTwoOptionDialogue(currentLine, trigger);
        }
        //Handle three option sequences with the correct GUI interface
        else if (currentLine.type == DialogueLine.LineType.ThreeOption || currentLine.type == DialogueLine.LineType.EmotionOption)
        {
            threeOptionDialogueBox.gameObject.SetActive(true); // Show the three option dialogue box
            linearDialogueBox.gameObject.SetActive(false); // Hide the linear dialogue box
            twoOptionDialogueBox.gameObject.SetActive(false); // Hide the two option dialogue box
            threeOptionDialogueBox.StartThreeOptionDialogue(currentLine, trigger);
        }
    }

    public bool isDialogueActive()
    {
        return linearDialogueBox.gameObject.activeSelf || twoOptionDialogueBox.gameObject.activeSelf || threeOptionDialogueBox.gameObject.activeSelf;
    }
}

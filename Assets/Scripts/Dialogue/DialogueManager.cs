using UnityEngine;
using System.Collections.Generic;

public class DialogueManager : MonoBehaviour
{
    private DialogueLine currentLine;

    public DialogueBox linearDialogueBox;
    public DialogueBox twoOptionDialogueBox;
    public DialogueBox threeOptionDialogueBox;


    private string trigger; //The object that triggered the dialogue

    public void Start()
    {
        linearDialogueBox.gameObject.SetActive(false);
        twoOptionDialogueBox.gameObject.SetActive(false);
        threeOptionDialogueBox.gameObject.SetActive(false);
    }

    /**public void Update()
    {
        DialogueLine.LineType lineType = currentLine.type;
        if (lineType == DialogueLine.LineType.Linear)
        {
            linearDialogueBox.DisplayDialogueBox();
            twoOptionDialogueBox.HideDialogueBox();
            threeOptionDialogueBox.HideDialogueBox();
        }
        else if (lineType == DialogueLine.LineType.TwoOption)
        {
            twoOptionDialogueBox.DisplayDialogueBox();
            linearDialogueBox.HideDialogueBox();
            threeOptionDialogueBox.HideDialogueBox();
        }
        else if (lineType == DialogueLine.LineType.ThreeOption)
        {
            threeOptionDialogueBox.DisplayDialogueBox();
            linearDialogueBox.HideDialogueBox();
            twoOptionDialogueBox.HideDialogueBox();
        }
    }*/

    public void setCurrentLine(DialogueLine line, string triggeredBy = null)
    {
        currentLine = line;
        trigger = triggeredBy;
        currentLine.content = " " + currentLine.content;
        Debug.Log("DialogueManager: Setting current line to: " + currentLine.content);
        //Handle linear sequences with the correct GUI interface
        if (currentLine.type == DialogueLine.LineType.Linear)
        {
            linearDialogueBox.enabled = true; // Ensure the linear dialogue box is enabled
            twoOptionDialogueBox.enabled = false; // Disable the two option dialogue box
            threeOptionDialogueBox.enabled = false; // Disable the three option dialogue box
            linearDialogueBox.StartLinearDialogue(currentLine, trigger);
        }
        //Handle two option sequences with the correct GUI interface
        else if (currentLine.type == DialogueLine.LineType.TwoOption)
        {
            Debug.Log("DialogueManager: Starting two option dialogue:" + currentLine.content);
            twoOptionDialogueBox.enabled = true; // Ensure the two option dialogue box is enabled
            linearDialogueBox.enabled = false; // Disable the linear dialogue box
            threeOptionDialogueBox.enabled = false; // Disable the three option dialogue box
            twoOptionDialogueBox.StartTwoOptionDialogue(currentLine, trigger);
        }
        //Handle three option sequences with the correct GUI interface
        else if (currentLine.type == DialogueLine.LineType.ThreeOption)
        {
            threeOptionDialogueBox.enabled = true; // Ensure the three option dialogue box is enabled
            linearDialogueBox.enabled = false; // Disable the linear dialogue box
            twoOptionDialogueBox.enabled = false; // Disable the two option dialogue box
            threeOptionDialogueBox.StartThreeOptionDialogue(currentLine, trigger);
        }
    }

    public bool isDialogueActive()
    {
        return linearDialogueBox.gameObject.activeSelf || twoOptionDialogueBox.gameObject.activeSelf || threeOptionDialogueBox.gameObject.activeSelf;
    }
}

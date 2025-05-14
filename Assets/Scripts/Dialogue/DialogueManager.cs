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

    public void setCurrentLine(DialogueLine line, string triggeredBy = null){
        currentLine = line;
        trigger = triggeredBy;
        currentLine.content = " " + currentLine.content;
        //Handle linear sequences with the correct GUI interface
        if(currentLine.type == DialogueLine.LineType.Linear){
            linearDialogueBox.StartLinearDialogue(currentLine, trigger);
        }
        //Handle two option sequences with the correct GUI interface
        else if(currentLine.type == DialogueLine.LineType.TwoOption){
            twoOptionDialogueBox.StartTwoOptionDialogue(currentLine, trigger);
        }
        //Handle three option sequences with the correct GUI interface
        else if(currentLine.type == DialogueLine.LineType.ThreeOption){
            threeOptionDialogueBox.StartThreeOptionDialogue(currentLine, trigger);
        }
    }

    public bool isDialogueActive(){
        return linearDialogueBox.gameObject.activeSelf || twoOptionDialogueBox.gameObject.activeSelf || threeOptionDialogueBox.gameObject.activeSelf;
    }
}

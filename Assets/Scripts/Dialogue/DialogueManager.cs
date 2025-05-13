using UnityEngine;
using System.Collections.Generic;

public class DialogueManager : MonoBehaviour
{
    private DialogueLine currentLine;

    public DialogueBox linearDialogueBox;
    public DialogueBox twoOptionDialogueBox;
    public DialogueBox threeOptionDialogueBox;

    public void Start()
    {
        linearDialogueBox.gameObject.SetActive(false);
        twoOptionDialogueBox.gameObject.SetActive(false);
        threeOptionDialogueBox.gameObject.SetActive(false);
    }

    public void setCurrentLine(DialogueLine line){
        currentLine = line;
        //Handle linear sequences with the correct GUI interface
        if(currentLine.type == DialogueLine.LineType.Linear){
            currentLine.content = " " + currentLine.content;
            linearDialogueBox.StartLinearDialogue(currentLine);
        }
        //Handle two option sequences with the correct GUI interface
        else if(currentLine.type == DialogueLine.LineType.TwoOption){
            twoOptionDialogueBox.StartTwoOptionDialogue(currentLine);
        }
        //Handle three option sequences with the correct GUI interface
        else if(currentLine.type == DialogueLine.LineType.ThreeOption){
            threeOptionDialogueBox.StartThreeOptionDialogue(currentLine);
        }
    }

    public bool isDialogueActive(){
        return linearDialogueBox.gameObject.activeSelf || twoOptionDialogueBox.gameObject.activeSelf || threeOptionDialogueBox.gameObject.activeSelf;
    }
}

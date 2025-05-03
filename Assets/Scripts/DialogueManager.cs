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
        List<DialogueLine> linearLines =  new List<DialogueLine>();
        linearLines.Add(currentLine);
        //Handle consecutive linear lines at once
        if(currentLine.type == DialogueLine.LineType.Linear){
            while (currentLine.nextLine != null){
                linearLines.Add(currentLine.nextLine);
                currentLine = currentLine.nextLine;
            }
            linearDialogueBox.StartLinearDialogue(linearLines);
        }
        //Handle two option sequences with the correct GUI interface
        else if(currentLine.type == DialogueLine.LineType.TwoOption){
            twoOptionDialogueBox.StartTwoOptionDialogue(line);
        }
        //Handle three option sequences with the correct GUI interface
        else if(currentLine.type == DialogueLine.LineType.ThreeOption){
            threeOptionDialogueBox.StartThreeOptionDialogue(line);
        }
    } 
}

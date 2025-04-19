using UnityEngine;
using System.Collections.Generic;

public class DialogueManager : MonoBehaviour
{
    private DialogueLine currentLine;

    public DialogueBox linearDialogueBox;
    public DialogueBox twoOptionDialogueBox;
    public DialogueBox threeOptionDialogueBox;

    public void setCurrentLine(DialogueLine line){
        currentLine = line;
        List<string> linearLines =  new List<string>();
        linearLines.Add(currentLine.content);
        //Iterate through the dialogue lines until there are no more options or next lines
        while (line.dialogueOptions != null || currentLine.nextLine != null){
            //If the first line doesn't have options, send all consecutive lines without options to be handled at once
            if(currentLine.type == DialogueLine.LineType.Linear){
                //linearDialogueBox.StartDialogue(line.content);
                while (currentLine.nextLine != null){
                    linearLines.Add(currentLine.nextLine.content);
                    currentLine = currentLine.nextLine;
                }
                linearDialogueBox.StartLinearDialogue(linearLines);
            }
            //After all consecutive linear lines are handled, handle option sequences with the correct GUI interface
            if(currentLine.type == DialogueLine.LineType.TwoOption){
                //twoOptionDialogueBox.StartDialogue(line, true);
            }
            else if(currentLine.type == DialogueLine.LineType.ThreeOption){
                //threeOptionDialogueBox.StartDialogue(line, true);
            }
        }
    } 
}

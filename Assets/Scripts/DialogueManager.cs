using UnityEngine;
using System.Collections.Generic;

public class DialogueManager : MonoBehaviour
{
    public int index;
    public DialogueLine currentLine;

    public DialogueBox linearDialogueBox;
    public DialogueBox twoOptionDialogueBox;
    public DialogueBox threeOptionDialogueBox;

    public void setCurrentLine(DialogueLine line){
        List<string> linearLines =  new List<string>();
        linearLines.Add(line.content);
        while (line.options.Length >= 1){
            //If the first line doesn't have options, send all consecutive lines without options to be handled at once
            if(currentLine.type == DialogueLine.LineType.Linear){
                //linearDialogueBox.StartDialogue(line.content);
                while (currentLine.options[0].type == DialogueLine.LineType.Linear){
                    linearLines.Add(currentLine.options[0].content);
                    currentLine = currentLine.options[0];
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

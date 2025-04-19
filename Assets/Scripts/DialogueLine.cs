using UnityEngine;

public class DialogueLine : MonoBehaviour
{
    public enum LineType{
            Linear,
            TwoOption,
            ThreeOption
    }

    public string content;
    public DialogueLine[] dialogueOptions;

    public DialogueLine nextLine;
    public LineType type;
    public DialogueLine(string content, DialogueLine next = null, DialogueLine[] options = null){
        if (options == null){
            type = LineType.Linear;
            nextLine = next;
        } else if (options.Length == 2){
            type = LineType.TwoOption;
        } else if (options.Length == 3){
            type = LineType.ThreeOption;
        } else {
            Debug.LogError("DialogueLine: Too many options for a single line of dialogue.");
        }
        this.content = content;
        this.dialogueOptions = options;
    }
}

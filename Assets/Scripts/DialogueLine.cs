using UnityEngine;

public class DialogueLine : MonoBehaviour
{
    public enum LineType{
            Linear,
            TwoOption,
            ThreeOption
    }

    public string content;
    public DialogueLine[] options;
    public LineType type;
    public DialogueLine(string content, DialogueLine[] possibleOptions){
        if (possibleOptions.Length <= 1){
            type = LineType.Linear;
        } else if (possibleOptions.Length == 2){
            type = LineType.TwoOption;
        } else if (possibleOptions.Length == 3){
            type = LineType.ThreeOption;
        } else {
            Debug.LogError("DialogueLine: Too many options for a single line of dialogue.");
        }
        this.content = content;
        this.options = possibleOptions;
    }
}

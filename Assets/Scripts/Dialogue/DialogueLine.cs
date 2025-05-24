using UnityEngine;

public class DialogueLine
{
    public enum LineType
    {
        Linear,
        TwoOption,
        ThreeOption,
        DialogueOption,
        Undefined
    }

    public string content;
    public DialogueLine[] dialogueOptions;

    public DialogueLine nextLine;
    public LineType type;

    public int score;
    public DialogueLine(string content, DialogueLine next = null, DialogueLine[] options = null, LineType type = LineType.Undefined, int score = 0)
    {
        if (type == LineType.Undefined)
        {
            if (options == null)
            {
                type = LineType.Linear;
                nextLine = next;
            }
            else if (options.Length == 2)
            {
                type = LineType.TwoOption;
            }
            else if (options.Length == 3)
            {
                type = LineType.ThreeOption;
            }
            else
            {
                Debug.LogError("DialogueLine: Too many options for a single line of dialogue.");
            }
        }
        else
        {
            this.type = type;
        }
        this.content = content;
        this.dialogueOptions = options;
        this.score = score;
    }

    public void addNextLine(DialogueLine next)
    {
        this.nextLine = next;
    }

    public void setOptions(DialogueLine[] options)
    {
        if (options.Length == 2)
        {
            this.type = LineType.TwoOption;
        }
        else if (options.Length == 3)
        {
            this.type = LineType.ThreeOption;
        }
        else
        {
            Debug.LogError("DialogueLine: Too many options for a single line of dialogue.");
        }
        this.dialogueOptions = options;
    }
}

using UnityEngine;

public class DialogueLine
{
    public enum LineType
    {
        Linear,
        TwoOption,
        ThreeOption,

        EmotionOption,
        DialogueOption,
        Undefined
    }

    public string content;
    public DialogueLine[] dialogueOptions;

    public DialogueLine nextLine;

    public DialogueLine previousLine;
    public LineType type;

    public int score;

    public bool visited;

    public string answer;

    public string feedback; //Geeback for the options selected by the player
    public DialogueLine(string content, DialogueLine next = null, DialogueLine[] options = null, LineType type = LineType.Undefined, int score = 0, string feedback = "None", string answer = "None")
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
        this.feedback = feedback;
        this.answer = answer;
        //Debug.Log("DialogueLine: Created line with content: " + content + ", type: " + type + ", score: " + score + ", feedback: " + feedback + ", answer: " + answer);
    }

    public void addNextLine(DialogueLine next)
    {
        this.nextLine = next;
    }

    public void addPreviousLine(DialogueLine previous)
    {
        this.previousLine = previous;
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

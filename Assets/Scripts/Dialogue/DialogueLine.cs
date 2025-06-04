using UnityEngine;

/// <summary>
/// Represents a single line of dialogue in a dialogue sequence.
/// </summary>
public class DialogueLine
{
    /// <summary>
    /// Datatype that represents the type of a dialogue line.
    /// It can be Linear (no options), TwoOption (two choices), ThreeOption (three choices),
    /// EmotionOption (three-option for emotion identification), DialogueOption (dialogue choice), or Undefined (not specified).
    /// </summary>
    public enum LineType
    {
        Linear,
        TwoOption,
        ThreeOption,

        EmotionOption,
        DialogueOption,
        Undefined
    }

    /// <summary>
    /// The text content of the dialogue line.
    /// </summary>
    public string content;

    /// <summary>
    /// Array of dialogue options to respond to this line. Empty in Linear lines, EmotionOption Lines and DialogueOption lines.
    /// </summary>
    public DialogueLine[] dialogueOptions;

    /// <summary>
    /// Next line of dialogue for Linear lines or following a DialogueOption or EmotionOption.
    /// </summary>
    public DialogueLine nextLine;

    /// <summary>
    /// Previous line of dialogue, used to navigate back in the dialogue sequence.
    /// </summary>
    public DialogueLine previousLine;

    /// <summary>
    /// Type of the dialogue line, which determines how it is handled.
    /// </summary>
    public LineType type;

    /// <summary>
    /// Score received by picking this line of dialogue. Used for DialogueOption lines.
    /// </summary>
    public int score;

    /// <summary>
    /// Marks if this line node was visited when building the dialogue tree. 
    /// </summary>
    public bool visited;

    /// <summary>
    /// Stores the correct answer for EmotionOption lines.
    /// </summary>
    public string answer;

    /// <summary>
    /// Stores feedback for the options selected by the player, when it exists.
    /// </summary>
    public string feedback;

    /// <summary>
    /// Creates a DialogueLine with the specified content, next line, options, type, score, feedback, and answer.
    /// If the type is Undefined, it will be determined based on other parameters provided.
    /// </summary>
    /// <param name="content">The text content of the dialogue line.</param>
    /// <param name="next">Next line of dialogue for Linear lines or following a DialogueOption or EmotionOption.</param>
    /// <param name="options">Array of dialogue options to respond to this line. Empty in Linear lines, EmotionOption Lines and DialogueOption lines.</param>
    /// <param name="type">Type of the dialogue line, which determines how it is handled.</param>
    /// <param name="score">Score received by picking this line of dialogue. Used for DialogueOption lines.</param>
    /// <param name="feedback">Feedback for the options selected by the player, when it exists.</param>
    /// <param name="answer">The correct answer for EmotionOption lines.</param>
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

    /// <summary>
    /// Sets next line of dialogue for this line.
    /// </summary>
    /// <param name="next">Line to be setup as this line's next line.</param>
    public void addNextLine(DialogueLine next)
    {
        this.nextLine = next;
    }

    /// <summary>
    /// Sets previous line of dialogue for this line.
    /// </summary>
    /// <param name="previous">Line to be setup as this line's previous line.</param>
    public void addPreviousLine(DialogueLine previous)
    {
        this.previousLine = previous;
    }

    /// <summary>
    /// Sets options for this dialogue line.
    /// If the options array has 2 elements, the type is set to TwoOption.
    /// If it has 3 elements, the type is set to ThreeOption.
    /// EmotionOption lines do not have options in JSON, so they are not handled here.
    /// </summary>
    /// <param name="options">Array of dialogue options.</param>
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

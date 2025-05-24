using UnityEngine;

public class DialogueSequence : MonoBehaviour
{
    DialogueLine firstLine;

    DialogueLine currentLine;
    string sequenceName;

    public DialogueSequence(DialogueLine firstLine, string name = "unnamed")
    {
        this.firstLine = firstLine;
        this.sequenceName = name;
        currentLine = firstLine;
    }

    void Start()
    {
        currentLine = firstLine;
    }

    void nextLine()
    {
        currentLine = currentLine.nextLine;
    }
}

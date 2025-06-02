using UnityEngine;

/// <summary>
/// Represents an interactive door in the game.
/// The door can prompt the player with different messages based on its type.
/// </summary>
public class Door : InteractiveObject
{
    /// <summary>
    /// Name of the Door Object.
    /// </summary>
    string doorName;

    /// <summary>
    /// Prompt to be displayed when the door is interacted with.
    /// </summary>
    DialogueLine prompt;

    /// <summary>
    /// Affirmative option for the door prompt.
    /// </summary>
    DialogueLine option1;

    /// <summary>
    /// Negative option for the door prompt.
    /// </summary>
    DialogueLine option2;

    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        option1 = new DialogueLine("Sim.", null, null);
        option2 = new DialogueLine("Não.", null, null);
        doorName = gameObject.name;
        switch (doorName)
        {
            case "LibraryDoor":
                prompt = new DialogueLine("Queres entrar na Biblioteca?", null, new DialogueLine[] { option1, option2 }, DialogueLine.LineType.TwoOption);
                break;
            case "ClassroomADoor":
                prompt = new DialogueLine("A tua sala está fechada. Aproveita o intervalo!", null, null);
                break;
            case "ClassroomBDoor":
            case "ClassroomCDoor":
            case "ClassroomDDoor":
                prompt = new DialogueLine("Esta não é a tua sala.", null, null);
                break;
            case "BarDoor":
                prompt = new DialogueLine("Queres entrar na Loja?", null, new DialogueLine[] { option1, option2 }, DialogueLine.LineType.TwoOption);
                break;
            case "AuditoriumDoor":
                prompt = new DialogueLine("O Auditório está fechado agora.", null, null);
                break;
            case "LabDoor":
                prompt = new DialogueLine("O Laboratório está fechado agora.", null, null);
                break;
            case "PlaygroundDoor":
                prompt = new DialogueLine("Queres entrar no Recreio?", null, new DialogueLine[] { option1, option2 }, DialogueLine.LineType.TwoOption);
                break;
            case "FrontEntranceDoor":
            case "LeftEntranceDoor":
            case "RightEntranceDoor":
                prompt = new DialogueLine("Não podes sair da escola agora.", null, null);
                break;
            default:
                prompt = new DialogueLine("Esta porta está trancada.", null, null);
                break;
        }
    }
    /// <summary>
    /// Handles interaction with doors on the Door object side.
    /// </summary>
    public void whenInteracted()
    {
        //Overriding the function from InteractiveObject
        dialogueManager.setCurrentLine(prompt, doorName);
    }
}

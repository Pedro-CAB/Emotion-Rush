using UnityEngine;

/// <summary>
/// Handles the door name and prompt data in the game.
/// </summary>
public class DoorModel : MonoBehaviour
{
    /// <summary>
    /// Returns the name of the door.
    /// </summary>
    /// <returns>Name of the door object.</returns>
    public string getDoorName()
    {
        return gameObject.name;
    }

    /// <summary>
    /// Returns the prompt for when the door is interacted with.
    /// </summary>
    /// <returns>DialogueLine object containing the door prompt.</returns>
    public DialogueLine getDoorPrompt()
    {
        DialogueLine option1 = new DialogueLine("Sim.", null, null);
        DialogueLine option2 = new DialogueLine("Não.", null, null);
        switch (gameObject.name)
        {
            case "LibraryDoor":
                return new DialogueLine("Queres entrar na Biblioteca?", null, new DialogueLine[] { option1, option2 }, DialogueLine.LineType.TwoOption);
            case "ClassroomADoor":
                return new DialogueLine("A tua sala está fechada. Aproveita o intervalo!", null, null);
            case "ClassroomBDoor":
            case "ClassroomCDoor":
            case "ClassroomDDoor":
                return new DialogueLine("Esta não é a tua sala.", null, null);
            case "StoreDoor":
                return new DialogueLine("Queres entrar na Loja?", null, new DialogueLine[] { option1, option2 }, DialogueLine.LineType.TwoOption);
            case "AuditoriumDoor":
                return new DialogueLine("O Auditório está fechado agora.", null, null);
            case "LabDoor":
                return new DialogueLine("O Laboratório está fechado agora.", null, null);
            case "PlaygroundDoor":
                return new DialogueLine("Queres entrar no Recreio?", null, new DialogueLine[] { option1, option2 }, DialogueLine.LineType.TwoOption);
            case "FrontEntranceDoor":
            case "LeftEntranceDoor":
            case "RightEntranceDoor":
                return new DialogueLine("Não podes sair da escola agora.", null, null);
            default:
                return new DialogueLine("Esta porta está trancada.", null, null);
        }
    }
}

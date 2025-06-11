using UnityEngine;

/// <summary>
/// Controls the logic of interactive doors in the game.
/// </summary>
public class DoorController : MonoBehaviour
{
    DoorModel doorModel;

    public void Start()
    {
        doorModel = GetComponent<DoorModel>();
    }

    /// <summary>
    /// Reference to the DialogueController for door prompts.
    /// </summary>
    public DialogueController dialogueController;

    /// <summary>
    /// Handles interaction with doors on the Door side.
    /// </summary>
    public void whenInteracted()
    {
        DialogueLine prompt = doorModel.getDoorPrompt();
        dialogueController.setCurrentLine(prompt, doorModel.getDoorName());
    }
}

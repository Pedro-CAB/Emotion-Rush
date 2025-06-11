using UnityEngine;
/// <summary>
/// View and Controller for the Interact Button in the game.
/// Handles interactions when the button is pressed.
/// </summary>
public class InteractButton : MonoBehaviour
{
    public PlayerController player;

    public AudioPlayer audioPlayer;

    public void Pressed()
    {
        audioPlayer.playButtonPushSound();
        player.interact();
    }
}
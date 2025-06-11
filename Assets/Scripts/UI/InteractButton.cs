using UnityEngine;
using UnityEngine.EventSystems;

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
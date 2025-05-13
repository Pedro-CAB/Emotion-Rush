using UnityEngine;
using UnityEngine.EventSystems;

public class InteractButton : MonoBehaviour
{
    public Player player;

    public void Pressed()
    {
        Debug.Log("Button Pressed");
        player.interact();
    }
}
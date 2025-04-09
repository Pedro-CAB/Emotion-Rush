using UnityEngine;
using UnityEngine.EventSystems;
//using Player;

public class InteractButton : MonoBehaviour
{
    public Player player;

    public void Pressed()
    {
        Debug.Log("Button Pressed");
        player.interact();
    }
}
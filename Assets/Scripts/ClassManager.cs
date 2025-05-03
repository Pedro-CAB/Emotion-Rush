using UnityEngine;

public class ClassManager : MonoBehaviour
{
    public Player player;

    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        player.setStaticScene(3); // Set the Player to start a Class Scene facing Right
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}

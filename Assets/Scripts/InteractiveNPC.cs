using UnityEngine;

public class InteractiveNPC : MonoBehaviour
{
    public int friendshipLevel = 0;
    public int maxFriendshipLevel = 100;

    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void adjustFriendship(int i){
        if (friendshipLevel + i > maxFriendshipLevel){
            friendshipLevel = maxFriendshipLevel;
        } else if (friendshipLevel + i < 0){
            friendshipLevel = 0;
        } else {
            friendshipLevel += i;
        }
    }

    public void whenInteracted(){ //Called when player interacts with character
        Debug.Log(gameObject.name + " interacted back!");
    }
}

using UnityEngine;
using System.Collections.Generic;

public class InteractiveNPC : MonoBehaviour
{
    public int friendshipLevel = 0;
    public int maxFriendshipLevel = 100;

    DialogueLine interactions; //Contains the first line of each possible interaction

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

    public List<string> whenInteracted(){ //Called when player interacts with character
        Debug.Log(gameObject.name + " interacted back!");
        List<string> lines = new List<string>();
        lines.Add("Olá! Tudo bem?"); // Add the first line to the list
        lines.Add("Este é um diálogo de teste!"); // Add the second line to the list
        lines.Add("Espero que funcione! :)"); // Add the third line to the list
        return lines;
    }

    public void createDialogueInteractions(){
        //Create Interactions Here
        //DialogueLine line1 = new DialogueLine("Olá! Tudo bem?", {new DialogueLine("Este é um diálogo de teste!", {new DialogueLine("Espero que funcione! :)", {})})});

        //Add the lines to the interactions list
    }
}

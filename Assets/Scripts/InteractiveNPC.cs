using UnityEngine;
using System.Collections.Generic;

public class InteractiveNPC : MonoBehaviour
{
    public int friendshipLevel = 0;
    public int maxFriendshipLevel = 100;

    List<DialogueLine> interactions; //Contains the first line of each possible interaction

    public DialogueManager dialogueManager;

    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        Debug.Log("NPC Created!");
        interactions = new List<DialogueLine>();
        createDialogueInteractions();
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
        Debug.Log(dialogueManager.name + " is the dialogue manager!");
        Debug.Log(interactions[0].content);
        dialogueManager.setCurrentLine(interactions[0]);
    }

    public void createDialogueInteractions(){
        //Create Interactions Here

        //Sample Linear Interaction
        DialogueLine line3 = new DialogueLine("Espero que funcione! :)", null, null);
        DialogueLine line2 = new DialogueLine("Este é um diálogo de teste!", line3, null);
        DialogueLine line1 = new DialogueLine("Olá! Tudo bem?", line2, null);

        //interactions.Add(line1);

        //Sample Two Option Interaction
        line1 = new DialogueLine("Ok... Fica para a próxima então.", null, null);
        line2 = new DialogueLine("Obrigado!", null, null);
        DialogueLine option1 = new DialogueLine("Sim.", line2, null);
        DialogueLine option2 = new DialogueLine("Não.", line1, null);
        line1 = new DialogueLine("Queres ajudar-me?", null, new DialogueLine[]{option1, option2});

        interactions.Add(line1);
    }
}

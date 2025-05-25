using UnityEngine;
using System.Collections.Generic;

public class InteractiveObject : MonoBehaviour
{

    //List<DialogueLine> interactions; //Contains the first line of each possible interaction

    public DialogueManager dialogueManager;

    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        Debug.Log(gameObject.name + ": InteractiveObject Created!");
        //interactions = new List<DialogueLine>();
        //createDialogueInteractions();
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    /**public void whenInteracted(){ //Called when player interacts with character
        Debug.Log(gameObject.name + " interacted back!");
        Debug.Log(dialogueManager.name + " is the dialogue manager!");
        //Debug.Log(interactions[0].content);
        //dialogueManager.setCurrentLine(interactions[0]);
    }*/
}

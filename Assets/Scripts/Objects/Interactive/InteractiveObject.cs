using UnityEngine;
using System.Collections.Generic;

public class InteractiveObject : MonoBehaviour
{

    //List<DialogueLine> interactions; //Contains the first line of each possible interaction

    public DialogueManager dialogueManager;

    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        //Debug.Log(gameObject.name + ": InteractiveObject Created!");
    }
}

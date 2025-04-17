using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;
using TMPro;

public class Dialogue : MonoBehaviour
{
    public TextMeshProUGUI textComponent; // Reference to the TextMeshProUGUI component for displaying text
    public string[] lines; // Array of strings to hold the dialogue lines
    public float textSpeed; // Speed at which the text is displayed
    private int index;
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        textComponent.text = string.Empty;
        gameObject.SetActive(false); // Hide the dialogue box at the start
    }

    public void StartDialogue(string[] l){
        gameObject.SetActive(true); // Show the dialogue box when starting the dialogue
        lines = l;
        index = 0;
        StartCoroutine(TypeLine()); // Start the coroutine to type out the first line of dialogue
    }

    void NextLine(){
        if (index < lines.Length - 1){
            index++;
            textComponent.text = string.Empty; // Clear the text component before displaying the next line
            StartCoroutine(TypeLine()); // Start the coroutine to type out the next line of dialogue
        }
        else{
            textComponent.text = string.Empty; // Clear the text component when all lines are displayed
            gameObject.SetActive(false);
        }
    }

    IEnumerator TypeLine(){
        foreach (char letter in lines[index].ToCharArray()){
            textComponent.text += letter; // Display each letter one by one
            yield return new WaitForSeconds(textSpeed); // Wait for the specified text speed before displaying the next letter
        }
    }

    public void SkipDialog(){
        if (textComponent.text == lines[index]){ //If previous line is already fully displayed
            NextLine();
        }
        else{
            StopAllCoroutines(); // Stop the typing coroutine if the button is pressed before the line is fully displayed
            textComponent.text = lines[index]; // Display the full line immediately
        }
        Debug.Log("Line Skipped!");

 
    }

    public void PickOptionA(){
        Debug.Log("Option A picked!");
    }

    public void PickOptionB(){
        Debug.Log("Option B picked!");
    }

    public void PickOptionC(){
        Debug.Log("Option C picked!");
    }
}

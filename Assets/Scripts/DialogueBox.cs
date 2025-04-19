using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;
using TMPro;

public class DialogueBox : MonoBehaviour
{
    public TextMeshProUGUI textComponent; // Reference to the TextMeshProUGUI component for displaying text

    public Button optionAButton; // Reference to the button for option A
    public Button optionBButton; // Reference to the button for option B
    public Button optionCButton; // Reference to the button for option C



    public List<string> lines; // Array of strings to hold the dialogue lines
    public float textSpeed; // Speed at which the text is displayed
    private int index;
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        textComponent.text = string.Empty;
        gameObject.SetActive(false); // Hide the dialogue box at the start
    }

    //Handling Linear Dialogue --------------------------------------------------------------------------------------------

    public void StartLinearDialogue(List<string> l, int option_amount = 1){
        if (option_amount == 1){ // If the dialogue is linear (one option)
            gameObject.SetActive(true); // Show the dialogue box when starting the dialogue
            lines = l;
            index = 0;
            StartCoroutine(TypeLine()); // Start the coroutine to type out the first line of dialogue
        }
        else{
            Debug.LogError("DialogueBox: Too many options for a single line of dialogue.");
        }
    }

    void NextLinearLine(){
        if (index < lines.Count - 1){
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
            NextLinearLine();
        }
        else{
            StopAllCoroutines(); // Stop the typing coroutine if the button is pressed before the line is fully displayed
            textComponent.text = lines[index]; // Display the full line immediately
        }
        Debug.Log("Line Skipped!");

 
    }

    //Handling Option Dialogue ----------------------------------------------------------------------------------------

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

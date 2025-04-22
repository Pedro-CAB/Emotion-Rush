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

    public TextMeshProUGUI optionAText; // Reference to the TextMeshProUGUI component for option A text
    
    public Button optionBButton; // Reference to the button for option B
    public TextMeshProUGUI optionBText; // Reference to the TextMeshProUGUI component for option B text
    public Button optionCButton; // Reference to the button for option C
    public TextMeshProUGUI optionCText; // Reference to the TextMeshProUGUI component for option C text

    public DialogueManager dialogueManager; // Reference to the DialogManager for handling dialogue logic

    public List<DialogueLine> lines; // Array of strings to hold the dialogue lines
    public float textSpeed; // Speed at which the text is displayed
    private int index;
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        textComponent.text = string.Empty;
        gameObject.SetActive(false); // Hide the dialogue box at the start
    }

    //Handling Linear Dialogue --------------------------------------------------------------------------------------------

    public void StartLinearDialogue(List<DialogueLine> l){
        gameObject.SetActive(true); // Show the dialogue box when starting the dialogue
        lines = l;
        index = 0;
        StartCoroutine(TypeLine()); // Start the coroutine to type out the first line of dialogue
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
        foreach (char letter in lines[index].content.ToCharArray()){
            textComponent.text += letter; // Display each letter one by one
            yield return new WaitForSeconds(textSpeed); // Wait for the specified text speed before displaying the next letter
        }
    }

    public void SkipDialog(){
        if (textComponent.text == lines[index].content){ //If previous line is already fully displayed
            NextLinearLine();
        }
        else{
            StopAllCoroutines(); // Stop the typing coroutine if the button is pressed before the line is fully displayed
            textComponent.text = lines[index].content; // Display the full line immediately
        }
        Debug.Log("Line Skipped!");

 
    }

    //Handling Option Dialogue ----------------------------------------------------------------------------------------

    public void StartTwoOptionDialogue(DialogueLine l){
        //optionAButton.gameObject.SetActive(false); // Hide option A button
        //optionBButton.gameObject.SetActive(false); // Hide option B button
        gameObject.SetActive(true); // Show the dialogue box when starting the dialogue
        lines = new List<DialogueLine>();
        lines.Add(l); // Add the first line of dialogue to the list

        index = 0;
        StartCoroutine(TypeLine());
        optionAText.text = l.dialogueOptions[0].content; // Set the text for option A button
        optionBText.text = l.dialogueOptions[1].content; // Set the text for option B button
    }

    public void StartThreeOptionDialogue(DialogueLine l){
        //optionAButton.gameObject.SetActive(false); // Hide option A button
        //optionBButton.gameObject.SetActive(false); // Hide option B button
        //optionCButton.gameObject.SetActive(false); // Hide option C button
        gameObject.SetActive(true); // Show the dialogue box when starting the dialogue
        lines = new List<DialogueLine>();
        lines.Add(l); // Add the first line of dialogue to the list

        index = 0;
        StartCoroutine(TypeLine());
        optionAText.text = l.dialogueOptions[0].content; // Set the text for option A button
        optionBText.text = l.dialogueOptions[1].content; // Set the text for option B button
        optionCText.text = l.dialogueOptions[2].content; // Set the text for option C button
    }
    
    public void PickOptionA(){
        DialogueLine currentLine = lines[index];
        DialogueLine chosenLine = currentLine.dialogueOptions[0];
        DialogueLine nextLine = chosenLine.nextLine;
        dialogueManager.setCurrentLine(nextLine);
        gameObject.SetActive(false); // Hide the dialogue box after picking an option
    }

    public void PickOptionB(){
        DialogueLine currentLine = lines[index];
        DialogueLine chosenLine = currentLine.dialogueOptions[1];
        DialogueLine nextLine = chosenLine.nextLine;
        dialogueManager.setCurrentLine(nextLine);
        gameObject.SetActive(false); // Hide the dialogue box after picking an option
    }

    public void PickOptionC(){
        DialogueLine currentLine = lines[index];
        DialogueLine chosenLine = currentLine.dialogueOptions[2];
        DialogueLine nextLine = chosenLine.nextLine;
        dialogueManager.setCurrentLine(nextLine);
        gameObject.SetActive(false); // Hide the dialogue box after picking an option
    }
}

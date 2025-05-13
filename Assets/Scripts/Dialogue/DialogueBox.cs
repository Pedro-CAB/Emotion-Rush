using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;
using TMPro;
using UnityEngine.SceneManagement;

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

    //public List<DialogueLine> lines; // Array of strings to hold the dialogue lines
    public DialogueLine currentLine;
    public float textSpeed; // Speed at which the text is displayed

    private string trigger;
    //private int index;
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        textComponent.text = string.Empty;
        //gameObject.SetActive(false); // Hide the dialogue box at the start
    }

    //Handling Linear Dialogue --------------------------------------------------------------------------------------------

    public void StartLinearDialogue(DialogueLine l, string triggeredBy = null){
        trigger = triggeredBy; // Store the object that triggered the dialogue
        gameObject.SetActive(true); // Show the dialogue box when starting the dialogue
        currentLine = l;
        //lines = l;
        //index = 0;
        StartCoroutine(TypeLine()); // Start the coroutine to type out the first line of dialogue
    }

    /**void NextLinearLine(){
        if (index < lines.Count - 1){
            index++;
            textComponent.text = string.Empty; // Clear the text component before displaying the next line
            StartCoroutine(TypeLine()); // Start the coroutine to type out the next line of dialogue
        }
        else{
            textComponent.text = string.Empty; // Clear the text component when all lines are displayed
            gameObject.SetActive(false);
        }
    }*/

    IEnumerator TypeLine(){
        //Debug.Log("Typing Line: " + lines[index].content); // Log the current line being typed
        //textComponent.text = currentLine.content;
        foreach (char letter in currentLine.content.ToCharArray()){
            textComponent.text += letter; // Display each letter one by one
            yield return new WaitForSeconds(textSpeed); // Wait for the specified text speed before displaying the next letter
        }
    }

    public void SkipDialog(){
        if (textComponent.text == currentLine.content){ //If previous line is already fully displayed
            if (currentLine.type == DialogueLine.LineType.Linear){ //Only for linear lines
                if (currentLine.nextLine != null){
                    Debug.Log("Line Fully Displayed. Displaying Next Line.");
                    textComponent.text = string.Empty; // Clear the text component before displaying the next line
                    HideDialogueBox(); // Hide the dialogue box before displaying the next line
                    dialogueManager.setCurrentLine(currentLine.nextLine); // Set the next line as the current line
                }
                else{
                    Debug.Log("Line Fully Displayed and No Next Line. Closing Dialogue Box...");
                    HideDialogueBox(); // Hide the dialogue box after picking an option
                }
            }
        }
        else{
            Debug.Log("Line Not Fully Displayed. Displaying Full Line...");
            StopAllCoroutines(); // Stop the typing coroutine if the button is pressed before the line is fully displayed
            textComponent.text = currentLine.content; // Display the full line immediately
        }
        Debug.Log("Line Skipped!");

 
    }

    //Handling Option Dialogue ----------------------------------------------------------------------------------------

    public void StartTwoOptionDialogue(DialogueLine l, string triggeredBy = null){
        trigger = triggeredBy; // Store the object that triggered the dialogue
        //optionAButton.gameObject.SetActive(false); // Hide option A button
        //optionBButton.gameObject.SetActive(false); // Hide option B button
        gameObject.SetActive(true); // Show the dialogue box when starting the dialogue
        currentLine = l;

        //index = 0;
        StartCoroutine(TypeLine());
        optionAText.text = l.dialogueOptions[0].content; // Set the text for option A button
        optionBText.text = l.dialogueOptions[1].content; // Set the text for option B button
    }

    public void StartThreeOptionDialogue(DialogueLine l, string triggeredBy = null){
        trigger = triggeredBy; // Store the object that triggered the dialogue
        //optionAButton.gameObject.SetActive(false); // Hide option A button
        //optionBButton.gameObject.SetActive(false); // Hide option B button
        //optionCButton.gameObject.SetActive(false); // Hide option C button
        gameObject.SetActive(true); // Show the dialogue box when starting the dialogue
        currentLine = l;

        //index = 0;
        StartCoroutine(TypeLine());
        optionAText.text = l.dialogueOptions[0].content; // Set the text for option A button
        optionBText.text = l.dialogueOptions[1].content; // Set the text for option B button
        optionCText.text = l.dialogueOptions[2].content; // Set the text for option C button
    }
    
    public void PickOptionA(){
        DialogueLine promptLine = currentLine;
        DialogueLine chosenLine = currentLine.dialogueOptions[0];
        DialogueLine nextLine = chosenLine.nextLine;
        if (nextLine != null){
            dialogueManager.setCurrentLine(nextLine);
        }
        if (trigger.Contains("Door")){
            string roomName = trigger.Substring(0, trigger.Length - 4); // Remove "Door" from the trigger name
            SceneManager.LoadScene(roomName); // Load the scene corresponding to the room name
        }
        HideDialogueBox(); // Hide the dialogue box after picking an option
    }

    public void PickOptionB(){
        DialogueLine promptLine = currentLine;
        DialogueLine chosenLine = currentLine.dialogueOptions[1];
        DialogueLine nextLine = chosenLine.nextLine;
        if (nextLine != null){
            dialogueManager.setCurrentLine(nextLine);
        }
        HideDialogueBox(); // Hide the dialogue box after picking an option
    }

    public void PickOptionC(){
        DialogueLine promptLine = currentLine;
        DialogueLine chosenLine = currentLine.dialogueOptions[2];
        DialogueLine nextLine = chosenLine.nextLine;
        if (nextLine != null){
            dialogueManager.setCurrentLine(nextLine);
        }
        HideDialogueBox(); // Hide the dialogue box after picking an option
    }

    public void HideDialogueBox(){
        Debug.Log("Hiding Dialogue Box");
        gameObject.SetActive(false); // Hide the dialogue box
        textComponent.text = string.Empty; // Clear the text component when hiding the dialogue box
    }
}

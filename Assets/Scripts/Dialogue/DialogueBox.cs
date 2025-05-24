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
    public BreakManager breakManager; // Reference to the BreakManager for handling break logic

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

    public void StartLinearDialogue(DialogueLine l, string triggeredBy = null)
    {
        //DisplayDialogueBox(); // Show the dialogue box when starting the dialogue
        trigger = triggeredBy; // Store the object that triggered the dialogue
        enabled = true; // Show the dialogue box when starting the dialogue
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

    IEnumerator TypeLine()
    {
        //Debug.Log("Typing Line: " + lines[index].content); // Log the current line being typed
        //textComponent.text = currentLine.content;
        textComponent.text = string.Empty; // Clear the text component before typing the new line
        foreach (char letter in currentLine.content.ToCharArray())
        {
            textComponent.text += letter; // Display each letter one by one
            yield return new WaitForSeconds(textSpeed); // Wait for the specified text speed before displaying the next letter
        }
    }

    public void SkipDialog()
    {
        if (textComponent.text == currentLine.content)
        { //If previous line is already fully displayed
            if (currentLine.type == DialogueLine.LineType.Linear)
            { //Only for linear lines
                if (currentLine.nextLine != null)
                {
                    textComponent.text = string.Empty; // Clear the text component before displaying the next line
                    //HideDialogueBox(); // Hide the dialogue box before displaying the next line
                    dialogueManager.setCurrentLine(currentLine.nextLine); // Set the next line as the current line
                }
                else
                {
                    //HideDialogueBox(); // Hide the dialogue box after picking an option
                }
            }
        }
        else
        {
            StopAllCoroutines(); // Stop the typing coroutine if the button is pressed before the line is fully displayed
            textComponent.text = currentLine.content; // Display the full line immediately
        }


    }

    //Handling Option Dialogue ----------------------------------------------------------------------------------------

    public void StartTwoOptionDialogue(DialogueLine l, string triggeredBy = null)
    {
        //DisplayDialogueBox();
        Debug.Log("DialogueBox: Starting Two Option Dialogue: " + l.content); // Log the content of the dialogue line
        trigger = triggeredBy; // Store the object that triggered the dialogue
        //optionAButton.gameObject.SetActive(false); // Hide option A button
        //optionBButton.gameObject.SetActive(false); // Hide option B button
        //gameObject.SetActive(true); // Show the dialogue box when starting the dialogue
        enabled = true; // Enable the dialogue box component to show the dialogue box
        currentLine = l;

        //index = 0;
        StartCoroutine(TypeLine());
        optionAText.text = l.dialogueOptions[0].content; // Set the text for option A button
        optionBText.text = l.dialogueOptions[1].content; // Set the text for option B button
    }

    public void StartThreeOptionDialogue(DialogueLine l, string triggeredBy = null)
    {
        //DisplayDialogueBox();
        trigger = triggeredBy; // Store the object that triggered the dialogue
        //optionAButton.gameObject.SetActive(false); // Hide option A button
        //optionBButton.gameObject.SetActive(false); // Hide option B button
        //optionCButton.gameObject.SetActive(false); // Hide option C button
        //gameObject.SetActive(true); // Show the dialogue box when starting the dialogue
        enabled = true; // Show the dialogue box when starting the dialogue
        currentLine = l;

        //index = 0;
        StartCoroutine(TypeLine());
        optionAText.text = l.dialogueOptions[0].content; // Set the text for option A button
        optionBText.text = l.dialogueOptions[1].content; // Set the text for option B button
        optionCText.text = l.dialogueOptions[2].content; // Set the text for option C button
    }

    public void PickOptionA()
    {
        DialogueLine promptLine = currentLine;
        DialogueLine chosenLine = currentLine.dialogueOptions[0];
        PlayerPrefs.SetInt("playerScoreIncrement", PlayerPrefs.GetInt("playerScoreIncrement") + chosenLine.score); // Increment the player's score based on the chosen line's score
        DialogueLine nextLine = chosenLine.nextLine;
        if (nextLine != null)
        {
            //Debug.Log("Next Line after Option A Picked: " + nextLine.content); // Log the content of the next line
            //Debug.Log("Type: " + nextLine.type); // Log the type of the next line
            //foreach (var option in nextLine.dialogueOptions)
            //{
            //    Debug.Log("Option: " + option.content); // Log the content of each option in the next line
            //    Debug.Log("Option Type: " + option.type); // Log the type of each option in the next line
            //}
            dialogueManager.setCurrentLine(nextLine);
        }
        if (trigger != null)
        {
            if (trigger.Contains("Door"))
            {
                string roomName = trigger.Substring(0, trigger.Length - 4); // Remove "Door" from the trigger name
                PlayerPrefs.SetString("gameState", "staticSceneDuringBreak"); // Save Current Game State
                Debug.Log(breakManager.timeLeft);
                PlayerPrefs.SetFloat("breakTimeLeft", breakManager.timeLeft); // Save the current break time left
                SceneManager.LoadScene(roomName); // Load the scene corresponding to the room name
            }
        }
        enabled = false; // Disable the dialogue box component after picking an option
        //Debug.Log("Hiding Dialogue Box C");
        //HideDialogueBox(); // Hide the dialogue box after picking an option
    }

    public void PickOptionB()
    {
        DialogueLine promptLine = currentLine;
        DialogueLine chosenLine = currentLine.dialogueOptions[1];
        PlayerPrefs.SetInt("playerScoreIncrement", PlayerPrefs.GetInt("playerScoreIncrement") + chosenLine.score); // Increment the player's score based on the chosen line's score
        DialogueLine nextLine = chosenLine.nextLine;
        if (nextLine != null)
        {
            dialogueManager.setCurrentLine(nextLine);
        }
        //Debug.Log("Hiding Dialogue Box D");
        //HideDialogueBox(); // Hide the dialogue box after picking an option
    }

    public void PickOptionC()
    {
        DialogueLine promptLine = currentLine;
        DialogueLine chosenLine = currentLine.dialogueOptions[2];
        PlayerPrefs.SetInt("playerScoreIncrement", PlayerPrefs.GetInt("playerScoreIncrement") + chosenLine.score); // Increment the player's score based on the chosen line's score
        DialogueLine nextLine = chosenLine.nextLine;
        if (nextLine != null)
        {
            dialogueManager.setCurrentLine(nextLine);
        }
        //Debug.Log("Hiding Dialogue Box E");
        //HideDialogueBox(); // Hide the dialogue box after picking an option
    }

    public void HideDialogueBox()
    {
        Debug.Log("Hiding Dialogue Box");
        this.enabled = false; // Disable the dialogue box component
        textComponent.text = string.Empty; // Clear the text component when hiding the dialogue box
    }
}

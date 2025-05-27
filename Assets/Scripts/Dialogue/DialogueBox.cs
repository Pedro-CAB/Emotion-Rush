using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;
using TMPro;
using UnityEngine.SceneManagement;
using System;
using System.Linq;

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

    public System.Random random;

    public AudioSource buttonPushSound; // Reference to the AudioSource for playing button sounds

    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        textComponent.text = string.Empty;
        random = new System.Random(); // Initialize the random number generator
        DontDestroyOnLoad(buttonPushSound); // Ensure the button sound persists across scenes
    }

    //Handling Linear Dialogue --------------------------------------------------------------------------------------------

    public void StartLinearDialogue(DialogueLine l, string triggeredBy = null)
    {
        trigger = triggeredBy; // Store the object that triggered the dialogue
        enabled = true; // Show the dialogue box when starting the dialogue
        currentLine = l;
        StartCoroutine(TypeLine()); // Start the coroutine to type out the first line of dialogue
    }

    IEnumerator TypeLine()
    {
        textComponent.text = string.Empty; // Clear the text component before typing the new line
        foreach (char letter in currentLine.content.ToCharArray())
        {
            textComponent.text += letter; // Display each letter one by one
            yield return new WaitForSeconds(textSpeed); // Wait for the specified text speed before displaying the next letter
        }
    }

    public void SkipDialog()
    {
        buttonPushSound.Play(); // Play the button sound when skipping dialogue
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
                    HideDialogueBox(); // Hide the dialogue box after picking an option
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
        trigger = triggeredBy; // Store the object that triggered the dialogue
        enabled = true; // Enable the dialogue box component to show the dialogue box
        currentLine = l;

        //index = 0;
        StartCoroutine(TypeLine());
        optionAText.text = l.dialogueOptions[0].content; // Set the text for option A button
        optionBText.text = l.dialogueOptions[1].content; // Set the text for option B button
    }

    public void StartThreeOptionDialogue(DialogueLine l, string triggeredBy = null)
    {
        trigger = triggeredBy; // Store the object that triggered the dialogue
        enabled = true; // Show the dialogue box when starting the dialogue
        currentLine = l;
        if (l.type == DialogueLine.LineType.EmotionOption)
        {
            List<string> emotions = new List<string> { "Alegria", "Tristeza", "Medo", "Nojo", "Raiva" };
            emotions.Remove(l.answer);
            List<string> shuffledEmotions = emotions.OrderBy(i => Guid.NewGuid()).ToList();
            List<string> options = new List<string> { };
            random = new System.Random(); // Initialize the random number generator
            int correctIndex = random.Next(0, 3);
            if (correctIndex == 0)
            {
                StartCoroutine(TypeLine());
                optionAText.text = l.answer;
                optionBText.text = shuffledEmotions[0]; // Set the text for option B button
                optionCText.text = shuffledEmotions[1]; // Set the text for option C button
            }
            else if (correctIndex == 1)
            {
                StartCoroutine(TypeLine());
                optionAText.text = shuffledEmotions[0]; // Set the text for option A button
                optionBText.text = l.answer;
                optionCText.text = shuffledEmotions[1]; // Set the text for option C button
            }
            else if (correctIndex == 2)
            {
                StartCoroutine(TypeLine());
                optionAText.text = shuffledEmotions[0]; // Set the text for option A button
                optionBText.text = shuffledEmotions[1]; // Set the text for option B button
                optionCText.text = l.answer;
            }
        }
        else
        {
            StartCoroutine(TypeLine());
            optionAText.text = l.dialogueOptions[0].content; // Set the text for option A button
            optionBText.text = l.dialogueOptions[1].content; // Set the text for option B button
            optionCText.text = l.dialogueOptions[2].content; // Set the text for option C button
        }
    }

    public void PickOptionA()
    {
        buttonPushSound.Play(); // Play the button sound when picking an option
        if (currentLine.type == DialogueLine.LineType.TwoOption || currentLine.type == DialogueLine.LineType.ThreeOption)
        {
            DialogueLine promptLine = currentLine;
            DialogueLine chosenLine = currentLine.dialogueOptions[0];
            PlayerPrefs.SetInt("playerScoreIncrement", PlayerPrefs.GetInt("playerScoreIncrement") + chosenLine.score); // Increment the player's score based on the chosen line's score
            DialogueLine nextLine = chosenLine.nextLine;
            if (nextLine != null)
            {
                dialogueManager.setCurrentLine(nextLine);
            }
            if (trigger != null && trigger.Contains("Door"))
            {
                string roomName = trigger.Substring(0, trigger.Length - 4); // Remove "Door" from the trigger name
                PlayerPrefs.SetString("gameState", "staticSceneDuringBreak"); // Save Current Game State
                Debug.Log(breakManager.getTimeLeft());
                PlayerPrefs.SetFloat("breakTimeLeft", breakManager.getTimeLeft()); // Save the current break time left
                SceneManager.LoadScene(roomName); // Load the scene corresponding to the room name
            }
            else if (chosenLine.feedback != "None")
            {
                if (PlayerPrefs.GetString("feedback") == "")
                {
                    PlayerPrefs.SetString("feedback", chosenLine.feedback); // Save the feedback for the chosen line
                }
                else
                {
                    PlayerPrefs.SetString("feedback", PlayerPrefs.GetString("feedback") + "\n" + chosenLine.feedback); // Append the feedback for the chosen line
                }
            }
            enabled = false; // Disable the dialogue box component after picking an option
        }
        else if (currentLine.type == DialogueLine.LineType.EmotionOption)
        {
            DialogueLine nextLine = currentLine.nextLine;
            if (nextLine != null)
            {
                dialogueManager.setCurrentLine(nextLine);
            }
            if (currentLine.answer == optionAText.text)
            {
                PlayerPrefs.SetInt("playerScoreIncrement", PlayerPrefs.GetInt("playerScoreIncrement") + 1); // Increment the player's score for the correct answer
                if (PlayerPrefs.GetString("identifiedEmotions").Contains(currentLine.answer))
                {
                    // Do nothing if the emotion has already been identified
                }
                else
                {
                    if (PlayerPrefs.GetString("identifiedEmotions") == "")
                    {
                        PlayerPrefs.SetString("identifiedEmotions", currentLine.answer); // Save the identified emotions of the day
                    }
                    else
                    {
                        PlayerPrefs.SetString("identifiedEmotions", PlayerPrefs.GetString("identifiedEmotions") + ", " + currentLine.answer); // Append the identified emotions of the day
                    }
                }
            }
            enabled = false; // Disable the dialogue box component after picking an option
        }
    }

    public void PickOptionB()
    {
        buttonPushSound.Play(); // Play the button sound when picking an option
        if (currentLine.type == DialogueLine.LineType.TwoOption || currentLine.type == DialogueLine.LineType.ThreeOption)
        {
            DialogueLine promptLine = currentLine;
            DialogueLine chosenLine = currentLine.dialogueOptions[1];
            PlayerPrefs.SetInt("playerScoreIncrement", PlayerPrefs.GetInt("playerScoreIncrement") + chosenLine.score); // Increment the player's score based on the chosen line's score
            DialogueLine nextLine = chosenLine.nextLine;
            if (nextLine != null)
            {
                dialogueManager.setCurrentLine(nextLine);
            }
            if (trigger != null && trigger.Contains("Door"))
            {
                //Player answered "No" to the door prompt
                //Do nothing and close the dialogue box.
                HideDialogueBox(); // Hide the dialogue box after picking an option
            }
            else if (chosenLine.feedback != "None")
            {
                if (PlayerPrefs.GetString("feedback") == "")
                {
                    PlayerPrefs.SetString("feedback", chosenLine.feedback); // Save the feedback for the chosen line
                }
                else
                {
                    PlayerPrefs.SetString("feedback", PlayerPrefs.GetString("feedback") + "\n" + chosenLine.feedback); // Append the feedback for the chosen line
                }
            }
            enabled = false; // Disable the dialogue box component after picking an option
        }
        else if (currentLine.type == DialogueLine.LineType.EmotionOption)
        {
            DialogueLine nextLine = currentLine.nextLine;
            if (nextLine != null)
            {
                dialogueManager.setCurrentLine(nextLine);
            }
            if (currentLine.answer == optionBText.text)
            {
                PlayerPrefs.SetInt("playerScoreIncrement", PlayerPrefs.GetInt("playerScoreIncrement") + 1); // Increment the player's score for the correct answer
            }
            enabled = false; // Disable the dialogue box component after picking an option
        }
    }

    public void PickOptionC()
    {
        buttonPushSound.Play(); // Play the button sound when picking an option
        if (currentLine.type == DialogueLine.LineType.TwoOption || currentLine.type == DialogueLine.LineType.ThreeOption)
        {
            DialogueLine promptLine = currentLine;
            DialogueLine chosenLine = currentLine.dialogueOptions[2];
            PlayerPrefs.SetInt("playerScoreIncrement", PlayerPrefs.GetInt("playerScoreIncrement") + chosenLine.score); // Increment the player's score based on the chosen line's score
            DialogueLine nextLine = chosenLine.nextLine;
            if (nextLine != null)
            {
                dialogueManager.setCurrentLine(nextLine);
            }
            enabled = false; // Disable the dialogue box component after picking an option
            if (chosenLine.feedback != "None")
            {
                if (PlayerPrefs.GetString("feedback") == "")
                {
                    PlayerPrefs.SetString("feedback", chosenLine.feedback); // Save the feedback for the chosen line
                }
                else
                {
                    PlayerPrefs.SetString("feedback", PlayerPrefs.GetString("feedback") + "\n" + chosenLine.feedback); // Append the feedback for the chosen line
                }
            }
        }
        else if (currentLine.type == DialogueLine.LineType.EmotionOption)
        {
            DialogueLine nextLine = currentLine.nextLine;
            if (nextLine != null)
            {
                dialogueManager.setCurrentLine(nextLine);
            }
            if (currentLine.answer == optionCText.text)
            {
                PlayerPrefs.SetInt("playerScoreIncrement", PlayerPrefs.GetInt("playerScoreIncrement") + 1); // Increment the player's score for the correct answer
            }
            enabled = false; // Disable the dialogue box component after picking an option
        }
    }

    public void HideDialogueBox()
    {
        Debug.Log("Hiding Dialogue Box");
        textComponent.text = string.Empty; // Clear the text component when hiding the dialogue box
        gameObject.SetActive(false); // Disable the dialogue box component
    }
}

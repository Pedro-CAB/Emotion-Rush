using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;
using TMPro;
using UnityEngine.SceneManagement;
using System;
using System.Linq;

/// <summary>
/// DialogueBox is a MonoBehaviour that manages the display and interaction of dialogue boxes in the game.
/// It handles both linear dialogue and option-based dialogue, allowing players to interact with characters and make choices that affect the game state.
/// It includes methods for starting dialogue lines, typing them out, picking options, and hiding the dialogue box.
/// </summary>
public class DialogueBox : MonoBehaviour
{
    /// <summary>
    /// TextMeshProUGUI component that contains the Dialogue Prompt presented to the player.
    /// </summary>
    public TextMeshProUGUI textComponent;

    /// <summary>
    /// Button component for the option A in dialogue options.
    /// </summary>
    public Button optionAButton;

    /// <summary>
    /// TextMeshProUGUI component for the text of option A in dialogue options.
    /// </summary>
    public TextMeshProUGUI optionAText;

    /// <summary>
    /// Button component for the option B in dialogue options.
    /// </summary>
    public Button optionBButton;

    /// <summary>
    /// TextMeshProUGUI component for the text of option B in dialogue options.
    /// </summary>
    public TextMeshProUGUI optionBText;

    /// <summary>
    /// Button component for the option C in dialogue options.
    /// </summary>
    public Button optionCButton;

    /// <summary>
    /// TextMeshProUGUI component for the text of option C in dialogue options.
    /// </summary>
    public TextMeshProUGUI optionCText;

    /// <summary>
    /// DialogueManager component that manages the dialogue logic in current scene.
    /// </summary>
    public DialogueManager dialogueManager;

    /// <summary>
    /// BreakManager component that manages the break scene logic in the game.
    /// </summary>
    public BreakManager breakManager;

    /// <summary>
    /// DialogueLine struct where the current line to be displayed is stored.
    /// </summary>
    public DialogueLine currentLine;

    /// <summary>
    /// Speed at which the text is typed in the dialogue box.
    /// </summary>
    public float textSpeed;

    /// <summary>
    /// Stores the name of the object that triggered the current dialogue, if there is one.
    /// Used for Door interactions.
    /// </summary>
    private string trigger;

    /// <summary>
    /// Component for generating random numbers used for randomizing options in Emotion Identification segments.
    /// </summary>
    public System.Random random;

    /// <summary>
    /// AudioSource component for playing button push sound effect.
    /// </summary>
    public AudioSource buttonPushSound;

    /// <summary>
    /// StateMachine component that manages the game state transitions.
    /// </summary>
    public GameStateMachine stateMachine;

    void Start()
    {
        textComponent.text = string.Empty;
        random = new System.Random(); // Initialize the random number generator
    }

    //Handling Linear Dialogue --------------------------------------------------------------------------------------------

    /// <summary>
    /// Start DIsplaying a Linear Dialogue Box and Typing a Linear Dialogue Line on it.
    /// </summary>
    /// <param name="l">Linear Line to be typed and displayed.</param>
    /// <param name="triggeredBy">Name of the object that triggered the current dialogue. Null, if no object triggered the dialogue.</param>
    public void StartLinearDialogue(DialogueLine l, string triggeredBy = null)
    {
        trigger = triggeredBy; // Store the object that triggered the dialogue
        enabled = true; // Show the dialogue box when starting the dialogue
        currentLine = l;
        StartCoroutine(TypeLine()); // Start the coroutine to type out the first line of dialogue
    }

    /// <summary>
    /// Coroutine that types out lines of dialogue one character at a time.
    /// </summary>
    /// <returns> IEnumerator that types out the line.</returns>
    IEnumerator TypeLine()
    {
        textComponent.text = string.Empty; // Clear the text component before typing the new line
        foreach (char letter in currentLine.content.ToCharArray())
        {
            textComponent.text += letter; // Display each letter one by one
            yield return new WaitForSeconds(textSpeed); // Wait for the specified text speed before displaying the next letter
        }
    }

    /// <summary>
    /// Shows whole line if the line wasn't fully typed yet, or skips to next line when line is linear and fully displayed.
    /// If line is complete but not linear, it does nothing.
    /// </summary>
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

    /// <summary>
    /// Starts displaying a Two Option Dialogue Box and typing a Two Option Dialogue Prompt for it.
    /// </summary>
    /// <param name="l">DialogueLine struct containing information on the line to be displayed.</param>
    /// <param name="triggeredBy">Name of the object that triggered the current dialogue. Null, if no object triggered the dialogue.</param>
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

    /// <summary>
    /// Starts displaying a Three Option Dialogue Box and typing a Two Option Dialogue Prompt for it.
    /// </summary>
    /// <param name="l">DialogueLine struct containing information on the line to be displayed.</param>
    /// <param name="triggeredBy">Name of the object that triggered the current dialogue. Null, if no object triggered the dialogue.</param>
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
    
    /// <summary>
    /// Processes the player's choice picked from the available buttons.
    /// </summary>
    /// <param name="option">String containing the option the player picked between A, B or C.</param>
    public void PickOption(string option)
    {
        StopAllCoroutines();
        buttonPushSound.Play(); // Play the button sound when picking an option
        int optionIndex = 0;
        if (option == "A")
        {
            optionIndex = 0; // Option A corresponds to index 0
        }
        else if (option == "B")
        {
            optionIndex = 1; // Option B corresponds to index 1
        }
        else if (option == "C")
        {
            optionIndex = 2; // Option C corresponds to index 2
        }

        if (currentLine.type == DialogueLine.LineType.TwoOption || currentLine.type == DialogueLine.LineType.ThreeOption)
        {
            DialogueLine promptLine = currentLine;
            DialogueLine chosenLine = currentLine.dialogueOptions[optionIndex];
            PlayerPrefs.SetInt("playerScoreIncrement", PlayerPrefs.GetInt("playerScoreIncrement") + chosenLine.score); // Increment the player's score based on the chosen line's score
            DialogueLine nextLine = chosenLine.nextLine;
            if (nextLine != null)
            {
                dialogueManager.setCurrentLine(nextLine);
            }
            if (trigger != null && trigger.Contains("Door"))
            {
                if (option == "A")
                {
                    string roomName = trigger.Substring(0, trigger.Length - 4); // Remove "Door" from the trigger name
                    //PlayerPrefs.SetString("gameState", "staticSceneDuringBreak"); // Save Current Game State
                    stateMachine.initiateStaticSceneDuringBreak(); // Change Game State to staticSceneDuringBreak
                    Debug.Log(breakManager.getTimeLeft());
                    PlayerPrefs.SetFloat("breakTimeLeft", breakManager.getTimeLeft()); // Save the current break time left
                    SceneManager.LoadScene(roomName); // Load the scene corresponding to the room name
                }
                else if (option == "B")
                {
                    //Player answered "No" to the door prompt
                    //Do nothing and close the dialogue box.
                    HideDialogueBox(); // Hide the dialogue box after picking an option
                }
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

            string selectedOptionText = option == "A" ? optionAText.text : option == "B" ? optionBText.text : optionCText.text;

            if (currentLine.answer == selectedOptionText)
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

    public void HideDialogueBox()
    {
        Debug.Log("Hiding Dialogue Box");
        textComponent.text = string.Empty; // Clear the text component when hiding the dialogue box
        gameObject.SetActive(false); // Disable the dialogue box component
    }
}

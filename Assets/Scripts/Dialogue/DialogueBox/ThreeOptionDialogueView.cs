using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;
using System;
using System.Linq;

/// <summary>
/// ThreeDialogueView is a class that serves as the View for dialogue boxes in the game.
/// </summary>
public class ThreeOptionDialogueView : DialogueView
{
    /// <summary>
    /// UI component for the text of option A in dialogue options.
    /// </summary>
    public TextMeshProUGUI optionAText;

    /// <summary>
    /// UI component for the text of option B in dialogue options.
    /// </summary>
    public TextMeshProUGUI optionBText;

    /// <summary>
    /// UI component for the text of option C in dialogue options.
    /// </summary>
    public TextMeshProUGUI optionCText;

    /// <summary>
    /// Starts displaying a Three Option Dialogue Box and typing a Two Option Dialogue Prompt for it.
    /// </summary>
    /// <param name="l">DialogueLine struct containing information on the line to be displayed.</param>
    /// <param name="triggeredBy">Name of the object that triggered the current dialogue. Null, if no object triggered the dialogue.</param>
    public override void handleDialogue(DialogueLine l, string triggeredBy = null)
    {
        trigger = triggeredBy; // Store the object that triggered the dialogue
        enabled = true; // Show the dialogue box when starting the dialogue
        currentLine = l;
        if (l.type == DialogueLine.LineType.EmotionOption)
        {
            List<string> emotions = new List<string> { "Alegria", "Tristeza", "Medo", "Nojo", "Raiva", "Desprezo", "Surpresa" };
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
        audioPlayer.playButtonPushSound(); // Play the button sound when picking an option
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

        if (currentLine.type == DialogueLine.LineType.ThreeOption)
        {
            DialogueLine chosenLine = currentLine.dialogueOptions[optionIndex];
            PlayerPrefs.SetInt("playerScoreIncrement", PlayerPrefs.GetInt("playerScoreIncrement") + chosenLine.score); // Increment the player's score based on the chosen line's score
            Debug.Log("Player Score Increment: " + PlayerPrefs.GetInt("playerScoreIncrement"));
            DialogueLine nextLine = chosenLine.nextLine;
            if (nextLine != null)
            {
                dialogueController.setCurrentLine(nextLine);
            }
            if (trigger != null && trigger.Contains("Door"))
            {
                if (option == "A")
                {
                    string roomName = trigger.Substring(0, trigger.Length - 4); // Remove "Door" from the trigger name
                    breakManager.saveTimeLeft(); // Save the current break time left
                    breakManager.initiateStaticSceneDuringBreak(roomName);
                }
                else if (option == "B")
                {
                    //Player answered "No" to the door prompt
                    //Do nothing and close the dialogue box.
                    HideDialogueView();
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
            string selectedOptionText = option == "A" ? optionAText.text : option == "B" ? optionBText.text : optionCText.text;
            Debug.Log("CurrentLine Content: " + currentLine.content);
            Debug.Log("Option Index: " + optionIndex);
            Debug.Log("Selected Option Text: " + selectedOptionText);
            Debug.Log("Current Line Answer: " + currentLine.answer);
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

            DialogueLine nextLine = currentLine.nextLine;
            if (nextLine != null)
            {
                dialogueController.setCurrentLine(nextLine);
            }
        }
    }
}

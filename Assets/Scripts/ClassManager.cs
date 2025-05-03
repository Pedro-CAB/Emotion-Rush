using UnityEngine;
using UnityEngine.SceneManagement;

public class ClassManager : MonoBehaviour
{
    public Player player;
    public DialogueManager dialogueManager;

    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        DialogueLine line = createSampleScene();
        player.setStaticScene(3); // Set the Player to start a Class Scene facing Right
        dialogueManager.setCurrentLine(line); // Set the DialogueManager to start the scene with the first line of dialogue
    }

    void Update(){
        if (!dialogueManager.isDialogueActive()){
            SceneManager.LoadScene("BreakScene");
        }
    }

    public DialogueLine createSampleScene(){
        //Create Interactions Here

        //Sample Linear Interaction
        DialogueLine line3 = new DialogueLine(" Espero que funcione! :)", null, null);
        DialogueLine line2 = new DialogueLine(" Este é um diálogo de teste!", line3, null);
        DialogueLine line1 = new DialogueLine(" Olá! Tudo bem?", line2, null);

        //interactions.Add(line1);

        //Sample Two Option Interaction
        line1 = new DialogueLine(" Ok... Fica para a próxima então.", null, null);
        line2 = new DialogueLine(" Obrigado!", null, null);
        DialogueLine option1 = new DialogueLine("Sim.", line2, null);
        DialogueLine option2 = new DialogueLine("Não.", line1, null);
        DialogueLine option3 = new DialogueLine("Talvez noutro dia.", line1, null);
        line1 = new DialogueLine(" Queres ajudar-me?", null, new DialogueLine[]{option1, option2, option3});

        return line1;
    }
}

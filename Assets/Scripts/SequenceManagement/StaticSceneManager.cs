using UnityEngine;
using UnityEngine.SceneManagement;

public class StaticSceneManager : MonoBehaviour
{
    public Player player;
    public DialogueManager dialogueManager;

    public Schedule schedule;

    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        DialogueLine line = createSampleScene();
        player.setStaticScene();
        dialogueManager.setCurrentLine(line); // Set the DialogueManager to start the scene with the first line of dialogue
    }

    void Update(){
        if (!dialogueManager.isDialogueActive()){
            SceneManager.LoadScene("BreakScene");
            if(PlayerPrefs.GetString("gameState") == "staticSceneOutsideBreak"){
                schedule.nextPhase();
            }
            else if (PlayerPrefs.GetString("gameState") == "staticSceneDuringBreak"){
                PlayerPrefs.SetFloat("timeLeft", PlayerPrefs.GetFloat("breakTimeLeft") - 300.0f);
            }
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
        line1 = new DialogueLine("Ok... Fica para a próxima então.", null, null);
        line2 = new DialogueLine("Obrigado!", null, null);
        DialogueLine option1 = new DialogueLine("Sim.", null, null);
        DialogueLine option2 = new DialogueLine("Não.", null, null);
        DialogueLine option3 = new DialogueLine("Talvez noutro dia.", line1, null);
        line1 = new DialogueLine("Queres ajudar-me?", null, new DialogueLine[]{option1, option2, option3});
        DialogueLine line0 = new DialogueLine("Olá! O meu nome é José!", line1, null);

        return line0;
    }
}

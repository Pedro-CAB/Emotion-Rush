using UnityEngine;

public class Door : InteractiveObject
{   
    string doorName;

    DialogueLine prompt;
    DialogueLine option1 = new DialogueLine("Sim.", null, null);
    DialogueLine option2 = new DialogueLine("Não.", null, null);

    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        doorName = gameObject.name;
        switch(doorName){
            case "LibraryDoor":
                prompt = new DialogueLine("Queres entrar na Biblioteca?", null, new DialogueLine[]{option1, option2});
                break;
            case "ClassroomADoor":
                prompt = new DialogueLine("A tua sala está fechada. Aproveita o intervalo!", null, null);
                break;
            case "ClassroomBDoor":
            case "ClassroomCDoor":
            case "ClassroomDDoor":
                prompt = new DialogueLine("Esta não é a tua sala.", null, null);
                break;
            case "BarDoor":
                prompt = new DialogueLine("Queres entrar no Bar?", null, new DialogueLine[]{option1, option2});
                break;
            case "AuditoriumDoor":
                prompt = new DialogueLine("O Auditório está fechado agora.", null, null);
                break;
            case "LabDoor":
                prompt = new DialogueLine("O Laboratório está fechado agora.", null, null);
                break;
            case "PlaygroundDoor":
                prompt = new DialogueLine("Queres entrar no Recreio?", null, new DialogueLine[]{option1, option2});
                break;
            case "FrontEntranceDoor":
            case "LeftEntranceDoor":
            case "RightEntranceDoor":
                prompt = new DialogueLine("Não podes sair da escola agora.", null, null);
                break;
            default:
                prompt = new DialogueLine("Esta porta está trancada.", null, null);
                break;
        }
    }

    public void whenInteracted(){ //Overriding the function from InteractiveObject
        dialogueManager.setCurrentLine(prompt);
    }
}

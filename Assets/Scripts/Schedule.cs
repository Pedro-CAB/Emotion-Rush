using UnityEngine;
using UnityEngine.SceneManagement;
using TMPro;

public class Schedule : MonoBehaviour
{
    int currentDay;
    string currentWeekDay; // Monday, Tuesday, Wednesday, Thursday, Friday
    int currentWeek;

    public TextMeshProUGUI currentWeekDayText; // Reference to the TextMeshProUGUI component for displaying the current week day
    public TextMeshProUGUI currentPhaseText; // Reference to the TextMeshProUGUI component for displaying the current day
    public TextMeshProUGUI currentRoomText; // Reference to the TextMeshProUGUI component for displaying the current day

    public GameObject scheduleUI; // Reference to the UI GameObject that contains the schedule information

    string currentDayPhase; // MorningClass1, MorningBreak, MorningClass2, LunchBreak, AfternoonClass
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        scheduleUI.gameObject.SetActive(true);
        int week = PlayerPrefs.GetInt("currentWeek");
        string weekStr = "Semana " + week.ToString();
        currentWeekDay = PlayerPrefs.GetString("currentWeekDay");
        string dayPhase = PlayerPrefs.GetString("currentPhase");
        currentDayPhase = PlayerPrefs.GetString("currentPhase");
        string currentSceneName = SceneManager.GetActiveScene().name;
        if (currentWeekDay == "Monday")
        {
            currentWeekDayText.text = "Segunda-feira";
        }
        else if (currentWeekDay == "Tuesday")
        {
            currentWeekDayText.text = "Terça-feira";
        }
        else if (currentWeekDay == "Wednesday")
        {
            currentWeekDayText.text = "Quarta-feira";
        }
        else if (currentWeekDay == "Thursday")
        {
            currentWeekDayText.text = "Quinta-feira";
        }
        else if (currentWeekDay == "Friday")
        {
            currentWeekDayText.text = "Sexta-feira";
        }

        if (dayPhase == "MorningClass1")
        {
            dayPhase = "Aula I"; // Default starting phase
        }
        else if (dayPhase == "MorningBreak")
        {
            dayPhase = "Intervalo da Manhã";
        }
        else if (dayPhase == "MorningClass2")
        {
            dayPhase = "Aula II";
        }
        else if (dayPhase == "LunchBreak")
        {
            dayPhase = "Intervalo do Almoço";
        }
        else if (dayPhase == "AfternoonClass")
        {
            dayPhase = "Aula III";
        }
        else
        {
            scheduleUI.gameObject.SetActive(false);
        }

        if (currentSceneName == "Classroom")
        {
            currentRoomText.text = "Sala A";
        }
        else if (currentSceneName == "Library")
        {
            currentRoomText.text = "Biblioteca";
        }
        else if (currentSceneName == "Auditorium")
        {
            currentRoomText.text = "Auditório";
        }
        else if (currentSceneName == "Lab")
        {
            currentRoomText.text = "Laboratório";
        }
        else if (currentSceneName == "Gym")
        {
            currentRoomText.text = "Campo Desportivo";
        }
        else if (currentSceneName == "BreakScene")
        {
            currentRoomText.text = "Livre";
        }
        else if (currentSceneName == "Playground")
        {
            currentRoomText.text = "Recreio";
        }
        else
        {
            currentRoomText.text = "Sala Desconhecida";
        }
        
        StartCoroutine(HideScheduleUIAfterDelay(1.5f));

    }

    private System.Collections.IEnumerator HideScheduleUIAfterDelay(float delay)
    {
        yield return new WaitForSeconds(delay);
        scheduleUI.gameObject.SetActive(false);
    }

    public void nextPhase(){
        Debug.Log("Current Phase: " + currentDayPhase);
        if (currentDayPhase == "MorningClass1")
        {
            Debug.Log("Advancing to Morning Break");
            currentDayPhase = "MorningBreak";
        }
        else if (currentDayPhase == "MorningBreak")
        {
            currentDayPhase = "MorningClass2";
        }
        else if (currentDayPhase == "MorningClass2")
        {
            currentDayPhase = "LunchBreak";
        }
        else if (currentDayPhase == "LunchBreak")
        {
            currentDayPhase = "AfternoonClass";
        }
        else if (currentDayPhase == "AfternoonClass")
        {
            currentDayPhase = "DayOutcome";
            PlayerPrefs.SetString("currentPhase", currentDayPhase);
            displayDayOutcome();
        }
        else if (currentDayPhase == "DayOutcome")
        {
            currentDayPhase = "MorningClass1";
        }
        else
        {
            currentDayPhase = "MorningClass1";
        }
        PlayerPrefs.SetString("currentPhase", currentDayPhase);
    }

    void displayDayOutcome()
    {
        SceneManager.LoadScene("DayOutcome");
    }

    public void nextDay()
    {
        currentDay++;
        if (currentDay > 4) // After Friday, new Week starts
        {
            currentWeek++;
        }
        calculateWeekDay();
        PlayerPrefs.SetInt("currentDay", currentDay);
        PlayerPrefs.SetInt("currentWeek", currentWeek);
        PlayerPrefs.SetString("feedback", "");
        PlayerPrefs.SetString("identifiedEmotions", "");
    }

    void calculateWeekDay(){
        int weekDayIndex = currentDay % 5;
        switch (weekDayIndex)
        {
            case 0:
                currentWeekDay = "Monday";
                break;
            case 1:
                currentWeekDay = "Tuesday";
                break;
            case 2:
                currentWeekDay = "Wednesday";
                break;
            case 3:
                currentWeekDay = "Thursday";
                break;
            case 4:
                currentWeekDay = "Friday";
                break;
        }
        PlayerPrefs.SetString("currentWeekDay", currentWeekDay);
    }


}

using UnityEngine;
using UnityEngine.SceneManagement;
using TMPro;

public class Schedule : MonoBehaviour
{
    int currentDay;
    string currentWeekDay; // Monday, Tuesday, Wednesday, Thursday, Friday
    int currentWeek;

    public TextMeshProUGUI currentWeekDayText; // Reference to the TextMeshProUGUI component for displaying the current week day
    public TextMeshProUGUI currentPhaseText; // Reference to the TextMeshProUGUI component for displaying the current day phase
    public TextMeshProUGUI currentRoomText; // Reference to the TextMeshProUGUI component for displaying the current room

    public GameObject scheduleUI; // Reference to the UI GameObject that contains the schedule information

    string currentDayPhase; // MorningClass1, MorningBreak, MorningClass2, LunchBreak, AfternoonClass
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        //scheduleUI.gameObject.SetActive(true);
        int week = PlayerPrefs.GetInt("currentWeek");
        string weekStr = "Semana " + week.ToString();
        string dayPhase = PlayerPrefs.GetString("currentPhase");
        currentDayPhase = PlayerPrefs.GetString("currentPhase");

        updateWeekDayUI();
        updateDayPhaseUI();
        updateRoomUI();

        if (scheduleUI != null)
        {
            StartCoroutine(HideScheduleUIAfterDelay(2f));   
        }

    }
    /// <summary>
    /// Updates Current Week Day GUI Text based on the currentWeekDay variable in Player Prefs.
    /// Does nothing if GUI element doesn't exist.
    /// </summary>
    private void updateWeekDayUI()
    {
        if (currentWeekDayText != null)
        {
            currentWeekDay = PlayerPrefs.GetString("currentWeekDay");
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
        }
    }

    public void updateWeekDay()
    {
        currentWeekDay = PlayerPrefs.GetString("currentWeekDay");
        Debug.Log("updateWeekDay :: Current Week Day: " + currentWeekDay);
        if (currentWeekDay == "Monday")
        {
            currentWeekDay = "Tuesday";
        }
        else if (currentWeekDay == "Tuesday")
        {
            currentWeekDay = "Wednesday";
        }
        else if (currentWeekDay == "Wednesday")
        {
            currentWeekDay = "Thursday";
        }
        else if (currentWeekDay == "Thursday")
        {
            currentWeekDay = "Friday";
        }
        else if (currentWeekDay == "Friday")
        {
            currentWeekDay = "Monday";
            currentWeek++;
        }

        Debug.Log("updateWeekDay :: Current Week Day: " + currentWeekDay);
        Debug.Log("updateWeekDay :: Current Week Day: " + PlayerPrefs.GetString("currentWeekDay"));

        PlayerPrefs.SetString("currentWeekDay", currentWeekDay);
    }
    /// <summary>
    /// Updates Current Day Phase GUI Text based on the currentPhase variable in Player Prefs.
    /// Does nothing if GUI element doesn't exist.
    /// </summary>
    private void updateDayPhaseUI()
    {
        if (currentPhaseText != null)
        {
            string dayPhase = PlayerPrefs.GetString("currentPhase");
            if (dayPhase == "MorningClass1")
            {
                currentPhaseText.text = "Aula I";
            }
            else if (dayPhase == "MorningBreak")
            {
                currentPhaseText.text = "Intervalo - Manhã";
            }
            else if (dayPhase == "MorningClass2")
            {
                currentPhaseText.text = "Aula II";
            }
            else if (dayPhase == "LunchBreak")
            {
                currentPhaseText.text = "Intervalo - Almoço";
            }
            else if (dayPhase == "AfternoonClass")
            {
                currentPhaseText.text = "Aula III";
            }
        }
    }
    /// <summary>
    /// Updates Current Room GUI Text based on the currentPhase variable in Player Prefs.
    /// Does nothing if GUI element doesn't exist.
    /// </summary>
    private void updateRoomUI()
    {
        if (currentRoomText != null)
        {
            string currentSceneName = SceneManager.GetActiveScene().name;
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
        }
    }

    private System.Collections.IEnumerator HideScheduleUIAfterDelay(float delay)
    {
        yield return new WaitForSeconds(delay);
        scheduleUI.gameObject.SetActive(false);
    }

    public void nextPhase(){
        Debug.Log("Phase Set From: " + currentDayPhase);
        if (currentDayPhase == "MorningClass1")
        {
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
        }
        else if (currentDayPhase == "DayOutcome")
        {
            currentDayPhase = "MorningClass1";
        }
        else
        {
            currentDayPhase = "MorningClass1";
        }
        Debug.Log("To: " + currentDayPhase);
        PlayerPrefs.SetString("currentPhase", currentDayPhase);
    }

    public void nextDay()
    {
        updateWeekDay();
        PlayerPrefs.SetInt("currentWeek", currentWeek);
        PlayerPrefs.SetString("feedback", "");
        PlayerPrefs.SetString("identifiedEmotions", "");
    }
}

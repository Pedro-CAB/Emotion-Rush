using UnityEngine;
using UnityEngine.SceneManagement;

public class Schedule : MonoBehaviour
{
    int currentDay;
    string currentWeekDay; // Monday, Tuesday, Wednesday, Thursday, Friday
    int currentWeek;

    string currentDayPhase; // MorningClass1, MorningBreak, MorningClass2, LunchBreak, AfternoonClass
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        currentDay = PlayerPrefs.GetInt("currentDay");
        currentWeek = PlayerPrefs.GetInt("currentWeek");
        currentWeekDay = PlayerPrefs.GetString("currentWeekDay");
        currentDayPhase = PlayerPrefs.GetString("currentPhase");
    }

    public void nextPhase(){
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

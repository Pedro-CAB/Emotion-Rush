using UnityEngine;

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
        int weekDayIndex = currentDay % 5; // Considering only 5 days in a week (Monday to Friday)
        switch (weekDayIndex)
        {
            case 0:
                currentWeekDay = "Segunda-feira";
                break;
            case 1:
                currentWeekDay = "Terça-feira";
                break;
            case 2:
                currentWeekDay = "Quarta-feira";
                break;
            case 3:
                currentWeekDay = "Quinta-feira";
                break;
            case 4:
                currentWeekDay = "Sexta-feira";
                break;
        }
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
            currentDayPhase = "MorningClass1";
            nextDay();
        }
        PlayerPrefs.SetString("currentPhase", currentDayPhase);
    }
    
    void nextDay(){
        currentDay++;
        if (currentDay > 4) // After Friday, new Week starts
        {
            currentWeek++;
        }
        calculateWeekDay();
        PlayerPrefs.SetInt("currentDay", currentDay);
        PlayerPrefs.SetInt("currentWeek", currentWeek);
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
    }


}

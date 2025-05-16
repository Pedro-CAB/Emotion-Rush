using UnityEngine;

public class ScoreManager : MonoBehaviour
{
    int classAScore;
    int classBScore;

    int classCScore;

    int classDScore;


    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        classAScore = PlayerPrefs.GetInt("classAScore");
        classBScore = PlayerPrefs.GetInt("classBScore");
        classCScore = PlayerPrefs.GetInt("classCScore");
        classDScore = PlayerPrefs.GetInt("classDScore");
    }

    void incrementScore(string className, int score)
    {
        switch (className)
        {
            case "A":
                classAScore += score;
                break;
            case "B":
                classBScore += score;
                break;
            case "C":
                classCScore += score;
                break;
            case "D":
                classDScore += score;
                break;
        }
    }

    void saveScores()
    {
        PlayerPrefs.SetInt("classAScore", classAScore);
        PlayerPrefs.SetInt("classBScore", classBScore);
        PlayerPrefs.SetInt("classCScore", classCScore);
        PlayerPrefs.SetInt("classDScore", classDScore);
    }
}

using UnityEngine;
using TMPro;

public class CoinUI : MonoBehaviour
{
    public TextMeshProUGUI currentCoinText;
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        currentCoinText.text = PlayerPrefs.GetInt("coins").ToString();
    }
}

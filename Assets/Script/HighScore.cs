using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class HighScore : MonoBehaviour
{
    public TextMeshProUGUI scoreText;
    public TextMeshProUGUI highScore;
    int score = 0;
    void Start()
    {
        
    }

    
    void Update()
    {
        highScore.text = PlayerPrefs.GetInt("HighScore").ToString();
    }

    public void saveScore()
    {
        PlayerPrefs.SetInt("HighScore", score);
    }
}

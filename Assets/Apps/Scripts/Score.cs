using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI; 

public class ScoreManager : MonoBehaviour
{
    public Text scoreText; 
    private int score = 0; 

    void Start()
    {
        if (scoreText == null)
            scoreText = GameObject.Find("ScoreText").GetComponent<Text>();

        UpdateScoreUI();
    }

    public void AddScore(int amount)
    {
        score += amount;
        UpdateScoreUI();
    }

    void UpdateScoreUI()
    {
        scoreText.text = "Score: " + score;
    }
}
using System.Collections;
using System.Collections.Generic;
using System.Xml.Serialization;
using UnityEngine;
using UnityEngine.UI;

public class ScoreScript : MonoBehaviour
{
    int Score;
    public Text ScoreText;
    public Text DeadScoreText;

    void Start()
    {
        Time.timeScale = 1.0f;
    }

    // Update is called once per frame
    void Update()
    {
        ScoreText.text = Score.ToString();
        DeadScoreText.text = Score.ToString();
    }

    public void Scoree(int score)
    {
        Score += score;
    }
}

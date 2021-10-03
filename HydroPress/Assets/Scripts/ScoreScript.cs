    using System.Collections;
using System.Collections.Generic;
using System.Net.Sockets;
using UnityEngine;
using UnityEngine.UI;
using PlayerPrefs = UnityEngine.PlayerPrefs;

public class ScoreScript : MonoBehaviour
{
    public static int scoreValue = 0;

    public Text score;
    public Text highScore;

    private PlayfabManager playfabManager;

    void Start()
    {
        playfabManager = GetComponentInParent<PlayfabManager>();
        highScore.text = "High Score: "+PlayerPrefs.GetInt("HighScore", 0).ToString();
    }

    // Update is called once per frame
    void Update()
    {
        score.text = "Score: " + scoreValue;
    }

    public void Reset()
    {
        PlayerPrefs.DeleteKey("HighScore");
        highScore.text = "High Score: " +"0";
    }

    public void UpdateHighscore()
    {
        if (scoreValue > PlayerPrefs.GetInt("HighScore", 0))
        {
            PlayerPrefs.SetInt("HighScore", scoreValue);
            playfabManager.SendLeaderboard(scoreValue);
        }
    }
}

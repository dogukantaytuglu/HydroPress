using System;
using System.Collections;
using System.Collections.Generic;
using UnityEditor;
using UnityEngine;

public class ObjectSpeedHandler : MonoBehaviour
{
    public static float speed;

    public float pitchValue;
    public TimeBar timeBar;
    public AudioManager audioManager;
    public float divider;

    private int scoreValue;
    private int tempScore;
    private Vector2 screenBounds;
    private float spawnPoint;


    // Start is called before the first frame update
    void Start()
    {
        //Get Screen Bounds
        screenBounds =
            Camera.main.ScreenToWorldPoint(new Vector3(Screen.width, Screen.height, Camera.main.transform.position.z));
        spawnPoint = Math.Abs(screenBounds.x - (screenBounds.x * 2.3f));

        divider = 1;

        tempScore = 0;
        pitchValue = 1;
    }

    // Update is called once per frame
    void Update()
    {
        speed = (spawnPoint / divider) *2;
        scoreValue = ScoreScript.scoreValue;

        if (scoreValue > 0 && scoreValue % 2 == 0 && scoreValue != tempScore)
        {
            ChangeMultiplier();
            timeBar.countdownMultiplier += 0.01f;

            pitchValue += 0.01f;
            audioManager.SetPitch("Theme", pitchValue);
        }
    }

    private void ChangeMultiplier()
    {
        if (divider > 0.5f)
        {
            divider -= 0.2f;
        }

        tempScore = scoreValue;
    }
}

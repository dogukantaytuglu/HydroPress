using System.Collections;
using System.Collections.Generic;
using PlayFab.DataModels;
using UnityEngine;

public class FriendlyObjectDetector : MonoBehaviour
{
    private PressBehaviour pressBehaviour;
    private TimeBar timeBar;
    private bool canPress;
    private string objectType;
    private AudioManager audioManager;
    private ScoreScript scoreScript;

    void Awake()
    {
        timeBar = FindObjectOfType<TimeBar>();
        audioManager = FindObjectOfType<AudioManager>();
        scoreScript = FindObjectOfType<ScoreScript>();
        pressBehaviour = GetComponentInParent<PressBehaviour>();
        canPress = false;
        objectType = "";
    }

    void Update()
    {
        canPress = PressBehaviour.canPress;
    }

    public void FriendlyObjectCheck()
    {
        if (canPress)
        {
            if (objectType == "FriendlyObject")
            {
                ScoreScript.scoreValue += 1;
                timeBar.AddTime(1f);
            }
            else
            {
                audioManager.Play("GameOver");
                timeBar.slider.value = 0;
            }
        }
    }

    void OnTriggerEnter2D(Collider2D other)
    {
        objectType = other.tag;
    }
}

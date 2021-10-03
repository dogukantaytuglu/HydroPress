using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.SocialPlatforms.Impl;
using UnityEngine.UI;

public class TimeBar : MonoBehaviour
{
    public Slider slider;
    public Gradient gradient;
    public Image fill;
    public float countdownMultiplier;
    public GameObject gameOverScreen;
    public ScoreScript scoreScript;
    public Button adButton;
    public int time = 5;



    void Start()
    {
        GetComponentInParent<GameManager>();
        SetMaxTime(time);
        slider.value = slider.maxValue;
        SetGradientColorToMax();
        countdownMultiplier = 1.0f;
    }

    void FixedUpdate()
    {
        if (slider.value > slider.minValue && GameManager.gameIsActive && FpsCounter.m_lastFramerate > GameManager.minFPS)
        {
            slider.value -= 1 * countdownMultiplier * Time.deltaTime;
            fill.color = gradient.Evaluate(slider.normalizedValue);
        }
        
    }
    public void SetGradientColorToMax()
    {
        fill.color = gradient.Evaluate(1f);
    }
    public void SetMaxTime(int time)
    {
        slider.maxValue = time;
    }

    public void SetTime(int time)
    {
        slider.value = time;
    }

    public void AddTime(float time)
    {
        slider.value += time;
    }

    public void ReduceTime(int time)
    {
        slider.value -= time;
    }

    void Update()
    {
        if (slider.value == 0)
        {
            PressBehaviour.canPress = false;
            scoreScript.UpdateHighscore();
            if (GameManager.addRewardCount == 0)
            {
                StopAllCoroutines();
                adButton.interactable = true;
                GameManager.gameIsActive = false;
                gameOverScreen.SetActive(true);
            }

            else
            {
                StopAllCoroutines();
                adButton.interactable = false;
                GameManager.gameIsActive = false;
                gameOverScreen.SetActive(true);
            }
            
        }
    }

}

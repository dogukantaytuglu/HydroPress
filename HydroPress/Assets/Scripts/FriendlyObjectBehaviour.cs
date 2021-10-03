using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class FriendlyObjectBehaviour : MonoBehaviour
{
    private GameManager gameManager;
    private ObjectSpeedHandler objectSpeedHandler;
    private PressBehaviour pressBehaviour;
    private Vector2 screenBounds;
    private float speedMultiplier;
    private TimeBar timeBar;
    private AudioManager audioManager;


    void Awake()
    {
        gameManager = FindObjectOfType<GameManager>();
        timeBar = FindObjectOfType<TimeBar>();
        pressBehaviour = FindObjectOfType<PressBehaviour>();
        audioManager = FindObjectOfType<AudioManager>();

    }


    void OnTriggerEnter2D(Collider2D other)
    {
        if (other.tag == "Press")
        {
            audioManager.Play("GameOver");
            timeBar.slider.value = 0;
        }
    }
}

using System.Collections;
using System.Linq;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class GameManager : MonoBehaviour
{
    public static bool gameIsActive;
    public static int addRewardCount = 0;
    public static int minFPS = 15;

    public AudioManager audioManager;
    public TimeBar timeBar;
    public ObjectSpeedHandler objectSpeedHandler;
    public ObjectSpawner objectSpawner;
    public PressBehaviour pressBehaviour;
    public GameObject gameOverScreen;
    public GameObject pauseMenu;
    public GameObject UI;
    public GameObject endGameScreen;
    public Button pauseButton;

    private bool pauseMenuActive;


    void Start()
    {
        gameIsActive = false;

        Application.targetFrameRate = 60;
    }

    void Update()
    {
        if (endGameScreen.activeSelf)
        {
            pauseButton.interactable = false;
        }
        else
        {
            pauseButton.interactable = true;
        }
    }

    public void PauseGame()
    {
        gameIsActive = !gameIsActive;
    }

    public void PauseMenu()
    {
        if (pauseMenu.activeSelf)
        {
            pauseMenu.SetActive(false);
        }
        else
        {
            pauseMenu.SetActive(true);
        }
    }

    public void ReturnHome()
    {
        //Load Scene
        SceneManager.LoadScene(0);

        //Set score and Reward counter to 0
        ScoreScript.scoreValue = 0;
        addRewardCount = 0;
    }

    public void RestartGame()
    {
        DestroyAllGameObjects();

        //Set score and Reward counter to 0
        ScoreScript.scoreValue = 0;
        addRewardCount = 0;

        //Set time to initial values
        timeBar.SetTime(timeBar.time);
        timeBar.countdownMultiplier = 1.0f;
        timeBar.SetGradientColorToMax();

        //Set speed to initial values
        objectSpeedHandler.divider = 1;
        audioManager.SetPitch("Theme",1f);
        objectSpeedHandler.pitchValue = 1;

        //Set object spawner values to init
        objectSpawner.distanceCounter = objectSpawner.distanceToMid;
        objectSpawner.beltCanMove = true;
        objectSpawner.spawnDelayer = 0;

        //Set game active and disable game over screen
        gameIsActive = true;
        gameOverScreen.SetActive(false);
    }

    public void GiveAdReward()
    {
        StartCoroutine(DelayedGiveReward());
    }

    IEnumerator DelayedGiveReward()
    {
        DestroyAllGameObjects();

        //Set object spawner values to init
        objectSpawner.distanceCounter = objectSpawner.distanceToMid;
        objectSpawner.beltCanMove = true;
        objectSpawner.spawnDelayer = 0;

        addRewardCount += 1;

        timeBar.AddTime(timeBar.time);
        timeBar.gameOverScreen.SetActive(false);
        timeBar.SetGradientColorToMax();

        yield return new WaitForSeconds(1);
        
        gameIsActive = true;
        StopCoroutine(DelayedGiveReward());
    }

    void DestroyAllGameObjects()
    {
        var friendlyObjects = GameObject.FindGameObjectsWithTag("FriendlyObject");
        var enemyObjects = GameObject.FindGameObjectsWithTag("EnemyObject");
        var allObjects = friendlyObjects.Concat(enemyObjects).ToArray();


        foreach (var o in allObjects)
        {
            Destroy(o);
        }
    }

}

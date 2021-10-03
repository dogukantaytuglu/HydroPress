using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEditor;
using UnityEngine;
using UnityEngine.UI;
using Random = UnityEngine.Random;

public class ObjectSpawner : MonoBehaviour
{
    public GameObject[] prefabList;
    public GameObject[] enemyPrefabList;
    public Slider slider;
    public bool beltCanMove;
    public int spawnDelayer = 2;
    public Vector2 screenBounds;
    public float distanceToMid;
    public float distanceCounter;

    private float timeLeft;
    private int friendlyObjectCounter;
    private float speedMultiplier;
    private bool canSpawn;
    private GameObject[] friendlyObjects;
    private GameObject[] enemyObjects;
    private GameObject[] allObjects;


    public void Awake()
    {
        //Get Screen Bounds
        screenBounds =
            Camera.main.ScreenToWorldPoint(new Vector3(Screen.width, Screen.height, Camera.main.transform.position.z));
        distanceToMid = Math.Abs(screenBounds.x - (screenBounds.x * 2.3f));
        distanceCounter = distanceToMid;


        //Spawn First Two Objects
        spawnRandomObject();
        spawnFirstFollowerObject();

        //Set Objects and Params
        beltCanMove = true;
    }

    void Update()
    {
        if (GameManager.gameIsActive)
        {
            timeLeft = slider.normalizedValue;

            if (beltCanMove)
            {
                if (spawnDelayer == 0)
                {
                    SpawnObject();
                    canSpawn = false;
                }

                friendlyObjects = GameObject.FindGameObjectsWithTag("FriendlyObject");
                enemyObjects = GameObject.FindGameObjectsWithTag("EnemyObject");
                allObjects = friendlyObjects.Concat(enemyObjects).ToArray();

                foreach (var o in allObjects)
                {
                    if (FpsCounter.m_lastFramerate > GameManager.minFPS)
                    {
                        o.transform.position += new Vector3(ObjectSpeedHandler.speed * Time.smoothDeltaTime, 0, 0);
                    }
                    if (o.transform.position.x >= distanceToMid)
                    {
                        Destroy(o.gameObject);
                    }
                }
                if (FpsCounter.m_lastFramerate > GameManager.minFPS)
                {
                    distanceCounter -= ObjectSpeedHandler.speed * Time.smoothDeltaTime;
                }
                if (distanceCounter <= 0.15f)
                {
                    distanceCounter = distanceToMid;
                    beltCanMove = false;
                    canSpawn = true;
                    PressBehaviour.canPress = true;
                }
            }
        }
    }

    private void spawnRandomObject()
    {
        GameObject gameObject = Instantiate(prefabList[Random.Range(0, prefabList.Length)]) as GameObject;
        gameObject.transform.position = new Vector3(screenBounds.x - (screenBounds.x * 2.3f), 0.3f, 0);



        if (gameObject.tag == "FriendlyObject")
        {
            friendlyObjectCounter += 1;
        }
        else
        {
            friendlyObjectCounter = 0;
        }
    }

    private void spawnFirstFollowerObject()
    {
        GameObject gameObject = Instantiate(prefabList[Random.Range(0, prefabList.Length)]) as GameObject;
        gameObject.transform.position = new Vector3(2 * (screenBounds.x - (screenBounds.x * 2.3f)), 0.3f, 0);
    }

    private void spawnEnemyObject()
    {
        GameObject gameObject = Instantiate(enemyPrefabList[Random.Range(0, enemyPrefabList.Length)]) as GameObject;
        gameObject.transform.position = new Vector3(screenBounds.x - (screenBounds.x * 2.3f), 0.3f, 0);

    }

    public void SpawnObject()
    {
        if (canSpawn)
        {
            if (timeLeft < 0.5 && friendlyObjectCounter > 2)
            {
                spawnEnemyObject();
                friendlyObjectCounter = 0;
            }
            else
            {
                spawnRandomObject();
            }
        }
    }

    public void StartBeltMovement()
    {
        if (!beltCanMove)
        {
            beltCanMove = true;
            PressBehaviour.canPress = false;
            if (spawnDelayer > 0)
            {
                spawnDelayer -= 1;
            }
        }
    }
}

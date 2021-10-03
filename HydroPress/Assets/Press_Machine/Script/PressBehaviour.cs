using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class PressBehaviour : MonoBehaviour
{
    public float pressTime = 0.7f;
    public Animator[] animators;
    public static bool canPress;
    public ObjectSpawner objectSpawner;

    void Awake()
    {
        canPress = false;

        PressUp();
    }

    void Update()
    {
    }
    
    void RunAnimations()
    {
        foreach (var animator in animators)
        {
            animator.SetBool("Pressed", true);
        }
    }

    void StopAnimations()
    {
        foreach (var animator in animators)
        {
            animator.SetBool("Pressed", false);
        }
    }

    public void Press()
    {
        if (canPress &&  GameManager.gameIsActive)
        {
            canPress = false;
            StartCoroutine(PressAction());
        }
    }

    IEnumerator PressAction()
    {
        PressDown();
        RunAnimations();
        yield return new WaitForSeconds(pressTime);
        StopAnimations();
        PressUp();
        objectSpawner.StartBeltMovement();
    }

    void PressDown()
    {
        Vector3 newPosition = transform.position; // We store the current position
        newPosition.y = 3.02f; // We set a axis, in this case the y axis
        transform.position = newPosition; // We pass it back
    }

    void PressUp()
    {
        Vector3 newPosition = transform.position; // We store the current position
        newPosition.y = 6.55f; // We set a axis, in this case the y axis
        transform.position = newPosition; // We pass it back
    }
}

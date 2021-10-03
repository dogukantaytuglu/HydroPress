using UnityEngine;

public class EnemyObjectBehaviour : MonoBehaviour
{
    [HideInInspector] public SpriteRenderer spriteRenderer;
    public Sprite newSprite;

    private TimeBar timeBar;
    private BoxCollider2D boxCollider2D;
    private AudioManager audioManager;
    private ScoreScript scoreScript;

    void Awake()
    {
        timeBar = FindObjectOfType<TimeBar>();
        audioManager = FindObjectOfType<AudioManager>();
        scoreScript = FindObjectOfType<ScoreScript>();
        spriteRenderer = GetComponent<SpriteRenderer>();
        boxCollider2D = GetComponent<BoxCollider2D>();

    }

    void OnTriggerEnter2D(Collider2D other)
    {
        if (other.tag == "Press")
        {
            spriteRenderer.sprite = newSprite;
            ScoreScript.scoreValue += 1;
            audioManager.Play("ObjectDestroyed");
            timeBar.AddTime(1f);
        }
    }

}

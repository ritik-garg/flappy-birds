using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Bird : MonoBehaviour
{
    public float upForce = 200f;
    public GameObject gameStartText;
    public Text levelText;
    public AudioClip[] birdSounds;

    private bool clickedOnce = false;
    private Rigidbody2D rb2d;
    private Animator animator;
    private AudioSource audioSource;

    public enum GameStatus {NOTSTARTED, DEAD, PLAYING, UPGRADING};
    public static GameStatus gameStatus;

    // Start is called before the first frame update
    void Start()
    {
        // instance = this;
        rb2d = GetComponent<Rigidbody2D>();
        animator = GetComponent<Animator>();
        gameStatus = GameStatus.NOTSTARTED;
        Physics2D.gravity = new Vector2(0, 0);
        levelText.text = "LEVEL: " + Levels.currentLevel.ToString();
        audioSource = GetComponent<AudioSource>();
    }

    // Update is called once per frame
    void Update()
    {
        if(!clickedOnce) {
            if (Input.GetMouseButtonDown(0)) {
                gameStatus = GameStatus.PLAYING;
                Physics2D.gravity = new Vector2(0, -9.81f);
                clickedOnce = true;
                gameStartText.SetActive(false);
            }
        }
        if(gameStatus == GameStatus.NOTSTARTED) {
            animator.SetTrigger("Flap");
        }
        else if(gameStatus == GameStatus.PLAYING) {
            if (Input.GetMouseButtonDown(0)) {
                animator.SetTrigger("Flap");
                rb2d.velocity = Vector2.zero;
                rb2d.AddForce(new Vector2(0, upForce));
                audioSource.PlayOneShot(birdSounds[0]);
            }
        }
    }

    private void OnCollisionEnter2D(Collision2D other) {
        if(gameStatus != GameStatus.DEAD) {
            audioSource.PlayOneShot(birdSounds[2]);
            animator.SetTrigger("Die");
            rb2d.velocity = Vector2.zero;
            gameStatus = GameStatus.DEAD;  
            audioSource.PlayOneShot(birdSounds[1]);
            GameController.instance.BirdDied();
        }
    }
}

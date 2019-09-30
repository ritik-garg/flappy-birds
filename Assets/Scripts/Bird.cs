using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Bird : MonoBehaviour
{
    //public static Bird instance;
    public float upForce = 200f;
    public GameObject gameStartText;
    private bool clickedOnce = false;
    private Rigidbody2D rb2d;
    private Animator animator;
    public enum GameStatus {NOTSTARTED, DEAD, PLAYING};
    public static GameStatus gameStatus;

    // Start is called before the first frame update
    void Start()
    {
        // instance = this;
        rb2d = GetComponent<Rigidbody2D>();
        animator = GetComponent<Animator>();
        gameStatus = GameStatus.NOTSTARTED;
        Physics2D.gravity = new Vector2(0, 0);
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
            }
        }
    }

    private void OnCollisionEnter2D(Collision2D other) {
        animator.SetTrigger("Die");
        rb2d.velocity = Vector2.zero;
        gameStatus = GameStatus.DEAD;  
        GameController.instance.BirdDied();
    }
}

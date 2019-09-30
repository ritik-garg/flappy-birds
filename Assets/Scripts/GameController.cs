using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class GameController : MonoBehaviour
{
    public static GameController instance;
    public Text scoreText;
    public Text highScoreText;
    public GameObject gameOverText;
    public bool gameOver = false;
    public static int highScore = 0;

    public float scrollSpeed = -2f;
    private int score = 0;

    void updateSpeed() {
        scrollSpeed = -Mathf.Max(2f, Mathf.Sqrt(score / 5) * Mathf.Log(score));
        Physics2D.gravity = new Vector2(0, -Mathf.Max(9.81f, 5f * Mathf.Sqrt(-scrollSpeed)));
    }
    // Start is called before the first frame update
    void Awake()
    {
        if(instance == null) {
            instance = this;
        }
        else if(instance != this){
            Destroy(gameObject);
        }
        scrollSpeed = -2f;
        scoreText.text = "Score : " + score.ToString(); 
        highScoreText.text = "High Score : " + highScore.ToString();  
        // highScoreText.text = "High Score : " + Physics2D.gravity.y.ToString();  
    }

    public void BirdScored() {
        if(Bird.gameStatus == Bird.GameStatus.DEAD) {
            return;
        }
        score++;
        updateSpeed();
        if(highScore < score) {
            highScore = score;
            highScoreText.text = "High Score : " + highScore.ToString();  
            // highScoreText.text = "High Score : " + Physics2D.gravity.y.ToString();
        }
        scoreText.text = "Score : " + score.ToString();
    }

    // Update is called once per frame
    void Update()
    {
        // if(gameOver && Input.GetMouseButtonDown(0)) {
        if(Bird.gameStatus == Bird.GameStatus.DEAD && Input.GetMouseButtonDown(0)) {
            SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex);
        }
    }

    public void BirdDied() {
        gameOverText.SetActive(true);
        // gameOver = true;
        Bird.gameStatus = Bird.GameStatus.DEAD;
    }
}

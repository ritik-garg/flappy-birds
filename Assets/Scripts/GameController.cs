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
    public GameObject levelText;
    public static int highScore = 0;
    public float scrollSpeed;

    // private float maxScrollSpeed = -5f;
    // private float acceleration = 0.0005f;
    private int score = 0;
    private int currentLevel;

    // void updateSpeed() {
    //     scrollSpeed = -Mathf.Max(2f, Mathf.Sqrt(score / 5) * Mathf.Log(score));
    //     Physics2D.gravity = new Vector2(0, -Mathf.Max(9.81f, 5f * Mathf.Sqrt(-scrollSpeed)));
    // }
    // Start is called before the first frame update
    void Awake()
    {
        if(instance == null) {
            instance = this;
        }
        else if(instance != this){
            Destroy(gameObject);
        }
        currentLevel = Levels.currentLevel;
        scrollSpeed = Levels.levelData[currentLevel - 1].scrollSpeed;
        highScore = currentLevel == Levels.maxLevels ? highScore : Levels.levelData[currentLevel - 1].maxScore;
        
        scoreText.text = "Score : " + score.ToString();
        if(currentLevel == Levels.maxLevels) {
            highScoreText.text = "High Score : " + highScore.ToString();
        }
        else {
            highScoreText.text = "Max Score : " + highScore.ToString();
        }
        
        // highScoreText.text = "High Score : " + highScore.ToString();  
        // highScoreText.text = "High Score : " + Physics2D.gravity.y.ToString();  
    }

    public void BirdScored() {
        if(Bird.gameStatus == Bird.GameStatus.DEAD || Bird.gameStatus == Bird.GameStatus.UPGRADING) {
            return;
        }

        score++;
        if(currentLevel == Levels.maxLevels) {
            if(highScore < score) {
                highScore = score;
                highScoreText.text = "High Score : " + highScore.ToString();  
            }
        }
        else {
            if(score >= highScore) {
                Levels.UpgradeLevel();
                Bird.gameStatus = Bird.GameStatus.UPGRADING;
            }
        }
        scoreText.text = "Score : " + score.ToString();
    }

    // Update is called once per frame
    void Update()
    {
        // if(gameOver && Input.GetMouseButtonDown(0)) {
        if((Bird.gameStatus == Bird.GameStatus.DEAD && Input.GetMouseButtonDown(0)) || (Bird.gameStatus == Bird.GameStatus.UPGRADING)) {
            SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex);
        }
        // else if(Bird.gameStatus == Bird.GameStatus.PLAYING) {
        //     if(scrollSpeed > maxScrollSpeed) {
        //         scrollSpeed -= acceleration;
        //     }
        //     highScoreText.text = "High Score : " + scrollSpeed.ToString(); 
        // }
    }

    public void BirdDied() {
        levelText.SetActive(false);
        gameOverText.SetActive(true);
        // gameOver = true;
        Bird.gameStatus = Bird.GameStatus.DEAD;
        Levels.UpgradeLevel();
    }
}

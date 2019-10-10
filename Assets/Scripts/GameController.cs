using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class GameController : MonoBehaviour
{
    public static GameController instance;
    public Text scoreText, highScoreText, scorePauseText, highScorePauseText, gameOverText, tapToStartText;
    public GameObject levelText, PauseUI, GameDetailsUI;
    public static int highScore = 0;
    public float scrollSpeed;

    private bool isGamePaused = false;
    private int score = 0, currentLevel;

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

    public void PauseGame() {
        Bird.gameStatus = Bird.GameStatus.PAUSED;
    }
    public void OnMenuButtonClick() {
        GameDetailsUI.SetActive(false);
        scorePauseText.text = "SCORE: " + score.ToString();
        
        if(currentLevel == Levels.maxLevels) {
            highScorePauseText.text = "HIGH SCORE: " + highScore.ToString();
        }
        else {
            highScorePauseText.text = "MAX SCORE: " + highScore.ToString();
        }

        if(Bird.gameStatus == Bird.GameStatus.DEAD) {
            gameOverText.text = "OOPS! BIRD DIED";
            tapToStartText.text = "FLAP TO RESTART";
        }
        else if(Bird.gameStatus == Bird.GameStatus.PAUSED) {
            gameOverText.text = "GAME PAUSED";
            tapToStartText.text = "TAP TO CONTINUE";
        }
        else {
            gameOverText.text = "LEVEL COMPLETED";
            tapToStartText.text = "TAP TO CONTINUE";
        }

        PauseUI.SetActive(true);
        Time.timeScale = 0f;
        isGamePaused = true;
    }

    // Update is called once per frame
    void Update()
    {
        if(Bird.gameStatus == Bird.GameStatus.DEAD || Bird.gameStatus == Bird.GameStatus.UPGRADING || Bird.gameStatus == Bird.GameStatus.PAUSED) {
            OnMenuButtonClick();
            if(Input.GetMouseButtonDown(0)) {
                Time.timeScale = 1f;
                if(Bird.gameStatus != Bird.GameStatus.PAUSED) {
                    SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex);
                }
                else {
                    Bird.gameStatus = Bird.GameStatus.PLAYING;
                    GameDetailsUI.SetActive(true);
                    PauseUI.SetActive(false);
                    isGamePaused = false;
                }
            }
        }
    }

    public void BirdDied() {
        levelText.SetActive(false);
        Bird.gameStatus = Bird.GameStatus.DEAD;
        Levels.UpgradeLevel();
    }
}

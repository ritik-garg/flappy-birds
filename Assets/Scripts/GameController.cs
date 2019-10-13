using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class GameController : MonoBehaviour
{
    public static GameController instance;
    public static int highScore = 0;

    public Text scoreText, highScoreText, scorePauseText, highScorePauseText, gameOverText, tapToStartText;
    public GameObject levelText, PauseUI, GameDetailsUI;
    public float scrollSpeed;

    private int score = 0, currentLevel;
    private float dispalyHeight, displayWidth;

    // Start is called before the first frame update
    void Awake()
    {
        if(instance == null) {
            instance = this;
        }
        else if(instance != this){
            Destroy(gameObject);
        }

        dispalyHeight = 0.85f * Screen.height;
        displayWidth = 0.85f * Screen.width;

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

    // Update is called once per frame
    void Update()
    {
        if(Bird.gameStatus == Bird.GameStatus.DEAD || Bird.gameStatus == Bird.GameStatus.UPGRADING || Bird.gameStatus == Bird.GameStatus.PAUSED) {
            OnMenuButtonClick();
            if(Input.GetMouseButtonDown(0)) {
                if(Input.mousePosition.y < dispalyHeight && Input.mousePosition.x < displayWidth) {
                    if(Bird.gameStatus != Bird.GameStatus.PAUSED) {
                        SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex);
                    }
                    else {
                        GameDetailsUI.SetActive(true);
                        PauseUI.SetActive(false);
                        Bird.gameStatus = Bird.GameStatus.PLAYING;
                    }
                    Time.timeScale = 1f;
                }
            }
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

    private void OnMenuButtonClick() {
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
    }

    public void BirdDied() {
        levelText.SetActive(false);
        Bird.gameStatus = Bird.GameStatus.DEAD;
        Levels.UpgradeLevel();
    }
}

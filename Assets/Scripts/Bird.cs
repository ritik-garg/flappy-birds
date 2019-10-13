using UnityEngine;
using UnityEngine.UI;

public class Bird : MonoBehaviour
{
    public GameObject gameStartText;
    public Text levelText;
    public AudioClip[] birdSounds;
    public Button speakerButton;
    public Sprite[] speakerImages;

    private bool clickedOnce = false;
    private Rigidbody2D rb2d;
    private Animator animator;
    private AudioSource audioSource;
    private float dispalyHeight, displayWidth, upForce = 250f;

    public enum GameStatus {NOTSTARTED, DEAD, PLAYING, UPGRADING, PAUSED};
    public static GameStatus gameStatus;

    // Start is called before the first frame update
    void Start()
    {
        dispalyHeight = 0.85f * Screen.height;
        displayWidth = 0.85f * Screen.width;

        rb2d = GetComponent<Rigidbody2D>();
        animator = GetComponent<Animator>();
        gameStatus = GameStatus.NOTSTARTED;
        Physics2D.gravity = new Vector2(0, 0);
        levelText.text = "LEVEL: " + Levels.currentLevel.ToString();
        audioSource = GetComponent<AudioSource>();
        
        if(!StartGame.isMute) {
            speakerButton.image.sprite = speakerImages[0];
        }
        else {
            speakerButton.image.sprite = speakerImages[1];
        }
    }

    // Update is called once per frame
    void Update()
    {
        if(gameStatus == GameStatus.NOTSTARTED) {
            animator.SetTrigger("Flap");
        }
        if(Input.GetMouseButtonDown(0)) {
            if(Input.mousePosition.y > dispalyHeight && Input.mousePosition.x > displayWidth) {
                if(gameStatus == GameStatus.PAUSED || gameStatus == GameStatus.DEAD || gameStatus == GameStatus.UPGRADING) {
                    OnClickMuteButton();
                }
                else {
                    gameStatus = GameStatus.PAUSED;
                }
            }
            else if(!clickedOnce) {
                gameStatus = GameStatus.PLAYING;
                Physics2D.gravity = new Vector2(0, -9.81f);
                clickedOnce = true;
                gameStartText.SetActive(false);
            }

            if(gameStatus == GameStatus.PLAYING && this.transform.position.y < 5f) {
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

    private void OnClickMuteButton() {
        if(!StartGame.isMute) {
            AudioListener.volume = 0;
            speakerButton.image.sprite = speakerImages[1];
            StartGame.isMute = true;
        }
        else {
            AudioListener.volume = 1;
            speakerButton.image.sprite = speakerImages[0];
            StartGame.isMute = false;
        }
    }
}

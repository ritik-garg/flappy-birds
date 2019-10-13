using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class StartGame : MonoBehaviour
{
    public static bool isMute = false;

    public Button speakerButton;
    public Sprite[] speakerImages;
    
    private float dispalyHeight, displayWidth;
    
    // Start is called before the first frame update
    void Start()
    {
        dispalyHeight = 0.85f * Screen.height;
        displayWidth = 0.85f * Screen.width;
    }

    // Update is called once per frame
    void Update()
    {
        if (Input.GetMouseButtonDown(0)) {
            if(Input.mousePosition.y > dispalyHeight && Input.mousePosition.x > displayWidth) {
                OnClickMuteButton();
            }
            else {
                SceneManager.LoadScene("Main");
            }
        }
    }

    private void OnClickMuteButton() {
        if(!isMute) {
            AudioListener.volume = 0;
            speakerButton.image.sprite = speakerImages[1];
            isMute = true;
        }
        else {
            AudioListener.volume = 1;
            speakerButton.image.sprite = speakerImages[0];
            isMute = false;
        }
    }
}

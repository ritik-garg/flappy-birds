using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class StartGame : MonoBehaviour
{
    public Text startText;
    public Button speakerButton;
    public Sprite[] speakerImages;
    private bool isMute = false;
    // Start is called before the first frame update
    void Start()
    {
        // startText = GetComponent<Text>();
        StartCoroutine(BlinkText());
    }

    public IEnumerator BlinkText() {
        while(true) {
            startText.text = "";
            yield return new WaitForSeconds(0.5f);
            startText.text = "TAP TO START";
            yield return new WaitForSeconds(0.5f);            
        }
    }
    // Update is called once per frame
    void Update()
    {

    }

    public void OnClickMuteButton() {
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

    public void ChangeScene() {
        SceneManager.LoadScene("Main");
    }
}

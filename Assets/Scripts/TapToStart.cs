using System.Collections;
using UnityEngine;
using UnityEngine.UI;

public class TapToStart : MonoBehaviour
{
    public Text startText;
    // Start is called before the first frame update
    void Start()
    {
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
}

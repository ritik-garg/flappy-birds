using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class TapToStart : MonoBehaviour
{
    public Text startText;
    // Start is called before the first frame update
    void Start()
    {
        // startText = GetComponent<Text>();
        StartCoroutine(BlinkText());
    }

    // Update is called once per frame
    void Update()
    {
        
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

using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Column : MonoBehaviour
{
    public AudioClip birdScored;
    private AudioSource audioSource;

    void Start() {
        audioSource = GetComponent<AudioSource>();  
    }
    private void OnTriggerEnter2D(Collider2D other) {
        if(other.GetComponent<Bird>() != null) {
            audioSource.PlayOneShot(birdScored);
            GameController.instance.BirdScored();
        }
    }
}

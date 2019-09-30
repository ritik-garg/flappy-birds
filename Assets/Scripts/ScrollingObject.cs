using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ScrollingObject : MonoBehaviour
{
    private Rigidbody2D rigidBody2D;
    // Start is called before the first frame update
    void Start()
    {
        rigidBody2D = GetComponent<Rigidbody2D>();
        rigidBody2D.velocity = new Vector2(GameController.instance.scrollSpeed, 0);
    }

    // Update is called once per frame
    void Update()
    {
        rigidBody2D.velocity = new Vector2(GameController.instance.scrollSpeed, 0);
        // if(GameController.instance.gameOver) {
        if(Bird.gameStatus == Bird.GameStatus.DEAD) {
            rigidBody2D.velocity = Vector2.zero;
        }
    }
}

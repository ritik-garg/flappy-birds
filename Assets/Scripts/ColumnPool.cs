using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ColumnPool : MonoBehaviour
{
    public int columnPoolSize = 5;
    public GameObject columnPrefab;



    private float spawnRate = 4f;
    private float columnMin = 0.9f;
    private float columnMax = 4.2f;
    private GameObject[] columns;
    private Vector2 objectPoolPosition = new Vector2(-10f, -25f);
    private float timeSinceLastSpawned;
    private float spawnXPosition = 10f;
    private int currentColumn = 0;
    // Start is called before the first frame update
    void Start()
    {
        columns = new GameObject[columnPoolSize];
        for(int i = 0; i < columnPoolSize; i++) {
            columns[i] = (GameObject) Instantiate(columnPrefab, objectPoolPosition, Quaternion.identity);
        }
    }

    void updateSpawnTime() {
        spawnRate = -8f / GameController.instance.scrollSpeed;
    }

    // Update is called once per frame
    void Update()
    {
        timeSinceLastSpawned += Time.deltaTime;
        // if(!GameController.instance.gameOver && timeSinceLastSpawned >= spawnRate) {
        if(Bird.gameStatus == Bird.GameStatus.PLAYING && timeSinceLastSpawned >= spawnRate) {
            timeSinceLastSpawned = 0;
            float spawnYPosition = Random.Range(columnMin, columnMax);
            columns[currentColumn++].transform.position = new Vector2(spawnXPosition, spawnYPosition);
            currentColumn %= columnPoolSize;
            updateSpawnTime();
        }
    }
}

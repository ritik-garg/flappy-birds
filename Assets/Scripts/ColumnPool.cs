using UnityEngine;

public class ColumnPool : MonoBehaviour
{
    public GameObject columnPrefab;

    private float spawnRate = 4f, columnMin = 0.9f, columnMax = 4.2f, timeSinceLastSpawned, spawnXPosition = 10f;
    private GameObject[] columns;
    private Vector2 objectPoolPosition = new Vector2(-10f, -25f);
    private int currentColumn = 0, columnPoolSize = 5;

    // Start is called before the first frame update
    void Start()
    {
        columns = new GameObject[columnPoolSize];
        for(int i = 0; i < columnPoolSize; i++) {
            columns[i] = (GameObject) Instantiate(columnPrefab, objectPoolPosition, Quaternion.identity);
        }
        updateSpawnTime();
    }

    void updateSpawnTime() {
        spawnRate = -8f / GameController.instance.scrollSpeed;
    }

    // Update is called once per frame
    void Update()
    {
        timeSinceLastSpawned += Time.deltaTime;
        if(Bird.gameStatus == Bird.GameStatus.PLAYING && timeSinceLastSpawned >= spawnRate) {
            timeSinceLastSpawned = 0;
            float spawnYPosition = Random.Range(columnMin, columnMax);
            columns[currentColumn++].transform.position = new Vector2(spawnXPosition, spawnYPosition);
            currentColumn %= columnPoolSize;
        }
    }
}

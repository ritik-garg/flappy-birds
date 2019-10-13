using UnityEngine;

public class Levels : MonoBehaviour
{
    public static int currentLevel = 1, maxLevels = 4, backgroundScene = 0;

    public struct LevelData 
    {
        public float scrollSpeed;
        public int maxScore;
    };

    public static LevelData[] levelData;

    // Start is called before the first frame update
    void Start()
    {
        levelData = new LevelData[maxLevels];
        for(int i = 0; i < maxLevels - 1; ++i) {
            levelData[i].scrollSpeed = -(3f + i);
            levelData[i].maxScore = 2;
        }
        levelData[maxLevels - 1].maxScore = int.MaxValue;
        levelData[maxLevels - 1].scrollSpeed = -5f;
    }

    public static void UpgradeLevel() 
    {
        backgroundScene = (backgroundScene + 1) % 2;
        if(Bird.gameStatus != Bird.GameStatus.DEAD) {
            if(currentLevel != maxLevels - 1) {
                currentLevel++;
            }
            else {
                currentLevel = maxLevels;
            }   
        }
    }
}

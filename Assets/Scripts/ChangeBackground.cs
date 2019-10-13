using UnityEngine;

public class ChangeBackground : MonoBehaviour
{
    public Sprite[] background;

    // Start is called before the first frame update
    void Start()
    {
        this.gameObject.transform.GetChild(0).GetChild(0).GetComponentInChildren<SpriteRenderer>().sprite = background[Levels.backgroundScene];
        this.gameObject.transform.GetChild(1).GetChild(0).GetComponentInChildren<SpriteRenderer>().sprite = background[Levels.backgroundScene];
    }
}

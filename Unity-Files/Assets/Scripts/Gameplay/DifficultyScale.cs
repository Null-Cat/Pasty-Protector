using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DifficultyScale : MonoBehaviour
{
    public GameObject time;
    [SerializeField] private int difficultyScaleInSeconds = 30;
    private int nextDifficultyLevel = 2;

    // Start is called before the first frame update
    void Start()
    {

    }

    // Update is called once per frame
    void Update()
    {
        if ((Mathf.FloorToInt(time.GetComponent<UITimer>().time) / difficultyScaleInSeconds) + 1 == nextDifficultyLevel)
        {
            nextDifficultyLevel++;
            this.GetComponent<ObjectCreatorArea>().spawnInterval = this.GetComponent<ObjectCreatorArea>().spawnInterval * 0.5f;
        }
    }
}

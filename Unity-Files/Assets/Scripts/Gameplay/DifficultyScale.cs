using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DifficultyScale : MonoBehaviour
{
    public GameObject time;
    [SerializeField] private int incrementDifficultyInSeconds = 30;
    [SerializeField] private int incrementAmountInSeconds = 25;
    private int currentDifficultyLevel = 0;

    // Start is called before the first frame update
    void Start()
    {

    }

    // Update is called once per frame
    void Update()
    {
        if (((Mathf.FloorToInt(time.GetComponent<UITimer>().time) - (incrementAmountInSeconds * currentDifficultyLevel)) / incrementDifficultyInSeconds) + 1 == 2)
        {
            currentDifficultyLevel++;
            incrementDifficultyInSeconds += incrementAmountInSeconds;
            this.GetComponent<ObjectCreatorArea>().spawnInterval = this.GetComponent<ObjectCreatorArea>().spawnInterval * 0.5f;
        }
    }
}

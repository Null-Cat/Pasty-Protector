using System.Collections;
using System.Collections.Generic;
using UnityEngine;



public class DifficultyScale : MonoBehaviour
{
    public GameObject time;
    public TypeOfDifficulty difficultyType;
    [SerializeField] private int incrementDifficultyInSeconds = 30;
    [SerializeField] private int incrementAmountInSeconds = 25;
    [SerializeField] private float difficultyCap = 0.5f;
    private int currentDifficultyLevel = 0;
    public enum TypeOfDifficulty
    {
        Exponential,
        Constant
    }
    // Update is called once per frame
    void Update()
    {
        if (GetComponent<ObjectCreatorArea>().spawnInterval <= difficultyCap) return;
        switch (difficultyType)
        {
            case TypeOfDifficulty.Exponential:
                if (((Mathf.FloorToInt(time.GetComponent<UITimer>().time) - (incrementAmountInSeconds * currentDifficultyLevel)) / incrementDifficultyInSeconds) == 1)
                {
                    currentDifficultyLevel++;
                    incrementDifficultyInSeconds += incrementAmountInSeconds;
                    this.GetComponent<ObjectCreatorArea>().spawnInterval = this.GetComponent<ObjectCreatorArea>().spawnInterval - 0.5f;
                }
                break;
            case TypeOfDifficulty.Constant:
                if ((Mathf.FloorToInt(time.GetComponent<UITimer>().time) >= (incrementDifficultyInSeconds * (currentDifficultyLevel + 1))))
                {
                    currentDifficultyLevel++;
                    this.GetComponent<ObjectCreatorArea>().spawnInterval = this.GetComponent<ObjectCreatorArea>().spawnInterval - 0.5f;
                }
                break;
        }
    }
}

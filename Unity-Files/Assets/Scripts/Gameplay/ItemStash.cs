using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ItemStash : MonoBehaviour
{
    [Header("Dropped Item Amount")]
    public int droppedItemAmount = 3;

    private void OnTriggerEnter2D(Collider2D other)
    {
        string objectTag = other.gameObject.tag;
        if (objectTag == "Enemy" && droppedItemAmount > 0)
        {
            droppedItemAmount -= 1;
            other.gameObject.GetComponent<EnemyReturn>().hasDroppableObject = true;
        }
    }

    public void AddDroppedItemAmountByOne()
    {
        droppedItemAmount += 1;
    }

    private void Update()
    {

    }
}

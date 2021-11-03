using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ItemStash : MonoBehaviour
{
    [Header("Dropped Item Amount")]
    public int droppedItemAmount = 3;

    private GameObject[] pasties;

    private void OnTriggerEnter2D(Collider2D other)
    {
        string objectTag = other.gameObject.tag;
        if (objectTag == "Enemy" && droppedItemAmount > 0)
        {
            droppedItemAmount -= 1;
            other.gameObject.GetComponent<EnemyReturn>().toggleHasDroppableObject();
        }
    }

    public void AddDroppedItemAmountByOne()
    {
        droppedItemAmount += 1;
    }

    private void Start()
    {
        pasties = GameObject.FindGameObjectsWithTag("Pastie");
    }
    private void Update()
    {
        if (droppedItemAmount == 0 && pasties[0].GetComponent<SpriteRenderer>().enabled == true)
        {
            foreach (GameObject pastie in pasties)
            {
                pastie.GetComponent<SpriteRenderer>().enabled = false;
            }
        }
        else if (droppedItemAmount != 0 && pasties[0].GetComponent<SpriteRenderer>().enabled == false)
        {
            foreach (GameObject pastie in pasties)
            {
                pastie.GetComponent<SpriteRenderer>().enabled = true;
            }
        }
    }
}

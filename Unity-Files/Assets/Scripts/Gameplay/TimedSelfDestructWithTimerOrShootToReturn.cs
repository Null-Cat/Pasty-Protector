using UnityEngine;
using System.Collections;

public class TimedSelfDestructWithTimerOrShootToReturn : MonoBehaviour
{

    // After this time, the object will be destroyed
    public float timeToDestruction;


    void Start()
    {
        //Invoke("DestroyMe", timeToDestruction);
    }

    // Update is called every frame, if the MonoBehaviour is enabled.
    void Update()
    {
        if (timeToDestruction > 0)
        {
            timeToDestruction -= Time.deltaTime;
            GetComponentInChildren<TextMesh>().text = Mathf.FloorToInt(timeToDestruction).ToString();
        }
        else
        {
            DestroyMe();
        }
    }

    // This function will destroy this object :(
    void DestroyMe()
    {
        HealthSystemAttribute healthScript = GameObject.Find("CollisionDetector").gameObject.GetComponent<HealthSystemAttribute>();
        if (healthScript != null)
        {
            // subtract health from the player
            healthScript.ModifyHealth(-1);
        }
        Destroy(gameObject);

        // Bye bye!
    }

    private void OnTriggerEnter2D(Collider2D other)
    {
        string objectTag = other.gameObject.tag;
        if (objectTag == "Bullet")
        {
            GameObject.Find("CollisionDetector").GetComponent<ItemStash>().AddDroppedItemAmountByOne();
            Destroy(other.gameObject);
            Destroy(gameObject);
        }
    }
}

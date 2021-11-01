using UnityEngine;
using System.Collections;

public class TimedSelfDestructWithTimer : MonoBehaviour
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
            GetComponentInChildren<TextMesh>().text = Mathf.Abs(timeToDestruction).ToString();
        }
        else
        {
            DestroyMe();
        }
    }

    // This function will destroy this object :(
    void DestroyMe()
    {
        Destroy(gameObject);

        // Bye bye!
    }
}

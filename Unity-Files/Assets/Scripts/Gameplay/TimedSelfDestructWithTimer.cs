using UnityEngine;
using System.Collections;

public class TimedSelfDestruct : MonoBehaviour
{

	// After this time, the object will be destroyed
	public float timeToDestruction;


	void Start ()
	{
		Invoke("DestroyMe", timeToDestruction);
	}

    // Update is called every frame, if the MonoBehaviour is enabled.
    void Update()
    {
        
    }

	// This function will destroy this object :(
	void DestroyMe()
	{
		Destroy(gameObject);

		// Bye bye!
	}
}

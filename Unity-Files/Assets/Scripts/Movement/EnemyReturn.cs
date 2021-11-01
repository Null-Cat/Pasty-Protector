using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(Rigidbody2D))]
public class EnemyReturn : Physics2DObject
{
    [Header("Movement Speed")]
    [Tooltip("The amount of speed in which the boat moves towards the castle")]
    public float normalSpeed = 5f;
    [Tooltip("The amount of speed in which the boat moves away from the castle")]
    public float returnSpeed = 5f;

    private bool isReturning = false;
    private Vector2 movement = new Vector2(0f, 0f);

    // Start is called before the first frame update
    void Start()
    {

    }

    // Update is called once per frame
    void Update()
    {
        if (isReturning)
        {
            movement = new Vector2(0f, returnSpeed);
        }
        else
        {
            movement = new Vector2(0f, -normalSpeed);
        }
    }

    //FixedUpdate is called every frame when the physics are calculated
    void FixedUpdate()
    {
        rigidbody2D.AddForce(movement);
    }
    void OnTriggerEnter2D(Collider2D otherCollider)
    {
        string playerTag = otherCollider.gameObject.tag;
        if (playerTag == "Player" || playerTag == "Player2")
        {
            HealthSystemAttribute healthScript = otherCollider.gameObject.GetComponent<HealthSystemAttribute>();
            if (healthScript != null)
            {
                // subtract health from the player
                healthScript.ModifyHealth(-1);
            }
            this.GetComponent<SpriteRenderer>().flipY = false;
            isReturning = true;
        }
    }
}

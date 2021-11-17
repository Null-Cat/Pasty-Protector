using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(Rigidbody2D))]
public class AlliedReturn : Physics2DObject
{
    [Header("Movement Speed")]
    [Tooltip("The amount of speed in which the boat moves towards the castle")]
    public float normalSpeed = 5f;
    [Tooltip("The amount of speed in which the boat moves away from the castle")]
    public float returnSpeed = 5f;
    [Header("Death Effect When Shot")]
    public GameObject deathEffect;
    public GameObject[] droppables;


    private bool isReturning = false;
    private bool hasDroppableObject = true;
    private Vector2 movement = new Vector2(0f, 0f);

    // Update is called once per frame
    void Update()
    {
        movement = (isReturning) ? new Vector2(0f, returnSpeed) : new Vector2(0f, -normalSpeed);
    }

    //FixedUpdate is called every frame when the physics are calculated
    void FixedUpdate()
    {
        rigidbody2D.AddForce(movement);
    }
    void OnTriggerEnter2D(Collider2D otherCollider)
    {
        string playerTag = otherCollider.gameObject.tag;
        if (otherCollider.gameObject.name == "CollisionDetectorAlly")
        {
            this.GetComponent<SpriteRenderer>().flipY = false;
            isReturning = true;
            dropObject();
        }
        if (playerTag == "Bullet")
        {
            DestroyShip();
        }
        else if (playerTag == "Finish" && isReturning)
        {
            Destroy(gameObject);
        }
    }
    public void DestroyShip(bool triggerDeathEffect = true)
    {
        if (deathEffect != null && triggerDeathEffect)
        {
            GameObject newDeathEffect = Instantiate<GameObject>(deathEffect);
            newDeathEffect.transform.position = this.transform.position;
        }
        Destroy(gameObject);
    }

    private void dropObject()
    {
        if (hasDroppableObject)
        {
            hasDroppableObject = false;
            Instantiate<GameObject>(droppables[Random.Range(0, droppables.Length)]).transform.position = this.transform.position;
        }
    }
}
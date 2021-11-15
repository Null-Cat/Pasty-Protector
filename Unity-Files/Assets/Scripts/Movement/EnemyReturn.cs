using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AddressableAssets;

[RequireComponent(typeof(Rigidbody2D))]
public class EnemyReturn : Physics2DObject
{
    [Header("Movement Speed")]
    [Tooltip("The amount of speed in which the boat moves towards the castle")]
    public float normalSpeed = 5f;
    [Tooltip("The amount of speed in which the boat moves away from the castle")]
    public float returnSpeed = 5f;
    [Header("Dropped Object")]
    public GameObject droppedObject;
    [Header("Death Effect When Shot")]
    public GameObject deathEffect;
    //[Header("Has Droppable Object")]
    public Sprite shipWithPasties;
    private bool hasDroppableObject = false;
    private bool isReturning = false;
    private Vector2 movement = new Vector2(0f, 0f);

    // Start is called before the first frame update
    void Start()
    {
        //Addressables.LoadAssetAsync<Sprite>("pastieShip").Completed += handle => { shipWithPasties = handle.Result; };
    }

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
        if (playerTag == "Player" || playerTag == "Player2")
        {
            this.GetComponent<SpriteRenderer>().flipY = false;
            isReturning = true;
        }
        if (playerTag == "Bullet")
        {
            DestroyShip();
        }
        if (playerTag == "Finish" && hasDroppableObject)
        {
            HealthSystemAttribute healthScript = GameObject.Find("CollisionDetector").gameObject.GetComponent<HealthSystemAttribute>();
            if (healthScript != null)
            {
                // subtract health from the player
                healthScript.ModifyHealth(-1);
            }
            Destroy(gameObject);
        }
        else if (playerTag == "Finish" && isReturning)
        {
            Destroy(gameObject);
        }
    }
    public void toggleHasDroppableObject()
    {
        hasDroppableObject = (hasDroppableObject) ? false : true;
        if (hasDroppableObject)
        {
            this.GetComponent<SpriteRenderer>().sprite = shipWithPasties;
        }
    }
    public void DestroyShip(bool triggerDeathEffect = true)
    {
        if (deathEffect != null && triggerDeathEffect)
        {
            GameObject newDeathEffect = Instantiate<GameObject>(deathEffect);
            newDeathEffect.transform.position = this.transform.position;
        }
        if (hasDroppableObject)
        {
            GameObject newDroppedObject = Instantiate<GameObject>(droppedObject);
            newDroppedObject.transform.position = new Vector2(this.transform.position.x, this.transform.position.y);
        }
        Destroy(gameObject);
    }
}

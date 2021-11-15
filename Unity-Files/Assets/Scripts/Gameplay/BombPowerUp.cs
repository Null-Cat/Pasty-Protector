using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BombPowerUp : MonoBehaviour
{
    public float explosionRange;
    public GameObject explosionEffect;

    // Start is called before the first frame update
    void Start()
    {

    }

    // Update is called once per frame
    void Update()
    {
        
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.CompareTag("Bullet"))
        {
            Explode();
        }
    }

    void Explode()
    {
        Collider2D[] colliders = Physics2D.OverlapCircleAll(gameObject.transform.position, explosionRange);

        if(colliders.Length > 0)
        {
            foreach (Collider2D boats in colliders)
            {
                if (boats.CompareTag("Enemy"))
                {
                    boats.GetComponent<EnemyReturn>().DestroyShip();
                }
            }
        }
        Destroy(gameObject);
    }
}

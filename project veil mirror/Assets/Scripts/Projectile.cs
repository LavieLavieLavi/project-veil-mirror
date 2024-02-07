using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Projectile : MonoBehaviour
{
    [SerializeField] private float projectileSpeed = 10f;
    [SerializeField] private int projectileDamage = 25;
    public Rigidbody2D rb;
    public GameObject impactEffect;

    // Start is called before the first frame update
    void Start()
    {
        rb.velocity = transform.right * projectileSpeed;
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject.tag == "Enemy")
        {
            collision.gameObject.GetComponent<EnemiesHealth>().enemyTakeDamage(projectileDamage);
        }

        GameObject cloneimpact = Instantiate(impactEffect, transform.position, transform.rotation);
        Destroy(gameObject); 
        Destroy(cloneimpact,.5f);
       // Debug.Log(collision.name);

        
    }
    
    //TODO::Add player damage too


}

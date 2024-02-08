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
        if(collision.gameObject.tag == "Ground")
        {
            GameObject cloneimpact = Instantiate(impactEffect, transform.position, transform.rotation);
            Destroy(gameObject);
            Destroy(cloneimpact, .5f);
            Debug.Log(collision.name);

        }
  

        //For vengeful
        if (collision.gameObject.tag == "Enemy")
        {
            GameObject cloneimpact = Instantiate(impactEffect, transform.position, transform.rotation);
            collision.gameObject.GetComponent<EnemiesHealth>().enemyTakeDamage(projectileDamage);
            Destroy(gameObject);
            Destroy(cloneimpact, .5f);
        }

        //For vengeful
        if (collision.gameObject.tag == "VengefulProjectile")
        {
            GameObject cloneimpact = Instantiate(impactEffect, transform.position, transform.rotation);
            Destroy(gameObject);
            Destroy(cloneimpact, .5f);
        }


        if (collision.gameObject.tag == "PossessiveEnemy")
        {
            GameObject cloneimpact = Instantiate(impactEffect, transform.position, transform.rotation);
            collision.gameObject.GetComponent<PossessiveHealth>().TakeDamage(projectileDamage);
            Destroy(gameObject);
            Destroy(cloneimpact, .5f);
        }


        if (collision.gameObject.tag == "MalevolentEnemy")
        {
            GameObject cloneimpact = Instantiate(impactEffect, transform.position, transform.rotation);
            collision.gameObject.GetComponent<MalevolentHealth>().TakeDamage(projectileDamage);
            Destroy(gameObject);
            Destroy(cloneimpact, .5f);
        }


    }
    
    //TODO::Add player damage too


}

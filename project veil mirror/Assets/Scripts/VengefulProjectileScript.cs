using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class VengefulProjectileScript : MonoBehaviour
{
    private GameObject player;
    private Rigidbody2D rb;
    public float speedForce;

    public GameObject vengeimpactEffect;


    // Start is called before the first frame update
    void Start()
    {
        player = GameObject.FindGameObjectWithTag("Player");
        rb = GetComponent<Rigidbody2D>();   

        Vector3 direction = player.transform.position - transform.position; // get the distance between two points
        rb.velocity = new Vector2(direction.x,direction.y).normalized * speedForce;
                       //normalize the  distance
        float rotation = Mathf.Atan2(-direction.y,-direction.x)*Mathf.Rad2Deg;
        transform.rotation = Quaternion.Euler(0, 0, rotation);


    }

    //Player taking damage
    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject.tag == "Player")
        {
            collision.gameObject.GetComponent<HarperHealth>().harperHurt(25);
            GameObject cloneimpact = Instantiate(vengeimpactEffect, transform.position, transform.rotation);
            Destroy(gameObject);
            Destroy(cloneimpact, .5f);
        }

        if (collision.gameObject.tag == "HarperProjectile")
        {
            GameObject cloneimpact = Instantiate(vengeimpactEffect, transform.position, transform.rotation);
            Destroy(gameObject);
            Destroy(cloneimpact, .5f);
        }

    }
}

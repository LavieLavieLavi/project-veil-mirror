using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MalevolentProjectile : MonoBehaviour
{
    [SerializeField] private float speed;
    [SerializeField] private float resetTime;
    private float lifetime;
    private Animator anim;
    private BoxCollider2D coll;

    private bool hit;

    public GameObject posImpactEffect;

    private void Awake()
    {
        anim = GetComponent<Animator>();
        coll = GetComponent<BoxCollider2D>();
    }

    public void ActivateProjectile()
    {
        hit = false;
        lifetime = 0;
        gameObject.SetActive(true);
        coll.enabled = true;
    }
    private void Update()
    {
        if (hit) return;
        float movementSpeed = speed * Time.deltaTime;
        transform.Translate(movementSpeed, 0, 0);

        lifetime += Time.deltaTime;
        if (lifetime > resetTime)
            gameObject.SetActive(false);
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject.tag == "Player")
        {
            collision.gameObject.GetComponent<HarperHealth>().harperHurt(25);
            GameObject cloneimpact = Instantiate(posImpactEffect, transform.position, transform.rotation);
            Destroy(cloneimpact, .5f);
            Deactivate();
        }


    }


    private void Deactivate()
    {
        gameObject.SetActive(false);
    }
}

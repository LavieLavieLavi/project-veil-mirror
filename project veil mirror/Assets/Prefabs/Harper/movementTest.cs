using System.Collections;
using System.Collections.Generic;
using System.Globalization;
using UnityEngine;

public class movementTest : MonoBehaviour
{
    private Rigidbody2D rb;
    private SpriteRenderer sprite;
    private Animator anim;

    private float dirX = 0f;
    [SerializeField] private float moveSpeed = 3f;
    [SerializeField] private float jumpForce = 3f;


    // Start is called before the first frame update
    void Start()
    {
        rb = GetComponent<Rigidbody2D>();
        sprite = GetComponent<SpriteRenderer>();
        anim = GetComponent<Animator>();
    }

    // Update is called once per frame
    void Update()
    {
    

        dirX = Input.GetAxisRaw("Horizontal");
        rb.velocity = new Vector2(dirX * moveSpeed, rb.velocity.y);

        // Jump Anim
        if (Input.GetKeyDown(KeyCode.Space))
        {
            rb.velocity = new Vector2(rb.velocity.x, jumpForce);
            anim.SetBool("Jump", true);
            Debug.Log("Space pressed.");
        }
        if (Input.GetKeyUp(KeyCode.Space))
        {
            anim.SetBool("Jump", false);
        }

        UpdateAnimationUpdate();

        // Hurt Anim
        if (Input.GetKeyDown(KeyCode.H))
        {
            anim.SetBool("Hurt", true);
            Debug.Log("H pressed.");
        }
        if (Input.GetKeyUp(KeyCode.H))
        {
            anim.SetBool("Hurt", false);
        }

        // Death Anim
        if (Input.GetKeyDown(KeyCode.J))
        {
            anim.SetBool("Dead", true);
            Debug.Log("Death!");
        }
        if (Input.GetKeyUp(KeyCode.J))
        {
            anim.SetBool("Dead", false);
        }

        // Attack anim
        if (Input.GetMouseButtonDown(0))
        {
            anim.SetBool("Attacking", true);
            Debug.Log("Left mouse pressed.");
        }

        if (Input.GetMouseButtonUp(0))
        {
            anim.SetBool("Attacking", false);
        }
    }

    private void UpdateAnimationUpdate()
    {
        //if (!IsOwner) return;

        // Movement Anim
        if (dirX > 0f)
        {
            anim.SetBool("Moving", true);
            sprite.flipX = false;
        }
        else if (dirX < 0f)
        {
            anim.SetBool("Moving", true);
            sprite.flipX = true;
        }
        else anim.SetBool("Moving", false);
    }
}

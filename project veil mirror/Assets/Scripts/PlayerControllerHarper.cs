using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Rendering;
using UnityEngine.XR;


public class PlayerControllerHarper : MonoBehaviour
{

    //Components
    private Rigidbody2D rb;
    SpriteRenderer sr;

    [Header("Movement Settings")]
    [SerializeField] private float movementSpeed = 5f;
    float moveInput;
    private Vector2 move;


    [Header("Jump Settings")]
    public Transform feetPos;
    public float checkRadius;
    [SerializeField]private bool isGrounded;
    public LayerMask groundLayer;
    [SerializeField] private float jumpForce = 5f;
    private float jumpTimeCounter;
    public float jumpTime;
    bool isJumping;


    //Shooting settings
    public GameObject projectilePrefab;
    public Transform firePoint;

 



    // Start is called before the first frame update
    void Start()
    {
        rb = GetComponent<Rigidbody2D>();
        sr = GetComponent<SpriteRenderer>();
    }


    // Update is called once per frame
    void Update()
    {
        isGrounded = Physics2D.OverlapCircle(feetPos.position, checkRadius, groundLayer);
        moveInput = Input.GetAxisRaw("Horizontal");

        //TODO:: Change to flip sprite renderer
        if (moveInput > 0)
        {
            transform.eulerAngles =  new Vector3(0, 0, 0);

    
        }else if (moveInput < 0)
        {
           transform.eulerAngles = new Vector3(0, 180, 0);
        }

        #region Jump
        if (isGrounded == true && Input.GetKeyDown(KeyCode.Space))
        {
            isJumping = true;
            jumpTimeCounter = jumpTime;
            rb.velocity = Vector2.up * jumpForce;
        }

        if (Input.GetKey(KeyCode.Space) && isJumping == true){
           if (jumpTimeCounter > 0 ) {
                rb.velocity = Vector2.up * jumpForce;
                jumpTimeCounter -= Time.deltaTime;
            }
            else
            {
                isJumping = false;
            }
            
        }

        if (Input.GetKeyUp(KeyCode.Space))
        {
            isJumping = false;
        }

        #endregion

        #region  Attack

        if (Input.GetKeyDown(KeyCode.Mouse0))
        {
            Debug.Log("Lmb clicked");
            Shoot();
        }
        #endregion




    }

    void FixedUpdate()
    {
        //Movement Logic
        rb.velocity = new Vector2(moveInput * movementSpeed, rb.velocity.y);

    }

    private void OnDrawGizmos()
    {
        Gizmos.color = Color.yellow;
        Gizmos.DrawWireSphere(feetPos.position, checkRadius);
    }


    void Shoot()
    {
        //Shooting logic
        Instantiate(projectilePrefab, firePoint.position, firePoint.rotation);

    }
  
}


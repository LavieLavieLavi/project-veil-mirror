using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Rendering;
using UnityEngine.XR;
using UnityEngine.Networking;
using System.Threading;

public class MovementPrototyping : MonoBehaviour
{

    //Components
    private Rigidbody2D rb;
    private Animator anim;
    public HarperHealth healthSystem;

    [Header("Movement Settings")]
    [SerializeField] private float movementSpeed = 5f;
    float moveInput;
    private Vector2 move;


    [Header("Jump Settings")]
    public Transform feetPos;
    public float checkRadius;
    [SerializeField] private bool isGrounded;
    public LayerMask groundLayer;
    [SerializeField] private float jumpForce = 5f;
    private float jumpTimeCounter;
    public float jumpTime;
    bool isJumping;


    [Header("Shooting Settings")]
    public GameObject projectilePrefab;
    public Transform firePoint;
    private float attackRate;
    public bool isAttacking;
    private Coroutine attackCoroutine;


    //Animation related variables
    private string currentState;
    const string HARPER_IDLE = "Harper Idle";
    const string HARPER_MOVING = "Harper Moving";
    const string HARPER_JUMP = "Harper Jump";
    const string HARPER_ATK = "Harper Attacking";
    const string HARPER_HURT = "Harper Hurt";
    const string HARPER_DEATH = "Harper Death";


    [Header("Hurt and Death Settings")]
    public bool isHurt;






    // Start is called before the first frame update
    void Start()
    {
        rb = GetComponent<Rigidbody2D>();
        anim = GetComponent<Animator>();
    }

    // Update is called once per frame
    void Update()
    {
        if (isHurt)
            return;


        isGrounded = Physics2D.OverlapCircle(feetPos.position, checkRadius, groundLayer);
        moveInput = Input.GetAxisRaw("Horizontal");
        attackRate += Time.deltaTime; //Getting the time passed by



        //movement anim
        if (isGrounded && isAttacking == false) {
            if (moveInput != 0)
            {
                ChangeAnimationState(HARPER_MOVING);
            }
            else
            {
                ChangeAnimationState(HARPER_IDLE);
            }
        }

        //Jumping animation
        if (isJumping && isGrounded == false && isAttacking == false )
        {
            Debug.Log("jump should play");
            ChangeAnimationState(HARPER_JUMP);
        }

        //ATK animation


        //Flipping the sprite
        if (moveInput > 0)
        {
            transform.eulerAngles = new Vector3(0, 0, 0);
        }
        else if (moveInput < 0)
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

        if (Input.GetKey(KeyCode.Space) && isJumping == true)
        {
            if (jumpTimeCounter > 0)
            {
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
        // Attack anim
        if (isHurt == false)
        {
            if (Input.GetMouseButtonDown(0))
            {
                isAttacking = true;
                if (attackRate > 1) //if time passed by is greater than 1 then proceed to the condition
                {
                    attackRate = 0; //reset the condition.
                    Debug.Log("Left mouse pressed.");
                    ChangeAnimationState(HARPER_ATK);
                    // Start the coroutine to handle attack logic
                    StartCoroutine(AttackCoroutine());
                }
            }
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

  /*  void CmdShoot()
    {
        // Call ShootLogic after 1 second
        Invoke("ShootLogic", 0.2f);
    }*/

    IEnumerator AttackCoroutine()
    {
        // Wait for a short duration to ensure the attack animation has started
        yield return new WaitForSeconds(0.2f); // Adjust the duration as needed

        // Shooting logic
        GameObject Projectile = Instantiate(projectilePrefab, firePoint.position, firePoint.rotation);

        // Wait for the attack animation to finish
        yield return new WaitForSeconds(anim.GetCurrentAnimatorStateInfo(0).length);

        // Set isAttacking back to false
        isAttacking = false;
    }

    /*  void ShootLogic()
      {
          // Shooting logic
          GameObject Projectile = (GameObject)Instantiate(projectilePrefab, firePoint.position, firePoint.rotation);    
          isAttacking = false;
      }*/


    //Hurt functions
    public void SetHurt()
    {
        isHurt = true;
        ChangeAnimationState(HARPER_HURT);
        // After some time, set isHurt back to false
        StartCoroutine(EndHurtAnimation());
    }

    // Coroutine to end the hurt animation after a delay
    IEnumerator EndHurtAnimation()
    {
        yield return new WaitForSeconds(.3f);
        isHurt = false;
    }


    void ChangeAnimationState(string newState)
    {
        if (currentState == newState) return;

        //play animation
        anim.Play(newState); 

        currentState = newState;    
    }

}



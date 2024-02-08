using System.Collections;
using UnityEngine;
using Mirror;

public class MovementPrototyping : NetworkBehaviour
{
    // Components
    private Rigidbody2D rb;
    private Animator anim;

    [Header("Movement Settings")]
    [SerializeField] private float movementSpeed = 5f;
    private float moveInput;

    [Header("Jump Settings")]
    public Transform feetPos;
    public float checkRadius;
    [SerializeField] private bool isGrounded;
    public LayerMask groundLayer;
    [SerializeField] private float jumpForce = 5f;
    private float jumpTimeCounter;
    public float jumpTime;
    private bool isJumping;

    [Header("Shooting Settings")]
    public GameObject projectilePrefab;
    public Transform firePoint;
    private float attackRate;
    public bool isAttacking;

    // Animation related variables
    private string currentState;
    const string HARPER_IDLE = "Harper Idle";
    const string HARPER_MOVING = "Harper Moving";
    const string HARPER_JUMP = "Harper Jump";
    const string HARPER_ATK = "Harper Attacking";
    const string HARPER_HURT = "Harper Hurt";

    [Header("Hurt and Death Settings")]
    public bool isHurt;

    void Start()
    {
        rb = GetComponent<Rigidbody2D>();
        anim = GetComponent<Animator>();
    }

    void Update()
    {
        if (!isLocalPlayer)
            return;

        if (isHurt)
            return;

        isGrounded = Physics2D.OverlapCircle(feetPos.position, checkRadius, groundLayer);
        moveInput = Input.GetAxisRaw("Horizontal");
        attackRate += Time.deltaTime;

        // Handle movement animations
        if (isGrounded && !isAttacking)
        {
            if (moveInput != 0)
                ChangeAnimationState(HARPER_MOVING);
            else
                ChangeAnimationState(HARPER_IDLE);
        }

        // Handle jumping animation
        if (isJumping && !isGrounded && !isAttacking)
            ChangeAnimationState(HARPER_JUMP);

        // Flip the sprite
        if (moveInput > 0)
            transform.eulerAngles = new Vector3(0, 0, 0);
        else if (moveInput < 0)
            transform.eulerAngles = new Vector3(0, 180, 0);

        // Handle Jump
        if (isGrounded && Input.GetKeyDown(KeyCode.Space))
        {
            isJumping = true;
            jumpTimeCounter = jumpTime;
            rb.velocity = Vector2.up * jumpForce;
        }

        if (Input.GetKey(KeyCode.Space) && isJumping)
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
            isJumping = false;

        // Handle Attack
        if (!isHurt && Input.GetMouseButtonDown(0))
        {
            isAttacking = true;
            if (attackRate > 0.5)
            {
                attackRate = 0;
                ChangeAnimationState(HARPER_ATK);
                CmdShoot();
            }
        }
    }

    void FixedUpdate()
    {
        // Handle movement
        rb.velocity = new Vector2(moveInput * movementSpeed, rb.velocity.y);
    }

    private void OnDrawGizmos()
    {
        Gizmos.color = Color.yellow;
        Gizmos.DrawWireSphere(feetPos.position, checkRadius);
    }

    [Command]
    void CmdShoot()
    {
        GameObject projectile = Instantiate(projectilePrefab, firePoint.position, firePoint.rotation);
        NetworkServer.Spawn(projectile);
        RpcPlayShootAnimation();
    }

    [ClientRpc]
    void RpcPlayShootAnimation()
    {
        ChangeAnimationState(HARPER_ATK);
        StartCoroutine(SetAttackingFalseDelayed());
    }

    IEnumerator SetAttackingFalseDelayed()
    {
        yield return new WaitForSeconds(anim.GetCurrentAnimatorStateInfo(0).length);
        isAttacking = false;
    }

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
        if (currentState == newState)
            return;

        anim.Play(newState);
        currentState = newState;
    }
}

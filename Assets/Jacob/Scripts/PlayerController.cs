using Guymon.DesignPatterns;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

public class PlayerController : Singleton<PlayerController>
{
    private Rigidbody2D rb;
    private NearbySlashable ns;

    public Transform groundCheck;
    public LayerMask groundLayer;

    [SerializeField] private float mag = 0;

    [SerializeField] private float gravity = 3.5f;

    [SerializeField] Animator animator;

    [SerializeField] private float thresholdDemand = 0.98f;
    [SerializeField] private float speedThreshold = 12.0f;
    [SerializeField] private float stopThreshold = 1;
    [SerializeField] private float baseMoveSpeed = 8.0f;
    [SerializeField] private float moveSpeed = 8.0f;
    [SerializeField] private float moveSpeedIncrease = 2.0f;
    [SerializeField] private float maxCatchDistance = 5.0f;
    [SerializeField] private AnimationCurve catchMoveSpeedIncrease;
    [SerializeField] private float momentumTime = 3.0f;
    [SerializeField] private float catchMomentumTime = 1.5f;
    [SerializeField] private float catchMoveSpeedMultiplier = 5;
    [SerializeField] private float jumpPower = 10.0f;
    [SerializeField] private float baseSlashPower = 10.0f;
    private float slashPower = 10.0f;
    [SerializeField] private float baseSlashLength = 1.2f;
    private float slashLength = 1.2f;
    [SerializeField, Range(0.1f,0.99f)] private float verticalSlashDamping = .5f;
    [SerializeField, Range(0.1f, 0.99f)] private float horizontalSlashDamping = .5f;

    [SerializeField] private SwordController swordController;
    [SerializeField] private float jumpControlTime = 1f;
    [SerializeField] private float coyoteTime = 0.1f;
    private float coyoteTimer = 0f;
    private float jumpControlTimer;

    private bool isJumping = false;
    private bool isSlashing = false;
    [SerializeField] public bool isHoldingSword { get; private set; }
    
    private bool isFacingRight = true;
    private Vector2 moveInput = Vector2.zero;

    private float targetSpeed;
    private float speedDifference;
    private float accelerationRate;
    private float movement;

    [SerializeField] private AudioClip slashClip;

    private void Start()
    {
        isHoldingSword = true;
        slashPower = baseSlashPower;
        slashLength = baseSlashLength;
        moveSpeed = baseMoveSpeed;
        rb = GetComponent<Rigidbody2D>();
        ns = GetComponent<NearbySlashable>();
        rb.gravityScale = gravity;
    }

    private void FixedUpdate()
    {
        if (isSlashing) return;

        targetSpeed = moveInput.x * moveSpeed;
        //gets the difference between current velocity and wanted velocity
        speedDifference = targetSpeed - rb.velocity.x;
        accelerationRate = (Mathf.Abs(targetSpeed) > 0.01f) ? 2.8f : 3f;
        movement = Mathf.Pow(Mathf.Abs(speedDifference) * accelerationRate, 1.21f) * Mathf.Sign(speedDifference);
        rb.AddForce(movement * Vector2.right);

        if (moveInput.y < 0)
        {
            rb.AddForce(2 * moveSpeed * -Vector2.up, ForceMode2D.Force);
        }

        if (isJumping)
        {
            if(jumpControlTimer > 0f)
            {
                rb.AddForce(Vector2.up * jumpPower, ForceMode2D.Force);
                jumpControlTimer -= Time.deltaTime;
            }
            else
            {
                isJumping = false;
            }
        }
    }

    private void Update()
    {
        timeFromThrow += Time.deltaTime;
        mag = rb.velocity.magnitude;
        animator.SetFloat("MoveSpeed", mag);

        if ((!isFacingRight && moveInput.x > 0f) || (isFacingRight && moveInput.x < 0f))
        {
            FlipX();
        }

        if (rb.velocity.magnitude > speedThreshold && !isSlashing && !isJumping)
        {
            rb.velocity *= thresholdDemand;
        }

        if (moveInput.x == 0 && Mathf.Abs(rb.velocity.x) < stopThreshold)
        {
            rb.velocity = new Vector2(0, rb.velocity.y);
        }

        if(coyoteTimer > 0)
        {
            coyoteTimer -= Time.deltaTime;
        }

        #region Throwing

        if (Input.GetKeyDown(KeyCode.L))
        {
            if (isHoldingSword)
            {
                swordController.Throw((isFacingRight) ? Vector2.right : Vector2.left, transform.position, isFacingRight);
                isHoldingSword = false;
                timeFromThrow = 0;
            }
            
            
        }

        if (Input.GetKeyUp(KeyCode.L))
        {
            if (timeFromThrow > 0.5f) //min pull back time
            {  
                float distanceCatch = swordController.Catch(transform);
                float normalizedDistance = distanceCatch / maxCatchDistance;
                float eval = catchMoveSpeedIncrease.Evaluate(normalizedDistance);
                //StartCoroutine(MomentumBuild(catchMoveSpeedIncrease.Evaluate(normalizedDistance) * catchMoveSpeedMultiplier, catchMomentumTime));
                rb.AddForce(rb.velocity.normalized * eval * catchMoveSpeedMultiplier, ForceMode2D.Impulse);

                isHoldingSword = true;
            }
        }

        if (Input.GetKey(KeyCode.L))
        {
            if (!isHoldingSword && timeFromThrow > 0.5f && swordController.sword.currentState != swordController.sword.hovering) //min pull back time
            {
                swordController.ReelIn();
            }
        }
        

        #endregion
        
    }

    private float timeFromThrow;
    
    private bool IsGrounded()
    {
        return Physics2D.OverlapCircle(groundCheck.position, 0.2f, groundLayer);
    }

    private void FlipX()
    {
        isFacingRight = !isFacingRight;
        Vector3 localScale = transform.localScale;
        localScale.x *= -1;
        transform.localScale = localScale;
    }

    #region input actions

    private void OnMove(InputValue inputValue)
    {
        moveInput = inputValue.Get<Vector2>();
    }

    public int jumpCount = 0;
    private void OnJump(InputValue inputValue)
    {
        if (inputValue.isPressed && (IsGrounded() || (coyoteTimer > 0 && jumpCount < 1)))
        {
            jumpCount++;
            isJumping = true;
            jumpControlTimer = jumpControlTime;
            rb.AddForce(new Vector2(0, jumpPower * 0.5f), ForceMode2D.Impulse);
        }

        if (!inputValue.isPressed)
        {
            isJumping = false;
        }
    }

    private bool CanSlash => !isSlashing && isHoldingSword;
    private void OnSlash(InputValue inputValue)
    {
        if (ns.GetClosestSlashable() != null && CanSlash)
        {
            rb.velocity = Vector2.zero;


            Slashable slashable = ns.GetClosestSlashable();

            slashable.OnSlashThrough(1);

            isSlashing = true;
            rb.gravityScale = 0;

            Vector3 slashDir = slashable.transform.position - transform.position;
            slashDir.Normalize();
            

            AudioManager.Instance.PlayOneShotOnSlashDir(slashClip, slashDir.y);

            float distance = Vector3.Distance(transform.position, slashable.transform.position);

            slashPower = baseSlashPower * distance;
            if (slashPower < baseSlashPower) slashPower = baseSlashPower;

            rb.AddForce(slashDir * slashPower, ForceMode2D.Impulse);

            slashLength = 1.7f * distance / rb.velocity.magnitude;

            if (slashLength < baseSlashLength) slashLength = baseSlashLength;

            swordController.Slash(transform, slashDir, slashLength * 0.25f);
            StartCoroutine(FinishSlash(slashDir));
        }
    }

    
    #endregion

    private IEnumerator FinishSlash(Vector3 slashDir)
    {
        yield return new WaitForSeconds(slashLength);
        
        isSlashing = false;
        rb.gravityScale = gravity;

        StartCoroutine(MomentumBuild(moveSpeedIncrease, momentumTime));

        //if (slashDir.y > 0)
        //{
            rb.AddForce(new Vector3(-slashDir.x * (slashPower * horizontalSlashDamping), -slashDir.y * (slashPower * verticalSlashDamping)), ForceMode2D.Impulse);
        //}
        //else
        //{
        //    rb.AddForce(new Vector3(-slashDir.x, 0, 0) * (slashPower * .5f * verticalSlashDamping), ForceMode2D.Impulse);
        //}
    }

    private IEnumerator MomentumBuild(float increase, float time)
    {
        moveSpeed += increase;
        yield return new WaitForSeconds(time);
        moveSpeed -= increase;
    }

    private void OnCollisionExit2D(Collision2D collision)
    {
        if(!IsGrounded())
        {
            coyoteTimer = coyoteTime;
        }
    }

    private void OnCollisionEnter2D(Collision2D collision)
    {
        if(IsGrounded())
        {
            jumpCount = 0;
        }
    }
}

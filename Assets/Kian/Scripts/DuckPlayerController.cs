using Cinemachine;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DuckPlayerController : MonoBehaviour
{
    public static DuckPlayerController instance;
    private float onGroundTimer;
    private float oGravity;

    public Animator animator;

    #region Movement/acceleration/physics
    [Header("MOVEMENT")]
    public bool canMove = true;
    [SerializeField, Range(0f, 30f)] private float maxMovementSpeed = 5;
    [SerializeField, Range(0f, 5f)] private float minWalkSpeed = 2;
    [SerializeField, Range(0f, 20f)] public float oMoveSpeed = 5;
    [SerializeField, Range(0f, 10f)] private float walkAcceleration = 5;
    [SerializeField, Range(0f, 5f)] private float velocityPower = 2f;
    private float targetSpeed;
    private float speedDifference;
    private float accelerationRate;
    private float movement;
    [HideInInspector] public float nMoveSpeed;
    [HideInInspector] public Rigidbody2D theRB;
    [HideInInspector] public Vector2 moveDir;
    [HideInInspector] public bool facingRight = true;

    public bool onGround;
    #endregion

    #region deceleration
    [Header("Deceleration")]
    [SerializeField, Range(0f, 10f)] private float oWalkDeceleration = 5;
    [SerializeField, Range(0f, 10f)] private float maxDeceleration = 5;
    [SerializeField, Range(0f, 10f)] private float minDeceleration = 1;
    [SerializeField, Range(0f, 5f)] private float jumpDecelerationIncrease = 1;
    [SerializeField, Range(0f, 5f)] private float decelerationDiminish = 1;
    [SerializeField] private float forcedDecelerationRaiseTimer;
    private float nWalkDeceleration;
    #endregion

    public Transform groundCheck;
    public LayerMask groundLayer;

    #region Camera variables
    [Header("CAMERA")]
    private Camera mainCam;
    #endregion

    #region Colliders
    [Header("COLLIDERS")]
    private BoxCollider2D boxCol;
    #endregion

    #region Jumping variables
    [Header("JUMPING")]
    [SerializeField, Range(0f, 20f)] private float oJumpForce = 5;
    private float nJumpForce;
    [SerializeField, Range(0f, 2f)] private float jumpTime = 1;
    [SerializeField, Range(0f, 2f)] private float jumpLeeway = 1;
    private float leewayCounter;
    private bool doLeewayJump = false;
    [SerializeField, Range(0f, 2f)] private float gCheckDist = 5;
    [SerializeField, Range(0f, 5f)] private float postJumpGravity = 0;
    [Range(0f, 40f)] public float maxFallSpeed;
    public int jumpsAvailable = 1;
    private int maxJumps;
    [SerializeField] private LayerMask groundMask;

    private RaycastHit2D groundHit;
    private float jumpCounter;
    private bool isJumping;
    private bool isRising; //use for animations
    public bool isFalling; //use for animations
    #endregion

    #region Dashing variables

    //[Header("DASHING")]
    //public float dashRange;
    //public LayerMask dLayerMask;
    //public RaycastHit2D dashCheck;
    //[SerializeField, Range(0f, 20f)] private float oDashForce = 5;
    //[SerializeField, Range(0f, 5f)] private float powerDashSpeedMultiplier = 1;
    //[SerializeField, Range(0f, 1f)] private float powerDashSpeedDiminish = 1;
    //[SerializeField, Range(0f, 1f)] private float powerDashJumpHeightDiminish = 1;
    //[SerializeField, Range(0f, 1f)] private float postDashVelocityMultX = 1;
    //[SerializeField, Range(0f, 1f)] private float postDashVelocityMultY = 1;
    //[SerializeField, Range(0f, 5f)] private float dashTimer = 1;

    //private CinemachineImpulseSource impulse;

    //private Vector3 preDashPos;
    //private Vector2 dashDir;
    //private Vector3 dashVector;
    //private float dashCounter;

    //[HideInInspector] public bool canDash = true;
    //[HideInInspector] public bool isDashing = false;
    #endregion


    //[SerializeField] private bool hasImpulse = true;
    //[SerializeField] private bool hasBoxCol = true;
    //[SerializeField] private bool hasRB = true;
    private void Awake()
    {
        if (instance == null)
        {
            instance = this;
        }
    }

    private void Start()
    {
        theRB = GetComponent<Rigidbody2D>();
        nWalkDeceleration = oWalkDeceleration;
;
        oGravity = theRB.gravityScale;
    }

    private void FixedUpdate()
    {
        if (!canMove)
        {
            theRB.velocity = new Vector2(0f, theRB.velocity.y);
            return;
        }


        moveDir.x = Input.GetAxisRaw("Horizontal");
        moveDir.y = Input.GetAxisRaw("Vertical");

        //gets the direciton we want to move in
        targetSpeed = moveDir.x * nMoveSpeed;
        //gets the difference between current velocity and wanted velocity
        speedDifference = targetSpeed - theRB.velocity.x;
        accelerationRate = (Mathf.Abs(targetSpeed) > 0.01f) ? walkAcceleration : nWalkDeceleration;
        movement = Mathf.Pow(Mathf.Abs(speedDifference) * accelerationRate, velocityPower) * Mathf.Sign(speedDifference);
        theRB.AddForce(movement * Vector2.right);

        if (onGround) animator?.SetFloat("Speed", Mathf.Abs(theRB.velocity.x));

        //makes it so the player doesn't slide once their speed is less than a certain amount;
        if ((Mathf.Sign(theRB.velocity.x) < minWalkSpeed && moveDir.x == 0))
        {
            theRB.velocity = new Vector2(0f, theRB.velocity.y);
        }

        //Change the gravity depending on situation
        //if (isDashing)
        //{
        //    theRB.gravityScale = 0;
        //}

        //forces the dash to always go the same distance
        //if (dashCounter > 0) dashCounter -= Time.fixedDeltaTime;

        //else if (!isDashing/* && isFalling*/ /*&& !WaterPhysics.inWater*/)
        //{
            theRB.gravityScale = oGravity + postJumpGravity;
        //}

        //forces the jump to always go the same distance
        if (jumpCounter > 0)
            jumpCounter -= Time.fixedDeltaTime;

        //if the player is falling faster than the max speed it sets their speed to the max speed
        if (theRB.velocity.y < -maxFallSpeed)
        {
            theRB.velocity = new Vector2(theRB.velocity.x, -maxFallSpeed);
        }
    }

    // Update is called once per frame
    void Update()
    {
        if (!canMove) return;


        #region checking direction
        //sets rotation based on movement direction
        if (moveDir.x > 0 && !facingRight)
        {
            flip();
        }
        else if (moveDir.x < 0 && facingRight)
        {
            flip();
        }
        #endregion

        //if the player is grounded it resets their jumps, the onGroundTimer increases
        //(needed because it puts the deceleration back to max)
        if (isGrounded())
        {
            jumpsAvailable = 1 /*+ PowerController.instance.extraJumps*/;
            onGroundTimer += Time.deltaTime;

            //if (Input.GetButtonDown("Fire1")) animator.SetTrigger("Attack");
            if (onGroundTimer >= forcedDecelerationRaiseTimer && nWalkDeceleration != maxDeceleration)
            {
                raiseDeceleration(1000f);
            }
        }
        //this makes sure to gets the direction they are dashing before they dash,
        //this helps make sure the player doesn't dash in a weird direction they weren't meaning to
        //if (!isDashing)
        //{
        //    dashDir = moveDir;
        //}

        //dash();

        jump();

    }

    #region dashing
    //private void dash()
    //{
    //    if (Input.GetButtonDown("Fire2") && moveDir != Vector2.zero && checkDashAvailable(true))
    //    {
    //        //audio
    //        //PlayerAudioController.instance.dashSound();

    //        dashDir.Normalize();
    //        dashCounter = dashTimer;
    //        isDashing = true;
    //        canDash = false;
    //        if (dashDir == Vector2.up || dashDir == Vector2.down)
    //        {
    //            nMoveSpeed = oMoveSpeed;
    //        }
    //    }

    //    if (dashCounter > 0)
    //    {

    //        theRB.velocity = new Vector3((oDashForce * dashDir.x), (oDashForce * dashDir.y), 0f);
    //    }

    //    else if (dashCounter <= 0)
    //    {
    //        //if the dash direction is anything except right or left it changes the speed so they don't get weird height
    //        if (isDashing && (dashDir != Vector2.left || dashDir != Vector2.right))
    //        {
    //            theRB.velocity = new Vector3(theRB.velocity.x * postDashVelocityMultX, theRB.velocity.y * postDashVelocityMultY, 0f);
    //        }
    //        isDashing = false;
    //        if (isGrounded() && checkDashAvailable(false))
    //        {
    //            canDash = true;
    //            dashDir = Vector2.zero;
    //        }
    //    }
    //}

    //check both TRUE is checking if they have the dash power AND they can dash.
    //check both FALSE is checking if they even have the dash power
    //private bool checkDashAvailable(bool checkBoth)
    //{
    //    //if (checkBoth) return PowerController.instance.hasDashPower && canDash;
    //    //else return PowerController.instance.hasDashPower;
    //    return true;
    //}
    #endregion

    private void jump()
    {
        maxJumps = 1 /*+ PowerController.instance.extraJumps*/;
        if ((Input.GetButtonDown("Jump") && jumpsAvailable > 0) || doLeewayJump)
        {
            nJumpForce = oJumpForce;
            doLeewayJump = false;
            leewayCounter = 0;
            if (isGrounded() && nMoveSpeed > oMoveSpeed)
            {
                raiseDeceleration(jumpDecelerationIncrease);
            }
            onGroundTimer = 0;
            if (moveDir.y < 0)
            {
                nJumpForce = oJumpForce / 1.5f;

            }

            jumpCounter = jumpTime;


            //if the player jumps while dashing it does the super dash
            //if (isDashing)
            //{
            //    //PlayerAudioController.instance.powerDashSound();
            //    //move speed and jump force get changed by a certain amount
            //    nMoveSpeed *= powerDashSpeedMultiplier + ((jumpsAvailable / maxJumps) * (powerDashSpeedDiminish - (maxJumps - jumpsAvailable)));
            //    //if speed is above max, set it equal to max speed
            //    if (nMoveSpeed > maxMovementSpeed)
            //    {
            //        nMoveSpeed = maxMovementSpeed;
            //    }
            //    nJumpForce *= powerDashJumpHeightDiminish;
            //    //lowers the deceleration
            //    nWalkDeceleration -= decelerationDiminish;
            //    if (nWalkDeceleration < minDeceleration)
            //    {
            //        nWalkDeceleration = minDeceleration;
            //    }
            //    canDash = true;
            //    isDashing = false;
            //}

            isJumping = true;
            isRising = true;
            isFalling = false;
            jumpsAvailable--;
            //}
        }

        //if the player continues to hold the jump button
        if (Input.GetButton("Jump"))
        {
            //if they arent grounded AND holding the button it increases the leewayCounter
            if (!isGrounded())
            {
                leewayCounter += Time.deltaTime;
            }
            //if they have been holding the button for less time than the leeway timer it lets them jump again
            else if (leewayCounter <= jumpLeeway)
            {
                doLeewayJump = true;
                //only does the leeway jump if the player touches the ground
                if (isGrounded())
                    jump();
            }

            //makes the player actually do the jumping
            if (jumpCounter > 0 && isJumping)
            {
                /*jumpCounter -= Time.deltaTime;*/
                theRB.velocity = new Vector2(theRB.velocity.x, nJumpForce);
                isRising = true;
            }
            //if the jumpcounter runs out of time it makes you fall
            else
            {
                isJumping = false;
                isRising = false;
                isFalling = true;
            }
        }

        //if the player lets go of the jump button
        if (Input.GetButtonUp("Jump"))
        {
            leewayCounter = 0;
            isJumping = false;
            isRising = false;
            isFalling = true;
        }
    }

    public bool isGrounded()
    {
        //groundHit = Physics2D.BoxCast(boxCol.bounds.center, boxCol.bounds.size, 0f, Vector2.down, gCheckDist, groundMask);
        //groundHit = Physics2D.OverlapCircle(groundCheck.position, 0.2f, groundLayer);
        if (Physics2D.OverlapCircle(groundCheck.position, 0.2f, groundLayer) && !isRising)
        {
            //animator?.SetBool("OnGround", true);
            //only resets move speed if the deceleration is at the max
            if (nWalkDeceleration == maxDeceleration)
                nMoveSpeed = oMoveSpeed;
            theRB.gravityScale = oGravity;
            nJumpForce = oJumpForce;
            isFalling = false;
            onGround = true;
            return true;
        }

        //animator?.SetBool("OnGround", false);
        onGround = false;
        return false;
    }

    private void flip()
    {
        //makes the facingRight bool become what it wasn't
        facingRight = !facingRight;

        //rotates the player (and all of its children) 180 degrees (this makes it look like the gun is behind the player as well)
        transform.Rotate(0f, 180f, 0f);
        nWalkDeceleration = oWalkDeceleration;

    }

    private void raiseDeceleration(float amount)
    {
        nWalkDeceleration += amount;
        if (nWalkDeceleration > maxDeceleration)
        {
            nWalkDeceleration = maxDeceleration;
        }
    }

    #region triggers
    private void OnTriggerEnter2D(Collider2D other)
    {
        //if (other.tag == "Trigger")
        //{
        //    other.GetComponent<TriggerParent>().doTriggerAction();
        //}

        //if (other.tag == "Camera Zone")
        //{
        //    CameraController.instance.setCinemachineFollow(other.GetComponentInChildren<CinemachineVirtualCamera>());
        //}

        if (other.tag == "Enemy")
        {
            //When knockback is figured out put it here
        }

        //if (other.tag == "Water")
        //{
        //    WaterPhysics.inWater = true;
        //}
    }

    private void OnTriggerExit2D(Collider2D other)
    {
        if (other.tag == "Camera Zone")
        {
            //CameraController.instance.resetPriority();
        }

        //if (other.tag == "Water")
        //{
        //    if (other.GetComponent<WaterProperties>().waterDir == WaterProperties.direction.UP)
        //    {
        //        theRB.velocity = new Vector2(theRB.velocity.x, theRB.velocity.y / (WaterPhysics.waterExitVelocityDiminish * (theRB.velocity.y / maxMovementSpeed)));
        //    }
        //}
    }

    public void Attack()
    {
        //var colliders = Physics2D.OverlapCircleAll(transform.position, 2);
        //foreach (var collider in colliders)
        //{
        //    if (collider.gameObject.TryGetComponent(out IDamagable damagable))
        //    {
        //        damagable.ApplyDamage(1000);
        //    }
        //}
    }
    #endregion
}

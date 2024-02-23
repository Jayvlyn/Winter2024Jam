using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

public class PlayerController : MonoBehaviour
{
    public Rigidbody2D rb;
    public NearbySlashable ns;

    public Transform groundCheck;
    public LayerMask groundLayer;

    [SerializeField] private float thresholdDemand = 1.0f;
    [SerializeField] private float speedThreshold = 12.0f;
    [SerializeField] private float speed = 8.0f;
    [SerializeField] private float jumpPower = 10.0f;

    private float horizontal;
    private bool isFacingRight = true;


    private void Start()
    {
        rb = GetComponent<Rigidbody2D>();
    }

    private void FixedUpdate()
    {
        rb.velocity = new Vector2(horizontal * speed, rb.velocity.y);
    }

    private void Update()
    {
        if((!isFacingRight && horizontal > 0f) || (isFacingRight && horizontal < 0f))
        {
            FlipX();
        }

        if(rb.velocity.magnitude > thresholdDemand)
        {

        }
    }

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
        Debug.Log("onmove");
        Vector2 input = inputValue.Get<Vector2>();
        horizontal = input.x;
    }
    
    private void OnJump(InputValue inputValue)
    {
        Debug.Log("onjump");
        if(inputValue.isPressed && IsGrounded())
        {
            rb.velocity = new Vector2(rb.velocity.x, jumpPower);
        }

        if(!inputValue.isPressed && rb.velocity.y > 0)
        {
            rb.velocity = new Vector2(rb.velocity.x, rb.velocity.y * 0.5f);
        }
    }

    private void OnSlash(InputValue inputValue)
    {
        Slashable slashable = ns.GetClosestSlashable();
    }

    private void OnThrow(InputValue inputValue)
    {

    }
    #endregion
}

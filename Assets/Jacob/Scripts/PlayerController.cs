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

    [SerializeField] private float gravity = 3.5f;

    [SerializeField] private float thresholdDemand = 0.98f;
    [SerializeField] private float speedThreshold = 12.0f;
    [SerializeField] private float moveSpeed = 8.0f;
    [SerializeField] private float jumpPower = 10.0f;
    [SerializeField] private float slashPower = 10.0f;
    [SerializeField] private float slashLength = 1.2f;
    private bool isSlashing;

    private bool isFacingRight = true;
    private Vector2 moveInput = Vector2.zero;

    private float targetSpeed;
    private float speedDifference;
    private float accelerationRate;
    private float movement;

    private void Start()
    {
        rb = GetComponent<Rigidbody2D>();
        ns = GetComponent<NearbySlashable>();
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
    }

    private void Update()
    {

        if ((!isFacingRight && moveInput.x > 0f) || (isFacingRight && moveInput.x < 0f))
        {
            FlipX();
        }

        if (rb.velocity.magnitude > speedThreshold)
        {
            rb.velocity *= thresholdDemand;
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
        moveInput = inputValue.Get<Vector2>();
    }

    private void OnJump(InputValue inputValue)
    {
        if (inputValue.isPressed && IsGrounded())
        {
            rb.AddForce(new Vector2(0, jumpPower), ForceMode2D.Impulse);
        }

        if (!inputValue.isPressed && rb.velocity.y > 0)
        {
            rb.velocity = new Vector2(rb.velocity.x, rb.velocity.y * 0.5f);
        }
    }

    private void OnSlash(InputValue inputValue)
    {

        if (ns.GetClosestSlashable() != null)
        {
            Slashable slashable = ns.GetClosestSlashable();

            slashable.OnSlashThrough(1);

            isSlashing = true;
            rb.gravityScale = 0;

            Vector3 slashDir = slashable.transform.position - transform.position;
            slashDir.Normalize();
            rb.AddForce(slashDir * slashPower, ForceMode2D.Impulse);

            StartCoroutine(FinishSlasher(slashDir));
        }
    }

    private void OnThrow(InputValue inputValue)
    {

    }
    #endregion

    private IEnumerator FinishSlasher(Vector3 slashDir)
    {
        yield return new WaitForSeconds(slashLength);

        isSlashing = false;
        rb.gravityScale = gravity;

        if (slashDir.y > 0)
        {
            rb.AddForce(-slashDir * (slashPower * .5f), ForceMode2D.Impulse);
        }
        else
        {
            rb.AddForce(new Vector3(-slashDir.x, 0, 0) * (slashPower * .5f), ForceMode2D.Impulse);
        }
    }
}

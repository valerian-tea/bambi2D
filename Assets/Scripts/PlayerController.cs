using System.Collections;
using UnityEngine;
using UnityEngine.InputSystem;

public class PlayerController : MonoBehaviour
{
    [Header("Movement Settings")]
    public float speed = 15f;
    public float jumpForce = 15f;

    [SerializeField]
    private Transform groundCheck;

    [SerializeField]
    private float groundCheckRadius = 0.2f;

    [SerializeField]
    private LayerMask groundLayer;

    private Rigidbody2D rb;
    private float movementInputX;
    private Animator animator;
    private PlayerInput input;
    private bool isMovementEnabled = true;
    public bool isFacingRight = true;

    [Header("Camera Settings")]
    public CameraFollow cameraFollow;

    [Header("Jump Settings")]
    private bool isGrounded;
    private bool wasGrounded;

    [Header("Dash Settings")]
    private float originalGravity;

    private bool canDash = true;
    private bool isDashing;

    [SerializeField]
    private float dashCooldown = 2f;

    [SerializeField]
    private float dashForce = 10f;

    void Start()
    {
        animator = GetComponentInChildren<Animator>();
        input = GetComponent<PlayerInput>();
        rb = GetComponent<Rigidbody2D>();
        originalGravity = rb.gravityScale;
    }

    public void OnMove(InputValue value)
    {
        movementInputX = value.Get<Vector2>().x;
    }

    public IEnumerator OnDash(InputValue value)
    {
        if (!canDash)
            yield break;
        isDashing = true;
        animator.SetInteger("actionID", 2);
        animator.SetTrigger("action");
        canDash = false;
        yield return new WaitForSeconds(0.1f);
        isDashing = false;
        yield return new WaitForSeconds(dashCooldown);
        canDash = true;
    }

    public void OnJump(InputValue value)
    {
        if (isGrounded)
        {
            Jump();
        }
    }

    void Jump()
    {
        rb.linearVelocityY = jumpForce;
        animator.SetInteger("actionID", 0);
        animator.SetTrigger("action");
    }

    bool CheckIfGrounded()
    {
        isGrounded = Physics2D.OverlapCircle(groundCheck.position, groundCheckRadius, groundLayer);
        animator.SetBool("isGrounded", isGrounded);
        return isGrounded;
    }

    void FixedUpdate()
    {
        if (!isMovementEnabled)
        {
            movementInputX = 0f;
        }

        float yRotation = transform.eulerAngles.y;
        if (yRotation == 0)
        {
            isFacingRight = true;
        }
        else if (yRotation == 180)
        {
            isFacingRight = false;
        }

        if (isDashing)
        {
            rb.gravityScale = 0f;
            rb.linearVelocityY = 0f;
            animator.SetFloat("yVelocity", 0f);
            if (isFacingRight)
                rb.linearVelocityX = dashForce;
            else
                rb.linearVelocityX = -dashForce;

            return;
        }
        else if (rb.linearVelocityY < 0)
        {
            rb.gravityScale = originalGravity * 2f; // Increase gravity for faster fall
        }
        else
        {
            rb.gravityScale = originalGravity;
        }

        bool isGrounded = CheckIfGrounded();

        if (movementInputX != 0)
            rb.linearVelocityX = movementInputX * speed;
        else
            rb.linearVelocityX = 0;

        animator.SetInteger("xVel", Mathf.Abs(Mathf.FloorToInt(rb.linearVelocityX)));
        animator.SetFloat("yVelocity", rb.linearVelocityY);

        Turn();

        if (isGrounded)
        {
            rb.gravityScale = originalGravity;
            if (Mathf.Abs(movementInputX) != 0)
                animator.SetBool("isWalking", true);
            else
                animator.SetBool("isWalking", false);
        }
    }

    private void Turn()
    {
        if (movementInputX > 0)
            transform.rotation = Quaternion.Euler(0, 0, 0);
        else if (movementInputX < 0)
            transform.rotation = Quaternion.Euler(0, 180, 0);
        cameraFollow.CallFlip();
    }

    public void OnAttack(InputValue value)
    {
        animator.SetInteger("attackID", 0);
        animator.SetTrigger("attack");
    }

    public void OnDownAttack(InputValue value)
    {
        animator.SetTrigger("downAttack");
    }

    public void StopMovement()
    {
        isMovementEnabled = false;
        input.DeactivateInput();
    }

    public void StartMovement()
    {
        isMovementEnabled = true;
        input.ActivateInput();
    }
}

using UnityEngine;
using UnityEngine.InputSystem;

public class PlayerController : MonoBehaviour
{
    public float speed = 15f;
    public float jumpForce = 15f;
    public Transform groundCheck;
    public float groundCheckRadius = 0.2f;
    public LayerMask groundLayer;
    private Rigidbody2D rb;
    private float movementInputX;
    private Animator animator;
    private SpriteRenderer spriteRenderer;
    private PlayerInput input;
    private bool isMovementEnabled = true;

    private bool isGrounded;

    void Start()
    {
        animator = GetComponent<Animator>();
        spriteRenderer = GetComponent<SpriteRenderer>();
        input = GetComponent<PlayerInput>();
        rb = GetComponent<Rigidbody2D>();
    }

    public void OnMove(InputValue value)
    {
        movementInputX = value.Get<Vector2>().x;
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
        animator.SetBool("isJumping", true);
    }

    void FixedUpdate()
    {
        if (!isMovementEnabled)
        {
            rb.linearVelocity = Vector2.zero;
            animator.SetFloat("xVelocity", 0);
            animator.SetFloat("yVelocity", 0);
            animator.SetBool("isWalking", false);
            animator.SetBool("isJumping", false);
            movementInputX = 0f;
            return;
        }

        isGrounded = Physics2D.OverlapCircle(groundCheck.position, groundCheckRadius, groundLayer);

        rb.linearVelocityX = movementInputX * speed;
        animator.SetFloat("xVelocity", Mathf.Abs(rb.linearVelocityX));
        animator.SetFloat("yVelocity", rb.linearVelocityY);

        if (movementInputX > 0)
            spriteRenderer.flipX = false;
        else if (movementInputX < 0)
            spriteRenderer.flipX = true;

        if (isGrounded)
        {
            animator.SetBool("isJumping", false);

            if (Mathf.Abs(movementInputX) != 0)
                animator.SetBool("isWalking", true);
            else
                animator.SetBool("isWalking", false);
        }
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

    public void WagTail()
    {
        animator.SetTrigger("isWagging");
    }
}

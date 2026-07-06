using UnityEngine;
using UnityEngine.InputSystem;
using Yarn.Unity;

public class PlayerController : MonoBehaviour
{
    public float speed = 15f;
    public float jumpForce = 15f;

    [SerializeField]
    private Transform groundCheck;

    public float groundCheckRadius = 0.2f;
    public LayerMask groundLayer;

    private Rigidbody2D rb;
    private float movementInputX;
    private Animator animator;
    private SpriteRenderer spriteRenderer;
    private PlayerInput input;
    private bool isMovementEnabled = true;

    private bool isGrounded;
    private bool wasGrounded;

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
        animator.SetTrigger("jump");
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

        bool isGrounded = CheckIfGrounded(); // Your existing ground check

        if (isGrounded && !wasGrounded)
        {
            // CHARACTER JUST HIT THE GROUND: Snapshot the velocity once!
            animator.SetFloat("impactForce", Mathf.Abs(rb.linearVelocity.y));
        }
        else if (!isGrounded)
        {
            // CHARACTER IS IN THE AIR: Keep updating live velocity for aerial transitions
            animator.SetFloat("yVelocity", rb.linearVelocity.y);
        }

        wasGrounded = isGrounded;

        if (movementInputX != 0)
            rb.linearVelocityX = movementInputX * speed;
        else
            rb.linearVelocityX = 0;

        animator.SetFloat("xVelocity", Mathf.Abs(rb.linearVelocityX));
        animator.SetFloat("yVelocity", rb.linearVelocityY);

        if (movementInputX > 0)
            spriteRenderer.flipX = false;
        else if (movementInputX < 0)
            spriteRenderer.flipX = true;

        if (isGrounded)
        {
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
}

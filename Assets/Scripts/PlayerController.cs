using System.Collections;
using UnityEngine;
using UnityEngine.InputSystem;
using Yarn.Unity;

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
    private SpriteRenderer spriteRenderer;
    private PlayerInput input;
    private bool isMovementEnabled = true;

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

    [Header("Attack Settings")]
    public float attackRange = 1.5f;
    public LayerMask enemyLayer;
    public GameObject attackHitbox;

    void Start()
    {
        animator = GetComponentInChildren<Animator>();
        spriteRenderer = GetComponentInChildren<SpriteRenderer>();
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
        // if (!canDash)
        //     yield break;
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
        Debug.Log(rb.gravityScale);
        if (!isMovementEnabled)
        {
            movementInputX = 0f;
        }

        if (isDashing)
        {
            rb.gravityScale = 0f;
            rb.linearVelocityY = 0f;
            animator.SetFloat("yVelocity", 0f);
            if (spriteRenderer.flipX == false)
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

        if (movementInputX > 0)
            spriteRenderer.flipX = false;
        else if (movementInputX < 0)
            spriteRenderer.flipX = true;

        if (isGrounded)
        {
            rb.gravityScale = originalGravity;
            if (Mathf.Abs(movementInputX) != 0)
                animator.SetBool("isWalking", true);
            else
                animator.SetBool("isWalking", false);
        }
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

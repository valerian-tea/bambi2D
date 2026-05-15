using UnityEngine;
using UnityEngine.InputSystem;

public class PlayerController : MonoBehaviour
{
    public float speed = 5f;
    private Rigidbody2D rb;
    private Vector2 movementInput;
    private Animator animator;
    private SpriteRenderer spriteRenderer;
    private PlayerInput input;
    private bool isMovementEnabled = true;

    // Called automatically if Behavior is "Send Messages"
    public void OnMove(InputValue value)
    {
        if (isMovementEnabled)
        {
            movementInput = value.Get<Vector2>();
        }
    }

    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        animator = GetComponent<Animator>();
        spriteRenderer = GetComponent<SpriteRenderer>();
        input = GetComponent<PlayerInput>();
        rb = GetComponent<Rigidbody2D>();
    }

    void FixedUpdate()
    {
        if (!isMovementEnabled)
        {
            animator.SetBool("isWalking", false);
            rb.linearVelocity = Vector2.zero;
            movementInput = Vector2.zero;
            return;
        }

        rb.linearVelocity = movementInput * speed;

        if (movementInput.x > 0)
            spriteRenderer.flipX = false;
        else if (movementInput.x < 0)
            spriteRenderer.flipX = true;

        if (movementInput.magnitude > 0)
            animator.SetBool("isWalking", true);
        else
            animator.SetBool("isWalking", false);
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

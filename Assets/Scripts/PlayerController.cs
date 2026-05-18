using UnityEngine;
using UnityEngine.InputSystem;

public class PlayerController : MonoBehaviour
{
    public float speed = 15f;
    private Rigidbody2D rb;
    private float movementInput;
    private Animator animator;
    private SpriteRenderer spriteRenderer;
    private PlayerInput input;
    private bool isMovementEnabled = true;

    public void OnMove(InputValue value)
    {
        if (isMovementEnabled)
        {
            movementInput = value.Get<Vector2>().x;
        }
    }

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
            movementInput = 0f;
            return;
        }

        rb.linearVelocityX = movementInput * speed;

        if (movementInput > 0)
            spriteRenderer.flipX = false;
        else if (movementInput < 0)
            spriteRenderer.flipX = true;

        if (Mathf.Abs(movementInput) != 0)
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

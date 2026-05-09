using System.Diagnostics;
using UnityEngine;
using UnityEngine.InputSystem;

public class PlayerController : MonoBehaviour
{
    private Vector2 movementInput;
    Animator animator;
    SpriteRenderer spriteRenderer;

    // Called automatically if Behavior is "Send Messages"
    public void OnMove(InputValue value)
    {
        movementInput = value.Get<Vector2>();
    }

    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        animator = GetComponent<Animator>();
        spriteRenderer = GetComponent<SpriteRenderer>();
    }

    // Update is called once per frame
    void Update()
    {
        transform.Translate(movementInput * Time.deltaTime * 5f);
        if (movementInput.x > 0)
            spriteRenderer.flipX = false;
        else if (movementInput.x < 0)
            spriteRenderer.flipX = true;

        if (movementInput.magnitude > 0)
            animator.SetBool("isWalking", true);
        else
            animator.SetBool("isWalking", false);
    }
}

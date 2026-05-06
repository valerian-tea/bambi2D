using UnityEngine;
using UnityEngine.InputSystem;

public class PlayerController : MonoBehaviour
{
    private Vector2 movementInput;
    Animator animator;

    // Called automatically if Behavior is "Send Messages"
    public void OnMove(InputValue value)
    {
        movementInput = value.Get<Vector2>();
    }

    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        animator = GetComponent<Animator>();
    }

    // Update is called once per frame
    void Update()
    {
        transform.Translate(movementInput * Time.deltaTime * 5f);
    }
}

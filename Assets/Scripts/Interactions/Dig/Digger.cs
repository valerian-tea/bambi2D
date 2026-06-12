using System.Collections;
using UnityEngine;
using UnityEngine.InputSystem;

public class Digger : MonoBehaviour
{
    private DigSpot currentDigSpot;
    private Animator animator;

    void Start()
    {
        animator = GetComponent<Animator>();
    }

    public void SetCurrentDigSpot(DigSpot spot)
    {
        currentDigSpot = spot;
    }

    public void OnInteract(InputValue value)
    {
        if (currentDigSpot != null)
        {
            animator.SetTrigger("dig");
            StartCoroutine(currentDigSpot.Dig());
        }
    }
}

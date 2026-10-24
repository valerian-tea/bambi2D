using System.Collections;
using UnityEngine;
using UnityEngine.InputSystem;

public class PlatformDropDown : MonoBehaviour
{
    private Collider2D playerCollider;
    private GameObject currentPlatform;
    private Animator animator;

    void Start()
    {
        playerCollider = GetComponent<Collider2D>();
        animator = GetComponent<Animator>();
    }

    public void DropDown()
    {
        if (currentPlatform != null)
        {
            animator.SetInteger("actionID", 3);
            animator.SetTrigger("action");
            StartCoroutine(DisableCollision());
        }
    }

    private IEnumerator DisableCollision()
    {
        Collider2D platformCollider = currentPlatform.GetComponent<Collider2D>();
        Physics2D.IgnoreCollision(playerCollider, platformCollider, true);
        yield return new WaitForSeconds(0.25f);
        Physics2D.IgnoreCollision(playerCollider, platformCollider, false);
    }

    private void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.gameObject.CompareTag("Platform"))
        {
            currentPlatform = collision.gameObject;
        }
    }

    private void OnCollisionExit2D(Collision2D collision)
    {
        if (collision.gameObject.CompareTag("Platform"))
        {
            currentPlatform = null;
        }
    }
}

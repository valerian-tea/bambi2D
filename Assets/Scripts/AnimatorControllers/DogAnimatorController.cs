using System.Collections;
using UnityEngine;
using Yarn.Unity;

public class DogAnimatorController : MonoBehaviour
{
    private Animator animator;
    private SpriteRenderer spriteRenderer;

    void Start()
    {
        animator = GetComponent<Animator>();
        spriteRenderer = GetComponent<SpriteRenderer>();
    }

    public void FaceRight()
    {
        spriteRenderer.flipX = false;
    }

    public void FaceLeft()
    {
        spriteRenderer.flipX = true;
    }

    [YarnCommand("set_dog_animation")]
    public void SetAction(string action)
    {
        if (action == "sad")
            animator.SetBool("isSad", true);
        else if (action == "wag")
        {
            animator.SetTrigger("wag");
            animator.SetBool("isSad", false);
        }
        else if (action == "paw")
        {
            animator.SetTrigger("paw");
        }
        else if (action == "neutral")
            animator.SetBool("isSad", false);
    }

    public IEnumerator Wag()
    {
        animator.SetTrigger("isWagging");
        yield return new WaitForSeconds(1.0f);
    }

    public IEnumerator Sad()
    {
        animator.SetBool("isSad", true);
        yield return new WaitForSeconds(1.0f);
    }
}

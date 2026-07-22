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
        if (action == "bark")
        {
            animator.SetInteger("emotion", 1);
        }
        else if (action == "sad")
            animator.SetInteger("emotion", 2);
        else if (action == "wag")
        {
            animator.SetInteger("emotion", 3);
        }
        else if (action == "neutral")
            animator.SetInteger("emotion", 0);
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

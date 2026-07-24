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
            animator.SetInteger("emoteID", 1);
        }
        else if (action == "sad")
            animator.SetInteger("emoteID", 2);
        else if (action == "wag")
        {
            animator.SetInteger("emoteID", 0);
        }
        animator.SetTrigger("emote");
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

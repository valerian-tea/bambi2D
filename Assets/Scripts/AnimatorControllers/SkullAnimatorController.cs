using System.Collections;
using UnityEngine;
using Yarn.Unity;

public class SkullAnimatorController : MonoBehaviour
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

    [YarnCommand("set_action_fire")]
    public void SetAction(string action)
    {
        animator.SetTrigger(action);
    }

    public IEnumerator FireDance()
    {
        animator.SetTrigger("SkullIntro");
        yield return new WaitForSeconds(1.0f);
    }
}

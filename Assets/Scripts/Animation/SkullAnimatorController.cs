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

    [YarnCommand("hide_skull")]
    public void HideSprite()
    {
        spriteRenderer.enabled = false;
    }

    [YarnCommand("show_skull")]
    public void ShowSprite()
    {
        spriteRenderer.enabled = true;
    }

    [YarnCommand("move_skull")]
    public void MoveSprite(GameObject destination)
    {
        transform.position = destination.transform.position;
    }

    [YarnCommand("set_skull_animation")]
    public void SetAction(string action)
    {
        animator.SetTrigger(action);
    }
}

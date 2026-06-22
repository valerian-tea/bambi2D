using System.Collections;
using UnityEngine;
using Yarn.Unity;

public class FireAnimatorController : MonoBehaviour
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

    [YarnCommand("hide_fire")]
    public void HideSprite()
    {
        spriteRenderer.enabled = false;
    }

    [YarnCommand("show_fire")]
    public void ShowSprite()
    {
        spriteRenderer.enabled = true;
    }

    [YarnCommand("move_fire")]
    public void MoveSprite(GameObject destination)
    {
        // transform.position = destination.transform.position;
        transform.position = new Vector2(0, 0);
        Debug.Log($"Fire moved to {transform.position}");
    }

    [YarnCommand("set_fire_animation")]
    public void SetAction(string action)
    {
        animator.SetTrigger(action);
    }
}

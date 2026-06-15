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

    public void MoveSprite(Vector2 position)
    {
        transform.position = position;
    }

    [YarnCommand("set_fire_animation")]
    public void SetAction(string action)
    {
        animator.SetTrigger(action);
    }
}

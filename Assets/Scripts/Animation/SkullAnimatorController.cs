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

    [YarnCommand("fade_skull")]
    public void ShowSprite(float targetAlpha)
    {
        StartCoroutine(FadeSpriteAlpha(targetAlpha));
    }

    private IEnumerator FadeSpriteAlpha(float targetAlpha)
    {
        float startValue = spriteRenderer.color.a;
        float timeElapsed = 0f;
        float fadeDuration = 1.0f; // Duration of the fade effect in seconds

        while (timeElapsed < fadeDuration)
        {
            timeElapsed += Time.deltaTime;
            float newAlpha = Mathf.Lerp(startValue, targetAlpha, timeElapsed / fadeDuration);
            Color newColor = new Color(
                spriteRenderer.color.r,
                spriteRenderer.color.g,
                spriteRenderer.color.b,
                newAlpha
            );
            spriteRenderer.color = newColor;
            yield return null;
        }

        // Ensure the final alpha value is set
        Color finalColor = new Color(
            spriteRenderer.color.r,
            spriteRenderer.color.g,
            spriteRenderer.color.b,
            targetAlpha
        );
        spriteRenderer.color = finalColor;
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

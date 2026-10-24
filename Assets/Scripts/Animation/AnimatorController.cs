using System.Collections;
using UnityEngine;
using Yarn.Unity;

public class AnimatorController : MonoBehaviour
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

    [YarnCommand("set_animation")]
    public void SetAction(int emoteID)
    {
        animator.SetInteger("emoteID", emoteID);
        animator.SetTrigger("emote");
    }

    [YarnCommand("show_sprite")]
    public void ShowSprite()
    {
        spriteRenderer.enabled = true;
    }

    [YarnCommand("hide_sprite")]
    public void HideSprite()
    {
        spriteRenderer.enabled = false;
    }

    [YarnCommand("fade_sprite")]
    public void FadeSprite(float targetAlpha)
    {
        if (spriteRenderer != null)
        {
            StartCoroutine(FadeSpriteAlpha(spriteRenderer, targetAlpha));
        }
    }

    [YarnCommand("fade_child_sprites")]
    public void FadeChildSprite(float targetAlpha)
    {
        SpriteRenderer[] childSpriteRenderers = GetComponentsInChildren<SpriteRenderer>(true);

        foreach (SpriteRenderer childSpriteRenderer in childSpriteRenderers)
        {
            StartCoroutine(FadeSpriteAlpha(childSpriteRenderer, targetAlpha));
        }
    }

    private IEnumerator FadeSpriteAlpha(SpriteRenderer targetSpriteRenderer, float targetAlpha)
    {
        float startValue = targetSpriteRenderer.color.a;
        float timeElapsed = 0f;
        float fadeDuration = 1.0f; // Duration of the fade effect in seconds

        while (timeElapsed < fadeDuration)
        {
            timeElapsed += Time.deltaTime;
            float newAlpha = Mathf.Lerp(startValue, targetAlpha, timeElapsed / fadeDuration);
            Color newColor = new Color(
                targetSpriteRenderer.color.r,
                targetSpriteRenderer.color.g,
                targetSpriteRenderer.color.b,
                newAlpha
            );
            targetSpriteRenderer.color = newColor;
            yield return null;
        }

        // Ensure the final alpha value is set
        Color finalColor = new Color(
            targetSpriteRenderer.color.r,
            targetSpriteRenderer.color.g,
            targetSpriteRenderer.color.b,
            targetAlpha
        );
        targetSpriteRenderer.color = finalColor;
    }
}

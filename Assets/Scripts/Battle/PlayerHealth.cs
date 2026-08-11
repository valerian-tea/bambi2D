using System;
using System.Collections;
using UnityEngine;

public class PlayerHealth : Health
{
    [Header("Player Settings")]
    public float invincibilityDuration = 1f;
    public Color flashColor = new Color(1f, 1f, 1f, 0.5f);

    private bool isInvincible;
    private SpriteRenderer spriteRenderer;
    private Color originalColor;
    public static event Action OnHealthChanged;

    protected override void Start()
    {
        base.Start();
        spriteRenderer = GetComponent<SpriteRenderer>();
        originalColor = spriteRenderer.color;
    }

    public override void TakeDamage(int damage, bool isFrontHit)
    {
        if (isInvincible)
            return;

        base.TakeDamage(damage, isFrontHit);
        OnHealthChanged?.Invoke();

        if (damage > 0 && isAlive)
        {
            StartCoroutine(Invincibility());
        }
    }

    private IEnumerator Invincibility()
    {
        isInvincible = true;
        float elapsed = 0f;
        const float flashInterval = 0.1f;

        while (elapsed < invincibilityDuration)
        {
            SetSpriteFlash(true);
            yield return new WaitForSeconds(flashInterval);
            SetSpriteFlash(false);
            yield return new WaitForSeconds(flashInterval);
            elapsed += flashInterval * 2f;
        }

        SetSpriteFlash(false);
        isInvincible = false;
    }

    private void SetSpriteFlash(bool isFlashing)
    {
        spriteRenderer.color = isFlashing ? flashColor : originalColor;
    }

    public bool IsInvincible()
    {
        return isInvincible;
    }
}

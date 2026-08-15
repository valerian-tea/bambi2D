using System.Collections;
using UnityEngine;

public class Health : MonoBehaviour
{
    [Header("Health Settings")]
    public int maxHealth = 40;
    protected int currentHealth;
    private bool isAlive = true;

    [Header("Invincibility Settings")]
    public float invincibilityDuration = 1f;
    public Color flashColor = new Color(1f, 1f, 1f, 0.5f);
    public float flashInterval = 0.1f;
    private bool isInvincible;
    private SpriteRenderer spriteRenderer;
    private Animator animator;
    private Color originalColor;

    [Header("Item Drop Settings")]
    public GameObject itemPrefab;

    protected virtual void Start()
    {
        currentHealth = maxHealth;
        animator = GetComponent<Animator>();
        spriteRenderer = GetComponent<SpriteRenderer>();
        originalColor = spriteRenderer.color;
    }

    public virtual void TakeDamage(int damage, bool isFrontHit)
    {
        if (!isAlive || isInvincible)
            return;

        currentHealth -= damage;
        currentHealth = Mathf.Clamp(currentHealth, 0, maxHealth);

        PlayHitAnimation(isFrontHit);
        Debug.Log($"{gameObject.name} took {damage} damage. Current health: {currentHealth}");

        if (currentHealth <= 0f)
        {
            Die();
        }

        if (damage > 0 && isAlive)
        {
            StartCoroutine(Invincibility());
        }
    }

    private IEnumerator Invincibility()
    {
        isInvincible = true;
        float elapsed = 0f;

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

    protected virtual void PlayHitAnimation(bool isFrontHit)
    {
        if (animator == null)
            return;

        animator.SetInteger("attackID", isFrontHit ? 1 : 2);
        animator.SetTrigger("attack");
    }

    protected virtual void Die()
    {
        isAlive = false;
        if (itemPrefab != null)
        {
            Instantiate(itemPrefab, transform.position, Quaternion.identity);
        }
        Destroy(gameObject);
    }

    public int GetCurrentHealth()
    {
        return currentHealth;
    }
}

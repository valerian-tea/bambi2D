using UnityEngine;

public class Health : MonoBehaviour
{
    [Header("Health Settings")]
    public int maxHealth = 40;
    protected int currentHealth;
    protected Animator animator;
    protected bool isAlive = true;

    protected virtual void Start()
    {
        currentHealth = maxHealth;
        animator = GetComponent<Animator>();
    }

    public virtual void TakeDamage(int damage, bool isFrontHit)
    {
        if (!isAlive)
            return;

        currentHealth -= damage;
        currentHealth = Mathf.Clamp(currentHealth, 0, maxHealth);

        PlayHitAnimation(isFrontHit);
        Debug.Log($"{gameObject.name} took {damage} damage. Current health: {currentHealth}");

        if (currentHealth <= 0f)
        {
            Die();
        }
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
        Destroy(gameObject);
    }

    public int GetCurrentHealth()
    {
        return currentHealth;
    }

    public bool IsAlive()
    {
        return isAlive;
    }
}

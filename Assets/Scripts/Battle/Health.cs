using UnityEngine;

public class Health : MonoBehaviour
{
    public float maxHealth = 40f;
    private float currentHealth;
    private Animator animator;

    void Start()
    {
        currentHealth = maxHealth;
        animator = GetComponent<Animator>();
    }

    public void TakeDamage(float damage, bool isFrontHit)
    {
        if (isFrontHit)
        {
            animator.SetInteger("attackID", 1);
        }
        else
        {
            animator.SetInteger("attackID", 2);
        }
        animator.SetTrigger("attack");
        currentHealth -= damage;
        Debug.Log($"{gameObject.name} took {damage} damage. Current health: {currentHealth}");
        if (currentHealth <= 0)
        {
            Die();
        }
    }

    void Die()
    {
        // Handle death logic here (e.g., play animation, disable character, etc.)
        Debug.Log($"{gameObject.name} has died.");
        Destroy(gameObject);
    }
}

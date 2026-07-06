using UnityEngine;

public class Health : MonoBehaviour
{
    public float maxHealth = 100f;
    private float currentHealth;

    void TakeDamage(float damage)
    {
        currentHealth -= damage;
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

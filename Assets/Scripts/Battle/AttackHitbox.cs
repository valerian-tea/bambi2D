using UnityEngine;

public class AttackHitbox : MonoBehaviour
{
    private void OnTriggerEnter2D(Collider2D other)
    {
        Debug.Log("Attack hitbox triggered by: " + other.name);
        Health enemyHealth = other.GetComponent<Health>();
        if (other.CompareTag("Enemy") && enemyHealth != null)
        {
            enemyHealth.TakeDamage(10);
            Debug.Log("Hit: " + other.name);
        }
    }
}

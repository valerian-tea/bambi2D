using UnityEngine;

public class PlayerAttackHitbox : MonoBehaviour
{
    public int damage = 10;

    private void OnTriggerEnter2D(Collider2D other)
    {
        Health enemyHealth = other.GetComponent<Health>();
        if (other.CompareTag("Enemy"))
        {
            enemyHealth.TakeDamage(damage, CalculateHitDirection(other));
            Debug.Log("Hit: " + other.name);
        }
    }

    private bool CalculateHitDirection(Collider2D other)
    {
        Vector2 attackerPos = transform.position;
        Vector2 targetPos = other.transform.position;
        bool attackFromRight = attackerPos.x > targetPos.x;

        SpriteRenderer targetSprite = other.GetComponent<SpriteRenderer>();
        bool targetFacingRight = targetSprite != null ? !targetSprite.flipX : true;

        return attackFromRight == targetFacingRight;
    }
}

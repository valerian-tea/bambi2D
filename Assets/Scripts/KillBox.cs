using UnityEngine;

public class KillBox : MonoBehaviour
{
    public GameObject respawnPoint;
    public int damage = 2;
    private Health playerHealth;
    private PlayerController playerController;

    private void OnTriggerEnter2D(Collider2D other)
    {
        if (other.CompareTag("Player"))
        {
            Die(other);
        }
    }

    private void Die(Collider2D other)
    {
        Debug.Log("Player fell off the edge and died!");

        playerHealth = other.GetComponent<Health>();
        playerController = other.GetComponent<PlayerController>();
        playerController.StopMovement();
        playerHealth.TakeDamage(damage, true);
        other.transform.position = respawnPoint.transform.position;
    }
}

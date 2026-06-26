using UnityEngine;

public class StaticEnemyAI : MonoBehaviour
{
    [Header("Detection Settings")]
    public float detectionRange = 10f;
    public float shootingRange = 8f;
    public LayerMask obstacleLayer;

    [Header("Combat Settings")]
    public GameObject projectilePrefab;
    public Transform firePoint;
    public float fireRate = 1f;

    [Header("Rotation Settings")]
    public float rotationSpeed = 5f;

    private Transform playerTransform;
    private float nextFireTime;

    void Start()
    {
        // Find the player automatically using their tag
        GameObject player = GameObject.FindGameObjectWithTag("Player");
        if (player != null)
        {
            playerTransform = player.transform;
        }
    }

    void Update()
    {
        if (playerTransform == null)
            return;

        float distanceToPlayer = Vector3.Distance(transform.position, playerTransform.position);

        // Check if player is within maximum detection range
        if (distanceToPlayer <= detectionRange)
        {
            // Verify line of sight using a Raycast
            if (HasLineOfSight())
            {
                LookAtPlayer();

                // Fire if the player is within shooting range and weapon is cooled down
                if (distanceToPlayer <= shootingRange && Time.time >= nextFireTime)
                {
                    Shoot();
                    nextFireTime = Time.time + 1f / fireRate;
                }
            }
        }
    }

    bool HasLineOfSight()
    {
        Vector3 directionToPlayer = (playerTransform.position - transform.position).normalized;
        float distanceToPlayer = Vector3.Distance(transform.position, playerTransform.position);

        // Raycast returns true if a solid obstacle blocks the view to the player
        if (Physics.Raycast(transform.position, directionToPlayer, distanceToPlayer, obstacleLayer))
        {
            return false; // Path blocked by an obstacle
        }

        return true; // Clear line of sight
    }

    void LookAtPlayer()
    {
        // Calculate direction on the horizontal plane (X and Z axes only)
        Vector3 targetDirection = playerTransform.position - transform.position;
        targetDirection.y = 0;

        if (targetDirection != Vector3.zero)
        {
            Quaternion targetRotation = Quaternion.LookRotation(targetDirection);
            transform.rotation = Quaternion.Slerp(
                transform.rotation,
                targetRotation,
                rotationSpeed * Time.deltaTime
            );
        }
    }

    void Shoot()
    {
        if (projectilePrefab != null && firePoint != null)
        {
            // Instantiate the projectile and let its own script handle its forward movement
            Instantiate(projectilePrefab, firePoint.position, firePoint.rotation);
        }
    }

    // Draw the detection zones directly in the Scene view for debugging
    void OnDrawGizmosSelected()
    {
        Gizmos.color = Color.yellow;
        Gizmos.DrawWireSphere(transform.position, detectionRange);

        Gizmos.color = Color.red;
        Gizmos.DrawWireSphere(transform.position, shootingRange);
    }
}

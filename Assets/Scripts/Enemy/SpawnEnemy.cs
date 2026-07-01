using UnityEngine;

public class SpawnEnemy : MonoBehaviour
{
    public GameObject enemyPrefab;

    public Transform spawnPoint;

    private bool hasSpawned = false;

    private void OnTriggerEnter2D(Collider2D other)
    {
        if (other.CompareTag("Player") && !hasSpawned)
        {
            Spawn();
        }
    }

    private void Spawn()
    {
        Instantiate(enemyPrefab, spawnPoint.position, spawnPoint.rotation);

        hasSpawned = true;
    }
}

using UnityEngine;

public class Wisp : MonoBehaviour
{
    public void OnTriggerEnter2D(Collider2D other)
    {
        if (other.CompareTag("Player"))
        {
            // Trigger the wisp's effect on the player
            Debug.Log("Wisp triggered by: " + other.name);
            // Add your wisp effect logic here
            //add animation
            Destroy(gameObject); // Destroy the wisp after triggering
        }
    }
}

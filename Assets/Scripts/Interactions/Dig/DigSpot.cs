using UnityEngine;
using UnityEngine.Tilemaps;

public class DigSpot : MonoBehaviour
{
    public Tilemap groundTilemap;
    public Vector3[] positionsToDelete;
    private Digger digger;

    private void OnTriggerEnter2D(Collider2D other)
    {
        if (other.CompareTag("Player"))
        {
            Debug.Log("Player entered dig spot");
            digger = other.GetComponent<Digger>();
            if (digger != null)
                digger.SetCurrentDigSpot(this);
        }
    }

    private void OnTriggerExit2D(Collider2D other)
    {
        if (other.CompareTag("Player"))
        {
            if (digger != null)
                digger.SetCurrentDigSpot(null);
        }
    }

    public void Dig()
    {
        for (int i = 0; i < positionsToDelete.Length; i++)
        {
            Vector3Int coordinate = groundTilemap.WorldToCell(positionsToDelete[i]);

            if (groundTilemap.HasTile(coordinate))
            {
                groundTilemap.SetTile(coordinate, null);
            }
        }
    }
}

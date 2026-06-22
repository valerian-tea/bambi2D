using System.Collections;
using UnityEngine;
using UnityEngine.InputSystem;
using UnityEngine.Tilemaps;

public class Digger : MonoBehaviour
{
    public Tilemap groundTilemap;

    private Animator animator;
    private SpriteRenderer spriteRenderer;

    void Start()
    {
        animator = GetComponent<Animator>();
        spriteRenderer = GetComponent<SpriteRenderer>();
    }

    public void OnSideDig(InputValue value)
    {
        Vector3Int digCoordinate;
        if (spriteRenderer.flipX) // Facing left
        {
            digCoordinate = new Vector3Int(
                Mathf.FloorToInt(this.transform.position.x - 1),
                Mathf.FloorToInt(this.transform.position.y),
                0
            );
        }
        else // Facing right
        {
            digCoordinate = new Vector3Int(
                Mathf.FloorToInt(this.transform.position.x + 1),
                Mathf.FloorToInt(this.transform.position.y),
                0
            );
        }

        if (groundTilemap.HasTile(digCoordinate))
        {
            animator.SetTrigger("digSide");
            StartCoroutine(DigAtWorldPosition(digCoordinate));
        }
    }

    // public void OnUpDig(InputValue value)
    // {
    //     Vector3Int digCoordinate = new Vector3Int(
    //         Mathf.FloorToInt(this.transform.position.x),
    //         Mathf.FloorToInt(this.transform.position.y + 1),
    //         0
    //     );

    //     animator.SetTrigger("dig");
    //     StartCoroutine(DigAtWorldPosition(digCoordinate));
    // }

    public void OnDownDig(InputValue value)
    {
        Vector3Int digCoordinate = new Vector3Int(
            Mathf.FloorToInt(this.transform.position.x),
            Mathf.FloorToInt(this.transform.position.y - 1),
            0
        );

        if (groundTilemap.HasTile(digCoordinate))
        {
            animator.SetTrigger("digDown");
            StartCoroutine(DigAtWorldPosition(digCoordinate));
        }
    }

    private IEnumerator DigAtWorldPosition(Vector3Int worldPos)
    {
        yield return new WaitForSeconds(0.7f);
        Vector3Int coordinate = groundTilemap.WorldToCell(worldPos);
        groundTilemap.SetTile(coordinate, null);
    }
}

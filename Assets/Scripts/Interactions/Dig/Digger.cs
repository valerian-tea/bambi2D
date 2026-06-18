using System.Collections;
using UnityEngine;
using UnityEngine.InputSystem;
using UnityEngine.Tilemaps;

public class Digger : MonoBehaviour
{
    public Tilemap groundTilemap;

    private Animator animator;

    void Start()
    {
        animator = GetComponent<Animator>();
    }

    public void OnLeftDig(InputValue value)
    {
        Vector3Int digCoordinate = new Vector3Int(
            Mathf.FloorToInt(this.transform.position.x - 1),
            Mathf.FloorToInt(this.transform.position.y),
            0
        );

        animator.SetTrigger("dig");
        StartCoroutine(DigAtWorldPosition(digCoordinate));
    }

    public void OnRightDig(InputValue value)
    {
        Vector3Int digCoordinate = new Vector3Int(
            Mathf.FloorToInt(this.transform.position.x + 1),
            Mathf.FloorToInt(this.transform.position.y),
            0
        );

        animator.SetTrigger("dig");
        StartCoroutine(DigAtWorldPosition(digCoordinate));
    }

    public void OnUpDig(InputValue value)
    {
        Vector3Int digCoordinate = new Vector3Int(
            Mathf.FloorToInt(this.transform.position.x),
            Mathf.FloorToInt(this.transform.position.y + 1),
            0
        );

        animator.SetTrigger("dig");
        StartCoroutine(DigAtWorldPosition(digCoordinate));
    }

    public void OnDownDig(InputValue value)
    {
        Vector3Int digCoordinate = new Vector3Int(
            Mathf.FloorToInt(this.transform.position.x),
            Mathf.FloorToInt(this.transform.position.y - 1),
            0
        );

        animator.SetTrigger("dig");
        StartCoroutine(DigAtWorldPosition(digCoordinate));
    }

    private IEnumerator DigAtWorldPosition(Vector3 worldPos)
    {
        yield return new WaitForSeconds(0.7f);
        Vector3Int coordinate = groundTilemap.WorldToCell(worldPos);

        if (groundTilemap.HasTile(coordinate))
        {
            groundTilemap.SetTile(coordinate, null);
        }
    }
}

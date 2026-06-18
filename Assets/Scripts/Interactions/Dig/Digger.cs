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

    // public void OnLeftDig(InputValue value)
    // {
    //     Debug.Log("Interact button pressed");
    //     Debug.Log(value);
    //     Debug.Log(this.transform.position);

    //     Vector3Int digCoordinate = groundTilemap.WorldToCell(this.transform.position);

    //     animator.SetTrigger("dig");
    //     StartCoroutine(Dig());
    // }

    // public void OnRightDig(InputValue value)
    // {
    //     Debug.Log("Interact button pressed");
    //     Debug.Log(value);
    //     Debug.Log(this.transform.position);

    //     Vector3Int digCoordinate = groundTilemap.WorldToCell(this.transform.position);

    //     animator.SetTrigger("dig");
    //     StartCoroutine(Dig());
    // }

    // public void OnUpDig(InputValue value)
    // {
    //     Vector3Int digCoordinate = groundTilemap.WorldToCell(this.transform.position);

    //     animator.SetTrigger("dig");
    //     StartCoroutine(currentDigSpot.Dig());
    // }

    public void OnDownDig(InputValue value)
    {
        Debug.Log("Interact button pressed");
        Debug.Log(this.transform.position);

        Vector3Int digCoordinate = new Vector3Int(
            Mathf.FloorToInt(this.transform.position.x),
            Mathf.FloorToInt(this.transform.position.y - 1),
            0
        );
        Debug.Log(digCoordinate);

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

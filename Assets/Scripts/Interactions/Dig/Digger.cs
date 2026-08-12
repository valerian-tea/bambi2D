using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;
using UnityEngine.Tilemaps;

public class Digger : MonoBehaviour
{
    public Tilemap groundTilemap;

    private Animator animator;
    private SpriteRenderer spriteRenderer;
    public static event Action OnDigAction;

    void Start()
    {
        animator = GetComponent<Animator>();
        spriteRenderer = GetComponent<SpriteRenderer>();
    }

    public void OnSideDig(InputValue value)
    {
        // animator.SetTrigger("digSide");
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
            StartCoroutine(DigAtWorldPosition(digCoordinate));
        }
    }

    public void OnDownDig(InputValue value)
    {
        Vector3Int digCoordinate = new Vector3Int(
            Mathf.FloorToInt(this.transform.position.x),
            Mathf.FloorToInt(this.transform.position.y - 1),
            0
        );

        if (groundTilemap.HasTile(digCoordinate))
        {
            animator.SetInteger("actionID", 1);
            animator.SetTrigger("action");
            StartCoroutine(DigAtWorldPosition(digCoordinate));
        }
    }

    private IEnumerator DigAtWorldPosition(Vector3Int worldPos)
    {
        yield return new WaitForSeconds(0.3f);
        Vector3Int coordinate = groundTilemap.WorldToCell(worldPos);
        groundTilemap.SetTile(coordinate, null);
        OnDigAction?.Invoke();
    }
}

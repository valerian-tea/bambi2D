using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Tilemaps;

public class FallingBlock : MonoBehaviour
{
    public Tilemap groundTilemap;

    private Rigidbody2D rb;
    private bool isFalling = false;
    private Vector3Int[] supportPositions;
    private Digger digger;

    void Start()
    {
        if (digger == null)
            digger = FindAnyObjectByType<Digger>();

        rb = GetComponent<Rigidbody2D>();
    }

    private void OnEnable()
    {
        if (digger == null)
            digger = FindAnyObjectByType<Digger>();

        Digger.OnDigAction += CheckSupport;
    }

    private void OnDisable()
    {
        Digger.OnDigAction -= CheckSupport;
    }

    private void TriggerFall()
    {
        isFalling = true;
        rb.bodyType = RigidbodyType2D.Dynamic;
    }

    public void CheckSupport()
    {
        if (isFalling || groundTilemap == null)
            return;

        TileBase[] supportTiles = GetPositionsAboveTopmostPoint();

        if (supportTiles.Length == 0)
        {
            TriggerFall();
            return;
        }
    }

    private TileBase[] GetPositionsAboveTopmostPoint()
    {
        Bounds bounds = GetBounds();
        float topY = bounds.max.y;

        int leftPoint = Mathf.FloorToInt(bounds.min.x);
        int rightPoint = Mathf.FloorToInt(bounds.max.x);
        int topPoint = Mathf.FloorToInt(topY);

        List<TileBase> tileList = new List<TileBase>();
        for (int i = 0; i <= rightPoint - leftPoint; i++)
        {
            Vector3Int gridPos = new Vector3Int(leftPoint + i, topPoint, 0);
            if (groundTilemap.HasTile(gridPos))
            {
                TileBase tileAbove = groundTilemap.GetTile(gridPos);
                tileList.Add(tileAbove);
            }
        }

        Debug.Log("Positions above topmost point: " + tileList.Count);

        return tileList.ToArray();
    }

    private Bounds GetBounds()
    {
        Collider2D collider = GetComponent<Collider2D>(); //Make sure the top of collider is just above a grid line
        return collider.bounds;
    }
}

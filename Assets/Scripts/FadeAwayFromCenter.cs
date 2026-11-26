using System;
using UnityEngine;

public class FadeAwayFromCenter : MonoBehaviour
{
    [Header("References")]
    public Transform playerTransform;

    // public Transform objectTransform;

    private SpriteRenderer spriteRenderer;
    private Color initialColor;

    [Header("Fade")]
    public float maxDistance = 10f;
    public Color targetColor = Color.black;

    void Start()
    {
        spriteRenderer = GetComponent<SpriteRenderer>();
        if (spriteRenderer != null)
            initialColor = spriteRenderer.color;
    }

    void FixedUpdate()
    {
        if (playerTransform == null || spriteRenderer == null)
            return;

        float distance = Math.Abs(
            playerTransform.position.x - this.gameObject.transform.position.x
        );

        Color blended = Color.Lerp(targetColor, initialColor, distance / maxDistance);
        spriteRenderer.color = blended;
    }
}

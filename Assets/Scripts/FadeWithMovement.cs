using UnityEngine;

public class FadeWithMovement : MonoBehaviour
{
    [Header("References")]
    public Transform playerTransform;
    public Collider2D fadeCollider;
    private SpriteRenderer spriteRenderer;

    private float leftPoint;
    private float rightPoint;

    void Start()
    {
        spriteRenderer = GetComponent<SpriteRenderer>();
        leftPoint = Mathf.FloorToInt(fadeCollider.bounds.min.x);
        rightPoint = Mathf.FloorToInt(fadeCollider.bounds.max.x);
    }

    void FixedUpdate()
    {
        if (playerTransform == null)
            return;

        float playerX = playerTransform.position.x;

        float progress = Mathf.InverseLerp(leftPoint, rightPoint, playerX);

        // Invert progress so moving right (1) decreases alpha, moving left (0) increases it
        float alpha = 1f - progress;

        // Apply the new alpha to the sprite color
        Color currentColor = spriteRenderer.color;
        currentColor.a = alpha;
        spriteRenderer.color = currentColor;
    }
}

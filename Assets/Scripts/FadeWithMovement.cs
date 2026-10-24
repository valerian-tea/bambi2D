using UnityEngine;

public class FadeWithMovement : MonoBehaviour
{
    [Header("References")]
    public Transform playerTransform;
    public Collider2D fadeCollider;
    private SpriteRenderer spriteRenderer;

    [Header("Fade")]
    [Tooltip("Color to blend the sprite towards as the player moves across the collider.")]
    public Color targetColor = Color.black;

    private Color initialColor;

    private float leftPoint;
    private float rightPoint;

    void Start()
    {
        spriteRenderer = GetComponent<SpriteRenderer>();
        if (spriteRenderer != null)
            initialColor = spriteRenderer.color;
        leftPoint = Mathf.FloorToInt(fadeCollider.bounds.min.x);
        rightPoint = Mathf.FloorToInt(fadeCollider.bounds.max.x);
    }

    void FixedUpdate()
    {
        if (playerTransform == null)
            return;

        float playerX = playerTransform.position.x;

        float progress = Mathf.InverseLerp(leftPoint, rightPoint, playerX);

        Color blended = Color.Lerp(initialColor, targetColor, progress);
        spriteRenderer.color = blended;
    }
}

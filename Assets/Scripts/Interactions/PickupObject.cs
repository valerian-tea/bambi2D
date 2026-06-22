using UnityEngine;
using UnityEngine.InputSystem;
using Yarn.Unity;

public class PickupObject : MonoBehaviour
{
    public Transform mouth;
    public Vector3 mouthOffset = new Vector3(0.3f, -0.12f, 0f);
    public float pickupRadius = 0.5f;
    public LayerMask pickupLayer;
    public VariableStorageBehaviour variableStorage;
    private GameObject carriedObject;
    private Collider2D carriedCollider;
    private SpriteRenderer carriedSprite;
    private Rigidbody2D carriedRb;
    private PlayerInput input;
    private SpriteRenderer playerSprite;

    void Start()
    {
        playerSprite = GetComponent<SpriteRenderer>();
        input = GetComponent<PlayerInput>();
    }

    void FixedUpdate()
    {
        if (carriedObject != null)
        {
            Vector3 currentOffset = mouthOffset;
            if (playerSprite.flipX)
                currentOffset.x *= -1f;

            carriedObject.transform.SetParent(mouth, worldPositionStays: false);
            carriedObject.transform.localPosition = currentOffset;
            carriedSprite.flipX = playerSprite.flipX;
        }
    }

    public void OnCrouch(InputValue value)
    {
        if (carriedObject == null)
            TryPickup();
        else
            Drop();
    }

    private void TryPickup()
    {
        Collider2D[] hits = Physics2D.OverlapCircleAll(
            transform.position,
            pickupRadius,
            pickupLayer
        );

        Debug.Log($"Found {hits.Length} colliders in pickup radius");
        Collider2D best = null;
        float bestDist = float.MaxValue;
        foreach (var c in hits)
        {
            float d = Vector2.Distance(mouth.position, c.transform.position);
            if (d < bestDist)
            {
                bestDist = d;
                best = c;
            }
        }

        if (best == null)
            return;

        carriedObject = best.gameObject;
        carriedCollider = carriedObject.GetComponent<Collider2D>();
        carriedSprite = carriedObject.GetComponent<SpriteRenderer>();
        carriedObject.transform.SetParent(mouth, worldPositionStays: false);
        Vector3 currentOffset = mouthOffset;
        if (playerSprite != null && playerSprite.flipX)
            currentOffset.x *= -1f;
        carriedObject.transform.localPosition = currentOffset;
    }

    private void Drop()
    {
        Destroy(carriedObject);

        if (variableStorage.TryGetValue("$hasBone", out bool hasBone) && hasBone)
        {
            variableStorage.SetValue("$hasBone", false);
            Debug.Log("Dropped the bone and updated $hasBone to 0.");
        }
    }

    void OnDrawGizmosSelected()
    {
        if (mouth != null)
        {
            Gizmos.color = Color.yellow;
            Gizmos.DrawWireSphere(mouth.position, pickupRadius);
        }
    }
}

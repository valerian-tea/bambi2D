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
    private PlayerInput input;
    private SpriteRenderer playerSprite;
    private GameObject carriedObject;
    private Collider2D carriedCollider;
    private SpriteRenderer carriedSprite;
    private Rigidbody2D carriedRb;

    void Start()
    {
        playerSprite = GetComponent<SpriteRenderer>();
        input = GetComponent<PlayerInput>();
    }

    void FixedUpdate()
    {
        if (carriedObject != null)
        {
            AlignItem();
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

    private void AlignItem()
    {
        carriedObject.transform.SetParent(mouth);
        Vector3 currentOffset = mouthOffset;
        if (playerSprite.flipX)
            currentOffset.x *= -1f;
        Debug.Log($"Aligning item to mouth with offset {currentOffset}");
        carriedSprite.flipX = playerSprite.flipX;
        carriedObject.transform.localPosition = currentOffset;
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

using UnityEngine;

public class PickupObject : MonoBehaviour
{
    public Vector3 mouthOffset = new Vector3(0.3f, 0f, 0f);
    public Transform playerMouth;
    public SpriteRenderer playerSprite;
    private SpriteRenderer itemSprite;
    private Collider2D collider2D;
    private bool isCarried = false;

    void Start()
    {
        collider2D = GetComponent<Collider2D>();
        itemSprite = GetComponent<SpriteRenderer>();
    }

    private void OnTriggerEnter2D(Collider2D other)
    {
        if (other.CompareTag("Player"))
        {
            Equip();
        }
    }

    public void Equip()
    {
        isCarried = true;
    }

    void Update()
    {
        if (isCarried)
        {
            collider2D.enabled = false;
            itemSprite.flipX = playerSprite.flipX;
            Vector3 currentOffset = mouthOffset;

            if (playerSprite.flipX)
            {
                currentOffset.x *= -1f;
            }

            transform.SetParent(playerMouth);
            transform.localPosition = currentOffset;
        }
    }
}

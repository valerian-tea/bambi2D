using UnityEngine;
using Yarn.Unity;

public class ObjectCommands : MonoBehaviour
{
    private Collider2D collider;

    void Start()
    {
        collider = GetComponent<Collider2D>();
    }

    [YarnCommand("disable_collider")]
    public void DisableCollider()
    {
        if (collider != null)
        {
            collider.enabled = false;
        }
    }

    [YarnCommand("disable_object")]
    public void DisableObject()
    {
        this.gameObject.SetActive(false);
    }

    [YarnCommand("destroy_item")]
    public void DestroyItem(GameObject item)
    {
        Destroy(item);
        Debug.Log($"Destroyed item: {item.name}");
    }
}

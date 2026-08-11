using UnityEngine;
using Yarn.Unity;

public class ObjectCommands : MonoBehaviour
{
    [YarnCommand("disable_collider")]
    public void DisableCollider(GameObject item)
    {
        Collider2D collider = item.GetComponent<Collider2D>();
        if (collider != null)
        {
            collider.enabled = false;
        }
    }

    [YarnCommand("disable_object")]
    public void DisableObject(GameObject item)
    {
        item.SetActive(false);
    }

    [YarnCommand("destroy_item")]
    public void DestroyItem(GameObject item)
    {
        Destroy(item);
        Debug.Log($"Destroyed item: {item.name}");
    }
}

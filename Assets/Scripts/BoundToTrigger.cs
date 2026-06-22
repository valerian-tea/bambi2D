using UnityEngine;
using Yarn.Unity;

public class BoundToTrigger : MonoBehaviour
{
    private Collider2D collider;

    void Start()
    {
        collider = GetComponent<Collider2D>();
    }

    [YarnCommand("bound_to_trigger")]
    public void ConvertToTrigger()
    {
        collider.isTrigger = true;
    }
}

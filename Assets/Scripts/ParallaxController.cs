using UnityEngine;

public class ParallaxController : MonoBehaviour
{
    public float scrollSpeed = 0.5f;
    private MeshRenderer meshRenderer;

    void Start()
    {
        meshRenderer = GetComponent<MeshRenderer>();
    }

    void Update()
    {
        // Increment the X offset based on time and speed
        Vector2 offset = new Vector2(Time.time * scrollSpeed, 0);
        meshRenderer.material.mainTextureOffset = offset;
    }
}

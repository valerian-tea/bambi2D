using UnityEngine;

public class Revealer : MonoBehaviour
{
    public GameObject targetObject;

    void Start()
    {
        if (targetObject != null)
            targetObject.SetActive(false);
    }

    private void OnTriggerEnter2D(Collider2D other)
    {
        if (other.CompareTag("Player"))
        {
            targetObject.SetActive(true);
            Debug.Log("Player entered revealer trigger, revealing object.");
        }
    }
}

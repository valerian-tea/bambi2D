using Unity.Cinemachine; // Use "using Cinemachine;" if using older Cinemachine versions
using UnityEngine;

public class RoomTrigger : MonoBehaviour
{
    [Header("Camera Settings")]
    [SerializeField]
    private GameObject roomVirtualCamera;

    private void OnTriggerEnter2D(Collider2D other)
    {
        if (other.CompareTag("Player"))
        {
            if (roomVirtualCamera != null)
            {
                roomVirtualCamera.SetActive(true);
            }
        }
    }

    private void OnTriggerExit2D(Collider2D other)
    {
        if (other.CompareTag("Player"))
        {
            if (roomVirtualCamera != null)
            {
                roomVirtualCamera.SetActive(false);
            }
        }
    }
}

using Unity.Cinemachine; // Use 'using Cinemachine;' if you are on Cinemachine v2
using UnityEngine;

public class ChangeCamera : MonoBehaviour
{
    [Header("Camera Settings")]
    [SerializeField]
    private CinemachineCamera triggerCamera;

    private int activePriority = 5;

    private int inactivePriority = 1;

    private void OnTriggerEnter2D(Collider2D other)
    {
        if (other.CompareTag("Player") && triggerCamera != null)
        {
            triggerCamera.Priority = activePriority;
        }
    }

    private void OnTriggerExit2D(Collider2D other)
    {
        if (other.CompareTag("Player") && triggerCamera != null)
        {
            triggerCamera.Priority = inactivePriority;
        }
    }

    public void SetCameraPriority(int priority)
    {
        if (triggerCamera != null)
        {
            triggerCamera.Priority = priority;
        }
    }
}

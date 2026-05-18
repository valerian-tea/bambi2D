using UnityEngine;
using UnityEngine.InputSystem;

public class Digger : MonoBehaviour
{
    private DigSpot currentDigSpot;

    public void SetCurrentDigSpot(DigSpot spot)
    {
        currentDigSpot = spot;
    }

    public void OnInteract(InputValue value)
    {
        if (currentDigSpot != null)
        {
            currentDigSpot.Dig();
        }
    }
}

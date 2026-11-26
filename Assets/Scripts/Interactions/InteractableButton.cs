using UnityEngine;
using UnityEngine.InputSystem;
using Yarn.Unity;

public class InteractableButton : MonoBehaviour
{
    [Header("UI Reference")]
    [SerializeField]
    private GameObject interactCanvas;

    [Header("Dialogue Reference")]
    [SerializeField]
    public string dialogueNode;

    private PlayerInput playerInputReference;
    private DialogueRunner dialogueRunner;

    private void Start()
    {
        if (interactCanvas != null)
        {
            interactCanvas.SetActive(false);
        }
        dialogueRunner = FindAnyObjectByType<DialogueRunner>();
    }

    private void OnTriggerEnter2D(Collider2D other)
    {
        if (other.CompareTag("Player"))
        {
            // Find the PlayerInput component on the player object that entered
            playerInputReference = other.GetComponent<PlayerInput>();

            if (playerInputReference != null)
            {
                if (interactCanvas != null)
                {
                    interactCanvas.SetActive(true);
                }

                // Dynamically subscribe to the "Interact" event using its string name
                playerInputReference.actions["Interact"].performed += OnInteractPressed;
            }
        }
    }

    private void OnTriggerExit2D(Collider2D other)
    {
        if (other.CompareTag("Player"))
        {
            CleanUpInput();
        }
    }

    private void OnInteractPressed(InputAction.CallbackContext context)
    {
        PerformInteraction();
    }

    private void PerformInteraction()
    {
        Debug.Log("Interacted with: " + gameObject.name);
        dialogueRunner.StartDialogue(dialogueNode);
        interactCanvas.SetActive(false);
        CleanUpInput();
    }

    private void CleanUpInput()
    {
        if (interactCanvas != null)
        {
            interactCanvas.SetActive(false);
        }

        // Always unsubscribe to prevent memory leaks and ghost inputs
        if (playerInputReference != null)
        {
            playerInputReference.actions["Interact"].performed -= OnInteractPressed;
            playerInputReference = null;
        }
    }

    private void OnDisable()
    {
        CleanUpInput();
    }
}

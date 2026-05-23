using UnityEngine;
using UnityEngine.InputSystem;
using Yarn.Unity;

public class DialogueTrigger : MonoBehaviour
{
    private DialogueRunner dialogueRunner;
    public string dialogueNode;

    private void Start()
    {
        dialogueRunner = FindFirstObjectByType<DialogueRunner>();
        if (dialogueRunner == null)
        {
            Debug.LogError("DialogueRunner not found in the scene.");
        }
    }

    private void OnTriggerEnter2D(Collider2D other)
    {
        if (other.CompareTag("Player"))
        {
            OpenDialogue();
        }
    }

    public void OpenDialogue()
    {
        dialogueRunner.StartDialogue(dialogueNode);
    }

    [YarnCommand("CloseDialogue")]
    public void CloseDialogue()
    {
        // dialogueRunner.Stop();
        this.gameObject.SetActive(false);
    }
}

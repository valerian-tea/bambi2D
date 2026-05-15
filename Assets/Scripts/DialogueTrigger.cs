using UnityEngine;
using UnityEngine.InputSystem;
using Yarn.Unity;

public class DialogueTrigger : MonoBehaviour
{
    public DialogueRunner dialogueRunner;
    public string dialogueNode;

    private void OnTriggerEnter2D(Collider2D other)
    {
        if (other.CompareTag("Player"))
        {
            OpenDialogue();
        }
    }

    void OpenDialogue()
    {
        dialogueRunner.StartDialogue(dialogueNode);
    }

    void CloseDialogue()
    {
        dialogueRunner.Stop();
    }
}

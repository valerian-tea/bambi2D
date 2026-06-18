using UnityEngine;
using UnityEngine.InputSystem;
using Yarn.Unity;

public class DialogueCommands : MonoBehaviour
{
    private DialogueRunner dialogueRunner;

    private void Start()
    {
        dialogueRunner = FindFirstObjectByType<DialogueRunner>();
        if (dialogueRunner == null)
        {
            Debug.LogError("DialogueRunner not found in the scene.");
        }
    }

    public void OpenDialogue(string dialogueNode)
    {
        dialogueRunner.StartDialogue(dialogueNode);
    }
}

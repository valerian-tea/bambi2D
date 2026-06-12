using UnityEngine;
using UnityEngine.InputSystem;
using Yarn.Unity;

public class InstructionCommands : MonoBehaviour
{
    [YarnCommand("enable_text")]
    public void EnableTextBox(CanvasGroup canvasGroup)
    {
        canvasGroup.alpha = 1;
        canvasGroup.interactable = true;
    }

    [YarnCommand("disable_text")]
    public void DisableTextBox(CanvasGroup canvasGroup)
    {
        canvasGroup.alpha = 0;
        canvasGroup.interactable = false;
    }
}

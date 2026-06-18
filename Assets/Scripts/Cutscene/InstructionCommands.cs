using UnityEngine;
using UnityEngine.InputSystem;
using Yarn.Unity;

public class InstructionCommands : MonoBehaviour
{
    [YarnCommand("enable_UI")]
    public void EnableUIElement(CanvasGroup canvasGroup)
    {
        canvasGroup.alpha = 1;
        canvasGroup.interactable = true;
    }

    [YarnCommand("disable_UI")]
    public void DisableUIElement(CanvasGroup canvasGroup)
    {
        canvasGroup.alpha = 0;
        canvasGroup.interactable = false;
    }
}

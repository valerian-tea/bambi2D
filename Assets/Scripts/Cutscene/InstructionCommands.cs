using UnityEngine;
using UnityEngine.InputSystem;
using Yarn.Unity;

public class InstructionCommands : MonoBehaviour
{
    public GameObject textBox;

    private void Start()
    {
        if (textBox == null)
        {
            Debug.LogError("TextBox not found in the scene.");
        }
    }

    [YarnCommand("enable_text")]
    public void EnableTextBox()
    {
        textBox.SetActive(true);
    }

    [YarnCommand("disable_text")]
    public void DisableTextBox()
    {
        textBox.SetActive(false);
    }
}

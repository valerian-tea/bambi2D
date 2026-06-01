using System.Collections;
using UnityEngine;
using Yarn.Unity;

public class AnimatorController : MonoBehaviour
{
    private Animator animator;

    void Start()
    {
        animator = GetComponent<Animator>();
    }

    [YarnCommand("set_emotion")]
    public void SetEmotion(string emotion)
    {
        if (emotion == "sad")
            animator.SetBool("isSad", true);
        else if (emotion == "happy")
            animator.SetTrigger("isWagging");
        else if (emotion == "neutral")
            animator.SetBool("isSad", false);
    }

    public IEnumerator Wag()
    {
        animator.SetTrigger("isWagging");
        yield return new WaitForSeconds(1.0f);
    }

    public IEnumerator Sad()
    {
        animator.SetBool("isSad", true);
        yield return new WaitForSeconds(1.0f);
    }
}

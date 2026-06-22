using UnityEngine;
using UnityEngine.Playables;
using Yarn.Unity;

public class CutsceneTrigger : MonoBehaviour
{
    public PlayableDirector director;

    private void OnTriggerEnter2D(Collider2D other)
    {
        Debug.Log("Player entered cutscene trigger.");
        if (other.CompareTag("Player"))
        {
            director.Play();
            gameObject.SetActive(false);
        }
    }

    [YarnCommand("play_timeline")]
    public void PlayTimeline()
    {
        director.Play();
    }
}

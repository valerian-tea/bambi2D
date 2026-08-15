using UnityEngine;
using UnityEngine.Playables;
using Yarn.Unity;

public class PlayCutscene : MonoBehaviour
{
    private PlayableDirector director;
    private Collider2D cutsceneTrigger;

    void Awake()
    {
        director = GetComponent<PlayableDirector>();
        cutsceneTrigger = GetComponent<Collider2D>();
    }

    [YarnCommand("play_timeline")]
    public void PlayTimeline()
    {
        director.Play();
    }

    private void OnTriggerEnter2D(Collider2D other)
    {
        if (other.CompareTag("Player"))
        {
            director.Play();
            cutsceneTrigger.enabled = false;
        }
    }
}

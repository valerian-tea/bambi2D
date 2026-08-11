using UnityEngine;
using UnityEngine.Playables;
using Yarn.Unity;

public class PlayCutscene : MonoBehaviour
{
    private PlayableDirector director;

    void Awake()
    {
        director = GetComponent<PlayableDirector>();
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
            gameObject.SetActive(false);
        }
    }
}

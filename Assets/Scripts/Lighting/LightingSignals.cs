using System.Collections;
using UnityEngine;
using UnityEngine.Rendering.Universal;
using Yarn.Unity;

public class LightingSignals : MonoBehaviour
{
    private Coroutine fadeCoroutine;

    [SerializeField]
    private float fadeDuration = 3.0f;
    private Light2D light2D;

    void Start()
    {
        light2D = GetComponent<Light2D>();
    }

    [YarnCommand("set_light_intensity")]
    public void SetLightIntensity(float targetValue)
    {
        light2D.intensity = targetValue;
    }

    [YarnCommand("fade_light")]
    public void StartFade(float targetValue)
    {
        StartCoroutine(FadeLightIntensity(light2D.intensity, targetValue));
    }

    private IEnumerator FadeLightIntensity(float startValue, float targetValue)
    {
        float timeElapsed = 0f;

        while (timeElapsed < fadeDuration)
        {
            timeElapsed += Time.deltaTime;
            light2D.intensity = Mathf.Lerp(startValue, targetValue, timeElapsed / fadeDuration);
            yield return null;
        }

        light2D.intensity = targetValue;
    }
}

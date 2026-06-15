using System.Collections;
using UnityEngine;
using UnityEngine.Rendering.Universal;
using Yarn.Unity;

public class LightingSignals : MonoBehaviour
{
    private Coroutine fadeCoroutine;
    public Light2D globalLight;

    [SerializeField]
    private float fadeDuration = 3.0f;

    public void StartFade(float targetValue)
    {
        StartCoroutine(FadeLightIntensity(globalLight.intensity, targetValue));
    }

    private IEnumerator FadeLightIntensity(float startValue, float targetValue)
    {
        float timeElapsed = 0f;

        while (timeElapsed < fadeDuration)
        {
            timeElapsed += Time.deltaTime;
            globalLight.intensity = Mathf.Lerp(startValue, targetValue, timeElapsed / fadeDuration);
            Debug.Log($"Fading light: {globalLight.intensity}");
            yield return null; // Wait for the next frame
        }

        globalLight.intensity = targetValue; // Ensure precision at the end
    }
}

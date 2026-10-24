using UnityEngine;
using UnityEngine.Rendering.Universal;

public class ShadowTriggerZone : MonoBehaviour
{
    [SerializeField]
    private float targetIntensity = 0.4f;
    public Light2D light2D;
    private LightingSignals lightingSignals;
    private float originalIntensity = 1.0f;

    void Start()
    {
        lightingSignals = light2D.GetComponent<LightingSignals>();
        originalIntensity = light2D.intensity;
    }

    private void OnTriggerEnter2D(Collider2D other)
    {
        if (!other.CompareTag("Player"))
            return;

        lightingSignals.StartFade(targetIntensity);
    }

    private void OnTriggerExit2D(Collider2D other)
    {
        if (!other.CompareTag("Player"))
            return;

        lightingSignals.StartFade(originalIntensity);
    }
}

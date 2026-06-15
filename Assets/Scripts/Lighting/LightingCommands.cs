using System.Collections;
using UnityEngine;
using UnityEngine.Rendering.Universal;
using Yarn.Unity;

public class LightingCommands : MonoBehaviour
{
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
}

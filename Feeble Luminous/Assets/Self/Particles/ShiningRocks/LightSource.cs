using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LightSource : MonoBehaviour
{
    public const float maxLightThreshold = 60f;
    public const float minLightThreshold = 0f;

    [Range(minLightThreshold, maxLightThreshold)]
    public float intensity;
    public Renderer rend;

    public void Start()
    {
        rend = GetComponent<Renderer>();
        Color final = Color.white * Mathf.GammaToLinearSpace(intensity / 8);
        rend.material.SetColor("_EmissiveColor", final);
        DynamicGI.SetEmissive(rend, final);
        //rend = GetComponent<Renderer>();
        //rend.material.SetColor("_EmissiveColor", Color.white * intensity);
    }

    public virtual void RefreshLight()
    {
        Color final = Color.white * Mathf.GammaToLinearSpace(intensity / 8);
        rend.material.SetColor("_EmissiveColor", final);
        DynamicGI.SetEmissive(rend, final);

        //rend.material.SetColor("_EmissiveColor", Color.white * intensity);
    }
}

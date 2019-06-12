using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ResetRock : LightSource
{
    private Light resetLight;
    private ParticleSystem lightParticles;
    private ParticleSystem.EmissionModule emission;

    private new void Start()
    {
        base.Start();
        resetLight = GetComponentInChildren<Light>();
        lightParticles = GetComponentInChildren<ParticleSystem>();
        emission = lightParticles.emission;
        StartCoroutine(SetLightToZero());
    }

    public override void RefreshLight()
    {
        base.RefreshLight();
        resetLight.intensity = 150000 * intensity;
        emission.rateOverTime = intensity * 0.25f;
        if(intensity == 0)
        {
            transform.tag = "Untagged";
        }
    }

    IEnumerator SetLightToZero()
    {
        yield return new WaitForSeconds(0.01f);
        resetLight.intensity = 0;
        RefreshLight();

    }
}

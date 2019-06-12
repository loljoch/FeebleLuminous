using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Door : MonoBehaviour
{
    MeshRenderer[] stones;
    public Transform particlePosition;
    public Material emmisiveMaterial;
    public float intensity;

    private void Start()
    {
        stones = GetComponentsInChildren<MeshRenderer>();
        for (int i = 0; i < stones.Length; i++)
        {
            stones[i].material = emmisiveMaterial;
        }
    }

    public void GiveLightToStones()
    {
        Color final = Color.white * Mathf.GammaToLinearSpace(intensity / 8);
        for (int i = 0; i < stones.Length; i++)
        {
            stones[i].material.SetColor("_EmissiveColor", final);
            DynamicGI.SetEmissive(stones[i], final);
        }

    }

    public IEnumerator OpenDoor()
    {
        float tempIntensity = intensity;

        for (int i = 0; i < tempIntensity; i++)
        {
            yield return new WaitForSeconds(0.01f);
            intensity -= 2;
            GiveLightToStones();
            if(intensity < 20)
            {
                Destroy(gameObject);
            }
        }

    }
}

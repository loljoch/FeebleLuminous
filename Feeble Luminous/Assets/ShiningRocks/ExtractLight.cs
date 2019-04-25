using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ExtractLight : MonoBehaviour
{
    public KeyCode lessenLightKey;
    [Range(0, 200)]
    public float intensity;
    Renderer rend;
    

    private void Start()
    {
        rend = GetComponent<Renderer>();
        rend.material.SetColor("_EmissiveColor", Color.white * intensity);
    }


    // Update is called once per frame
    void Update()
    {

        if (Input.GetKey(lessenLightKey))
        {
            if (intensity <= 0.1f)
            {
                intensity = 0;
            } else
            {
                if (intensity > 2) {
                    intensity--;
                } else
                {
                    intensity -= 0.1f;
                }
                rend.material.SetColor("_EmissiveColor", Color.white * intensity);
            } 
            
        } 
    }
}

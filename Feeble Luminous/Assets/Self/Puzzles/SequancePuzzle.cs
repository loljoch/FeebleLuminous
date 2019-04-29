using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SequancePuzzle : Puzzle
{
    LightSource[] lightsources;

    // Start is called before the first frame update
    void Start()
    {
        lightsources = GetComponentsInChildren<LightSource>();
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}

using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class OnStartUp : MonoBehaviour
{
    public Animator ScreenFade;

    private void Start()
    {
        ScreenFade.SetBool("isReverse", true);
    }

    
}

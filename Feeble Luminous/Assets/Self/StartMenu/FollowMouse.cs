using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FollowMouse : MonoBehaviour
{
    public int zPos = 2;
    Vector3 mousePosition;

    private void Awake()
    {
        Cursor.visible = false;
    }

    private void Update()
    {
        mousePosition = Input.mousePosition;
        mousePosition.z = zPos;

        transform.position = Camera.main.ScreenToWorldPoint(mousePosition); 
    }
}

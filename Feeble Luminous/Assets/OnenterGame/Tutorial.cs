using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Tutorial : MonoBehaviour
{
    public int lmbShown, rmbShown;
    public bool textActivated = false;
    Camera camera;
    RaycastHit hit;

    public GameObject lmbText, rmbText;

    private void Start()
    {
        camera = Camera.main;
    }

    void Update()
    {
        ShootRay();
        CheckIfTutorialIsOver();
    }


    void ShootRay()
    {
        //Shoots a raycast from the camera's position and gives back name of object
        if (Physics.Raycast(camera.transform.position, camera.transform.forward, out hit, 10))
        {
            if (hit.transform.tag != null)
            {
                if (hit.transform.CompareTag("Interactable") && hit.transform.gameObject.GetComponent<LightSource>() != null && !textActivated)
                {
                    if (hit.transform.gameObject.GetComponent<LightSource>().intensity > 0)
                    {
                        rmbShown++;
                        StartCoroutine(ActivateText(rmbText));
                        
                    } else
                    {
                        lmbShown++;
                        StartCoroutine(ActivateText(lmbText));
                    }
                }
            }
        }
    }

    void CheckIfTutorialIsOver()
    {
        if(lmbShown >= 2 && rmbShown >= 2)
        {
            enabled = false;
        }
    }

    IEnumerator ActivateText(GameObject text)
    {
        textActivated = true;
        text.SetActive(true);
        yield return new WaitForSeconds(3);
        text.SetActive(false);
        textActivated = false;
    }
}

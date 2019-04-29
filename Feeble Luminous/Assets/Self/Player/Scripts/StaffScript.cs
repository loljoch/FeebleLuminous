using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class StaffScript : LightSource
{
    public int transferToLightButton, transferFromLightButton;
    [SerializeField] private float extractTime;
    [Range(0f, 200f)]
    [SerializeField] private float interactableRange;

    public ParticleSystem transferParticles;
    private RaycastHit hit;
    private RaycastHit testHit;

    private Camera camera;

    private new void Start()
    {
        base.Start();
        camera = Camera.main;
    }

    // Update is called once per frame
    void Update()
    {
        HandleInput();
    }

    void HandleInput()
    {
        if (Input.GetMouseButtonDown(transferToLightButton))
        {
            ShootRay("TransferLightTo");
        } else if (Input.GetMouseButtonDown(transferFromLightButton))
        {
            ShootRay("TransferLightFrom");
        }

    }

    private void ShootRay(string methodName)
    {

        Debug.Log("shot ray");
        
        //Shoots a raycast from the camera's position and gives back name of object
        if (Physics.Raycast(camera.transform.position, camera.transform.forward, out hit, interactableRange))
        {
            Debug.DrawLine(camera.transform.position, (camera.transform.forward.normalized*interactableRange) + camera.transform.position, Color.red, 10f);
            Debug.Log(hit.transform.tag);
            if (hit.transform.tag != null)
            {
                if (hit.transform.CompareTag("Interactable") && hit.transform.gameObject.GetComponent<LightSource>() != null)
                {
                    StartCoroutine(methodName, hit.transform.gameObject);
                }
            }
            


        }
    }

    private bool ShootTestRay(GameObject testObject)
    {

        if (Physics.Raycast(camera.transform.position, camera.transform.forward, out testHit, interactableRange))
        {
            if (testHit.transform.gameObject == testObject)
            {
                return true;
            } else
            {
                return false;
            }

        } else
        {
            return false;
        }
    }

    IEnumerator TransferLightTo(GameObject otherObject)
    {
        yield return new WaitForSeconds(minLightThreshold);

        LightSource otherLightSource = otherObject.GetComponent<LightSource>();
        float newIntensity, newOtherIntensity;
        float forLoopValue = 0;

        //Sees if we can insert light into other object and proceeds to do so if stone isn't full yet
        if (otherLightSource.intensity < maxLightThreshold)
        {
            Debug.Log("yeet");
            float tempIntensitiesAddUp = otherLightSource.intensity + intensity;
            

            if (tempIntensitiesAddUp > maxLightThreshold)
            {
                newIntensity = tempIntensitiesAddUp % maxLightThreshold;
                newOtherIntensity = maxLightThreshold;
                forLoopValue = maxLightThreshold - otherLightSource.intensity;

            } else if (tempIntensitiesAddUp <= maxLightThreshold)
            {
                newIntensity = minLightThreshold;
                newOtherIntensity = tempIntensitiesAddUp;
                forLoopValue = intensity;
            }
        }

        StartCoroutine(TransferLight(forLoopValue, otherLightSource, true));

    }

    IEnumerator TransferLightFrom(GameObject otherObject)
    {
        yield return new WaitForSeconds(minLightThreshold);

        LightSource otherLightSource = otherObject.GetComponent<LightSource>();
        float newIntensity, newOtherIntensity;
        float forLoopValue = 0;

        //Sees if we can take light from the other object and proceeds to do so if staff isn't full yet
        if (intensity < maxLightThreshold)
        {
            float tempIntensitiesAddUp = otherLightSource.intensity + intensity;


            if (tempIntensitiesAddUp > maxLightThreshold)
            {
                newOtherIntensity = tempIntensitiesAddUp % maxLightThreshold;
                newIntensity = maxLightThreshold;
                forLoopValue = maxLightThreshold - intensity;
            } else if (tempIntensitiesAddUp <= maxLightThreshold)
            {
                newOtherIntensity = minLightThreshold;
                newIntensity = tempIntensitiesAddUp;
                forLoopValue = otherLightSource.intensity;
            }
        }

        StartCoroutine(TransferLight(forLoopValue, otherLightSource, false));

    }

    IEnumerator TransferLight(float loopValue, LightSource otherObject, bool extractSelf)
    {
        switch (extractSelf)
        {
            case true:
                ParticleSystem tempParticles = Instantiate(transferParticles, transform.position, Quaternion.LookRotation(Vector3.forward));
                tempParticles.GetComponent<particleAttractorMove>().target = otherObject.transform;
                tempParticles.Play();
                for (int i = 0; i < loopValue; i++)
                {
                    intensity--;
                    otherObject.intensity++;
                    otherObject.RefreshLight();
                    RefreshLight();
                    if (Input.GetMouseButton(transferToLightButton) && ShootTestRay(otherObject.gameObject))
                    {
                        tempParticles.transform.position = transform.position;
                        tempParticles.GetComponent<particleAttractorMove>().target = otherObject.transform;
                        yield return new WaitForSeconds(extractTime);
                    } else
                    {
                        break;
                    }
                }
                tempParticles.Stop();
                break;

            case false:
                tempParticles = Instantiate(transferParticles, hit.point, Quaternion.LookRotation(Vector3.back));
                tempParticles.GetComponent<particleAttractorMove>().target = transform;
                tempParticles.Play();
                for (int i = 0; i < loopValue; i++)
                {
                    
                    intensity++;
                    otherObject.intensity--;
                    otherObject.RefreshLight();
                    RefreshLight();
                    if (Input.GetMouseButton(transferFromLightButton) && ShootTestRay(otherObject.gameObject))
                    {
                        tempParticles.transform.position = testHit.point;
                        tempParticles.GetComponent<particleAttractorMove>().target = transform;
                        yield return new WaitForSeconds(extractTime);
                    } else
                    {
                        break;
                    }
                }
                tempParticles.Stop();
                break;

            default:
                break;
        }
    }
}

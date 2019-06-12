using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PuzzleElement : LightSource
{
    public SphereCollider resetRock;

    public GameObject transferParticles;
    public SequencePuzzle puzzleManager;
    public float lightThreshold;
    public char puzzleData;
    string interactableTag;

    private new void Start()
    {
        base.Start();
        interactableTag = transform.tag;
        puzzleManager = transform.parent.gameObject.GetComponent<SequencePuzzle>();
        resetRock = GetComponentInChildren<SphereCollider>();
        transferParticles = Instantiate(transferParticles, transform);
    }

    void SendInfoToPuzzle()
    {
        puzzleManager.currentSequence += puzzleData;
        puzzleManager.CheckSequence();
    }

    public override void RefreshLight()
    {
        base.RefreshLight();
        if(intensity == lightThreshold)
        {
            SendInfoToPuzzle();
        }
    }

    public void TransferLightToReset(Transform transformTo, bool reset)
    {
        transferParticles.GetComponent<ParticleSystem>().Play();
        transferParticles.GetComponent<particleAttractorMove>().target = transformTo;
        if (reset)
        {
            StartCoroutine(ResetTimer());
        } else
        {
            StartCoroutine(ResetToZero());

        }
    }

    IEnumerator ResetToZero()
    {
        transform.tag = "Untagged";
        float loopValue = intensity;
        for (int i = 0; i < loopValue; i++)
        {
            intensity--;
            base.RefreshLight();

            yield return new WaitForSeconds(0.02f);
        }

        transferParticles.GetComponent<particleAttractorMove>().target = null;
        transferParticles.GetComponent<ParticleSystem>().Stop();
    }

    IEnumerator ResetTimer()
    {
        transform.tag = "Untagged";
        resetRock.tag = "Untagged";
        float loopValue = intensity;
        for (int i = 0; i < loopValue; i++)
        {
            intensity--;
            base.RefreshLight();

            resetRock.GetComponent<LightSource>().intensity++;
            resetRock.GetComponent<LightSource>().RefreshLight();

            yield return new WaitForSeconds(0.02f);
        }
        resetRock.tag = interactableTag;
        transform.tag = interactableTag;
        transferParticles.GetComponent<particleAttractorMove>().target = null;
        transferParticles.GetComponent<ParticleSystem>().Stop();
    }
}

using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SequencePuzzle : MonoBehaviour
{
    [SerializeField] private PuzzleElement[] puzzleElements;
    public float[] lightThresholds;
    List<char> sequenceList;
    [SerializeField] private string goodSequence;
    [SerializeField] private Door[] doors;
    public string currentSequence;

    void Start()
    {

        puzzleElements = GetComponentsInChildren<PuzzleElement>();

        //Makes sure that the puzzles aren't impossible
        sequenceList = new List<char>();
        //string tempSequence = "";
        //int tempIncrease = 0;
        foreach (char letter in goodSequence)
        {
            sequenceList.Add(letter);
            //if(tempIncrease < puzzleElements.Length)
            //{
            //    sequenceList.Add(letter);
            //    tempSequence += letter;
            //    tempIncrease++;
            //} else
            //{
            //    goodSequence = tempSequence;
            //    break;
            //}
        }


        for (int i = 0; i < puzzleElements.Length; i++)
        {
            puzzleElements[i].puzzleData = sequenceList[i];
            puzzleElements[i].lightThreshold = lightThresholds[i];
        }
    }

    public void CheckSequence()
    {
        if (currentSequence.Length == goodSequence.Length)
        {

            if (currentSequence == goodSequence && puzzleElements[0].intensity >= lightThresholds[0] || goodSequence.Length > 3)
            {
                StartCoroutine(OpenDoor());
            } else
            {
                currentSequence = null;
                ResetLight();
            }
        }
    }

    IEnumerator OpenDoor()
    {
        float maxIntensity= 0;

        for (int i = 0; i < puzzleElements.Length; i++)
        {
            maxIntensity += puzzleElements[i].intensity;
            for (int w = 0; w < doors.Length; w++)
            {
                puzzleElements[i].TransferLightToReset(doors[w].particlePosition, false);
            }
        }



        for (int i = 0; i < maxIntensity; i++)
        {
            yield return new WaitForSeconds(0.01f);
            for (int w = 0; w < doors.Length; w++)
            {
                doors[w].intensity += 3;
                doors[w].GiveLightToStones();
            }
        }

        for (int i = 0; i < doors.Length; i++)
        {
            doors[i].StartCoroutine(doors[i].OpenDoor());
        }
    }

    private void ResetLight()
    {
        for (int i = 0; i < puzzleElements.Length; i++)
        {
            puzzleElements[i].TransferLightToReset(puzzleElements[i].resetRock.transform, true);
        }
    }


}

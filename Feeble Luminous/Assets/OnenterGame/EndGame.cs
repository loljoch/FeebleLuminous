using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class EndGame : MonoBehaviour
{
    public Animator ScreenFade;
    public GameObject endText;

    private void OnTriggerEnter(Collider other)
    {
        ScreenFade.SetBool("isReverse", false);
        ScreenFade.SetBool("isLightingUp", true);
        endText.SetActive(true);
        FindObjectOfType<CharacterController>().enabled = false;
        FindObjectOfType<CharacterController>().GetComponent<AudioSource>().enabled = false;
        StartCoroutine(BackToHome());
    }



    IEnumerator BackToHome()
    {
        yield return new WaitForSeconds(10);
        Cursor.lockState = CursorLockMode.None;
        SceneManager.LoadScene("StartMenu");
        SceneManager.UnloadSceneAsync("CaveScene");
    }
}

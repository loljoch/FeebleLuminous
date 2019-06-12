using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class StartMenu : MonoBehaviour
{
    Animator titleAnimation;
    public Animator lightScreenUp;
    public Animator whiteScreen;
    public Animator startButton;


    private void Start()
    {
        titleAnimation = GetComponent<Animator>();
    }

    public void StartGame()
    {
        titleAnimation.SetBool("ShowingTitle", true);
        lightScreenUp.SetBool("isLightingUp", true);
        whiteScreen.SetBool("isLightingUp", true);
        startButton.SetBool("isLightingUp", true);
        StartCoroutine(SwitchScene());
    }

    IEnumerator SwitchScene()
    {
        yield return new WaitForSeconds(6);
        SceneManager.LoadScene("CaveScene");
        AudioManager.Instance.SwitchBackground();
        SceneManager.UnloadSceneAsync("StartMenu");
    }
}

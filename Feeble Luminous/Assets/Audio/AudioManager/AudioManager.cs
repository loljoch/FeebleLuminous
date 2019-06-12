using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AudioManager : MonoBehaviour
{
    private static AudioManager instance;
    public static AudioManager Instance
    {
        get { return instance; }
        set { instance = value; }
    }

    AudioSource[] audioSources;
    [SerializeField] AudioSource backgroundSource;

    public AudioClip suckLight;
    [SerializeField] AudioClip bgStartMenu;
    [SerializeField] AudioClip bgCaveScene;

    private void Awake()
    {
        Instance = null;
        if(Instance == null)
        {
            Instance = this;
        } else
        {
            Destroy(gameObject);
        }

        DontDestroyOnLoad(gameObject);
        SwitchBackground();
    }

    public void SwitchBackground()
    {
        if(backgroundSource.clip == bgStartMenu)
        {
            backgroundSource.clip = bgCaveScene;
            backgroundSource.volume = 0.2f;
        } else
        {
            backgroundSource.clip = bgStartMenu;
            backgroundSource.volume = 0.142f; 
        }

        backgroundSource.Play();
    }

    private void GetAudioSources()
    {
        audioSources = GetComponents<AudioSource>();
    }

    public void PlaySound(AudioClip clip)
    {
        MakeAudioSourceAvailable();
        GetAudioSources();

        for (int i = 0; i < audioSources.Length; i++)
        {
            if (!audioSources[i].isPlaying)
            {
                audioSources[i].clip = clip;
                audioSources[i].Play();
                break;
            }
        }

    }

    public void StopSound(AudioClip clip)
    {
        for (int i = 0; i < audioSources.Length; i++)
        {
            if(audioSources[i].clip = clip)
            {
                audioSources[i].Stop();
            }
        }
    }

    private void MakeAudioSourceAvailable()
    {
        GetAudioSources();
        int availableAudioSources = 0;
        for (int i = 0; i < audioSources.Length; i++)
        {
            if (audioSources[i].isPlaying)
            {
                continue;
            } else
            {
                availableAudioSources++;
            }
        }

        if (availableAudioSources == 0)
        {
            gameObject.AddComponent<AudioSource>();
            GetAudioSources();
        }
    }


}

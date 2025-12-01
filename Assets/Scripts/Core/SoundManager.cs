using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Audio;
using UnityEngine.UIElements;

public class SoundManager : MonoBehaviour
{
    public static SoundManager Instance { get; private set; }
    public List<AudioSource> sourceList = new List<AudioSource>();
    public AudioClip backgroundMusic;
    public AudioSource audioSource;
    public AudioMixer audioMixer;
    public float defaultVolume = 0.5f;
    

    private void Start()
    {
        if (Instance == null)
        {
            Instance = this;
            DontDestroyOnLoad(gameObject);

            audioSource = GetComponent<AudioSource>();
            audioSource.clip = backgroundMusic;
            audioSource.loop = true;
            audioSource.Play();

            // Chỉ set volume mặc định lần đầu
            SetMusicVolume(defaultVolume);
            SetSFXVolume(defaultVolume);

        }
        else
        {
            Destroy(gameObject);
        }
    }


    public void SetMusicVolume(float value)
    {
        if (value == 0)
            audioMixer.SetFloat("Music", -80f);
        else
        {
            float mapped = Mathf.Lerp(0.0001f, 2f, value);
            audioMixer.SetFloat("Music", Mathf.Log10(mapped) * 20f);
        }
    }

    public void SetSFXVolume(float value)
    {
        if (value == 0)
            audioMixer.SetFloat("SFX", -80f);
        else
        {
            float mapped = Mathf.Lerp(0.0001f, 2f, value);
            audioMixer.SetFloat("SFX", Mathf.Log10(mapped) * 20f);
        }
    }
}

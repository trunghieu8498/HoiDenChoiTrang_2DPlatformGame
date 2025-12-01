using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class SettingUI : MonoBehaviour
{
    public Slider masterSlider;
    public Slider sfxSlider;

    private void Start()
    {
        masterSlider.value = 0.5f;
        sfxSlider.value = 0.5f;

        masterSlider.onValueChanged.AddListener(v => SoundManager.Instance.SetMusicVolume(v));
        sfxSlider.onValueChanged.AddListener(v => SoundManager.Instance.SetSFXVolume(v));
    }

    public void OnResetButton()
    {
        masterSlider.value = SoundManager.Instance.defaultVolume;
        sfxSlider.value = SoundManager.Instance.defaultVolume;
    }
}

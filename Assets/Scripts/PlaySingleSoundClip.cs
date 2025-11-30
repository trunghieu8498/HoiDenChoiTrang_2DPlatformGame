using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlaySingleSoundClip : MonoBehaviour
{
    public void PlaySound()
    {
        AudioSource source = GetComponent<AudioSource>();
        source.Play();
    }
}

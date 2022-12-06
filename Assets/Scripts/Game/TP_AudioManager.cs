using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TP_AudioManager : MonoBehaviour
{
    private AudioSource[] audioSources;

    private void Start()
    {
        audioSources = GetComponents<AudioSource>();
    }

    public void PlayAudioShoot()
    {
        audioSources[1].Play();
    }

    public void PlayAudioScore()
    {
        audioSources[2].Play();
    }

    public void PlayAudioPropellant()
    {
        audioSources[3].Play();
    }

    public void StopAudioPropellant()
    {
        audioSources[3].Stop();
    }

    public void PlayAudioRespawn()
    {
        audioSources[4].Play();
    }

    public void StopAudioRespawn()
    {
        audioSources[4].Stop();
    }
}

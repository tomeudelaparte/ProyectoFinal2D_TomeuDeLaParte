using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TP_AudioManager : MonoBehaviour
{
    // Private references
    private AudioSource[] audioSources;

    private void Start()
    {
        // Gets references
        audioSources = GetComponents<AudioSource>();
    }

    // When player shoots
    public void PlayAudioShoot()
    {
        audioSources[1].Play();
    }

    // When destroys an asteroid
    public void PlayAudioScore()
    {
        audioSources[2].Play();
    }

    // When player goes forward
    public void PlayAudioPropellant()
    {
        audioSources[3].Play();
    }

    // When player not goes forward
    public void StopAudioPropellant()
    {
        audioSources[3].Stop();
    }

    // When player is respawning
    public void PlayAudioRespawn()
    {
        audioSources[4].Play();
    }

    // When player is not respawning
    public void StopAudioRespawn()
    {
        audioSources[4].Stop();
    }
}

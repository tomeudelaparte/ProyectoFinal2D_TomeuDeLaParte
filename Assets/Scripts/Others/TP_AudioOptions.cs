using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.Audio;

public class TP_AudioOptions : MonoBehaviour
{
    [Header("DATE PERSISTENCE")]
    private TP_DataPersistence dataPersistence;

    [Header("AUDIO SETTINGS")]
    public Slider generalSlider;
    public Slider musicSlider;
    public Slider effectSlider;

    [Header("AUDIO MIXER")]
    public AudioMixer audioMixer;

    [Header("DEFAULT AUDIO SETTINGS")]
    private float DefaultGeneralVolume = 1f;
    private float DefaultMusicVolume = 1f;
    private float DefaultEffectsVolume = 1f;

    void Start()
    {
        dataPersistence = FindObjectOfType<TP_DataPersistence>();

        LoadSavedSettings();
    }

    private void LoadSavedSettings()
    {
        GetGeneralVolume();
        GetMusicVolume();
        GetEffectsVolume();
    }

    public void GetGeneralVolume()
    {
        if (!dataPersistence.HasKey("General Volume"))
        {
            SetGeneralVolume(DefaultGeneralVolume);
        }

        generalSlider.value = dataPersistence.GetFloat("General Volume");
    }

    public void GetMusicVolume()
    {
        if (!dataPersistence.HasKey("Music Volume"))
        {
            SetMusicVolume(DefaultMusicVolume);
        }

        musicSlider.value = dataPersistence.GetFloat("Music Volume");
    }

    public void GetEffectsVolume()
    {
        if (!dataPersistence.HasKey("Effects Volume"))
        {
            SetEffectsVolume(DefaultEffectsVolume);
        }

        effectSlider.value = dataPersistence.GetFloat("Effects Volume");
    }

    public void SetGeneralVolume(float volume)
    {
        audioMixer.SetFloat("General Volume", Mathf.Log10(volume) * 20);

        dataPersistence.SetFloat("General Volume", volume);
    }

    public void SetMusicVolume(float volume)
    {
        audioMixer.SetFloat("Music Volume", Mathf.Log10(volume) * 20);

        dataPersistence.SetFloat("Music Volume", volume);
    }

    public void SetEffectsVolume(float volume)
    {
        audioMixer.SetFloat("Effects Volume", Mathf.Log10(volume) * 20);

        dataPersistence.SetFloat("Effects Volume", volume);
    }
}

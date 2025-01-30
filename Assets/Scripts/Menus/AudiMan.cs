using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Audio;
using UnityEngine.UI;


public class AudiMan : MonoBehaviour
{
    public AudioMixer mixer;
    public Slider masterVolumeSlider, sfxVolumeSlider, ambienceVolumeSlider, musicVolumeSlider;

    void Start()
    {
        // Optionally set initial slider values from saved settings
        LoadVolumes();
    }

    public void SetMasterVolume(float volume)
    {
        mixer.SetFloat("MasterVolume", Mathf.Log10(volume) * 20);
    }

    public void SetSFXVolume(float volume)
    {
        mixer.SetFloat("SFXVolume", Mathf.Log10(volume) * 20);
    }

    public void SetAmbienceVolume(float volume)
    {
        mixer.SetFloat("AmbienceVolume", Mathf.Log10(volume) * 20);
    }

    public void SetMusicVolume(float volume)
    {
        mixer.SetFloat("MusicVolume", Mathf.Log10(volume) * 20);
    }

    private void LoadVolumes()
    {
        // Load volumes from PlayerPrefs or similar if needed
        // Example: masterVolumeSlider.value = PlayerPrefs.GetFloat("MasterVolume", 0.75f);
    }
}

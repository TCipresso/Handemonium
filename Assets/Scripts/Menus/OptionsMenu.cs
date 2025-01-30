using UnityEngine;
using UnityEngine.UI;
using UnityEngine.Audio;

public class OptionsMenu : MonoBehaviour
{
    public AudioMixer audioMixer;
    public Slider sensitivitySlider;
    public Slider masterVolumeSlider;
    public Slider sfxVolumeSlider;
    public Slider ambienceVolumeSlider;
    public Slider musicVolumeSlider;

    void Start()
    {
        // Initialize sliders (optional step if you want to set defaults or load saved settings)
    }

    public void SetSensitivity(float sensitivity)
    {
        Debug.Log("Received sensitivity value: " + sensitivity);
        PlayerMovement.Instance.sensitivity = sensitivity;
    }


    public void SetMasterVolume(float volume)
    {
        audioMixer.SetFloat("MasterVolume", Mathf.Log10(volume) * 20); // Convert slider value to dB
    }

    public void SetSFXVolume(float volume)
    {
        audioMixer.SetFloat("SFXVolume", Mathf.Log10(volume) * 20);
    }

    public void SetAmbienceVolume(float volume)
    {
        audioMixer.SetFloat("AmbienceVolume", Mathf.Log10(volume) * 20);
    }

    public void SetMusicVolume(float volume)
    {
        audioMixer.SetFloat("MusicVolume", Mathf.Log10(volume) * 20);
    }
}

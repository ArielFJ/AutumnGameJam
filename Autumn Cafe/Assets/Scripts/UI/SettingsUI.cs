using System;
using UnityEngine;
using UnityEngine.Audio;
using UnityEngine.UI;

public class SettingsUI : MonoBehaviour
{
    [Header("Mixer")]
    [SerializeField] private AudioMixer _generalAudioMixer;

    [Header("Sliders")]
    [SerializeField] private Slider _masterVolumeSlider;
    [SerializeField] private Slider _musicVolumeSlider;
    [SerializeField] private Slider _sfxVolumeSlider;

    void Awake()
    {
        _masterVolumeSlider.onValueChanged.AddListener(HandleMasterValueChanged);
        _musicVolumeSlider.onValueChanged.AddListener(HandleMusicValueChanged);
        _sfxVolumeSlider.onValueChanged.AddListener(HandleSFXValueChanged);
    }

    private void Start()
    {
        _masterVolumeSlider.value = SettingsManager.Instance.MasterVolume;
        _musicVolumeSlider.value = SettingsManager.Instance.MusicVolume;
        _sfxVolumeSlider.value = SettingsManager.Instance.SFXVolume;
    }

    private void HandleMasterValueChanged(float value)
    {
        SettingsManager.Instance.MasterVolume = value;
        HandleVolumeValueChanged(value, nameof(SettingsManager.MasterVolume));
    }

    private void HandleSFXValueChanged(float value)
    {
        SettingsManager.Instance.SFXVolume = value;
        HandleVolumeValueChanged(value, nameof(SettingsManager.SFXVolume));
    }

    private void HandleMusicValueChanged(float value)
    {
        SettingsManager.Instance.MusicVolume = value;
        HandleVolumeValueChanged(value, nameof(SettingsManager.MusicVolume));
    }

    private void HandleVolumeValueChanged(float value, string parameterName)
    {
        // Volume goes from -80 to 0
        var volume = (value - 1) * 80f;
        _generalAudioMixer.SetFloat(parameterName, volume);
    }
}

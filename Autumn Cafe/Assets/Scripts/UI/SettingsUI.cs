using System.Collections;
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
        SettingsManager.Instance.SetAudioMixer(_generalAudioMixer);

        _masterVolumeSlider.onValueChanged.AddListener(SettingsManager.Instance.HandleMasterValueChanged);
        _musicVolumeSlider.onValueChanged.AddListener(SettingsManager.Instance.HandleMusicValueChanged);
        _sfxVolumeSlider.onValueChanged.AddListener(SettingsManager.Instance.HandleSFXValueChanged);
    }

    private void Start()
    {
        _masterVolumeSlider.value = SettingsManager.Instance.MasterVolume;
        _musicVolumeSlider.value = SettingsManager.Instance.MusicVolume;
        _sfxVolumeSlider.value = SettingsManager.Instance.SFXVolume;
    }
}

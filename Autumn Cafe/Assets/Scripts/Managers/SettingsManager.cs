using UnityEngine;
using UnityEngine.Audio;

[DefaultExecutionOrder(-100)]
public class SettingsManager : MonoBehaviour
{
    public static SettingsManager Instance;

    [field: SerializeField] public float MasterVolume { get; set; } = 1f;
    [field: SerializeField] public float MusicVolume { get; set; } = 1f;
    [field: SerializeField] public float SFXVolume { get; set; } = 1f;
    [SerializeField] private AudioMixer _audioMixer;

    private void Awake()
    {
        if (Instance != null)
        {
            Destroy(gameObject);
            return;
        }
        Instance = this;
        DontDestroyOnLoad(gameObject);

        MasterVolume = PlayerPrefs.GetFloat(nameof(MasterVolume), MasterVolume);
        MusicVolume = PlayerPrefs.GetFloat(nameof(MusicVolume), MusicVolume);
        SFXVolume = PlayerPrefs.GetFloat(nameof(SFXVolume), SFXVolume);
    }

    private void OnDisable()
    {
        PlayerPrefs.SetFloat(nameof(MasterVolume), MasterVolume);
        PlayerPrefs.SetFloat(nameof(MusicVolume), MusicVolume);
        PlayerPrefs.SetFloat(nameof(SFXVolume), SFXVolume);
    }

    public void SetAudioMixer(AudioMixer audioMixer)
    {
        _audioMixer = audioMixer;

        HandleVolumeValueChanged(MasterVolume, nameof(MasterVolume));
        HandleVolumeValueChanged(MusicVolume, nameof(MusicVolume));
        HandleVolumeValueChanged(SFXVolume, nameof(SFXVolume));
    }

    public void HandleMasterValueChanged(float value)
    {
        MasterVolume = value;
        HandleVolumeValueChanged(value, nameof(MasterVolume));
    }

    public void HandleSFXValueChanged(float value)
    {
        SFXVolume = value;
        HandleVolumeValueChanged(value, nameof(SFXVolume));
    }

    public void HandleMusicValueChanged(float value)
    {
        Debug.Log("Changed");
        MusicVolume = value;
        HandleVolumeValueChanged(value, nameof(MusicVolume));
    }

    private void HandleVolumeValueChanged(float value, string parameterName)
    {
        // Volume goes from -50 to 0
        //var volume = (value - 1) * 80f;
        var volume = Mathf.RoundToInt(Mathf.Lerp(-50, 0, value));
        _audioMixer.SetFloat(parameterName, volume);
    }
}

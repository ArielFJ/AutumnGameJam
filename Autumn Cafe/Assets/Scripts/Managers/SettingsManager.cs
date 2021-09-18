using UnityEngine;

public class SettingsManager : MonoBehaviour
{
    public static SettingsManager Instance;

    [field: SerializeField] public float MasterVolume { get; set; } = 1f;
    [field: SerializeField] public float MusicVolume { get; set; } = 1f;
    [field: SerializeField] public float SFXVolume { get; set; } = 1f;

    private void Awake()
    {
        if (Instance != null)
        {
            Destroy(gameObject);
            return;
        }
        Instance = this;
        DontDestroyOnLoad(gameObject);

        MasterVolume = PlayerPrefs.GetFloat(nameof(MasterVolume));
        MusicVolume = PlayerPrefs.GetFloat(nameof(MusicVolume));
        SFXVolume = PlayerPrefs.GetFloat(nameof(SFXVolume));
    }

    private void OnDisable()
    {
        PlayerPrefs.SetFloat(nameof(MasterVolume), MasterVolume);
        PlayerPrefs.SetFloat(nameof(MusicVolume), MusicVolume);
        PlayerPrefs.SetFloat(nameof(SFXVolume), SFXVolume);
    }
}

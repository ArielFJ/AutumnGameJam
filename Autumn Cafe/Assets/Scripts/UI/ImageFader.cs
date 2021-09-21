using System;
using DG.Tweening;
using UnityEngine;
using UnityEngine.Events;
using UnityEngine.UI;

public class ImageFader : MonoBehaviour
{
    [SerializeField] private Image _image;
    [SerializeField] private bool _visible;
    [SerializeField] private float _fadeInDuration;
    [SerializeField] private float _fadeOutDuration;

    [Header("Fade In")]
    [Tooltip("If true, the Fade In animation will play on awake, won't work if FadeOutOnAwake is checked")]
    [SerializeField] private bool _playFadeInOnAwake;
    public UnityEvent onFadeInStarted;
    public UnityEvent onFadeInEnded;
    
    [Header("Fade Out")]
    [Tooltip("If true, the Fade Out animation will play on awake, won't work if FadeInOnAwake is checked")]
    [SerializeField] private bool _playFadeOutOnAwake;
    public UnityEvent onFadeOutStarted;
    public UnityEvent onFadeOutEnded;

    private void Start()
    {
        // If Visible, show the image by default
        _image.DOFade(_visible ? 1 : 0, 0f);

        if (_playFadeInOnAwake && !_playFadeOutOnAwake) FadeIn();
        
        if (!_playFadeInOnAwake && _playFadeOutOnAwake) FadeOut();
    }

    public void FadeIn()
    {
        onFadeInStarted?.Invoke();

        var color = _image.color;
        _image.color = new Color(color.r, color.g, color.b, 1);
        _image
            .DOFade(0, _fadeInDuration)
            .SetUpdate(true)
            .OnComplete(() => onFadeInEnded?.Invoke())
            .OnKill(() => _image.color = new Color(color.r, color.g, color.b, 0));
    }

    public void FadeOut()
    {
        onFadeOutStarted?.Invoke();

        var color = _image.color;
        _image.color = new Color(color.r, color.g, color.b, 0);
        _image
            .DOFade(1, _fadeOutDuration)
            .SetUpdate(true)
            .OnComplete(() => onFadeOutEnded?.Invoke())
            .OnKill(() => _image.color = new Color(color.r, color.g, color.b, 1));
    }
}

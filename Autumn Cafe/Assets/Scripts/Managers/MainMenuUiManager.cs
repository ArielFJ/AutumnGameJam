using System;
using DG.Tweening;
using UnityEngine;

public class MainMenuUiManager : MonoBehaviour
{
    [SerializeField] private GameObject _optionsUI;
    [SerializeField, Min(0)] private float _tweeningTime = 1f;
    [SerializeField] private Ease _easeType;

    private Vector3 _originalOptionsPosition;

    private void OnEnable()
    {
        _originalOptionsPosition = _optionsUI.transform.position;
        _optionsUI.transform.DOMoveX(-Screen.width / 2, 0);
        //_optionsUI.SetActive(false);
    }

    public void GoToOptions()
    {
        //_menuUI.SetActive(false);
        _optionsUI.SetActive(true);
        _optionsUI.transform
            .DOMoveX(_originalOptionsPosition.x, _tweeningTime)
            .SetEase(_easeType)
            .SetUpdate(true);
    }

    public void GoToMainMenu()
    {
        _optionsUI.transform
            .DOMoveX(-Screen.width / 2, _tweeningTime)
            .SetEase(_easeType)
            .SetUpdate(true)
            .OnComplete(() => _optionsUI.SetActive(false));
    }
}

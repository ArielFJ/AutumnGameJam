using DG.Tweening;
using UnityEngine;
using UnityEngine.UI;

[DefaultExecutionOrder(100)]
public class PlayingLevelUIManager : MonoBehaviour
{
    [SerializeField] private GameObject _pauseMenuUI;
    [SerializeField] private GameObject _pauseMenuBackground;
    [SerializeField] private GameObject _optionsUI;

    [SerializeField, Min(0)] private float _tweeningTime = 1f;
    [SerializeField] private Ease _easeType;

    private GameObject[] _allMenuUi;

    private void Start()
    {
        _allMenuUi = new[] { _pauseMenuUI, _optionsUI };
        _optionsUI.transform.DOMoveX(-Screen.width / 2, 0);
        _pauseMenuUI.transform.DOMoveX(-Screen.width / 2, 0);
        _pauseMenuBackground.transform.DOMoveX(-Screen.width / 2, 0);
        _pauseMenuUI.SetActive(false);
        _pauseMenuBackground.SetActive(false);
    }

    private void OnEnable()
    {
        GameManager.Instance.onPause += ShowPauseMenu;
        GameManager.Instance.onResume += HideAllMenus;
    }

    private void OnDisable()
    {
        GameManager.Instance.onPause -= ShowPauseMenu;
        GameManager.Instance.onResume -= HideAllMenus;
    }

    public void HideAllMenus()
    {
        DOTween.Clear();
        HideMenu(_pauseMenuBackground, -Screen.width);
        foreach (var obj in _allMenuUi)
        {
            HideMenu(obj);
        }
    }

    public void ResumeGame() => GameManager.Instance.ResumeGame();

    public void ShowOptionsMenu()
    {
        //HideAllMenus();
        ShowMenu(_optionsUI);
    }

    public void HideOptionsMenu()
    {
        //HideAllMenus();
        HideMenu(_optionsUI);
    }

    public void ShowPauseMenu()
    {
        //HideAllMenus();
        DOTween.Clear();
        ShowMenu(_pauseMenuUI);
        ShowMenu(_pauseMenuBackground, Screen.width);
    }

    private void ShowMenu(GameObject menu, float? distanceToMove = null)
    {
        var distance = distanceToMove ?? Screen.width / 2;
        menu.SetActive(true);
        menu.transform
            .DOMoveX(distance, _tweeningTime)
            .SetEase(_easeType)
            .SetUpdate(true);
    }

    private void HideMenu(GameObject menu, float? distanceToMove = null)
    {
        var distance = distanceToMove ?? -Screen.width / 2;
        menu.transform
            .DOMoveX(distance, _tweeningTime)
            .SetEase(_easeType)
            .SetUpdate(true)
            .OnComplete(() => menu.SetActive(false));
    }
}

using UnityEngine;

public class MainMenuUiManager : MonoBehaviour
{
    [SerializeField] private GameObject _menuUI;
    [SerializeField] private GameObject _optionsUI;

    public void GoToOptions()
    {
        //_menuUI.SetActive(false);
        // TODO: use doTween to make it appear
        _optionsUI.SetActive(true);
    }

    public void GoToMainMenu()
    {
        //_menuUI.SetActive(true);
        _optionsUI.SetActive(false);
    }
}

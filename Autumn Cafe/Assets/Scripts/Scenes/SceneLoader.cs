using UnityEngine;
using UnityEngine.SceneManagement;

public class SceneLoader : MonoBehaviour
{
    public static bool WasSceneLoaded { get; private set; }

    public void LoadSceneByIndex(int index)
    {
        SceneManager.LoadScene(index);
        WasSceneLoaded = true;
        GameManager.Instance.PrepareToPlay();
    }

    public void ReloadScene() => SceneManager.LoadScene(SceneManager.GetActiveScene().name);

    public void Exit()
    {
#if UNITY_EDITOR
        UnityEditor.EditorApplication.isPlaying = false;
#else
        Application.Quit();
#endif
    }
}

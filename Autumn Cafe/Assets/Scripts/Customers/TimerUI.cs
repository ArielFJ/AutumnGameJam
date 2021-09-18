using UnityEngine;
using UnityEngine.UI;

public class TimerUI : MonoBehaviour
{
    [SerializeField] private GameObject _timerUIContainer;
    [SerializeField] private Image _referenceImage;
    [SerializeField] private Timer _timer;
    [SerializeField] private Gradient _timerGradient;

    // Update is called once per frame
    void Update()
    {
        var currentTime = _timer.CurrentTime / _timer.MaxTime;
        _referenceImage.color = _timerGradient.Evaluate(currentTime);
        _referenceImage.fillAmount = currentTime;
    }

    public void HideTimerUI()
    {
        if (_timerUIContainer) _timerUIContainer.SetActive(false);
    }
}

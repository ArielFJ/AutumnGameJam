using System;
using System.Collections;
using UnityEngine;

[RequireComponent(typeof(Timer))]
[DefaultExecutionOrder(-100)]
public class SceneTimerManager : MonoBehaviour
{
    public static SceneTimerManager Instance;

    public Action onSceneTimerOver;

    [SerializeField] private float _startTimeDelay = 3f;

    private Timer _timer;
    private TimerUI _timerUi;
    private void Awake()
    {
        Instance = this;
        _timer = GetComponent<Timer>();
        TryGetComponent(out _timerUi);
    }

    private IEnumerator Start()
    {
        yield return new WaitForSeconds(_startTimeDelay);
        _timer.StartTimer();
    }

    private void OnEnable()
    {
        _timer.onTimerTickFinished += GameOver;
    }

    private void OnDisable()
    {
        _timer.onTimerTickFinished -= GameOver;
    }

    private void GameOver()
    {
        // TODO: Check new high scores
        onSceneTimerOver?.Invoke();

        StartCoroutine(DelayGameOverCall());
    }

    IEnumerator DelayGameOverCall()
    {
        while (CustomerManager.Instance.CustomersInWorld > 0)
        {
            yield return null;
        }

        GameManager.Instance.GameOver();
        if (_timerUi) _timerUi.HideTimerUI();
    }
}

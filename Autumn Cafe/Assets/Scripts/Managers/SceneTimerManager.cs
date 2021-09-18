using System;
using System.Collections;
using UnityEngine;

[RequireComponent(typeof(Timer))]
public class SceneTimerManager : MonoBehaviour
{
    public static SceneTimerManager Instance;

    public Action onSceneTimerOver;

    private Timer _timer;
    private TimerUI _timerUi;
    private void Awake()
    {
        Instance = this;
        _timer = GetComponent<Timer>();
        TryGetComponent(out _timerUi);
    }

    private void Start()
    {
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

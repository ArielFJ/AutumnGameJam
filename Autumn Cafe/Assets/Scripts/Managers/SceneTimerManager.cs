using System;
using UnityEngine;

[RequireComponent(typeof(Timer))]
public class SceneTimerManager : MonoBehaviour
{
    public static SceneTimerManager Instance;

    private Timer _timer;

    private void Awake()
    {
        Instance = this;
        _timer = GetComponent<Timer>();
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
        Debug.Log("Game Over");
    }
}

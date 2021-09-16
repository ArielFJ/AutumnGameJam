using System;
using TMPro;
using UnityEngine;

public class ScoreManager : MonoBehaviour
{
    public static ScoreManager Instance;

    [SerializeField] private TMP_Text scoreText;

    [field: SerializeField] public int Score { get; private set; }

    private void Awake()
    {
        Instance = this;
    }

    private void Start()
    {
        AddScore(0);
    }

    public void AddScore(int score)
    {
        Score += score > 0 ? score : 0;
        scoreText.text = $"Score: {Score}";
    }
}

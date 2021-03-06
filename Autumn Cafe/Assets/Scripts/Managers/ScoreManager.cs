using System;
using TMPro;
using UnityEngine;

[RequireComponent(typeof(AudioSource))]
public class ScoreManager : MonoBehaviour
{
    public static ScoreManager Instance;

    [field: SerializeField] public int Score { get; private set; }
    [SerializeField] private TMP_Text scoreText;
    [SerializeField] private AudioClip highScoreClip;
    [SerializeField] private string scorePrefixText;

    [Header("Game Over Menu")]
    [SerializeField] private GameObject gameOverUIContainer;
    [SerializeField] private GameObject newHighScore;
    [SerializeField] private TMP_Text gameOverScoreText;

    private int _highScore;
    private AudioSource _audioSource;

    private void Awake()
    {
        Instance = this;
        _audioSource = GetComponent<AudioSource>();
    }

    private void Start()
    {
        AddScore(0);
        _highScore = PlayerPrefs.GetInt(nameof(_highScore), _highScore);
    }

    private void OnEnable()
    {
        GameManager.Instance.onGameOver += ShowGameOverUI;
    }

    private void OnDisable()
    {
        GameManager.Instance.onGameOver -= ShowGameOverUI;
    }

    public void AddScore(int score)
    {
        Score += score > 0 ? score : 0;
        scoreText.text = $"{scorePrefixText}{Score}";
    }

    public bool CheckHighScore()
    {
        if (Score <= _highScore) return false;

        _highScore = Score;
        PlayerPrefs.GetInt(nameof(_highScore), _highScore);
        return true;
    }

    public void ShowGameOverUI()
    {
        if (CheckHighScore())
        {
            newHighScore.SetActive(true);
            _audioSource.PlayOneShot(highScoreClip);
        }

        scoreText.gameObject.SetActive(false);

        gameOverScoreText.text = $"{Score}";
        gameOverUIContainer.SetActive(true);
    }
}

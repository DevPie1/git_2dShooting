using System;
using TMPro;
using UnityEngine;

public class ScoreManager : MonoBehaviour
{
    //싱글톤 패턴
    // 1. 전역적으로 접근 가능하다.
    // 2. 인스턴스 (생성된 객체)가 하나임을 보장한다.
    public static ScoreManager Instance;

    private int _bestScore;
    private int _currentScore;

    [SerializeField] private TextMeshProUGUI _bestScoreTextUI;
    [SerializeField] private TextMeshProUGUI _currentScoreTextUI;

    private void Awake()
    {
        Instance = this;
    }

    private void Update()
    {
        Refresh();
    }

    private void Refresh()
    {
        _bestScoreTextUI.text = $"BestScore: {_bestScore}";
        _currentScoreTextUI.text = $"Score : {_currentScore}";
    }

    public void AddScore(int score)
    {
        if (score <= 0) return;

        _currentScore += score;

        if (_currentScore > _bestScore)
        {
            _bestScore = _currentScore;
        }
    }
}
using System;
using TMPro;
using UnityEditorInternal;
using UnityEngine;

public class ScoreManager : MonoBehaviour
{
    //싱글톤 패턴
    // 1. 전역적으로 접근 가능하다.
    // 2. 인스턴스 (생성된 객체)가 하나임을 보장한다.
    public static ScoreManager _instance;
    public static ScoreManager Instance => _instance;
    private int _bestScore;
    private int _currentScore = 0;
    public int Score => _currentScore;

    public void Spend(int amount)
    {
        _currentScore -= amount;
        Refresh();
    }

    private const string SaveKey = "BestScore";

    [SerializeField] private TextMeshProUGUI _bestScoreTextUI;
    [SerializeField] private TextMeshProUGUI _currentScoreTextUI;

    private void Awake()
    {
        if (_instance != null)
        {
            Destroy(gameObject);
            return;
        }

        _instance = this;
    }

    private void Start()
    {
        // 저장 : Set() 시리즈를 이용해서 변수 저장이 가능하다.
        if (PlayerPrefs.HasKey(SaveKey))
        {
            _bestScore = PlayerPrefs.GetInt(SaveKey);
        }

        _bestScore = PlayerPrefs.GetInt(SaveKey, 0);

        Refresh();
    }

    private void Update()
    {
        Refresh();
    }

    private void Refresh()
    {
        _bestScoreTextUI.text = $"BestScore: {_bestScore:N0}";
        _currentScoreTextUI.text = $"Score : {_currentScore:N0}";
    }

    public void AddScore(int score)
    {
        if (score <= 0) return;

        _currentScore += score;

        if (_currentScore > _bestScore)
        {
            _bestScore = _currentScore;
            PlayerPrefs.SetInt(SaveKey, _bestScore);
            PlayerPrefs.Save();
        }

        Refresh();
    }
}
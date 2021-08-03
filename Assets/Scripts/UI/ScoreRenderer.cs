using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ScoreRenderer : MonoBehaviour
{
    int _score;
    float _scoreIncreasingSpeed = 5f;
    UnityEngine.UI.Text _scoreText;

    void Start()
    {
        _scoreText = GetComponent<UnityEngine.UI.Text>();
    }

    void SetScore(int score) => _score = score;
    void AddScore(int score) => _score += score;

    void Update()
    {
        UpdateScoreText();
    }

    void UpdateScoreText() {
        string displayScore = GetCurrentScore().ToString();
        while (displayScore.Length < 10) displayScore = '0' + displayScore;
        _scoreText.text = displayScore;
    }

    int GetCurrentScore() {
        int addingScore = Mathf.RoundToInt(_scoreIncreasingSpeed * Time.deltaTime);
        int currentScore = int.Parse(_scoreText.text) + addingScore;
        return Mathf.Clamp(currentScore, 0, _score);
    }
}

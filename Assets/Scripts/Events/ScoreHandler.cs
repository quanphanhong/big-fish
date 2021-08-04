using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ScoreHandler : MonoBehaviour
{
    int _score;
    float _scoreIncreasingSpeed = 5000f;
    UnityEngine.UI.Text _scoreText;

    void Start()
    {
        _scoreText = GetComponent<UnityEngine.UI.Text>();
    }

    public void SetScore(int score) => _score = score;
    public void AddScore(int score) => _score += score;
    public void ResetScore() => _score = 0;

    void Update()
    {
        Debug.Log(_score);
        UpdateScoreText();
    }

    void UpdateScoreText() {
        string displayScore = GetCurrentScore().ToString();
        while (displayScore.Length < 10) displayScore = '0' + displayScore;
        _scoreText.text = displayScore;
    }

    int GetCurrentScore() {
        int addingScore = Mathf.RoundToInt(_scoreIncreasingSpeed * Time.deltaTime);
        Debug.Log(addingScore);
        int currentScore = int.Parse(_scoreText.text) + addingScore;
        return Mathf.Clamp(currentScore, 0, _score);
    }
}

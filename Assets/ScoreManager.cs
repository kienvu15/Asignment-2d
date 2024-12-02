using TMPro;
using UnityEngine;

public class ScoreManager : MonoBehaviour
{
    [SerializeField] private TextMeshProUGUI scoreText; 
    private int score = 0; 

    public void AddScore(int value)
    {
        score += value; // Tăng điểm
        UpdateScoreText(); // Cập nhật hiển thị
    }

    private void UpdateScoreText()
    {
        scoreText.text = "Score: " + score.ToString();
    }
}

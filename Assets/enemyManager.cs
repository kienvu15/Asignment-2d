using UnityEngine;

public class EnemyManager : MonoBehaviour
{
    [SerializeField] private GameObject winCanvas;
    [SerializeField] private GameObject loseCanvas;
    // Tham chiếu đến Canvas Win

    private void Start()
    {
        // Ẩn Canvas khi bắt đầu
        if (winCanvas != null)
        {
            winCanvas.SetActive(false);
        }
        if (loseCanvas != null)
        { 
            loseCanvas.SetActive(false);
        }
        
            Time.timeScale = 1f;
    }

    private void Update()
    {
        // Kiểm tra nếu không còn Enemy nào trong game
        if (GameObject.FindGameObjectsWithTag("Enemy").Length == 0)
        {
            ShowWinCanvas();
        }

        if (GameObject.FindGameObjectsWithTag("Player").Length == 0)
        {
            ShowLoseCanvas();
        }

    }

    public void ShowWinCanvas()
    {
        if (winCanvas != null)
        {
            winCanvas.SetActive(true);
            Time.timeScale = 0f; // Dừng thời gian khi hiển thị màn hình thắng
        }
    }

    public void ShowLoseCanvas()
    {
        if (winCanvas != null)
        {
            loseCanvas.SetActive(true);
            Time.timeScale = 0f;
        }
    }

}

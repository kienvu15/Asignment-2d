using UnityEngine;
using UnityEngine.SceneManagement;

public class WinCanvasController : MonoBehaviour
{
    

    public void RestartGame()
    {
        SceneManager.LoadScene(1);
        Time.timeScale = 1f;
    }

    public void LoadNextLevel()
    {
        
        SceneManager.LoadScene(1); 
    }
}

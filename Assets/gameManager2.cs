using UnityEngine;
using UnityEngine.SceneManagement;

public class StartMenuManager : MonoBehaviour
{
    // Hàm bắt đầu game
    public void PlayGame()
    {
        SceneManager.LoadScene(1); 
    }

   
    
}

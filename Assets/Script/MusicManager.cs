using UnityEngine;

public class MusicManager : MonoBehaviour
{
    public AudioSource backgroundMusic; // Nhạc nền
    public AudioSource battleMusic;     // Nhạc chiến đấu
    

    private bool isInBattle = false;

    void Start()
    {
        // Bắt đầu phát nhạc nền
        if (backgroundMusic != null && !backgroundMusic.isPlaying)
        {
            backgroundMusic.Play();
        }
    }

    public void StartBattleMusic()
    {
        if (!isInBattle && battleMusic != null)
        {
            // Tắt nhạc nền và phát nhạc chiến đấu
            backgroundMusic.Stop();
            battleMusic.Play();
            isInBattle = true;
        }
    }

    public void StopBattleMusic()
    {
        if (isInBattle && battleMusic != null)
        {
            // Tắt nhạc chiến đấu và phát lại nhạc nền
            battleMusic.Stop();
            backgroundMusic.Play();
            isInBattle = false;
        }
    }
}

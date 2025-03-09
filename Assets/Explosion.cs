using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Explosion : MonoBehaviour
{
    Animator animator;
    Rigidbody2D rb;
    public MusicManager musicManager;
    // Start is called before the first frame update
    void Start()
    {
        animator = GetComponent<Animator>();
    }

    // Update is called once per frame
    void Update()
    {
        OnEXplosion();
    }

    public void OnEXplosion()
    {
        if (Input.GetKeyDown(KeyCode.R))
        {
            animator.Play("EXCCplsion");
            musicManager.StartBattleMusic();
        }
    }
    public void Des3troy()
    {
        Destroy(gameObject);
        musicManager.StopBattleMusic();
    }
}

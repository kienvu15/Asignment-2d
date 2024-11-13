using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Berral : MonoBehaviour
{
    Animator anim;
    [SerializeField] Rigidbody2D explosion;
    CapsuleCollider2D ojBerral;
    // Start is called before the first frame update
    void Start()
    {
        anim = GetComponent<Animator>();

        GameObject eplosionRange = GameObject.FindWithTag("Explosion");
        if (eplosionRange != null)
        {
            explosion = eplosionRange.GetComponent<Rigidbody2D>();
        }
        ojBerral = GetComponent<CapsuleCollider2D>();
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void deactiveBox()
    {
        ojBerral.enabled = false;
    }
    public void activeExplosion()
    {
        explosion.gameObject.SetActive(true);
    }

    public void dEactiveExplosion()
    {
        explosion.gameObject.SetActive(false);
    }

    public void StopAnimator()
    {
        Destroy(gameObject);
    }
}

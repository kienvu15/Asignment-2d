using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Sheep : MonoBehaviour
{
    Animator anim;
    Rigidbody2D rb;
    CircleCollider2D circleCollider;
    void Start()
    {
        anim = GetComponent<Animator>();
        circleCollider = GetComponent<CircleCollider2D>();
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void On2Destrot()
    {
        Destroy(gameObject);
    }

    public void enableCollider()
    {
        circleCollider.enabled = false;
    }
}

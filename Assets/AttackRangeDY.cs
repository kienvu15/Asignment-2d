using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AttackRangeDY : MonoBehaviour
{
    Rigidbody2D rb;
    private bool canShot = false;
    public bool CanShot => canShot;
    // Start is called before the first frame update
    void Start()
    {
        rb = GetComponent<Rigidbody2D>();
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.CompareTag("Player"))
        {
            Debug.Log("true");
            canShot = true;
        }
    }

    public void OnTriggerExit2D(Collider2D collision)
    {
        if (collision.CompareTag("Player"))
        {
            Debug.Log("false");
            canShot = false;
        }
    }
}

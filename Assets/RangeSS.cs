using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RangeSS : MonoBehaviour
{
    Rigidbody2D rb;

    private bool chase = false;
    public bool isChase => chase;
    // Start is called before the first frame update
    void Start()
    {
        rb= GetComponent<Rigidbody2D>();
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.CompareTag("Player"))
        {
            chase = true;
            Debug.Log("Dung");
        }
    }

    public void OnTriggerExit2D(Collider2D collision)
    {
        if (collision.CompareTag("Player"))
        {
            chase = false;
        }
    }
}

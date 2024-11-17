using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TNTGoblin : MonoBehaviour
{
    // Start is called before the first frame update
    Rigidbody2D rb;
    Animator anim;

    public Transform pivot;
    public Transform Player;
    public GameObject Dynamine;

    private bool isFacingRight = true;
    public float speedShot = 3f;

    [SerializeField] private HealthDisplay healthDisplay;
    
    void Start()
    {
        rb = GetComponent<Rigidbody2D>();
        anim = GetComponent<Animator>();


    }

    // Update is called once per frame
    void Update()
    {
        Shot();
        
    }
    public void Shot()
    {
        GameObject dynamineoj = Instantiate(Dynamine, pivot.position, Quaternion.identity);
        Rigidbody2D dynamine = dynamineoj.GetComponent<Rigidbody2D>();
        if (isFacingRight)
        {
            dynamine.velocity = new Vector2(speedShot, 0);
        }
    }
}

using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TestLeran : MonoBehaviour
{
    Rigidbody2D Player;
    float movement;
    [SerializeField] public float moveSpeed = 5f;
    // Start is called before the first frame update
    void Start()
    {
        Player = GetComponent<Rigidbody2D>();
    }

    // Update is called once per frame
    void Update()
    {
        Move();
    }

    public void Move()
    {
        movement = Input.GetAxisRaw("Horizontal");
       

        Player.velocity = new Vector2(movement * moveSpeed, Player.velocity.y);
    }

}

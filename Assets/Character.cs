using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Character : MonoBehaviour
{
    private Rigidbody2D rb;
    private Animator anim;

    public float moveSpeed = 5f;
    private float movementX, movementY;

    private bool isFacingRight = true;

    public GameObject attackcc;
    // Start is called before the first frame update
    void Start()
    {
        rb = GetComponent<Rigidbody2D>();
        anim = GetComponent<Animator>();

        
    }

    // Update is called once per frame
    void Update()
    {
        movement();
        Flip();
        attack();
    }

    public void movement()
    {
        movementX = Input.GetAxisRaw("Horizontal");
        movementY = Input.GetAxisRaw("Vertical");
        Vector2 movement = new Vector2 (movementX, movementY).normalized;
        rb.velocity = movement*moveSpeed;
        anim.SetBool("run", movementX!=0|| movementY!=0);
    }

    public void Flip()
    {
        if((movementX>0 && !isFacingRight) ||(movementX<0 && isFacingRight))
        {
            isFacingRight = !isFacingRight;
            Vector3 Scale = rb.transform.localScale;
            Scale.x *= -1;
            rb.transform.localScale = Scale;
        }
    }

    public void attack()
    {
        if (Input.GetKeyDown(KeyCode.B))
        {
            anim.SetTrigger("atk");
        }
    }

    public void RessAttack()
    {
        anim.ResetTrigger("atk");
    }

    public void Steacttack()
    {
        attackcc.gameObject.SetActive(true);
    }
}

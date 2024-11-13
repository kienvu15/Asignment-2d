using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEditor.Animations;
using UnityEngine;
using UnityEngine.Tilemaps;

public class PlayerController : MonoBehaviour
{

    Rigidbody2D Player;
    BoxCollider2D Pl;
    Animator anim;
    TilemapRenderer BrigdetilemapRenderer;
    [SerializeField] GameObject AttackHitBox;
    [SerializeField] GameObject AttackYHitBox;
    [SerializeField] GameObject MinusAttackYHitBox;
    [SerializeField] GameObject Pivot;
    

    [SerializeField] public float moveSpeed = 5f;
    private float OriginalmoveSpeed;
    private float movementX, movementY;
    private bool isFacingRight = true;
    
    

    [SerializeField] public float dashForce = 10f;
    [SerializeField] public float dashSpeed = 8f;

    [SerializeField] GameObject Arrow;
    [SerializeField] private float arrowSpeed = 10f;

    [SerializeField] public float maxHealth = 3f;
    [SerializeField] private HealthDisplay healthDisplay;

    void Start()
    {
        Player = GetComponent<Rigidbody2D>();
        anim = GetComponent<Animator>();
        Pl = GetComponent<BoxCollider2D>();
        GameObject Bridge = GameObject.FindWithTag("Brigde");
        if(Bridge != null )
        {
            BrigdetilemapRenderer = Bridge.GetComponent<TilemapRenderer>();
        }

        OriginalmoveSpeed = moveSpeed;
    }

    void Update()
    {
        Move();
        Flip();
        Attack();
        Dash();
        Shot();
        Shot2();

        if (maxHealth <= 0)
        {
            anim.Play("Die");
            Pl.enabled = false;
        }
    }

    void FixedUpdate()
    {
        
    }

    public void Move()
    {
            movementX = Input.GetAxisRaw("Horizontal");
            movementY = Input.GetAxisRaw("Vertical");
            Vector2 moment = new Vector2(movementX, movementY).normalized;

            Player.velocity = moment * moveSpeed;
            anim.SetBool("Run", movementX != 0 || movementY != 0);   
    }

    public void Flip()
    {
        if((movementX>0 && !isFacingRight) || (movementX<0 && isFacingRight))
        {
            isFacingRight = !isFacingRight;
            Vector3 Scale = Player.transform.localScale;
            Scale.x *= -1;
            Player.transform.localScale = Scale;
        }
    }

    public void Dash()
    {
        if (Input.GetKeyDown(KeyCode.B))
        {
            Vector2 moment = new Vector2(movementX, movementY).normalized;
            Player.velocity = moment * dashForce * dashSpeed;
        }
    }

    public void Attack()
    {
        if (Input.GetKeyDown(KeyCode.V))
        {
            if (movementY > 0) 
            {
                anim.SetTrigger("yattack");
                moveSpeed = 0;
            }
            else if (movementY < 0) 
            {
                anim.SetTrigger("minusYattack");
                moveSpeed = 0;
            }
            else 
            {
                anim.SetTrigger("attack");
                moveSpeed = 0;
            }

            anim.SetBool("isAttacking", true);
            
        }
    }

    public void OnAttackComplete()
    {
        anim.ResetTrigger("attack");
        anim.ResetTrigger("yattack");
        anim.ResetTrigger("minusYattack");
        anim.SetBool("isAttacking", false);
        moveSpeed = OriginalmoveSpeed;
        DisActiveHitbox();
    }

    public void ActiveHitbox()
    {
       AttackHitBox.SetActive(true);
    }
    public void ActiveYhitbox()
    {
        AttackYHitBox.SetActive(true);
    }
    public void ActiveMinusYHitbox()
    {
        MinusAttackYHitBox.SetActive(true);
    }
    public void DisActiveHitbox()
    {
       AttackHitBox.SetActive(false);
       AttackYHitBox.SetActive(false);
       MinusAttackYHitBox.SetActive(false);
    }

    public void Shot()
    {
        if (Input.GetKeyDown(KeyCode.C) && movementX !=0)
        {
            GameObject shot = Instantiate(Arrow, AttackHitBox.transform.position, Quaternion.identity);
            Rigidbody2D arrow = shot.GetComponent<Rigidbody2D>();

            if (isFacingRight)
            {
                arrow.velocity = new Vector2(arrowSpeed, 0);
            }
            else
            {
                arrow.velocity = new Vector2(-arrowSpeed, 0);
                Vector3 Scale = arrow.transform.localScale;
                Scale.x *= -1;
                arrow.transform.localScale = Scale;
            }
            Destroy(shot, 2f);
            
        }
        if (Input.GetKeyDown(KeyCode.C) && (movementY > 0))
        {
            GameObject shot = Instantiate(Arrow, Pivot.transform.position, Quaternion.Euler(0, 0, 90));
            Rigidbody2D arrow = shot.GetComponent<Rigidbody2D>();
            arrow.velocity = new Vector2(0, arrowSpeed);
            Destroy(shot, 2f);
        }
        else if (Input.GetKeyDown(KeyCode.C) && (movementY < 0))
        {
            GameObject shot = Instantiate(Arrow, Pivot.transform.position, Quaternion.Euler(0, 0, -90));
            Rigidbody2D arrow = shot.GetComponent<Rigidbody2D>();
            arrow.velocity = new Vector2(0, -arrowSpeed);
            Destroy(shot, 2f);
        }


    }

    public void Shot2()
    {
        
    }


    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.CompareTag("Floor2Trigger"))
        {
            Player.gameObject.layer = LayerMask.NameToLayer("Floor2");
            if (BrigdetilemapRenderer != null)
            {
                BrigdetilemapRenderer.sortingLayerName = "Foreground";
            }
        }
        else if(collision.CompareTag("DefaultTigger"))
        {
            Player.gameObject.layer = LayerMask.NameToLayer("Default");
            BrigdetilemapRenderer.sortingLayerName = "UpGround";
        }
        else if(collision.CompareTag("Floor1Trigger"))
        {
            Player.gameObject.layer = LayerMask.NameToLayer("Floor1");
        }

        if(collision.CompareTag("Item"))
        {
            Destroy(collision.gameObject);
            maxHealth++;
            healthDisplay.UpdateHearts(maxHealth);
        }
        if (collision.CompareTag("Gbattackhitbox"))
        {
            maxHealth--;
            healthDisplay.UpdateHearts(maxHealth);
            
        }
    }

    public void destroyPlayer()
    {
        Destroy(gameObject);
    }
}

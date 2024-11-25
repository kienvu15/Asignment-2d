using TMPro;
using Unity.VisualScripting;
using UnityEngine;
using static UnityEditor.Experimental.GraphView.GraphView;

public class Enemy : MonoBehaviour
{

    [SerializeField] public RangeSS rangeSS;
    Rigidbody2D rb;
    Animator anim;
    BoxCollider2D Golbin;
    private int hitCount = 0;
    private int maxHits = 3;
    [SerializeField] Rigidbody2D AttackRange;
    [SerializeField] Rigidbody2D AttackHitBox;
    private bool isFacingRight = true;
    
    public Transform Player;

    private Vector3 OriginPosition;
    public float moveSpeed = 5f;
    

    private Transform currentTarget;

    private bool isGonnaAttack=false;

    public Transform Target1;
    public Transform Target2;
    public Transform Target3;

    public Rigidbody2D RangeSS;

    private void Start()
    {

        GameObject Rangesse = GameObject.FindWithTag("RangeSS");
        if(Rangesse != null)
        {
            RangeSS = Rangesse.GetComponent<Rigidbody2D>();
        }

        GameObject Target1oj = GameObject.Find("Target1");
        if(Target1oj != null)
        {
            Transform Target1 = Target1oj.GetComponent<Transform>();
        }
        GameObject Target2oj = GameObject.Find("Target2");
        if (Target1oj != null)
        {
            Transform Target2 = Target2oj.GetComponent<Transform>();
        }
        GameObject Target3oj = GameObject.Find("Target3");
        if (Target1oj != null)
        {
            Transform Target3 = Target3oj.GetComponent<Transform>();
        }

        OriginPosition = transform.position;
        currentTarget = Target1;

        rb = GetComponent<Rigidbody2D>();
        Golbin = GetComponent<BoxCollider2D>();
        anim = GetComponent<Animator>();
        GameObject attackener = GameObject.FindWithTag("AttackRange");
        if (attackener != null)
        {
            AttackRange = attackener.GetComponent<Rigidbody2D>();
        }
        GameObject attackHitboxoj = GameObject.FindWithTag("Gbattackhitbox");
        if (attackHitboxoj != null)
        {
            AttackHitBox = attackHitboxoj.GetComponent<Rigidbody2D>();
        }    
    }

    public void Update()
    {
        if (IsPlayerInRange())
        {
            Attack();
            isGonnaAttack = true;
        }
        else
        {
            OnAttackComplete();
            isGonnaAttack = false;
        }

        FacePlayer();
        Movee();
        ChasePlayer();
    }

    public void ChasePlayer()
    {
        if (isGonnaAttack == false) 
    { 
        if (rangeSS.isChase)
        {
            if (Player.position.x > transform.position.x && !isFacingRight)
            {
                Flip();
            }
            else if (Player.position.x < transform.position.x && isFacingRight)
            {
                Flip();
            }

            rb.position = Vector2.MoveTowards(rb.position, Player.position, moveSpeed * 1.5f * Time.deltaTime);
            anim.SetBool("Runing", true);
        }
    }
    }
    
    public void Movee()
    {
        if (isGonnaAttack == false) 
        {
            if ((currentTarget.position.x > transform.position.x && !isFacingRight) ||
            (currentTarget.position.x < transform.position.x && isFacingRight))
            {
                Flip();
            }


            rb.position = Vector2.MoveTowards(rb.position, currentTarget.position, moveSpeed * Time.deltaTime);
            anim.SetBool("Runing", true);

            if (Vector2.Distance(rb.position, currentTarget.position) < 0.1f)
            {
                if (currentTarget == Target1)
                {
                    currentTarget = Target2;
                }
                else if (currentTarget == Target2)
                {
                    currentTarget = Target3;
                }
                else if (currentTarget == Target3)
                {
                    currentTarget = Target1; 
                }
                
            }

        }

    }

    
    public void FacePlayer()
    {
        if (Player == null) return;

        if (Player.position.x > transform.position.x && !isFacingRight)
        {
            Flip();
        }
        else if (Player.position.x < transform.position.x && isFacingRight)
        {
            Flip();
        }
    }

    private void Flip()
    {
        isFacingRight = !isFacingRight;
        Vector3 scale = transform.localScale;
        scale.x *= -1;
        transform.localScale = scale;
    }
    public void OnTriggerEnter2D(Collider2D collision)
    {
        if(collision.CompareTag("AttackPosition"))
        {
            hitCount++;

            if (hitCount >= maxHits)
            {
                anim.SetTrigger("Dead");
                Golbin.enabled = false;
            }
        }
    }
    private bool IsPlayerInRange()
    {
        return AttackRange.OverlapPoint(Player.transform.position);
    }

    public void Attack()
    { 
        anim.SetBool("isattack", true);
    }
    public void OnAttackComplete()
    {   
        anim.SetBool("isattack", false);
        deactiveHitBox();
    }
    public void activeHitBox()
    {
        AttackHitBox.gameObject.SetActive(true);
    }
    public void deactiveHitBox()
    {
        AttackHitBox.gameObject.SetActive(false);
    }
    public void DestroyItself()
    {
        Destroy(gameObject);
    }
}

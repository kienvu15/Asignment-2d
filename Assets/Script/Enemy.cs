using Unity.VisualScripting;
using UnityEngine;
using static UnityEditor.Experimental.GraphView.GraphView;

public class Enemy : MonoBehaviour
{
    Rigidbody2D rb;
    Animator anim;
    BoxCollider2D Golbin;
    private int hitCount = 0;
    private int maxHits = 3;
    [SerializeField] Rigidbody2D AttackRange;
    [SerializeField] Rigidbody2D AttackHitBox;
    private bool isFacingRight = true;
    
    public Transform Player;
    Rigidbody2D pl;

    /*
    public float moveSpeed = 3f; // Tốc độ di chuyển của enemy
    public float changeDirectionTime = 2f; // Thời gian thay đổi hướng
    Animator anim;
    private Vector2 targetPosition;
    private Rigidbody2D rb;
    private bool isFacingRight = true; // Biến để xác định hướng

    

    private void Update()
    {
        MoveTowardsTarget(); // Di chuyển enemy về phía vị trí mục tiêu
        FlipIfNeeded(); // Kiểm tra và lật enemy nếu cần

        // Kiểm tra nếu enemy đang di chuyển và đặt biến isRunning trong Animator
        bool isMoving = rb.velocity.magnitude > 0.1f;
        anim.SetBool("Runing", isMoving); // "isRunning" là tên parameter trong Animator
    }

    private void SetRandomTargetPosition()
    {
        // Tạo một vị trí ngẫu nhiên trong một phạm vi nhất định
        targetPosition = new Vector2(Random.Range(-8f, 8f), Random.Range(-4f, 4f)); // Điều chỉnh phạm vi theo nhu cầu của bạn
    }

    private void MoveTowardsTarget()
    {
        // Tính hướng và vận tốc
        Vector2 direction = (targetPosition - rb.position).normalized; // Tính hướng từ enemy đến vị trí mục tiêu
        rb.velocity = direction * moveSpeed; // Đặt vận tốc cho Rigidbody2D
    }

    private void FlipIfNeeded()
    {
        // Kiểm tra hướng di chuyển để quyết định có cần lật hình hay không
        if ((rb.velocity.x > 0 && !isFacingRight) || (rb.velocity.x < 0 && isFacingRight))
        {
            isFacingRight = !isFacingRight;
            Vector3 scale = transform.localScale;
            scale.x *= -1; // Lật theo trục x
            transform.localScale = scale;
        }
    }
    */
    private void Start()
    {
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

        GameObject PlayerP = GameObject.FindWithTag("Player");
        pl = PlayerP.GetComponent<Rigidbody2D>();
    }
    public void Update()
    {
        if (IsPlayerInRange())
        {
            Attack();
        }
        else 
        {
            OnAttackComplete();
        }

        if (pl == null) 
        {
            Debug.Log("DUng");
        }

        FacePlayer();
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

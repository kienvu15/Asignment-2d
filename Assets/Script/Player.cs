using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Player : MonoBehaviour
{
    public float moveSpeed = 5f;
    private Rigidbody2D rb;
    private Vector2 movement;

    Animator anim;

    // Biến để theo dõi hướng của nhân vật
    private bool facingRight = true;
    private bool isAttacking = false; // Biến để theo dõi trạng thái tấn công

    // Start is called before the first frame update
    void Start()
    {
        rb = GetComponent<Rigidbody2D>();
        anim = GetComponent<Animator>(); // Gán Animator vào biến anim
    }

    // Update is called once per frame
    void Update()
    {
        // Nhận đầu vào từ người chơi khi không trong trạng thái tấn công
        if (!isAttacking)
        {
            movement.x = Input.GetAxisRaw("Horizontal");
            movement.y = Input.GetAxisRaw("Vertical");
            movement.Normalize();

            // Gọi hàm "Run" từ animator nếu có chuyển động
            if (movement.magnitude > 0)
            {
                anim.SetBool("Run", true);
                Flip(movement.x); // Gọi hàm Flip khi có chuyển động
            }
            else
            {
                anim.SetBool("Run", false);
            }
        }

        // Kiểm tra nếu nhấn phím "B" để kích hoạt tấn công
        if (Input.GetKeyDown(KeyCode.B) && !isAttacking)
        {
            anim.SetTrigger("attack");       // Gọi trigger "attack" trong animator
            anim.SetBool("isAttacking", true); // Đặt isAttacking trong animator
            isAttacking = true;               // Đặt trạng thái isAttacking để ngăn di chuyển
        }
    }

    void FixedUpdate()
    {
        // Di chuyển nhân vật nếu không trong trạng thái tấn công
        if (!isAttacking)
        {
            rb.MovePosition(rb.position + movement * moveSpeed * Time.fixedDeltaTime);
        }
    }

    // Hàm để lật nhân vật
    void Flip(float horizontalMovement)
    {
        // Nếu nhân vật di chuyển sang phải và đang quay sang trái
        if (horizontalMovement > 0 && !facingRight)
        {
            // Lật nhân vật sang phải
            facingRight = true;
            Vector3 scale = transform.localScale;
            scale.x = Mathf.Abs(scale.x); // Đặt chiều rộng thành dương
            transform.localScale = scale;
        }
        // Nếu nhân vật di chuyển sang trái và đang quay sang phải
        else if (horizontalMovement < 0 && facingRight)
        {
            // Lật nhân vật sang trái
            facingRight = false;
            Vector3 scale = transform.localScale;
            scale.x = -Mathf.Abs(scale.x); // Đặt chiều rộng thành âm
            transform.localScale = scale;
        }
    }

    // Hàm được gọi khi animation tấn công kết thúc (sử dụng Animation Event)
    public void OnAttackComplete()
    {
        anim.ResetTrigger("attack");        // Đặt lại trigger attack
        anim.SetBool("isAttacking", false); // Đặt lại bool isAttacking về false trong animator
        isAttacking = false;                // Reset trạng thái tấn công trong script
    }

    // Hàm Trigger để chuyển Layer khi di chuyển giữa các tầng
    private void OnTriggerEnter2D(Collider2D other)
    {
        // Kiểm tra nếu nhân vật đang vào tầng 1
        if (other.CompareTag("Floor1Trigger"))
        {
            gameObject.layer = LayerMask.NameToLayer("Floor1"); // Đổi Layer thành Floor1
        }
        // Kiểm tra nếu nhân vật đang vào tầng 2
        else if (other.CompareTag("Floor2Trigger"))
        {
            gameObject.layer = LayerMask.NameToLayer("Floor2"); // Đổi Layer thành Floor2
        }
        else if (other.CompareTag("DefaultTigger"))
        {
            gameObject.layer = LayerMask.NameToLayer("Default"); // Đổi Layer thành Floor2
        }
    }
}

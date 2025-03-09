using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEditor.Animations;
using UnityEngine;
using UnityEngine.Tilemaps;
using UnityEngine.UI;

public class PlayerController : MonoBehaviour
{
    Enemy enemy;
    Rigidbody2D Player;
    BoxCollider2D Pl;
    Animator anim;
    TilemapRenderer BrigdetilemapRenderer;
    [SerializeField] GameObject AttackHitBox;
    [SerializeField] GameObject AttackYHitBox;
    [SerializeField] GameObject MinusAttackYHitBox;
    [SerializeField] GameObject Pivot;
    [SerializeField] GameObject Copy;

    [SerializeField] public CharacterData characterData;
    public float moveSpeed = 5f;
    [SerializeField] public ArrowData arrowData;
    private float OriginalmoveSpeed;
    private Coroutine gobackmovespeed;
    private float movementX, movementY;
    private bool isFacingRight = true;
    
    
    [SerializeField] GameObject Arrow;
    [SerializeField] GameObject Player23;
    

    [SerializeField] public float maxHealth = 3f;
    /*[SerializeField] private HealthDisplay healthDisplay;*/
    [SerializeField] private Slider healthSlider;
    private float currentHealth;

    public MusicManager musicManager;
    public ScoreManager scoreManager;
    private bool isScale= false;
    void Start()
    {
        currentHealth = maxHealth;
        healthSlider.maxValue = maxHealth;
        healthSlider.value = currentHealth;

        Player = GetComponent<Rigidbody2D>();
        anim = GetComponent<Animator>();
        Pl = GetComponent<BoxCollider2D>();
        GameObject Bridge = GameObject.FindWithTag("Brigde");
        if(Bridge != null )
        {
            BrigdetilemapRenderer = Bridge.GetComponent<TilemapRenderer>();
        }

        OriginalmoveSpeed = moveSpeed;
        musicManager = FindObjectOfType<MusicManager>();
        scoreManager = FindObjectOfType<ScoreManager>();
    }

    void Update()
    {
        Cpopu();
            Move();
        
        
        Flip();
        Attack();
        Dash();
        Shot();
        Shot2();
        

        if (currentHealth <= 0)
        {
            anim.Play("Die");
            Pl.enabled = false;
        }

        Scale();
        

    }

    void FixedUpdate()
    {
        
    }
    public void Scale()
    {
        if (Input.GetKeyDown(KeyCode.Z) && isScale == false)
        {

            Vector3 Scalepl = Player.transform.localScale;
            Scalepl = new Vector3(2, 2, 2);
            Player.transform.localScale = Scalepl;
            isScale = true;
        }
        else if (Input.GetKeyDown(KeyCode.Z) && isScale == true)
        {
            Vector3 Scalepl = Player.transform.localScale;
            Scalepl = new Vector3(1, 1, 1);
            Player.transform.localScale = Scalepl;
            isScale = false;
        }

    }
    public void Scale2()
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
            Player.velocity = moment * characterData.dashForce * characterData.dashSpeed;
        }
    }

    public void Attack()
    {
        if (Input.GetKeyDown(KeyCode.V))
        {
            if (movementY > 0) 
            {
                anim.SetTrigger("yattack");
                characterData.moveSpeed = 0;
            }
            else if (movementY < 0) 
            {
                anim.SetTrigger("minusYattack");
                characterData.moveSpeed = 0;
            }
            else 
            {
                anim.SetTrigger("attack");
                characterData.moveSpeed = 0;
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
        characterData.moveSpeed = OriginalmoveSpeed;
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
            StartCoroutine(ContinuousShooting());
        }

        if (Input.GetKeyDown(KeyCode.C) && (movementY > 0))
        {
            StartCoroutine(ContinuousShooting2());
        }

        else if (Input.GetKeyDown(KeyCode.C) && (movementY < 0))
        {
            StartCoroutine(ContinuousShooting3());
        }
    }

    
    public void Shot2()
    {
        
        if (Input.GetKeyDown(KeyCode.F))
        {
            
            GameObject shot = Instantiate(Arrow, Pivot.transform.position, Quaternion.identity);
            Rigidbody2D arrow = shot.GetComponent<Rigidbody2D>();
            if (isFacingRight)
            {
                arrow.velocity = new Vector2(arrowData.arrowSpeed, 0);
            }
            else
            {
                arrow.velocity = new Vector2(-arrowData.arrowSpeed, 0);
                Vector3 Scale = arrow.transform.localScale;
                Scale.x *= -1;
                arrow.transform.localScale = Scale;
            }
            Destroy(shot, 2f);
        }
        
    }

    public IEnumerator ContinuousShooting()
    {
        for(int i = 1; i < 5; i++)
        {
            GameObject shot = Instantiate(Arrow, AttackHitBox.transform.position, Quaternion.identity);
            Rigidbody2D arrow = shot.GetComponent<Rigidbody2D>();
            if (isFacingRight)
            {
                arrow.velocity = new Vector2(arrowData.arrowSpeed, 0);            
            }
            else
            {
                arrow.velocity = new Vector2(-arrowData.arrowSpeed, 0);
                Vector3 Scale = arrow.transform.localScale;
                Scale.x *= -1;
                arrow.transform.localScale = Scale; 
            }
            Destroy(shot, 2f);
            yield return new WaitForSeconds(0.5f);
        }
    }

    public IEnumerator ContinuousShooting2()
    {
        for (int i = 1; i < 5; i++)
        {
            GameObject shot = Instantiate(Arrow, Pivot.transform.position, Quaternion.Euler(0, 0, 90));
            Rigidbody2D arrow = shot.GetComponent<Rigidbody2D>();
            arrow.velocity = new Vector2(0, arrowData.arrowSpeed);
            Destroy(shot, 2f);

            
            yield return new WaitForSeconds(0.5f);
        }
    }

    public IEnumerator ContinuousShooting3()
    {
        for (int i = 1; i < 5; i++)
        {
            GameObject shot = Instantiate(Arrow, Pivot.transform.position, Quaternion.Euler(0, 0, 90));
            Rigidbody2D arrow = shot.GetComponent<Rigidbody2D>();
            arrow.velocity = new Vector2(0, -arrowData.arrowSpeed);
            Destroy(shot, 2f);


            yield return new WaitForSeconds(0.5f);
        }
    }


    public void Cpopu()
    {
        if (Input.GetKeyDown(KeyCode.RightShift))
        {
            GameObject Player2 = Instantiate(Player23, Copy.transform.position, Quaternion.identity);
            
        }
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
            scoreManager.AddScore(1);
            Destroy(collision.gameObject);
            /*maxHealth++;
            healthDisplay.UpdateHearts(maxHealth);*/

            moveSpeed = moveSpeed * 3;
            gobackmovespeed = StartCoroutine(OriginalMoveSpeed());
        }
        if (collision.CompareTag("Gbattackhitbox"))
        {
            
            currentHealth--;
            healthSlider.value = currentHealth;
            /*healthDisplay.UpdateHearts(maxHealth); */
        }
        if (collision.CompareTag("Dynamite"))
        {
            
            currentHealth--;
            healthSlider.value = currentHealth;
            /*healthDisplay.UpdateHearts(maxHealth);*/
        }
        if (collision.CompareTag("AttackRange"))
        {
            // Phát nhạc chiến đấu khi va chạm với quái
            musicManager.StartBattleMusic();
        }
    }

    public IEnumerator OriginalMoveSpeed()
    {
        yield return new WaitForSeconds(3);
        moveSpeed = OriginalmoveSpeed;
    }
    void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.gameObject.CompareTag("Enemy"))
        {
            // Phát nhạc chiến đấu khi va chạm với quái
            musicManager.StartBattleMusic();
        }
    }
    void OnCollisionExit2D(Collision2D collision)
    {
        if (collision.gameObject.CompareTag("Enemy"))
        {
            // Tắt nhạc chiến đấu khi rời khỏi quái
            musicManager.StopBattleMusic();
        }
    }

    private void OnTriggerExit2D(Collider2D collision)
    {
        if (collision.CompareTag("AttackRange"))
        {
            musicManager.StopBattleMusic();
        }
    }

    public void destroyPlayer()
    {
        Destroy(gameObject);
    }
}

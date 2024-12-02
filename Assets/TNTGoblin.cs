using JetBrains.Annotations;
using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.UI;

public class TNTGoblin : MonoBehaviour
{
    // Start is called before the first frame update
    Rigidbody2D rb;
    Animator anim;

    public Transform pivot;
    public Transform Player;
    public GameObject Dynamine;
    
    [SerializeField] private Slider healthSlider;

    private bool isFacingRight = true;
    
    public AttackRangeDY RangeDY;

    
    private float maxHealth = 3f;
    private float currentHealth;

    

    void Start()
    {
        rb = GetComponent<Rigidbody2D>();
        anim = GetComponent<Animator>();

        currentHealth = maxHealth;
        healthSlider.maxValue = maxHealth;
        healthSlider.value = currentHealth;

        InvokeRepeating(nameof(Shot), 0f, 3f);
    }

    // Update is called once per frame
    void Update()
    {
        Flip();
        StartShoting();
    }

    public void StartShoting()
    {
        if (RangeDY.CanShot)
        {
            anim.SetTrigger("canShot");
        }
    }
    public void Shot()
    {
        if (RangeDY.CanShot)
        {
            Vector2 targetPosition = Player.position;
            GameObject dynamineoj = Instantiate(Dynamine, pivot.position, Quaternion.identity);
            Bullet bullet = dynamineoj.GetComponent<Bullet>();
            bullet.SetTarget(targetPosition);
        }
    }


    public void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.CompareTag("AttackPosition"))
        {
            currentHealth --;
            healthSlider.value = currentHealth;

            if (currentHealth <= 0)
            {
                Die();
            }
        }
        if (collision.CompareTag("Arrow"))
        {
            currentHealth--;
            healthSlider.value = currentHealth;
            if (currentHealth <= 0)
            {
                Die();

            }
        }

    }

    private void Die()
    {
        Destroy(gameObject);
    }

    public void Flip()
    {
        if (Player.transform.position.x > rb.position.x && !isFacingRight)
        {
            isFacingRight = !isFacingRight;
            Vector3 scale = rb.transform.localScale;
            scale.x *= -1;
            rb.transform.localScale = scale;
        }else if(Player.transform.position.x<rb.position.x && isFacingRight)
        {
            isFacingRight = !isFacingRight;
            Vector3 scale = rb.transform.localScale;
            scale.x *= -1;
            rb.transform.localScale = scale;
        }
    }

}

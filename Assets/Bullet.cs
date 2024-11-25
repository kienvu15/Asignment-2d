using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Bullet : MonoBehaviour
{
    Animator anim;
    private Vector2 targetPosition; 
    public float moveSpeed = 5f;    

    private bool startEx = false;
    private Coroutine explosion;
    private void Start()
    {
        anim = GetComponent<Animator>();
    }
    public void SetTarget(Vector2 target)
    {
        targetPosition = target; 
    }

    void Update()
    {
        transform.position = Vector2.MoveTowards(transform.position, targetPosition, moveSpeed * Time.deltaTime);
        if (Vector2.Distance(transform.position,targetPosition) < 0.1f)
        {
            StopAtTarget();
            
        }
    }

    private void StopAtTarget()
    {
        Rigidbody2D rb = GetComponent<Rigidbody2D>();
        if (rb != null)
        {
            rb.velocity = Vector2.zero;
            rb.isKinematic = true;
        }
        transform.position = targetPosition;

        Invoke(nameof(TriggerExplosion), 0f); // Thay thế Coroutine
    }

    private void TriggerExplosion()
    {
        anim.SetBool("exx", true);
    }


    private IEnumerator TriggerExplosionAfterDelay(float delay)
    {
        yield return new WaitForSeconds(delay);

        if (startEx)
        {
            anim.SetBool("exx", true);
            
        }
    }

    public void On0Destroy()
    {
        Destroy(gameObject);
    }
}

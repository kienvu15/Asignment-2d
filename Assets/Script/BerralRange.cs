using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BerralRange : MonoBehaviour
{
    [SerializeField] private Animator anim;

    
    private bool playerInTrigger = false;
    private Coroutine explosionCoroutine;
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.CompareTag("Player")) 
        {
            anim.SetBool("Awaken", true);
            playerInTrigger = true;
            explosionCoroutine = StartCoroutine(TriggerExplosionAfterDelay(3f));
        }
    }

    public void OnTriggerExit2D(Collider2D collision)
    {
        if(collision.CompareTag("Player"))
        {
            anim.SetBool("Awaken", false);
            playerInTrigger = false;

            
            if (explosionCoroutine != null)
            {
                StopCoroutine(explosionCoroutine);
                explosionCoroutine = null;
            }
        }
    }
    private IEnumerator TriggerExplosionAfterDelay(float delay)
    {
        yield return new WaitForSeconds(delay);

        if (playerInTrigger) 
        {
            anim.Play("Berral-explosion"); 
            
        }
    }
 

}

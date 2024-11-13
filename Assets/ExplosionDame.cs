using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ExplosionDame : MonoBehaviour
{
    PlayerController player;
    Rigidbody rb;
    [SerializeField] private HealthDisplay healthDisplay;

    // Start is called before the first frame update
    void Start()
    {
        player = GameObject.FindWithTag("Player").GetComponent<PlayerController>();
        rb = GetComponent<Rigidbody>();
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.CompareTag("Player"))
        {
            player.maxHealth--;
            healthDisplay.UpdateHearts(player.maxHealth);
        }
    }
}

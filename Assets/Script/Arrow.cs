using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Arrow : MonoBehaviour
{
    Rigidbody2D Rigidbody;
    public Transform Arrowposition;
    public GameObject Explosion;
    // Start is called before the first frame update
    void Start()
    {
        Rigidbody = GetComponent<Rigidbody2D>();
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.CompareTag("Enemy"))
        {
            GameObject Explo = Instantiate(Explosion, Rigidbody.transform.position, Quaternion.identity);
            Destroy(Explo, 1f);
        }
    }
}

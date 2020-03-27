using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Enemy : MonoBehaviour
{
    private Rigidbody2D rb;
    public float speed;
    public GameObject deathParticle;

    private void Start()
    {
        rb = GetComponent<Rigidbody2D>();
    }

    private void Update()
    {
        
    }

    public void Destroy()
    {
        Destroy(gameObject);
        Instantiate(deathParticle, transform.position, Quaternion.identity);
    }


}

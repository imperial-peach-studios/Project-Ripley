using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BulletBehaviour : MonoBehaviour
{
    [SerializeField] private float mySpeed;
    private float myDirection;
    [SerializeField] private float myDeathRate;
    private float myDeathTimer;

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        myDeathTimer += Time.deltaTime;

        transform.Translate(new Vector2(mySpeed, myDirection));
        if (myDeathTimer > myDeathRate)
        {
            Destroy(gameObject);
        }
    }

    void OnTriggerEnter2D(Collider2D other)
    {
        if (other.transform.tag == "Wall")
        {
            Destroy(gameObject);
        }
        else if (other.transform.tag == "Enemy")
        {
            Destroy(gameObject);
        }
    }
}

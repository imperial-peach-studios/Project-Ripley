﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BulletBehaviour : MonoBehaviour
{
    public float speed;
    public float deathRate;
    public float deathTimer = 0;
    public float direction;
    float spreadFactor;
    float damage;

    public bool knockBack = false;
    public bool stun = false;
    public float knockBackPower;
    public float knockBackLength;
    public float stunLength;

    public float SpreadFactor
    {
        set
        {
            spreadFactor = value;
        }
    }
    public float Damage
    {
        set
        {
            damage = value;
        }
    }


    void Start ()
    {
        direction += Random.Range(-spreadFactor, spreadFactor);
	}

    void Update()
    {
        deathTimer += Time.deltaTime;

        transform.Translate(new Vector2(speed, direction));
        if (deathTimer > deathRate)
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
        else if(other.transform.tag == "Enemy")
        {
            Debug.Log("HE");
            Vector2 knockBackDirection = other.transform.position - transform.position;
            knockBackDirection.Normalize();

            EnemyKnockedBack enemyKnock = other.GetComponent<EnemyKnockedBack>();
            enemyKnock.GetKockedBackInfo(knockBack, knockBackDirection, knockBackLength, knockBackPower);
            EnemyStunned enemyStunned = other.GetComponent<EnemyStunned>();
            enemyStunned.GetStunnedInfo(stun, stunLength);

            Destroy(gameObject);

        }
    }
}
using System.Collections;
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
    public Properties p;

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


    void Start()
    {
        direction += Random.Range(-p.spreadFactor, p.spreadFactor);
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

    public void GetHitInfo(float knockL, float knockB, float stunL, float d)
    {
        knockBackLength = knockL;
        knockBackPower = knockB;
        stunLength = stunL;
        damage = d;
    }

    void OnTriggerEnter2D(Collider2D other)
    {
        if (other.transform.tag == "Wall")
        {
            Destroy(gameObject);
        }
        else if(other.transform.tag == "Enemy")
        {
            //Debug.Log("Hit Enemy");
            Vector2 knockBackDirection = other.transform.position - transform.position;
            knockBackDirection.Normalize();

            EnemyKnockedBack enemyKnock = other.GetComponent<EnemyKnockedBack>();
            //enemyKnock.GetKockedBackInfo(knockBack, knockBackDirection, knockBackLength, knockBackPower);
            //enemyKnock.GetKockedBackInfo(knockBack, knockBackDirection, p.knockLength, p.knockBack);
            enemyKnock.GetKockedBackInfo(knockBack, knockBackDirection, knockBackLength, knockBackPower);

            EnemyStunned enemyStunned = other.GetComponent<EnemyStunned>();
            //enemyStunned.GetStunnedInfo(stun, p.stunLength);
            enemyStunned.GetStunnedInfo(stun, stunLength);

            EnemyHealth enemyHealth = other.GetComponent<EnemyHealth>();
            //enemyHealth.DecreaseHealthWith(p.damage);
            enemyHealth.DecreaseHealthWith(damage);

            Destroy(gameObject);
        }
    }
}
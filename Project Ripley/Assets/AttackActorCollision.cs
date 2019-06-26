using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AttackActorCollision : MonoBehaviour
{
    //[SerializeField] LayerMask collideWithLayer;
    //bool hit = false;
    bool isAttacking = false;
    //List<GameObject> enemiesHit = new List<GameObject>();
    GameObject enemyHit;
    bool giveDamage = false;
    [SerializeField] bool currentlyKnocking;
    [SerializeField] bool currentlyStunning;

    float damage;
    float knockBack;
    float stan;
    float knockLength;
    ItemSettings itemSettings;

    public void SetHasAttacked(bool hasAttacked)
    {
        isAttacking = hasAttacked; 
    }

    void OnTriggerStay2D(Collider2D collision)
    {
        if(isAttacking && collision.gameObject.layer == LayerMask.NameToLayer("Enemy") && collision.gameObject.tag == "Enemy")
        {
            if(enemyHit != collision.gameObject)
            {
                enemyHit = collision.gameObject;
                giveDamage = false;
            }
        }
    }

    void Update()
    {
        if(enemyHit != null && giveDamage == false)
        {
            Debug.Log("Gave Damage To Enemy: " + enemyHit.name);

            Vector2 knockBackDirection = enemyHit.transform.position - transform.position;
            Debug.Log(knockBackDirection);
            knockBackDirection.Normalize();

            EnemyKnockedBack enemyKnock = enemyHit.GetComponent<EnemyKnockedBack>();
            enemyKnock.GetKockedBackInfo(currentlyKnocking, knockBackDirection, knockLength, knockBack);
            EnemyStunned enemyStunned = enemyHit.GetComponent<EnemyStunned>();
            enemyStunned.GetStunnedInfo(currentlyStunning, stan);

            itemSettings.Decrease();
            giveDamage = true;
        }
    }

    public void UpdateStats(float knockBackStrength, float knockBackLength, float stan, float damage, ItemSettings itemSettings)
    {
        this.damage = damage;
        this.knockBack = knockBackStrength;
        this.knockLength = knockBackLength;
        this.stan = stan;
        this.itemSettings = itemSettings;
    }

    public void ResetEnemyHit()
    {
        enemyHit = null;
    }
}
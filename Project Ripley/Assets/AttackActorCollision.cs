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
    float waitAfterAttackTimer = 0;
    [SerializeField] float waitAfterAttackLenght;
    [SerializeField] float shrinkSpeed;
    [SerializeField] float shrinkSize;
    Vector3 previousScale;

    float damage;
    float knockBack;
    float stan;
    float knockLength;
    ItemSettings itemSettings;
    Animator anim;

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
            previousScale = transform.parent.transform.localScale;
            //anim.StopPlayback();
            transform.parent.localScale = new Vector3(transform.parent.localScale.x * shrinkSize, transform.parent.localScale.y * shrinkSize, transform.parent.localScale.z * shrinkSize);
            anim.speed = 0;
            
            giveDamage = true;
        }

        if(anim.speed == 0)
        {
            waitAfterAttackTimer += Time.deltaTime;
            
            transform.parent.localScale = Vector3.MoveTowards(transform.parent.localScale, previousScale, shrinkSpeed * Time.deltaTime);

            if(waitAfterAttackTimer > waitAfterAttackLenght)
            {
                waitAfterAttackTimer = 0;
                anim.speed = 1;
            }
        }
    }

    public void UpdateStats(float knockBackStrength, float knockBackLength, float stan, float damage, ItemSettings itemSettings, Animator animator)
    {
        this.damage = damage;
        this.knockBack = knockBackStrength;
        this.knockLength = knockBackLength;
        this.stan = stan;
        this.itemSettings = itemSettings;
        this.anim = animator;
    }

    public void ResetEnemyHit()
    {
        enemyHit = null;
    }
}
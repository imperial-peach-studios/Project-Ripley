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
    [SerializeField] float waitAfterAttackLength;
    
    [SerializeField] Animator anim;
    Vector3 previousScale;
    [SerializeField] Vector3 shrinkSize;
    [SerializeField] float shrinkSpeed;
    float damage;
    float knockBack;
    float stan;
    float knockLength;
    ItemSettings itemSettings;

    public void SetHasAttacked(bool hasAttacked)
    {
        isAttacking = hasAttacked; 
    }

    void OnTriggerEnter2D(Collider2D collision)
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

            //Properties p = Equipment.Instance.CurrentSelectedItem()?.GetComponent<ItemInfo>()?.Properties;
            ItemInfo iI = Equipment.Instance.CurrentSelectedItem()?.GetComponent<ItemInfo>();
             
            Vector2 knockBackDirection = enemyHit.transform.position - transform.position;
            knockBackDirection.Normalize();

            EnemyKnockedBack enemyKnock = enemyHit.GetComponent<EnemyKnockedBack>();
            enemyKnock.GetKockedBackInfo(currentlyKnocking, knockBackDirection, iI.knockLength, iI.knockBack);
            EnemyStunned enemyStunned = enemyHit.GetComponent<EnemyStunned>();
            enemyStunned.GetStunnedInfo(currentlyStunning, iI.stunLength);

            EnemyHealth enemyHealth = enemyHit.GetComponent<EnemyHealth>();
            enemyHealth.DecreaseHealthWith(iI.damage);

            //itemSettings.Decrease();
            //p.Decrease();
            Equipment.Instance.CurrentSelectedItem()?.GetComponent<ItemInfo>().DecreaseDurability();

            previousScale = transform.parent.localScale;
            //transform.parent.localScale *= shrinkSize;
            transform.parent.localScale = new Vector3(transform.parent.localScale.x * shrinkSize.x, transform.parent.localScale.y * shrinkSize.y, transform.parent.localScale.z * shrinkSize.z);

            anim.speed = 0f;
            //anim.playbackTime = 0.10f;
            //anim.StopPlayback();
            giveDamage = true;
        }

        if(anim != null)
        {
            if (anim.speed != 1)
            {
                waitAfterAttackTimer += Time.deltaTime;

                transform.parent.localScale = Vector3.MoveTowards(transform.parent.localScale, previousScale, shrinkSpeed * Time.deltaTime);

                if (waitAfterAttackTimer > waitAfterAttackLength)
                {
                    //anim.speed = 0.8f;
                    //anim.speed = 1f;
                    anim.speed = 1f;

                    //anim.playbackTime = 1;
                    //ResetEnemyHit();
                    waitAfterAttackTimer = 0;
                }
            }
        }
    }

    public void UpdateStats(float knockBackStrength, float knockBackLength, float stan, float damage, ItemSettings itemSettings, Animator anim)
    {
        this.damage = damage;
        this.knockBack = knockBackStrength;
        this.knockLength = knockBackLength;
        this.stan = stan;
        this.itemSettings = itemSettings;
        this.anim = anim;
    }

    public void ResetEnemyHit()
    {
        enemyHit = null;
        giveDamage = false;
        isAttacking = false;
        //anim.speed = 1;
    }
}
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MeleeCollisionManager : MonoBehaviour
{
    bool isAttacking = false;
    //List<GameObject> enemiesHit = new List<GameObject>();
    GameObject enemyHit;
    bool giveDamage = false;
    [SerializeField] bool currentlyKnocking;
    [SerializeField] bool currentlyStunning;
    float waitAfterAttackTimer = 0;
    [SerializeField] float waitAfterAttackLength;

    private Animator myAnim;

    void Start()
    {
        myAnim = GetComponentInParent<Animator>();
    }

    public void SetHasAttacked(bool hasAttacked)
    {
        isAttacking = hasAttacked;
    }

    void OnTriggerEnter2D(Collider2D collision)
    {
        if (isAttacking && collision.gameObject.layer == LayerMask.NameToLayer("Enemy"))
        {
            if (enemyHit != collision.gameObject)
            {
                enemyHit = collision.gameObject;
                giveDamage = false;
            }
        }
    }

    void Update()
    {
        if (enemyHit != null && giveDamage == false)
        {
            Debug.Log("Gave Damage To Enemy: " + enemyHit.name);

            Item currentItem = Equipment.Instance.GetSelectedItem();
            //currentItem.DecreaseDurability();

            myAnim.speed = 0f;
            giveDamage = true;
        }

        if (myAnim != null)
        {
            if (myAnim.speed != 1)
            {
                waitAfterAttackTimer += Time.deltaTime;

                if (waitAfterAttackTimer > waitAfterAttackLength)
                {
                    myAnim.speed = 1f;
                    waitAfterAttackTimer = 0;
                }
            }
        }
    }

    public void ResetEnemyHit()
    {
        enemyHit = null;
        giveDamage = false;
        isAttacking = false;
    }
}
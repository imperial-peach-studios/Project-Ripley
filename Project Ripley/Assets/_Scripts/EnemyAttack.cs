using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyAttack : MonoBehaviour
{
    [SerializeField] float attackRate = 1f;
    [SerializeField] float distanceToFromAttack;
    [SerializeField] int damage;

    [SerializeField] EnemyAttackCollision eAC;

    private float attackTimer = 0f;
    private bool readyToAttack = false;
    private bool attack = false;
    EnemyInfo enemyInfo;

    void Awake()
    {
        enemyInfo = GetComponent<EnemyInfo>();
    }

    void Update()
    {
        eAC.SetDamage(damage);

        if (enemyInfo.GetKnockedDown())
        {
            attackTimer = 0;
            readyToAttack = false;
        }
        else if(enemyInfo.GetCurrentSight())
        {
            readyToAttack = InDistanceAndCanAttack(enemyInfo.GetLastPosition());
        }
        else
        {
            readyToAttack = false;
        }

        if (readyToAttack)
        {
            attackTimer += Time.deltaTime;

            if (attackTimer > attackRate)
            {
                attackTimer = 0;
                Attack();
            }
        }
        else
        {
            attackTimer = 0;
        }

        enemyInfo.SetReadyToAttack(readyToAttack);
        enemyInfo.SetAttacked(attack);
    }

    void OnDrawGizmos()
    {
        Gizmos.color = Color.green;
        Gizmos.DrawWireSphere(transform.position, distanceToFromAttack);        
    }

    void Attack()
    {
        attack = true;
    }

    void ResetAttack()
    {
        attack = false;
    }

    public void SetAttackToFalse()
    {
        attack = false;
    }

    public bool InDistanceAndCanAttack(Vector2 target)
    {
        float targetDistance = Vector2.Distance(transform.position, target);

        if(targetDistance <= distanceToFromAttack)
            return true;

        return false;
    }
}
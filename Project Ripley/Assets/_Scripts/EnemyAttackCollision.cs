using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyAttackCollision : MonoBehaviour
{
    bool canAttack = false;
    int damage = 0;

    public void AtivateAttack()
    {
        canAttack = true;
    }
    public void DeActivateAttack()
    {
        canAttack = false;
    }

    void OnTriggerEnter2D(Collider2D collision)
    {
        if(collision.gameObject.layer == LayerMask.NameToLayer("Player"))
        {
            if(canAttack)
            {
                PlayerHealth pH = collision.gameObject.GetComponent<PlayerHealth>();

                if (pH != null)
                {
                    pH.DecreaseHealthWith(damage);
                }
            }
        }
    }

    public void SetDamage(int damage)
    {
        this.damage = damage;
    }
}
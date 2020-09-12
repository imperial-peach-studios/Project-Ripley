using System.Collections;
using System.Collections.Generic;
using UnityEngine;
//using Pathfinding;

public class EnemyAnimation : MonoBehaviour
{
    EnemyInfo enemyInfo;
    EnemyEvent enemyEvent;
    EnemyHealth enemyHealth;
    //AIPath path;
    Animator anim;

    void Awake()
    {
        enemyEvent = GetComponent<EnemyEvent>();
        enemyHealth = GetComponent<EnemyHealth>();
        //path = GetComponent<AIPath>();
        anim = GetComponentInChildren<Animator>();
    }

    void Update()
    {
        float moving = 0f;
        //if(path.velocity != Vector3.zero)
        //{
        //    moving = 1;
        //}

        if(enemyHealth.IsDead())
        {
            anim.SetBool("Dead", true);
        }
        else
        {
            if(anim.GetBool("Dead"))
            {
                anim.SetBool("Dead", false);
            }
        }

        anim.SetFloat("Horizontal", enemyEvent.GetEnemyInfo().GetCurrentDirection().x);
        anim.SetFloat("Vertical", enemyEvent.GetEnemyInfo().GetCurrentDirection().y);
        anim.SetFloat("Moving", moving);
        anim.SetBool("Attacked", enemyEvent.GetEnemyInfo().HasAttacked());
    }
}
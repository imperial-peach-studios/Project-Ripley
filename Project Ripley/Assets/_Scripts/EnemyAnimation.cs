using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Pathfinding;

public class EnemyAnimation : MonoBehaviour
{
    EnemyInfo enemyInfo;
    EnemyHealth enemyHealth;
    AIPath path;
    Animator anim;

    // Start is called before the first frame update
    void Awake()
    {
        enemyInfo = GetComponent<EnemyInfo>();
        enemyHealth = GetComponent<EnemyHealth>();
        path = GetComponent<AIPath>();
        anim = GetComponentInChildren<Animator>();
    }

    void Update()
    {
        float moving = 0f;
        if(path.velocity != Vector3.zero)
        {
            moving = 1;
        }

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

        anim.SetFloat("Horizontal", enemyInfo.GetCurrentDirection().x);
        anim.SetFloat("Vertical", enemyInfo.GetCurrentDirection().y);
        anim.SetFloat("Moving", moving);
        anim.SetBool("Attacked", enemyInfo.HasAttacked());
    }
}
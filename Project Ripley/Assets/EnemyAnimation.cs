using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Pathfinding;

public class EnemyAnimation : MonoBehaviour
{
    EnemyInfo enemyInfo;
    AIPath path;
    Animator anim;

    // Start is called before the first frame update
    void Start()
    {
        enemyInfo = GetComponent<EnemyInfo>();
        path = GetComponent<AIPath>();
        anim = GetComponentInChildren<Animator>();
    }

    // Update is called once per frame
    void Update()
    {
        float moving = 0f;
        if(path.velocity != Vector3.zero)
        {
            moving = 1;
        }

        anim.SetFloat("Horizontal", enemyInfo.GetCurrentDirection().x);
        anim.SetFloat("Vertical", enemyInfo.GetCurrentDirection().y);
        anim.SetFloat("Moving", moving);
        anim.SetBool("Attacked", enemyInfo.HasAttacked());
    }
}
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyStunned : MonoBehaviour
{
    EnemyEvent enemyEvent;
    float stunnedLength;
    float stunnedTimer = 0;
    
    void Start()
    {
        enemyEvent = GetComponent<EnemyEvent>();
    }
    
    void Update()
    {
        if(enemyEvent.GetEnemyInfo().GetStunned())
        {
            stunnedTimer += Time.deltaTime;

            if(stunnedTimer > stunnedLength)
            {
                enemyEvent.GetEnemyInfo().SetStunned(false);
                stunnedTimer = 0;
            }
        }
    }

    public void GetStunnedInfo(bool stunned , float stunnedLength)
    {
        enemyEvent.GetEnemyInfo().SetStunned(stunned);
        this.stunnedLength = stunnedLength;
        stunnedTimer = 0;
    }
}
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyStunned : MonoBehaviour
{
    EnemyInfo enemyInfo;
    float stunnedLength;
    float stunnedTimer = 0;
    
    void Start()
    {
        enemyInfo = GetComponent<EnemyInfo>();
    }
    
    void Update()
    {
        if(enemyInfo.GetStunned())
        {
            stunnedTimer += Time.deltaTime;

            if(stunnedTimer > stunnedLength)
            {
                enemyInfo.SetStunned(false);
                stunnedTimer = 0;
            }
        }
    }

    public void GetStunnedInfo(bool stunned , float stunnedLength)
    {
        enemyInfo.SetStunned(stunned);
        this.stunnedLength = stunnedLength;
        stunnedTimer = 0;
    }
}
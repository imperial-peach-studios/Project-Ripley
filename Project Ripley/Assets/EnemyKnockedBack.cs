using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Pathfinding;

public class EnemyKnockedBack : MonoBehaviour
{
    EnemyInfo enemyInfo;
    Vector2 knockDir;
    float knockLength;
    float knockPower;
    float knockTimer = 0;

    void Start()
    {
        enemyInfo = GetComponent<EnemyInfo>();
    }

    public void GetKockedBackInfo(bool knocked, Vector2 direction, float knockedDownLength, float knockedDownPower)
    {
        enemyInfo.SetKnockedDown(knocked);
        knockDir = direction;
        knockLength = knockedDownLength;
        knockPower = knockedDownPower;
        knockTimer = 0;
    }

    void Update()
    {
        if(enemyInfo.GetKnockedDown())
        {
            knockTimer += Time.deltaTime;

            Vector2 newPos = ((Vector2)transform.position + (knockDir * knockPower));

            if(knockTimer < knockLength)
            {
                GetComponent<AIPath>().Teleport(newPos, true);
            }
            else if(knockTimer > knockLength)
            {
                enemyInfo.SetKnockedDown(false);
                knockTimer = 0;
            }
        }
    }
}
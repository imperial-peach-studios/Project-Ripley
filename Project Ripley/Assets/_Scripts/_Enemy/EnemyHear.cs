using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Pathfinding;

public class EnemyHear : MonoBehaviour
{
    [Range(0, 20)] [SerializeField] float hearingRange;
    [SerializeField] LayerMask collideWithLayers;
    [SerializeField] AIPath path;
    EnemyEvent enemyEvent;
    
    void Awake()
    {
        enemyEvent = GetComponent<EnemyEvent>();
    }

    void Update()
    {
        Vector2 origin = transform.position;
        float radius = hearingRange;

        Collider2D[] hitSound = Physics2D.OverlapCircleAll(origin, radius, collideWithLayers, 0f);
        
        foreach(Collider2D hit in hitSound)
        {
            path.destination = hit.transform.position;

            if (path.remainingDistance <= radius)
            {
                //Debug.Log("Sound Heard " + path.remainingDistance);

                PlaySoundManager p = hit.transform.GetComponent<PlaySoundManager>();

                if(p != null)
                {
                    if (p.IsInRange(transform.position) && !PlayersMovementData.InsideASafeHouse)
                    {
                        enemyEvent.GetEnemyInfo().SetLastSight(hit.transform.position);
                        enemyEvent.GetEnemyInfo().SetHeardNoise(true);
                    }
                }
            }
            else
            {
                //Debug.Log("Sound Ignored " + path.remainingDistance);
            }
        }
    }

    void OnDrawGizmosSelected()
    {
        Gizmos.color = Color.blue;
        Gizmos.DrawWireSphere(transform.position, hearingRange);
    }
}
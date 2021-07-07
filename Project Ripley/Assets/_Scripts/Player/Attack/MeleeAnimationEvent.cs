using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MeleeAnimationEvent : MonoBehaviour
{
    private MeleeCollisionManager myMeleeCollisionManager;
    private RangeManager myRangeManager;

    void Awake()
    {
        myMeleeCollisionManager = GetComponentInChildren<MeleeCollisionManager>();
        myRangeManager = GetComponentInChildren<RangeManager>();
    }

    public void AttackTriggerEnable()
    {
        myMeleeCollisionManager.SetHasAttacked(true);
    }
    public void AttackTriggerDisable()
    {
        myMeleeCollisionManager.ResetEnemyHit();
    }

    public void Fire()
    {
        myRangeManager.SetHasFired(true);
    }
}
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyResetAttack : MonoBehaviour
{
    EnemyAttack attack;

    void Start()
    {
        attack = GetComponentInParent<EnemyAttack>();   
    }

    public void SetAttackToFalse()
    {
        attack.SetAttackToFalse();
    }
}
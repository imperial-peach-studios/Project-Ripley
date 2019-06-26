using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(ItemInfo))]
public class MeleeWeaponManager : MonoBehaviour
{
    public MeleeWeaponsSO meleeWeaponsSO;
    public bool canAttack = false;
    public float attackCounter = 0;
    public float maxCombo;
    float firingTimer = 0;
    //public float firingRate;
    //public float damage;

    void Start()
    {
        //GetComponent<BoxCollider2D>().size = meleeWeaponsSO.attackCollisionSize;
        //GetComponent<BoxCollider2D>().offset = meleeWeaponsSO.attackCollisionOffset;
    }

    void Update()
    {
        if(attackCounter > maxCombo && canAttack == true)
        {
            attackCounter = 0;
        }
        if (Input.GetMouseButtonDown(0) && canAttack == false)
        {
            attackCounter += 1;
        }
    }
}
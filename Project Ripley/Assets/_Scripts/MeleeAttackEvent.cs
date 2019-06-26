using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MeleeAttackEvent : MonoBehaviour
{
    InventorySO invetory;
    PlayerAttack pA;
    [SerializeField] GameObject attackActor;
    ItemSettings itemSettings;
    AttackActorCollision attackA;
    FireActor fireA;

    void Start()
    {
        invetory = GetComponent<NewPlayerInvetory>().invetorySO;
        pA = GetComponent<PlayerAttack>();
        attackA = attackActor.GetComponent<AttackActorCollision>();
        fireA = attackActor.GetComponent<FireActor>();
    }

    public void AttackTriggerEnable()
    {
        attackA.enabled = true;

        ItemInfo itemInfo;

        if(invetory.currentWeapon == 1)
        {
            itemSettings = invetory.primary.GetComponent<ItemSettings>();
        }
        else
        {
            itemSettings = invetory.secondary.GetComponent<ItemSettings>();
        }

        attackA.UpdateStats(itemSettings.meleeOS.knockBack, itemSettings.meleeOS.knockLength, itemSettings.meleeOS.stanLength, itemSettings.meleeOS.damage, itemSettings);
        
        attackA.SetHasAttacked(true);
    }
    public void AttackTriggerDisable()
    {
        attackA.SetHasAttacked(false);
        attackA.ResetEnemyHit();

        attackA.enabled = false;
    }

    public void Fire()
    {
        fireA.enabled = true;

        if (invetory.currentWeapon == 1)
        {
            itemSettings = invetory.primary.GetComponent<ItemSettings>();
        }
        else
        {
            itemSettings = invetory.secondary.GetComponent<ItemSettings>();
        }
        GunSO gunSO = itemSettings.gunOS;

        fireA.GetFireInfo(gunSO.weaponBullet, gunSO.damage, gunSO.firingRate, gunSO.numberOfBulletsFired, gunSO.spreadFactor);
    }
}


//if(invetory.currentWeapon == 1)
//{
//    //invetory.primary.GetComponent<MeleeWeaponManager>().canAttack = true;

//}
//else
//{
//    invetory.secondary.GetComponent<MeleeWeaponManager>().canAttack = true;
//}
//pA.hasAttacked = true;

using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[ExecuteInEditMode]
public class ItemInfo : MonoBehaviour
{
    private Sprite uiIcon;
    private string itemInfo;
    private Vector2 CollisionBoxSize;
    private Vector2 pickUpBoxSize;
    private int animationID;
    ItemSettings itemSettings;
    MeleeWeaponsSO melee;
    GunSO gun;

    bool updateOnce = false;

    void Start()
   {
        //       if (GetComponent<ConsumableItemManager>() != null)
        //       {
        //           itemInfo = GetComponent<ConsumableItemManager>().consumableItem.info;
        //           uiIcon = GetComponent<ConsumableItemManager>().consumableItem.UIIcon;
        //           CollisionBoxSize = GetComponent<ConsumableItemManager>().consumableItem.CollisionBoxSize;
        //           pickUpBoxSize = GetComponent<ConsumableItemManager>().consumableItem.pickUpBoxSize;
        //       }
        //       else if(GetComponent<WeaponManager>() != null)
        //       {
        //           itemInfo = GetComponent<WeaponManager>().weaponCreator.info;
        //           uiIcon = GetComponent<WeaponManager>().weaponCreator.uiIcon;
        //           CollisionBoxSize = GetComponent<WeaponManager>().weaponCreator.CollisionBoxSize;
        //           pickUpBoxSize = GetComponent<WeaponManager>().weaponCreator.pickUpBoxSize;
        //       }

        //if (GetComponent<ConsumableItemManager>() != null)
        //{
        //    itemInfo = GetComponent<ConsumableItemManager>().consumableItem.info;
        //    uiIcon = GetComponent<ConsumableItemManager>().consumableItem.UIIcon;
        //    CollisionBoxSize = GetComponent<ConsumableItemManager>().consumableItem.CollisionBoxSize;
        //    pickUpBoxSize = GetComponent<ConsumableItemManager>().consumableItem.pickUpBoxSize;
        //    animationID = GetComponent<ConsumableItemManager>().consumableItem.animationID;
        //}
        //else if (GetComponent<GunManager>() != null)
        //{
        //    itemInfo = GetComponent<GunManager>().gunSO.info;
        //    uiIcon = GetComponent<GunManager>().gunSO.uiIcon;
        //    CollisionBoxSize = GetComponent<GunManager>().gunSO.CollisionBoxSize;
        //    pickUpBoxSize = GetComponent<GunManager>().gunSO.pickUpBoxSize;
        //    animationID = GetComponent<GunManager>().gunSO.animationID;
        //}
        //else if (GetComponent<MeleeWeaponManager>() != null)
        //{
        //    itemInfo = GetComponent<MeleeWeaponManager>().meleeWeaponsSO.info;
        //    uiIcon = GetComponent<MeleeWeaponManager>().meleeWeaponsSO.uiIcon;
        //    CollisionBoxSize = GetComponent<MeleeWeaponManager>().meleeWeaponsSO.CollisionBoxSize;
        //    pickUpBoxSize = GetComponent<MeleeWeaponManager>().meleeWeaponsSO.pickUpBoxSize;
        //    animationID = GetComponent<MeleeWeaponManager>().meleeWeaponsSO.animationID;
        //}
    }

    public void UpdateInfo()
    {
        if(GetComponent<ItemSettings>() != null)
        {
            itemSettings = GetComponent<ItemSettings>();
            itemSettings.UpdateStats();

            if (itemSettings.meleeOS != null)
            {
                melee = itemSettings.meleeOS;

                itemInfo = melee.info;
                uiIcon = melee.uiIcon;
                animationID = melee.animationID;
            }
            else if (itemSettings.gunOS != null)
            {
                gun = itemSettings.gunOS;

                itemInfo = gun.info;
                uiIcon = gun.uiIcon;
                animationID = gun.animationID;
            }
        }
    }

    public void DecreaseStats()
    {
        itemSettings.Decrease();
    }

    public string GetItemInfo()
    {
        return itemInfo;
    }
    public Vector2 GetCollisionBoxSize()
    {
        return CollisionBoxSize;
    }
    public Vector2 GetPickUpBoxSize()
    {
        return pickUpBoxSize;
    }

    public Sprite GetUISprite()
    {
        return uiIcon;
    }
    public int GetAnimationID()
    {
        return animationID;
    }
}
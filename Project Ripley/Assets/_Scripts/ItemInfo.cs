using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[ExecuteInEditMode]
public class ItemInfo : MonoBehaviour
{
    [SerializeField] Sprite uiIcon;
    private string itemInfo;
    private Vector2 CollisionBoxSize;
    private Vector2 pickUpBoxSize;
    private int animationID;
    ItemSettings itemSettings;
    [SerializeField] MeleeWeaponsSO melee;
    [SerializeField] GunSO gun;
    public TypeOfItem typeOfItem;

    bool updateOnce = false;

    public enum TypeOfItem
    {
        None,
        Melee,
        Range,
        Consumable
    }

    public void Enable()
    {
        if(melee != null)
        {
            typeOfItem = TypeOfItem.Melee;
            animationID = melee.animationID;
        }
        else if(gun != null)
        {
            typeOfItem = TypeOfItem.Range;
        }
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
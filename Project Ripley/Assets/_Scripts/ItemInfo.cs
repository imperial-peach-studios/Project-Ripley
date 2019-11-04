using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[ExecuteInEditMode]
public class ItemInfo : MonoBehaviour
{
    public Sprite uiIcon;
    private string itemInfo;
    private Vector2 CollisionBoxSize;
    private Vector2 pickUpBoxSize;
    public int animationID;
    ItemSettings itemSettings;
    public MeleeWeaponsSO melee;
    public GunSO gun;
    public TypeOfItem typeOfItem;

    public float startDurability;
    public float durability;
    public float durabilityDecrease;
    public float damage;
    public float knockBack;
    public float knockLength;
    public float stunLength;
    public int bullet;
    public int numberOfBulletsFired;
    public float firingRate;
    public float spreadFactor;

    public GameObject bulletObject;

    public bool noDurability = false;

    private Properties p;
    public Properties Properties { get { return p; } }

    public enum TypeOfItem
    {
        None,
        Melee,
        Range,
        Consumable
    }

    public void UpdateI()
    {
        durability = startDurability;
        noDurability = false;
        if(p == null)
        {
            if (melee != null)
            {
                //typeOfItem = TypeOfItem.Melee;
                //animationID = melee.animationID;
                //uiIcon = melee.uiIcon;
                //p = new Properties(melee.durability, melee.durabilityDecrease, 0, 0, melee.attackRate, melee.knockBack, melee.knockLength, melee.stanLength, melee.damage, 0);
            }
            else if (gun != null)
            {
                //typeOfItem = TypeOfItem.Range;
                //animationID = gun.animationID;
                //uiIcon = gun.uiIcon;
                //p = new Properties(gun.durability, gun.durabilityDecrease, gun.bullet, gun.numberOfBulletsFired, gun.firingRate, gun.knockBack, gun.knockLength, gun.stunLength, gun.damage, gun.spreadFactor);
                //p.SetBullet(gun.weaponBullet);
                //bulletObject = gun.weaponBullet;
            }
        }   
        //animationID = 
    }
    public bool GetDura()
    {
        return noDurability;
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

    public void DecreaseDurability()
    {
        //p.Decrease();
        durability -= durabilityDecrease;

        if(durability <= 0)
        {
            durability = 0;
            noDurability = true;
        }

        Debug.Log("Dura " + durability);
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

[System.Serializable]
public class Properties
{
    public int bullet;
    public int numberOfBulletsFired;
    public float firingRate;
    public float knockBack;
    public float knockLength;
    public float stunLength;
    public float damage;
    public float spreadFactor;
    public float durability;
    public float durabilityDecrease;

    public GameObject bulletObject;

    public Properties(float dura, float duraDecrase, int bull, int numberOfBullets, float firingR, float knockB, float knockL, float stunL, float damag, float spreadF)
    {
        durability = dura;
        durabilityDecrease = duraDecrase;
        bullet = bull;
        numberOfBulletsFired = numberOfBullets;
        firingRate = firingR;
        knockBack = knockB;
        knockLength = knockL;
        stunLength = stunL;
        damage = damag;
        spreadFactor = spreadF;
    }

    public void SetBullet(GameObject b)
    {
        bulletObject = b;
    }

    public void Decrease()
    {
        durability -= durabilityDecrease;

        

    }
}
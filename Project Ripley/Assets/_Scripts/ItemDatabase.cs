using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ItemDatabase : MonoBehaviour
{
    public List<WeaponManager> weaponManager = new List<WeaponManager>();


    public enum ItemType
    {
        Weapon,
        Health
    }

    public void Fire(ItemType itemType, int itemIndex, int itemId)
    {
        if(itemType == ItemType.Weapon)
        {
            //List<WeaponManager> newSubWeapons = weaponManager[itemIndex]
        }
    }
}
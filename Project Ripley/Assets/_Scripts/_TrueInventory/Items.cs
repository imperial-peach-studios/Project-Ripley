using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[System.Serializable]
public class Items
{
    [HideInInspector]
    [SerializeField] public string state;

    [Header("Other Options")]
    //[HideInInspector]
    //[SerializeField] public Sprite uiIcon;
    [HideInInspector]
    [SerializeField] public string spriteName = "";
    [SerializeField] public string objectName = "";
    [SerializeField] public int selectedIconIndex = 0;
    //[SerializeField] public Vector3 pos = Vector3.zero;
    [SerializeField] public float x;
    [SerializeField] public float y;
    [SerializeField] public bool lootedItem = false;
    [SerializeField] public ItemInfo.TypeOfItem typeOfItem;
    [SerializeField] public int animationID;

    [Header("Durability Options")]
    [SerializeField] public float startDurability;
    [SerializeField] public float durability;
    [SerializeField] public float durabilityDecrease;

    [SerializeField] public bool noDurability = false;

    public void DecreaseDurability()
    {
        durability -= durabilityDecrease;

        if (durability <= 0)
        {
            durability = 0;
            noDurability = true;
        }

        Debug.Log("Dura " + durability);
    }
    public Vector3 GetPosition()
    {
        return new Vector3(x, y, 0);
    }
    public void SetPosition(Vector3 pos)
    {
        x = pos.x;
        y = pos.y;
    }

    public void SetState(int state, int index)
    {
        state = Mathf.Clamp(state, 0, 1);
        if(state == 0)
        {
            this.state = "Empty " + index;
        }
        else if(state == 1)
        {
            this.state = "Item " + index;
        }
    }
}

[System.Serializable]
public class Consumable : Items
{
    [Header("Health Options")]
    [SerializeField] public float healthIncrease;

    [Header("Hunger Options")]
    [SerializeField] public float hungerIncrease;

    [SerializeField] public float consumeTimeRate;

}

[System.Serializable]
public class Weapons : Items
{
    [Header("Damage Options")]
    [SerializeField] public float damage;

    [Header("Knockback Options")]
    [SerializeField] public float knockBackPower;
    [SerializeField] public float knockBackLength;

    [Header("Stun Options")]
    [SerializeField] public float stunLength;
}

[System.Serializable]
public class Melee : Weapons
{
    
}

[System.Serializable]
public class Range : Weapons
{
    [Header("Bullet Options")]
    [SerializeField] public int numberOfBulletsFired;
    [SerializeField] public float firingRate;
    [SerializeField] public float spreadFactor;
}
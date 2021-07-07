using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public enum ItemCategory
{
    None,
    Melee,
    Range,
    Consumable
}

[System.Serializable]
public class Item
{
    [SerializeField] string myName;
    [SerializeField] private ItemFactory.ItemType myItemType = ItemFactory.ItemType.None;
    [SerializeField] private ItemCategory myItemCategory;
    [SerializeField, Range(0, 12)] private int myItemID;

    private string myState = "";
    private string mySpriteName = "";
    private string myObjectName = "";

    private int mySelectedIconIndex = 0;

    private float myPosX;
    private float myPosY;

    public string GetSpriteName() 
    {
        return mySpriteName;
    }
    public void SetSpriteName(string aName)
    {
        mySpriteName = aName;
    }
    public string GetObjectName()
    {
        return myObjectName;
    }
    public void SetObjectName(string aName)
    {
        myObjectName = aName;
    }

    public int GetSelectedIconIndex()
    {
        return mySelectedIconIndex;
    }
    public void SetSelectedIconIndex(int aIndex)
    {
        mySelectedIconIndex = aIndex;
    }
    public int GetItemID()
    {
        return myItemID;
    }
    public void SetItemID(int aID)
    {
        myItemID = aID;
    }

    public Vector3 GetPosition()
    {
        return new Vector3(myPosX, myPosY, 0);
    }
    public void SetPosition(Vector3 aPos)
    {
        myPosX = aPos.x;
        myPosY = aPos.y;
    }

    public ItemCategory GetItemCategory()
    {
        return myItemCategory;
    }
    public void SetItemCategory(ItemCategory aItem)
    {
        myItemCategory = aItem;
    }

    public ItemFactory.ItemType GetItemType()
    {
        return myItemType;
    }

    public void SetItemType(ItemFactory.ItemType aItem)
    {
        myItemType = aItem;
    }

    public void SetName(string aName)
    {
        myName = aName;
    }
}
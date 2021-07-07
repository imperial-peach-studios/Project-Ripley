using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ItemFactory : MonoBehaviour
{
    public static ItemFactory Instance;

    [SerializeField] Sprite[] mySprites = new Sprite[(int)ItemType.Count];
    private Item[] myItems = new Item[(int)ItemType.Count];
    [Space]
    public bool myUpdate = false;
    [Space]
    [SerializeField] Melee[] myMeleeWeapons = new Melee[(int)ItemType.Count - 4];
    [SerializeField] Range[] myRangeWeapons = new Range[(int)ItemType.Count - 4];
    [SerializeField] Consumable[] myConsumables = new Consumable[(int)ItemType.Count - 4];

    public enum ItemType
    {
        None = -1,
        Knife,
        Paddel,
        Pistol,
        Shotgun,
        SmallMedicine,
        Count
    }

    void Awake()
    {
        if (Instance == null)
        {
            //DontDestroyOnLoad(gameObject);
            Instance = this;
        }
        else if (Instance != this)
        {
            //Destroy(gameObject);
        }

        Initialize();
    }

    public void Initialize()
    {
        for (int i = 0; i < myItems.Length; i++)
        {
            if (i < 2)
            {
                //myItems[i] = new Melee();
                myItems[i] = myMeleeWeapons[i];
            }
            else if (i < 4)
            {
                //myItems[i] = new Range();
                myItems[i] = myRangeWeapons[i - 2];
            }
            else if (i < 5)
            {
                //myItems[i] = new Consumable();
                myItems[i] = myConsumables[i - 4];
            }

            myItems[i].SetName(((ItemType)i).ToString());
        }

        SetMelee();
        SetRange();
        SetConsumable();
    }

    void SetMelee()
    {
        for(int i = 0; i < 2; i++)
        {
            //if (myItems[i].GetShouldOverrideBaseValues() == false)
            //{
            //    myItems[i].SetItemID(i + 1);
            //    myItems[i].SetItemItem(((ItemType)i));
            //    myItems[i].SetItemType(ItemCategory.Melee);
            //}

            myItems[i].SetItemID(i + 1);
            myItems[i].SetItemType(((ItemType)i));
            myItems[i].SetItemCategory(ItemCategory.Melee);
        }

        //myItems[(int)ItemItem.Crowbar].SetItemID(1);
        //myItems[(int)ItemItem.Crowbar].SetItemItem(ItemItem.Crowbar);
        //myItems[(int)ItemItem.Crowbar].SetItemType(ItemType.Melee);

        //myItems[(int)ItemItem.Paddel].SetItemID(2);
        //myItems[(int)ItemItem.Paddel].SetItemItem(ItemItem.Paddel);
        //myItems[(int)ItemItem.Paddel].SetItemType(ItemType.Melee);

        //myItems[(int)ItemItem.Knife].SetItemID(3);
        //myItems[(int)ItemItem.Knife].SetItemItem(ItemItem.Knife);
        //myItems[(int)ItemItem.Knife].SetItemType(ItemType.Melee);

        //myItems[(int)ItemItem.Pan].SetItemID(4);
        //myItems[(int)ItemItem.Pan].SetItemItem(ItemItem.Pan);
        //myItems[(int)ItemItem.Pan].SetItemType(ItemType.Melee);
    }
    void SetRange()
    {
        for (int i = 2; i < 4; i++)
        {
            //if (myItems[i].GetShouldOverrideBaseValues() == false)
            //{
            //    myItems[i].SetItemID(i + 1);
            //    myItems[i].SetItemItem(((ItemType)i));
            //    myItems[i].SetItemType(ItemCategory.Range);
            //}

            myItems[i].SetItemID(i + 1);
            myItems[i].SetItemType(((ItemType)i));
            myItems[i].SetItemCategory(ItemCategory.Range);
        }

        //myItems[(int)ItemItem.Revolver].SetItemID(5);
        //myItems[(int)ItemItem.Revolver].SetItemItem(ItemItem.Revolver);
        //myItems[(int)ItemItem.Revolver].SetItemType(ItemType.Range);

        //myItems[(int)ItemItem.Shotgun].SetItemID(6);
        //myItems[(int)ItemItem.Shotgun].SetItemItem(ItemItem.Shotgun);
        //myItems[(int)ItemItem.Shotgun].SetItemType(ItemType.Range);

        //myItems[(int)ItemItem.Pistol].SetItemID(7);
        //myItems[(int)ItemItem.Pistol].SetItemItem(ItemItem.Pistol);
        //myItems[(int)ItemItem.Pistol].SetItemType(ItemType.Range);

        //myItems[(int)ItemItem.M4].SetItemID(8);
        //myItems[(int)ItemItem.M4].SetItemItem(ItemItem.M4);
        //myItems[(int)ItemItem.M4].SetItemType(ItemType.Range);
    }
    void SetConsumable()
    {
        for (int i = 4; i < 5; i++)
        {
            //if(myItems[i].GetShouldOverrideBaseValues() == false)
            //{
            //    myItems[i].SetItemID(i + 1);
            //    myItems[i].SetItemItem(((ItemType)i));
            //    myItems[i].SetItemType(ItemCategory.Consumable);
            //}

            myItems[i].SetItemID(i + 1);
            myItems[i].SetItemType(((ItemType)i));
            myItems[i].SetItemCategory(ItemCategory.Consumable);
        }

        //myItems[(int)ItemItem.SmallMedicine].SetItemID(9);
        //myItems[(int)ItemItem.SmallMedicine].SetItemItem(ItemItem.SmallMedicine);
        //myItems[(int)ItemItem.SmallMedicine].SetItemType(ItemType.Consumable);

        //myItems[(int)ItemItem.BigMedicine].SetItemID(10);
        //myItems[(int)ItemItem.BigMedicine].SetItemItem(ItemItem.BigMedicine);
        //myItems[(int)ItemItem.BigMedicine].SetItemType(ItemType.Consumable);

        //myItems[(int)ItemItem.Snack].SetItemID(11);
        //myItems[(int)ItemItem.Snack].SetItemItem(ItemItem.Snack);
        //myItems[(int)ItemItem.Snack].SetItemType(ItemType.Consumable);

        //myItems[(int)ItemItem.Can].SetItemID(12);
        //myItems[(int)ItemItem.Can].SetItemItem(ItemItem.Can);
        //myItems[(int)ItemItem.Can].SetItemType(ItemType.Consumable);
    } 

    public Item GetItem(ItemType aItem)
    {
        return myItems[(int)aItem];
    }
    public ItemCategory GetItemCategory(ItemType aItem)
    {
        return myItems[(int)aItem].GetItemCategory();
    }
    public Sprite GetSprite(ItemType aItem)
    {
        if((int)aItem < 0)
        {
            return null;
        }

        return mySprites[(int)aItem];
    }
    public int GetItemID(ItemType aItem)
    {
        return myItems[(int)aItem].GetItemID();
    }
}
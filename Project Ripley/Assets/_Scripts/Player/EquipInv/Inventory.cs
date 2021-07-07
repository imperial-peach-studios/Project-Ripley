using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Inventory : MonoBehaviour
{
    public static Inventory Instance;

    [SerializeField] GameObject myPickUpObject;
    [SerializeField] LayerMask myDropMask;
    [SerializeField] Vector2 myDropSize = new Vector2(0.6f, 0.6f);

    bool myIsDropping = false;

    Equipment.Selected myCurrentSelected;

    Item myMelee;
    Item myRange;

    int myPistolAmmo;
    int myShotgunAmmo;

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
    }

    void Update()
    {
        if (Player.Instance.CanChangeState(PlayerState.Dropping))
        {
            if (Input.GetKeyDown(KeyCode.Q) && CanDrop() && myIsDropping == false)
            {
                Player.Instance.UpdateStateTo(PlayerState.Dropping);
                myIsDropping = true;
            }

            if (myIsDropping == false)
            {
                return;
            }

            if (myIsDropping)
            {
                TryToRemove();

                myIsDropping = false;
                Player.Instance.UpdateStateTo(PlayerState.Idle);
            }
        }
    }

    public bool CanDrop()
    { //Can't Drop if Looting
        Collider2D[] hit = Physics2D.OverlapBoxAll(transform.position, myDropSize, 0f, myDropMask);
        return hit.Length == 0;
    }

    public void TryToRemove()
    {
        Item current = GetItem();

        if (current == null)
        {
            return;
        }

        GameObject newPickUp = (Instantiate(myPickUpObject, transform.position, Quaternion.identity) as GameObject);

        ItemHolder pickUpItem = newPickUp.GetComponent<ItemHolder>();
        pickUpItem.TransferPropertiesToItem(current);
        //newPickUp.name = pickUpItem.GetItem().GetObjectName();

        current = new Item();
        //OnInventoryChanged?.Invoke(slotIndex);
    }

    public void TryToRemove(Equipment.Selected aSelected)
    {
        Item current = aSelected == Equipment.Selected.Primary ? myMelee : myRange;

        if (current == null)
        {
            return;
        }

        GameObject newPickUp = (Instantiate(myPickUpObject, transform.position, Quaternion.identity) as GameObject);

        ItemHolder pickUpItem = newPickUp.GetComponent<ItemHolder>();
        pickUpItem.TransferPropertiesToItem(current);
        //newPickUp.name = pickUpItem.GetItem().GetObjectName();

        current = new Item();
        //OnInventoryChanged?.Invoke(slotIndex);
    }

    public Item GetMeleeOrRange()
    {
        return myCurrentSelected == Equipment.Selected.Primary ? myMelee : myRange;
    }

    public Item GetItem()
    {
        return GetMeleeOrRange();
    }

    public void SetMelee(Item aItem)
    {
        myMelee = aItem;
    }

    public void SetRange(Item aItem)
    {
        myRange = aItem;
    }

    public void AddItem(Item aItem)
    {
        if (aItem.GetItemCategory() == ItemCategory.Melee)
        {
            if (myMelee != null)
            {
                TryToRemove(Equipment.Selected.Primary);
            }
            SetMelee(aItem);
            Player.Instance.equipment.Primary = 0;
        }
        else if (aItem.GetItemCategory() == ItemCategory.Range)
        {
            if (myRange != null)
            {
                TryToRemove(Equipment.Selected.Secondary);
            }
            SetRange(aItem);
            Player.Instance.equipment.Secondary = 0;
        }
    }

    public ItemFactory.ItemType GetItemItem(Equipment.Selected aSelected)
    {
        if (aSelected == Equipment.Selected.Primary)
        {
            if (myMelee == null)
            {
                return ItemFactory.ItemType.None;
            }

            return myMelee.GetItemType();
        }
        else if (aSelected == Equipment.Selected.Secondary)
        {
            if (myRange == null)
            {
                return ItemFactory.ItemType.None;
            }

            return myRange.GetItemType();
        }

        return ItemFactory.ItemType.None;
    }

    public ItemCategory GetItemType(Equipment.Selected aSelected)
    {
        if (aSelected == Equipment.Selected.Primary)
        {
            return myMelee.GetItemCategory();
        }
        else if (aSelected == Equipment.Selected.Secondary)
        {
            return myRange.GetItemCategory();
        }

        return ItemCategory.None;
    }

    public void SetSelected(Equipment.Selected aNewSelected)
    {
        myCurrentSelected = aNewSelected;
    }

    public void AddPistolAmmo(int aAmountToaAdd)
    {
        myPistolAmmo += aAmountToaAdd;
    }
    public void AddShotgunAmmo(int aAmountToAdd)
    {
        myShotgunAmmo += aAmountToAdd;
    }

    public int GetPistolAmmo()
    {
        return myPistolAmmo;
    }

    public int GetShotgunAmmo()
    {
        return myShotgunAmmo;
    }

    public void AddAmmo(ItemFactory.ItemType aItemItem, int aAmountToAdd)
    {
        if (aItemItem == ItemFactory.ItemType.Pistol)
        {
            myPistolAmmo += aAmountToAdd;
        }
        else if (aItemItem == ItemFactory.ItemType.Shotgun)
        {
            myShotgunAmmo += aAmountToAdd;
        }
    }

    public int GetAmmo(ItemFactory.ItemType aItemItem)
    {
        if (aItemItem == ItemFactory.ItemType.Pistol)
        {
            return myPistolAmmo;
        }
        else if (aItemItem == ItemFactory.ItemType.Shotgun)
        {
            return myShotgunAmmo;
        }

        return 0;
    }
}
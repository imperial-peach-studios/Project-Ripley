using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;

public class Inventory : MonoBehaviour
{
    public static Inventory Instance;
    [HideInInspector]
    public GameObject[] inventory = new GameObject[10];
    public Items[] itemInventory = new Items[10];
    public bool IsInventoryFull => GetFirstEmptySlot() == -1;

    public event Action<int> OnInventoryChanged;
    public GameObject GetInventorySlot(int index) => inventory[index];
    public Items GetItemInventorySlot(int index) => itemInventory[index];

    //[SerializeField] GameObject player;
    [SerializeField] Vector2 dropSize = new Vector2(0.6f, 0.6f);
    [SerializeField] LayerMask collideWithLayer;
    [SerializeField] GameObject pickUpObject;
    [SerializeField] Sprite[] itemIconSprites = new Sprite[10];

    public enum Item
    {
        Knife,
        Pan,
        Crowbar,
        Bat,
        M4,
        Pistol,
        Revolver,
        Shotgun,
        SmallMedicine,
        BigMedicine,
        Can,
        Snack
    }

    void Awake()
    {
        inventory = new GameObject[10];

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

    void Start()
    {
        GameData.OnSavePlayer += OnSavePlayerInventory;
        GameData.OnLoadPlayer += OnLoadPlayerInventory;

        for(int i = 0; i < itemInventory.Length; i++)
        {
            itemInventory[i].SetState(0, i);
        }
    }

    void OnSavePlayerInventory()
    {
        GameData.aData.pData.SaveInventoriesData(itemInventory);
    }
    void OnLoadPlayerInventory()
    {
        GameData.aData.pData.LoadInventoriesData(ref itemInventory);

        for (int i = 0; i < itemInventory.Length; i++)
        {
            OnInventoryChanged?.Invoke(i);
        }
    }

    public int GetFirstEmptySlot()
    {
        for (int i = 0; i < itemInventory.Length; i++)
        {
            if(!(itemInventory[i] is Melee m) && !(itemInventory[i] is Range r) && !(itemInventory[i] is Consumable c))
            {
                return i;
            }
        }
        return -1;
    }

    public bool CanDrop()
    { //Can't Drop if Looting
        Collider2D[] hit = Physics2D.OverlapBoxAll(transform.position, dropSize, 0f, collideWithLayer);
        return hit.Length == 0;
    }

    public bool AddItemToFirstEmptySlot(Items iI)
    {
        int slot = GetFirstEmptySlot();
        return AddItemToIndex(iI, slot);
    }

    public bool AddItemToIndex(Items item, int slot)
    {
        if (slot <= -1)
            return false;

        item.SetState(1, slot);
        itemInventory[slot] = item;
        OnInventoryChanged?.Invoke(slot);
        return true;
    }

    public Sprite GetSprite(string name, Items item)
    {
        for (int i = 0; i < itemIconSprites.Length; i++)
        {
            if (name == itemIconSprites[i].name)
            {
                //Debug.Log(itemIconSprites[i]);
                return itemIconSprites[i];
            }
        }

        return CheckForCorrectSprite(name, item);
    }

    public Sprite CheckForCorrectSprite(string name, Items item)
    {
        for(int i = 0; i < itemIconSprites.Length; i++)
        {
            string spriteName = itemIconSprites[i].name;

            if (CheckToSeeIfEqual(name, spriteName))
            {
                return itemIconSprites[i];
            }
        }
        return null;
    }

    bool CheckToSeeIfEqual(string name, string iconName)
    {
        if(name == null)
        {
            return false;
        }

        for(int i = 0; i < name.Length;)
        {
            if(i == 0)
                if (name.Length != iconName.Length || name[i] != iconName[i])
                    return false;

            if(name[i] == iconName[i])
            {
                if(i == (name.Length - 1))
                {
                    return true;
                }

                i++;
                continue;
            }
            break;
        }

        return false;
    }

    public void TryToRemoveItemAtIndex(int slotIndex)
    {
        if (itemInventory[slotIndex] != null)
        {
            if (itemInventory[slotIndex] is Melee m || itemInventory[slotIndex] is Range r || itemInventory[slotIndex] is Consumable c)
            {
                GameObject newPickUp = (Instantiate(pickUpObject, transform.position, Quaternion.identity) as GameObject);
                
                ItemInfo pickUpItem = newPickUp.GetComponent<ItemInfo>();
                itemInventory[slotIndex].SetState(0, slotIndex);
                pickUpItem.SetType(itemInventory[slotIndex]);
                newPickUp.name = pickUpItem.I.objectName;
                
                itemInventory[slotIndex] = new Items();

                OnInventoryChanged?.Invoke(slotIndex);
            }
        }
    }

    public void DestroyItemAtIndex(int slotIndex)
    {
        itemInventory[slotIndex] = new Items();
        OnInventoryChanged?.Invoke(slotIndex);
    }
}
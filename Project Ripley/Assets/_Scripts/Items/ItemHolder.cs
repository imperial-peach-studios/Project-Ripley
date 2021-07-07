using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ItemHolder : MonoBehaviour
{
    [SerializeField] public ItemFactory.ItemType myItemType;
    [SerializeField] public ItemCategory myItemCategory;
    [SerializeField] public bool myUseCustomValues = false;

    private Item myItem;
    public Item Item
    {
        get
        {
            if (myItemCategory == ItemCategory.Melee)
                return myMelee;
            else if (myItemCategory == ItemCategory.Range)
                return myRange;
            else if (myItemCategory == ItemCategory.Consumable)
                return myConsumable;
            return myItem;
        }
        set
        {
            if (myItemCategory == ItemCategory.Melee)
                myMelee = (Melee)value;
            else if (myItemCategory == ItemCategory.Range)
                myRange = (Range)value;
            else if (myItemCategory == ItemCategory.Consumable)
                myConsumable = (Consumable)value;
        }
    }

    [SerializeField] public Melee myMelee;
    [SerializeField] public Range myRange;
    [SerializeField] public Consumable myConsumable;

    private SpriteRenderer mySpriteRenderer;

    [SerializeField] private int selectedIconIndex;
    public int SelectedIconIndex
    {
        get
        {
            return selectedIconIndex;
        }
        set
        {
            selectedIconIndex = value;
        }
    }

    void Start()
    {
        //GameData.OnSavePlayer += OnNewSave;
        //GameData.OnLoadPlayer += OnNewLoad;
        //GameData.BeforeLoadPlayer += BeforeLoad;

        mySpriteRenderer = GetComponent<SpriteRenderer>();
        mySpriteRenderer.sprite = ItemFactory.Instance.GetSprite(myItemType);

        myItemCategory = ItemFactory.Instance.GetItemCategory(myItemType);
        TransferPropertiesToItem();
    }

    public void AddItem()
    {
        //SetItem();
        TransferPropertiesToItem();

        //if (InventoryUI.IsMouseOverUI())
        //{
        //    int currentSlotIndex = InventoryUI.GetCurrentMouseOverSlotIndex();

        //    Player.Instance.inventory.TryToRemoveItemAtIndex(currentSlotIndex);
        //    Player.Instance.inventory.AddItemToIndex(Item, currentSlotIndex);

        //    if(Input.GetKey(KeyCode.LeftControl))
        //    {
        //        if(Player.Instance.equipment.SelectedEQ == Equipment.Selected.Primary)
        //        {
        //            Player.Instance.equipment.Primary = currentSlotIndex;
        //        }
        //        else
        //        {
        //            Player.Instance.equipment.Secondary = currentSlotIndex;
        //        }
        //    }
        //}
        //else
        //{
        //    Player.Instance.inventory.AddItemToFirstEmptySlot(Item);

        //}

        Player.Instance.inventory.AddItem(Item);
        Destroy(gameObject);

        //if (InventoryUI.IsMouseOverUI())
        //{
        //    int currentSlotIndex = InventoryUI.GetCurrentMouseOverSlotIndex();

        //    //Inventory.Instance.TryToRemoveItemAtIndex(currentSlotIndex);
        //    Player.Instance.inventory.TryToRemoveItemAtIndex(currentSlotIndex);

        //    //I.spriteName = iconNames[SelectedIconIndex];

        //    //Inventory.Instance.AddItemToIndex(I, currentSlotIndex);
        //    Player.Instance.inventory.AddItemToIndex(I, currentSlotIndex);

        //    if (Input.GetKey(KeyCode.LeftControl))
        //    {
        //        //if (Equipment.Instance.SelectedEQ == Equipment.Selected.Primary)
        //        //{
        //        //    Equipment.Instance.Primary = currentSlotIndex;
        //        //}
        //        //else
        //        //{
        //        //    Equipment.Instance.Secondary = currentSlotIndex;
        //        //}

        //        if (Player.Instance.equipment.SelectedEQ == Equipment.Selected.Primary)
        //        {
        //            Player.Instance.equipment.Primary = currentSlotIndex;
        //        }
        //        else
        //        {
        //            Player.Instance.equipment.Secondary = currentSlotIndex;
        //        }
        //    }
        //}
        //else
        //{
        //    //I.spriteName = iconNames[SelectedIconIndex];

        //    //Inventory.Instance.AddItemToFirstEmptySlot(I);
        //    Player.Instance.inventory.AddItemToFirstEmptySlot(I);
        //}

        //if (gameObject.layer == LayerMask.NameToLayer("Loot"))
        //{
        //    gameObject.layer = 0;
        //    lootItem = true;
        //    //GameData.aData.iData.AddDestroyedItem(I);
        //    this.enabled = false;
        //}
        //else
        //{
        //    //GameData.aData.iData.AddDestroyedItem(I);
        //    Destroy(gameObject);
        //}

        //Player.Instance.inventory.AddItemToFirstEmptySlot(Item);
        //Destroy(gameObject);
    }

    public void OnSave()
    {
        if(this != null)
        {
            TransferPropertiesToItem();
            //Send Item to GameData<-
        }
    }

    public void OnBeforeLoad()
    {
        //if (this != null)
        //{
        //    if (I == null)
        //    {
        //        Debug.Log("Destroyed List Before Load In Item");
        //        Destroy(gameObject);
        //        return;
        //    }
        //    Debug.Log("BEFORE LOAD IN ITEM");
        //    SetItem();
        //    GameData.aData.iData.ExistsInDestroyedList(I, true);
        //}
    }

    public void OnLoad()
    {
        //if (this != null)
        //{
        //    //SetItem();
        //    if (!GameData.aData.iData.ExistsNewInSavedList(I))
        //    {
        //        if (lootItem)
        //        {
        //            this.enabled = false;
        //        }
        //        else
        //        {
        //            Destroy(gameObject);
        //        }
        //        return;
        //    }

        //    I = GameData.aData.iData?.NewOnLoad(I);

        //    transform.position = I.GetPosition();

        //    if (spriteR == null)
        //    {
        //        spriteR = GetComponent<SpriteRenderer>();
        //    }

        //    //spriteR.sprite = Inventory.Instance.GetSprite(I.spriteName, I);
        //    //spriteR.sprite = Player.Instance.inventory.GetSprite(I.spriteName, I);

        //    uiSprite = Player.Instance.inventory.GetSprite(I.spriteName, I);
        //    spriteR.sprite = UISprite;
        //    typeOfItem = I.typeOfItem;
        //    SelectedIconIndex = I.selectedIconIndex;
        //    lootItem = I.lootedItem;

        //    if (lootItem)
        //    {
        //        gameObject.SetActive(true);
        //    }

        //    GameData.data.AllItemsFinishedLoading();
        //}
    }

    public Item GetItem()
    {
        return Item;
    }

    public void TransferPropertiesToItem(Item aItem = null)
    {
        if (aItem == null)
        {
            Item = ItemFactory.Instance.GetItem(myItemType);
            Item.SetObjectName(transform.name);
        }
        else
        {
            myItemType = aItem.GetItemType();
            if(mySpriteRenderer == null)
            {
                mySpriteRenderer = GetComponent<SpriteRenderer>();
            }
            mySpriteRenderer.sprite = ItemFactory.Instance.GetSprite(myItemType);
            myItemCategory = ItemFactory.Instance.GetItemCategory(myItemType);
            Item = aItem;
            transform.name = aItem.GetObjectName();
        }
    }
}
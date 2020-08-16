using System;
using UnityEngine;

[ExecuteInEditMode]
public class ItemInfo : MonoBehaviour
{
    public TypeOfItem typeOfItem;

    public enum TypeOfItem
    {
        None,
        Melee,
        Range,
        Consumable
    }

    private Items i;
    public Items I
    {
        get
        {
            if (typeOfItem == TypeOfItem.Melee)
                return meele;
            else if (typeOfItem == TypeOfItem.Range)
                return raange;
            else if (typeOfItem == TypeOfItem.Consumable)
                return consumable;
            return i;
        }
        set
        {
            if (typeOfItem == TypeOfItem.Melee)
                meele = (Melee)value;
            else if (typeOfItem == TypeOfItem.Range)
                raange = (Range)value;
            else if (typeOfItem == TypeOfItem.Consumable)
                consumable = (Consumable)value;

            //i = value;
        }
    }

    SpriteRenderer spriteR;
    [SerializeField] private Sprite uiSprite;
    public Sprite UISprite
    {
        get
        {
            return uiSprite;
        }
        set
        {
            uiSprite = value;
            if(uiSprite != null && I != null)
            {
                I.spriteName = uiSprite.name;
            }
        }
    }

    [HideInInspector] public string spriteIconName;
    [SerializeField,HideInInspector] public string[] iconNames = { "Knife_UI", "Paddel_UI", "Crowbar_UI", "Pan_UI", "M4", "Pistol_UI", "Revolver_UI", "Shotgun_UI", "SmallMedicine_UI", "BigMedicine_UI", "Snack_UI", "Can_UI", "", "" };
    [HideInInspector] public int meleeIconIndex = 0;
    [HideInInspector] public int rangeIconIndex = 0;
    [HideInInspector] public int consumableIndex = 0;

    public int atIndex;
    public bool lootItem = false;

    private int selectedIconIndex;
    public int SelectedIconIndex
    {
        private get
        {
            if (typeOfItem == TypeOfItem.Melee)
                return meleeIconIndex;
            else if (typeOfItem == TypeOfItem.Range)
                return rangeIconIndex + 4;
            else if (typeOfItem == TypeOfItem.Consumable)
                return consumableIndex + 8;

            return 12;
        }
        set
        {
            if (typeOfItem == TypeOfItem.Melee)
                meleeIconIndex = value;
            else if (typeOfItem == TypeOfItem.Range)
                rangeIconIndex = value;
            else if (typeOfItem == TypeOfItem.Consumable)
                consumableIndex = value;

            //selectedIconIndex = value;
        }
    }

    private bool addTolist = false;

    public Melee meele;
    public Range raange;
    public Consumable consumable;

    void Start()
    {
        GameData.OnSavePlayer += OnNewSave;
        GameData.OnLoadPlayer += OnNewLoad;
        GameData.BeforeLoadPlayer += BeforeLoad;

        spriteR = GetComponent<SpriteRenderer>();

        spriteR.sprite = UISprite;
    }

    void BeforeLoad()
    {
        if(this != null)
        {
            if(I == null)
            {
                Debug.Log("Destroyed List Before Load In Item");
                Destroy(gameObject);
                return;
            }
            Debug.Log("BEFORE LOAD IN ITEM");
            SetItem();
            //GameData.ExistsInDestroyedList(I, true);
            GameData.aData.iData.ExistsInDestroyedList(I, true);
        }
    }

    void OnNewSave()
    {
        if(this != null)
        {
            SetItem();
            atIndex = GameData.aData.iData.OnNewSave(I);
        }
    }

    void OnNewLoad()
    {
        if(this != null)
        {
            //SetItem();
            if (!GameData.aData.iData.ExistsNewInSavedList(I))
            {
                if (lootItem)
                {
                    this.enabled = false;
                }
                else
                {
                    Destroy(gameObject);
                }
                return;
            }

            I = GameData.aData.iData?.NewOnLoad(I);

            transform.position = I.GetPosition();

            if (spriteR == null)
            {
                spriteR = GetComponent<SpriteRenderer>();
            }

            //spriteR.sprite = Inventory.Instance.GetSprite(I.spriteName, I);
            //spriteR.sprite = Player.Instance.inventory.GetSprite(I.spriteName, I);

            uiSprite = Player.Instance.inventory.GetSprite(I.spriteName, I);
            spriteR.sprite = UISprite;
            typeOfItem = I.typeOfItem;
            SelectedIconIndex = I.selectedIconIndex;
            lootItem = I.lootedItem;

            if (lootItem)
            {
                gameObject.SetActive(true);
            }

            GameData.data.AllItemsFinishedLoading();
        }
    }

    public void TryToAddItemToInventory()
    {
        SetItem();

        if (InventoryUI.IsMouseOverUI())
        {
            int currentSlotIndex = InventoryUI.GetCurrentMouseOverSlotIndex();

            //Inventory.Instance.TryToRemoveItemAtIndex(currentSlotIndex);
            Player.Instance.inventory.TryToRemoveItemAtIndex(currentSlotIndex);

            //I.spriteName = iconNames[SelectedIconIndex];

            //Inventory.Instance.AddItemToIndex(I, currentSlotIndex);
            Player.Instance.inventory.AddItemToIndex(I, currentSlotIndex);

            if (Input.GetKey(KeyCode.LeftControl))
            {
                //if (Equipment.Instance.SelectedEQ == Equipment.Selected.Primary)
                //{
                //    Equipment.Instance.Primary = currentSlotIndex;
                //}
                //else
                //{
                //    Equipment.Instance.Secondary = currentSlotIndex;
                //}

                if(Player.Instance.equipment.SelectedEQ == Equipment.Selected.Primary)
                {
                    Player.Instance.equipment.Primary = currentSlotIndex;
                }
                else
                {
                    Player.Instance.equipment.Secondary = currentSlotIndex;
                }
            }
        }
        else
        {
            //I.spriteName = iconNames[SelectedIconIndex];

            //Inventory.Instance.AddItemToFirstEmptySlot(I);
            Player.Instance.inventory.AddItemToFirstEmptySlot(I);
        }

        if (gameObject.layer == LayerMask.NameToLayer("Loot"))
        {
            gameObject.layer = 0;
            lootItem = true;
            //GameData.aData.iData.AddDestroyedItem(I);
            this.enabled = false;
        }
        else
        {
            //GameData.aData.iData.AddDestroyedItem(I);
            Destroy(gameObject);
        }
    }

    public void SetType(Items item)
    {
        string spriteName = "";
        if(item is Melee m)
        {
            meele = m;
            typeOfItem = TypeOfItem.Melee;
            spriteName = m.spriteName;
        }
        else if(item is Range r)
        {
            raange = r;
            typeOfItem = TypeOfItem.Range;
            spriteName = r.spriteName;
        }
        else if(item is Consumable c)
        {
            consumable = c;
            typeOfItem = TypeOfItem.Consumable;
            spriteName = c.spriteName;
        }

        if(spriteR == null)
        {
            spriteR = GetComponent<SpriteRenderer>();
        }
        
        if(item is Melee me || item is Range ra || item is Consumable co)
        {
            //spriteR.sprite = Inventory.Instance?.GetSprite(spriteName, item);
            //spriteR.sprite = Player.Instance.inventory.GetSprite(spriteName, item);
            UISprite = Player.Instance.inventory.GetSprite(item.spriteName, item);
            spriteR.sprite = UISprite;
        }
    }

    public int GetAnimationID()
    {
        return I.animationID;
    }

    public string GetItemName()
    {
        return (iconNames[SelectedIconIndex]);
    }

    public void SetItem(Items iI = null)
    {
        if(iI == null)
        {
            I.typeOfItem = typeOfItem;
            I.selectedIconIndex = SelectedIconIndex;
            //I.spriteName = iconNames[SelectedIconIndex];
            if(this != null)
            {
                I.SetPosition(transform.position);
                I.objectName = transform.name;
            }
            I.lootedItem = lootItem;
        }
        else
        {
            I.typeOfItem = iI.typeOfItem;
            I.selectedIconIndex = iI.selectedIconIndex;
            //I.spriteName = iI.spriteName;
            I.SetPosition(iI.GetPosition());
            I.objectName = iI.objectName;
            I.animationID = iI.animationID;
            I.lootedItem = iI.lootedItem;
        }
    }

    public void LoadItem(Items iI)
    {
        typeOfItem = iI.typeOfItem;
        SelectedIconIndex = iI.selectedIconIndex;
        name = iI.objectName;
        lootItem = iI.lootedItem;

        SetType(iI);

        if (spriteR == null)
        {
            spriteR = GetComponent<SpriteRenderer>();
        }

        //spriteR.sprite = Inventory.Instance?.GetSprite(iI.spriteName, iI);

       // spriteR.sprite = Player.Instance.inventory.GetSprite(iI.spriteName, iI);
        spriteR.sprite = UISprite;
    }
}
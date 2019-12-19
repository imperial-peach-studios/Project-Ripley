using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class InventorySlots : MonoBehaviour
{
    List<InventorySlotColl> slots = new List<InventorySlotColl>();
    InventoryUI invUI;
    [SerializeField] float iconSize;

    void Awake()
    {
        for (int i = 0; i < 10; i++)
        {
            slots.Add(transform.GetChild(i).GetComponent<InventorySlotColl>());
        }
        invUI = transform.parent.parent.GetComponent<InventoryUI>();

        Equipment.Instance.OnSelectedHasChanged += a => OnEQChanged(-1, -1);
        Equipment.Instance.OnPrimaryChanged += OnEQChanged;
        Equipment.Instance.OnSecondaryChanged += OnEQChanged;

        Inventory.Instance.OnInventoryChanged += UpdateSlotSprite;
    }

    void Update()
    {
        int index = GetIndex();

        if (index != -1)
        {
            for (int i = 0; i < slots.Count; i++)
            {
                if (i != index)
                {
                    slots[i].transform.localScale = Vector3.one;
                    slots[i].transform.SetSiblingIndex(i);
                    
                    if(slots[i].BarImage.sprite != invUI.blueGrid)
                    {
                        slots[i].BarImage.sprite = invUI.greyGrid;
                    }
                }
            }

            slots[index].transform.localScale = Vector3.one * iconSize;
            slots[index].transform.SetAsLastSibling();

            SlotInput(index);
        }
        else
        {
            for(int i = 0; i < slots.Count; i++)
            {
                if(i == Equipment.Instance.Primary && Equipment.Instance.SelectedEQ == Equipment.Selected.Primary
                    || i == Equipment.Instance.Secondary && Equipment.Instance.SelectedEQ == Equipment.Selected.Secondary)
                {
                    slots[i].transform.localScale = Vector3.one * iconSize;
                    slots[i].transform.SetAsLastSibling();
                }
                else
                {
                    slots[i].transform.localScale = Vector3.one;

                    if(slots[i].BarImage.sprite == invUI.greenGrid)
                    {
                        slots[i].BarImage.sprite = invUI.greyGrid;
                    }
                }
            }
        }

        if (Input.GetKeyDown(KeyCode.Q) && Inventory.Instance.CanDrop()) //&& !iSO.GetLootingMode()
        {
            int selectedIndex = 0;
            if (InventoryUI.IsMouseOverUI())
            {
                selectedIndex = InventoryUI.GetCurrentMouseOverSlotIndex();
            }
            else
            {
                selectedIndex = Equipment.Instance.GetCurrentIndex();
            }

            Items iI = Inventory.Instance.GetInventorySlot(selectedIndex)?.GetComponent<ItemInfo>()?.I;
            if (iI != null)
            {
                if (GameData.aData.iData.ExistsInDestroyedList(iI))
                {
                    GameData.aData.iData.ItemRemoveFromDestroyed(Inventory.Instance.GetInventorySlot(selectedIndex).GetComponent<ItemInfo>().I);
                }
                //if (GameData.ExistsInDestroyedList(iI))
                //{
                //    GameData.ItemRemoveFromDestroyed(Inventory.Instance.GetInventorySlot(selectedIndex).GetComponent<ItemInfo>().I);
                //}
            }

            Inventory.Instance.TryToRemoveItemAtIndex(selectedIndex);
        }
    }

    void UpdateSlotSprite(int index)
    {
        //var newSprite = Inventory.Instance.GetItemInventorySlot(index)?.uiIcon ?? null; //.GetComponent<ItemInfo>()?.GetUISprite() 
        Items item = Inventory.Instance.GetItemInventorySlot(index);
        var newSprite = Inventory.Instance?.GetSprite(item.spriteName, item) ?? null;

        slots[index].ChildImage.sprite = newSprite;
        slots[index].ChildImage.enabled = newSprite != null;
    }

    int GetIndex()
    {
        for (int i = 0; i < slots.Count; i++)
        {
            if (slots[i].mouseOver)
            {
                return i;
            }
        }
        return -1;
    }

    void SlotInput(int slotIndex)
    {
        if (Input.GetKeyDown(KeyCode.Alpha1))
        {
            if (slotIndex != Equipment.Instance.Secondary)
                Equipment.Instance.Primary = slotIndex;
            else
                Equipment.Instance.SwapEquipment();
        }
        else if (Input.GetKeyDown(KeyCode.Alpha2))
        {
            if (slotIndex != Equipment.Instance.Primary)
                Equipment.Instance.Secondary = slotIndex;
            else
                Equipment.Instance.SwapEquipment();
        }

        if (slotIndex != Equipment.Instance.Primary && slotIndex != Equipment.Instance.Secondary)
        {
            if(slots[slotIndex].BarImage.sprite != invUI.blueGrid)
            {
                slots[slotIndex].BarImage.sprite = invUI.greenGrid;
            }
        }

        if (Input.GetKeyDown(KeyCode.Q) && Inventory.Instance.CanDrop()) //&& !iSO.GetLootingMode()
        {
            //Inventory.Instance.TryToRemoveItem(slotIndex);

            //Inventory.Instance.TryToRemoveItems(slotIndex);
        }
    }

    void OnEQChanged(int oldEQ, int newEQ)
    {
        for (int i = 0; i < slots.Count; i++)
        {
            if (Equipment.Instance.Primary == i || Equipment.Instance.Secondary == i)
            {
                slots[i].BarImage.sprite = invUI.blueGrid;
            }
            else
            {
                slots[i].BarImage.sprite = invUI.greyGrid;
            }

            UpdateScale(i);
        }
    }

    void UpdateScale(int slotIndex)
    {
        if (slotIndex == Equipment.Instance.Primary && Equipment.Instance.SelectedEQ == Equipment.Selected.Primary
            || slotIndex == Equipment.Instance.Secondary && Equipment.Instance.SelectedEQ == Equipment.Selected.Secondary)
        {
            slots[slotIndex].transform.localScale = Vector3.one * iconSize;
            slots[slotIndex].transform.SetAsLastSibling();
        }
        else
        {
            slots[slotIndex].transform.localScale = Vector3.one;
            slots[slotIndex].transform.SetSiblingIndex(slotIndex);
        }
    }
}
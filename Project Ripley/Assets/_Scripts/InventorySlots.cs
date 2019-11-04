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
    }

    void UpdateSlotSprite(int index)
    {
        var newSprite = Inventory.Instance.GetInventorySlot(index)?.GetComponent<ItemInfo>()?.GetUISprite() ?? null;

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
            Inventory.Instance.TryToRemoveItem(slotIndex);
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
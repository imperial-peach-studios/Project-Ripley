using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class InventoryBarUI : MonoBehaviour
{
    public int slotIndex;
    [SerializeField] float iconSize;
    InventoryUI invUI;
    Image barImage;
    Image slotImage;

    void Start()
    {
        slotIndex = int.Parse(transform.name[transform.name.Length - 1].ToString());
        //index is 1-9 and then 0
        //we want 0-9
        slotIndex -= 1;
        if (slotIndex == -1)
            slotIndex = 9;

        invUI = transform.parent.parent.parent.GetComponent<InventoryUI>();

        barImage = GetComponent<Image>();
        barImage.sprite = invUI.greyGrid;

        slotImage = transform.GetChild(0).GetComponent<Image>();
    }
    
    void Update()
    {
        Equipment.Selected selectedBar = Equipment.Instance.SelectedEQ;

        if (selectedBar == Equipment.Selected.Primary && slotIndex == 1)
        {
            if (Inventory.Instance.GetInventorySlot(Equipment.Instance.Primary) != null)
            {
                GameObject slotItem = Inventory.Instance.GetInventorySlot((Equipment.Instance.Primary));

                slotImage.sprite = slotItem.GetComponent<ItemInfo>().GetUISprite();
                slotImage.enabled = true;
            }
            else
            {
                slotImage.enabled = false;
            }
        }
        else if(selectedBar == Equipment.Selected.Secondary && slotIndex == 2)
        {
            if (Inventory.Instance.GetInventorySlot(Equipment.Instance.Secondary) != null)
            {
                GameObject slotItem = Inventory.Instance.GetInventorySlot((Equipment.Instance.Secondary));

                slotImage.sprite = slotItem.GetComponent<ItemInfo>().GetUISprite();
                slotImage.enabled = true;
            }
            else
            {
                slotImage.enabled = false;
            }
        }
    }
}
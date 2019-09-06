using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class InventoryToolTip : MonoBehaviour
{
    [SerializeField] InventoryUI invUI;
    Text toolTipText;

    void Awake()
    {
        toolTipText = transform.GetChild(0).GetComponent<Text>();
        invUI.OnMouseExit += slot => gameObject.SetActive(false);
        invUI.OnMouseEnter += OnMouseEnterSlot;
    }

    private void OnMouseEnterSlot(InventorySlotColl slot)
    {
        gameObject.SetActive(true);
        var text = "Just Hands, You Use Them To Fight";

        var itemInSlot = Inventory.Instance.GetInventorySlot(slot.slotIndex);
        if (itemInSlot != null)
            text = itemInSlot.GetComponent<ItemInfo>().GetItemInfo();

        toolTipText.text = text;
    }
}
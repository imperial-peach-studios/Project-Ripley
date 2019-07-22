using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class InventorySlotUI : MonoBehaviour
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

        Equipment.Instance.OnPrimaryChanged += OnEQChanged;
        Equipment.Instance.OnSecondaryChanged += OnEQChanged;
    }

    void Update()
    {
        if (Inventory.Instance.GetInventorySlot(slotIndex) != null)
        {
            GameObject slotItem = Inventory.Instance.GetInventorySlot((slotIndex));
            
            slotImage.sprite = slotItem.GetComponent<ItemInfo>().GetUISprite();
            slotImage.enabled = true;
        }
        else
        {
            slotImage.enabled = false;
        }
    }

    void OnEQChanged(int oldEQ, int newEQ)
    {
        if (oldEQ == slotIndex)
        {
            barImage.sprite = invUI.greyGrid;
        }
        else if (newEQ == slotIndex)
        {
            barImage.sprite = invUI.blueGrid;
        }
    }
    
    void OnMouseEnter()
    {
        Vector3 scale = transform.localScale;
        scale.x = iconSize;
        scale.y = iconSize;
        transform.localScale = scale;

        transform.SetAsLastSibling();
        invUI.InvokeOnMouseEnter(this);
    }

    void OnMouseOver()
    {
        if (Input.GetKeyDown(KeyCode.Alpha1) && slotIndex != Equipment.Instance.Secondary)
        {
            Equipment.Instance.Primary = slotIndex;
        }
        else if (Input.GetKeyDown(KeyCode.Alpha2) && slotIndex != Equipment.Instance.Primary)
        {
            Equipment.Instance.Secondary = slotIndex;
        }

        if(slotIndex != Equipment.Instance.Primary && slotIndex != Equipment.Instance.Secondary)
        {
            barImage.sprite = invUI.greenGrid;
        }

        if (Input.GetKeyDown(KeyCode.Q) && Inventory.Instance.CanDrop()) //&& !iSO.GetLootingMode()
        {
            Inventory.Instance.TryToRemoveItem(slotIndex);
        }
    }
    
    void OnMouseExit()
    {
        Vector3 scale = transform.localScale;
        scale.x = 1;
        scale.y = 1;
        transform.localScale = scale;

        transform.SetSiblingIndex(slotIndex);

        barImage.sprite = invUI.greyGrid;
        invUI.InvokeOnMouseExit(this);
    }
}
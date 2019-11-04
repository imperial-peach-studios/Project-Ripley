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

    void Awake()
    {
        slotIndex = int.Parse(transform.name[transform.name.Length - 1].ToString());
        //index is 1-9 and then 0
        //we want 0-9
        slotIndex -= 1;
        if (slotIndex == -1)
            slotIndex = 9;

        barImage = GetComponent<Image>();
        slotImage = transform.GetChild(0).GetComponent<Image>();
        invUI = transform.parent.parent.parent.GetComponent<InventoryUI>();

        Equipment.Instance.OnSelectedHasChanged += a =>  OnEQChanged(-1,-1);

        Equipment.Instance.OnPrimaryChanged += OnEQChanged;
        Equipment.Instance.OnSecondaryChanged += OnEQChanged;
    }


    void OnEQChanged(int oldEQ, int newEQ)
    {
        if (slotIndex == Equipment.Instance.Primary || slotIndex == Equipment.Instance.Secondary)
        {
            barImage.sprite = invUI.blueGrid;
            //Debug.Log("Set to blue " + newEQ);
        }
        else
        {
            barImage.sprite = invUI.greyGrid;
        }

        UpdateScale();
        
    }


    void UpdateScale()
    {
        if (slotIndex == Equipment.Instance.Primary && Equipment.Instance.SelectedEQ == Equipment.Selected.Primary
            || slotIndex == Equipment.Instance.Secondary && Equipment.Instance.SelectedEQ == Equipment.Selected.Secondary)
        {
            barImage.transform.localScale = Vector3.one * iconSize;
        }
        else
        {
            barImage.transform.localScale = Vector3.one;
        }
    }

    void OnMouseEnter()
    {
        Vector3 scale = transform.localScale;
        scale.x = iconSize;
        scale.y = iconSize;
        transform.localScale = scale;

        transform.SetAsLastSibling();
        //invUI.InvokeOnMouseEnter(this);
    }

    void OnMouseOver()
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

        //if (slotIndex == Equipment.Instance.Primary || slotIndex == Equipment.Instance.Secondary)
        //{
        //    barImage.sprite = invUI.blueGrid;
        //}

        //if (slotIndex != Equipment.Instance.Primary && slotIndex != Equipment.Instance.Secondary)
        //{
        //    barImage.sprite = invUI.greenGrid;
        //}

        if (Input.GetKeyDown(KeyCode.Q) && Inventory.Instance.CanDrop()) //&& !iSO.GetLootingMode()
        {
            Inventory.Instance.TryToRemoveItem(slotIndex);
        }
    }

    void OnMouseExit()
    {
        UpdateScale();

        transform.SetSiblingIndex(slotIndex);

        //if(barImage.sprite == invUI.greenGrid)
        //     barImage.sprite = invUI.greyGrid;
        //invUI.InvokeOnMouseExit(this);
    }
}
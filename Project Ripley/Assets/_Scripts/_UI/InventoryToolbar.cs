using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class InventoryToolbar : MonoBehaviour
{
    [SerializeField] Image primaryBar, secondaryBar;
    [SerializeField] float scaleUp;
    Vector3 normalScale;

    void Awake()
    {
        normalScale = primaryBar.transform.localScale;

        Equipment.Instance.OnPrimaryChanged += (oldEq, newEq) => OnEqChanged(primaryBar, oldEq, newEq);
        Equipment.Instance.OnSecondaryChanged += (oldEq, newEq) => OnEqChanged(secondaryBar, oldEq, newEq); 

        Equipment.Instance.OnSelectedHasChanged += OnSelectedHasChanged;
    }

    void OnSelectedHasChanged(Equipment.Selected newSelected)
    {
        if(newSelected == Equipment.Selected.Primary)
        {
            primaryBar.transform.localScale = normalScale * scaleUp;
            secondaryBar.transform.localScale = normalScale;

            primaryBar.transform.SetAsLastSibling();
        }
        else
        {
            primaryBar.transform.localScale = normalScale;
            secondaryBar.transform.localScale = normalScale * scaleUp;

            secondaryBar.transform.SetAsLastSibling();
        }
    }

    void OnEqChanged(Image bar, int oldEQ, int newEQ)
    {
        //var newSprite = Inventory.Instance.GetItemInventorySlot(newEQ)?.uiIcon ?? null; //GetComponent<ItemInfo>()?.GetUISprite()
        Items item = Inventory.Instance.GetItemInventorySlot(newEQ);
        var newSprite = Inventory.Instance?.GetSprite(item.spriteName, item) ?? null;

        Image childBar = bar.transform.GetChild(0).GetComponent<Image>();
        childBar.sprite = newSprite;
        childBar.enabled = newSprite != null;
    }
}
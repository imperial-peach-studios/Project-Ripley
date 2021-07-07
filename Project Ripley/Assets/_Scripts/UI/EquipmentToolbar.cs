using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class EquipmentToolbar : MonoBehaviour
{
    [SerializeField] Image myPrimaryBar, mySecondaryBar;
    [SerializeField] float myScaleUp;
    Vector3 myNormalScale;

    void Awake()
    {
        myNormalScale = myPrimaryBar.transform.localScale;

        //Equipment.Instance.OnPrimaryChanged += (oldEq, newEq) => OnEqChanged(myPrimaryBar, oldEq, newEq);
        //Equipment.Instance.OnSecondaryChanged += (oldEq, newEq) => OnEqChanged(mySecondaryBar, oldEq, newEq);

        Equipment.Instance.OnPrimaryChanged += (oldEq, newEq) => OnNewEqChanged(myPrimaryBar, Equipment.Selected.Primary);
        Equipment.Instance.OnSecondaryChanged += (oldEq, newEq) => OnNewEqChanged(mySecondaryBar, Equipment.Selected.Secondary);

        Equipment.Instance.OnSelectedHasChanged += OnSelectedHasChanged;
    }

    void OnSelectedHasChanged(Equipment.Selected newSelected)
    {
        if (newSelected == Equipment.Selected.Primary)
        {
            myPrimaryBar.transform.localScale = myNormalScale * myScaleUp;
            mySecondaryBar.transform.localScale = myNormalScale;

            myPrimaryBar.transform.SetAsLastSibling();
        }
        else
        {
            myPrimaryBar.transform.localScale = myNormalScale;
            mySecondaryBar.transform.localScale = myNormalScale * myScaleUp;

            mySecondaryBar.transform.SetAsLastSibling();
        }
    }

    //void OnEqChanged(Image bar, int oldEQ, int newEQ)
    //{
    //    //var myItemItem = Player.Instance.inventory.GetItemItem(newEQ);
    //    var myItemItem = Player.Instance.newInvetory.GetItemItem();
    //    var newSprite = ItemFactory.Instance.GetSprite(myItemItem);

    //    if((int)myItemItem < 0 || (int)myItemItem > (int)ItemFactory.ItemItem.Count)
    //    {
    //        Image childBar2 = bar.transform.GetChild(0).GetComponent<Image>();
    //        childBar2.sprite = null;
    //        childBar2.enabled = false;
    //        return;
    //    }

    //    Image childBar = bar.transform.GetChild(0).GetComponent<Image>();
    //    childBar.sprite = newSprite;
    //    childBar.enabled = Player.Instance.newInvetory.GetItemType() != ItemType.None;

    //    //childBar.enabled = Player.Instance.inventory.GetItemType(newEQ) != ItemType.None;

    //    //var newSprite = Inventory.Instance.GetItemInventorySlot(newEQ)?.uiIcon ?? null; //GetComponent<ItemInfo>()?.GetUISprite()
    //    //Items item = Inventory.Instance.GetItemInventorySlot(newEQ);
    //    //var newSprite = Inventory.Instance?.GetSprite(item.spriteName, item) ?? null;

    //    //Image childBar = bar.transform.GetChild(0).GetComponent<Image>();
    //    //childBar.sprite = newSprite;
    //    //childBar.enabled = newSprite != null;
    //}

    void OnNewEqChanged(Image bar, Equipment.Selected aSelected)
    {
        //var myItemItem = Player.Instance.inventory.GetItemItem(newEQ);
        var myItemItem = Player.Instance.inventory.GetItemItem(aSelected);
        var newSprite = ItemFactory.Instance.GetSprite(myItemItem);

        if ((int)myItemItem < 0 || (int)myItemItem > (int)ItemFactory.ItemType.Count)
        {
            Image childBar2 = bar.transform.GetChild(0).GetComponent<Image>();
            childBar2.sprite = null;
            childBar2.enabled = false;
            return;
        }

        Image childBar = bar.transform.GetChild(0).GetComponent<Image>();
        childBar.sprite = newSprite;
        childBar.enabled = Player.Instance.inventory.GetItemType(aSelected) != ItemCategory.None;

        //childBar.enabled = Player.Instance.inventory.GetItemType(newEQ) != ItemType.None;

        //var newSprite = Inventory.Instance.GetItemInventorySlot(newEQ)?.uiIcon ?? null; //GetComponent<ItemInfo>()?.GetUISprite()
        //Items item = Inventory.Instance.GetItemInventorySlot(newEQ);
        //var newSprite = Inventory.Instance?.GetSprite(item.spriteName, item) ?? null;

        //Image childBar = bar.transform.GetChild(0).GetComponent<Image>();
        //childBar.sprite = newSprite;
        //childBar.enabled = newSprite != null;
    }
}
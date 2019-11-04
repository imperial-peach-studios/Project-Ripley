using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class InventorySlotColl : MonoBehaviour
{
    public int slotIndex;
    public bool mouseOver = false;
    InventoryUI invUI;
    Image barImage;
    public Image BarImage
    {
        get { return barImage; }
        set { barImage = value; }
    }
    Image childImage;
    public Image ChildImage
    {
        get { return childImage; }
        set { childImage = value; }
    }

    void Awake()
    {
        slotIndex = int.Parse(transform.name[transform.name.Length - 1].ToString());
        //index is 1-9 and then 0
        //we want 0-9
        slotIndex -= 1;
        if (slotIndex == -1)
            slotIndex = 9;

        barImage = GetComponent<Image>();
        childImage = transform.GetChild(0).GetComponent<Image>();
        invUI = transform.parent.parent.parent.GetComponent<InventoryUI>();
    }

    void OnMouseEnter()
    {
        invUI.InvokeOnMouseEnter(this);
    }

    void OnMouseOver()
    {
        mouseOver = true;
    }

    void OnMouseExit()
    {
        mouseOver = false;
        invUI.InvokeOnMouseExit(this);
    }
}
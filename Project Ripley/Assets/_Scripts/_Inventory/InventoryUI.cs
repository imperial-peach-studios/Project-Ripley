using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class InventoryUI : MonoBehaviour
{
    public Sprite greenGrid;
    public Sprite greyGrid;
    public Sprite blueGrid;

    public delegate void OnMouse(InventorySlotUI slot);

    public event OnMouse OnMouseExit;
    public void InvokeOnMouseExit(InventorySlotUI slot) => OnMouseExit?.Invoke(slot);

    public event OnMouse OnMouseEnter;
    public void InvokeOnMouseEnter(InventorySlotUI slot) => OnMouseEnter?.Invoke(slot);

    static int onMouseIndex;
    static bool IsMouseOver = false;
    
    void Awake()
    {
        OnMouseEnter += OnMouseEnterSlot;
        OnMouseExit += slot => IsMouseOver = false;
    }

    void OnMouseEnterSlot(InventorySlotUI slot)
    {
        onMouseIndex = slot.slotIndex;

        IsMouseOver = true;
    }

    public static int GetCurrentMouseOverSlotIndex()
    {
        return onMouseIndex;
    }

    public static bool IsMouseOverUI()
    {
        return IsMouseOver;
    }
}
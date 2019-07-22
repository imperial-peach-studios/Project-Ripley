using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

public class PickUpGiver : MonoBehaviour
{
    [SerializeField] GameObject storedItem;
    private bool addTolist = false;

    public void TryToAddItemToInventory()
    {
        if (InventoryUI.IsMouseOverUI())
        {
            int currentSlotIndex = InventoryUI.GetCurrentMouseOverSlotIndex();

            Inventory.Instance.TryToRemoveItem(currentSlotIndex);
            Inventory.Instance.AddItemToSlot(storedItem, currentSlotIndex);
        }
        else
        {
            Inventory.Instance.AddItem(storedItem);
        }

        Destroy(gameObject);
    }

    public void StoreItem(GameObject newItem)
    {
        storedItem = newItem;
    }
}
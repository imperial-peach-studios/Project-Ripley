using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

public class PickUpGiver : MonoBehaviour
{
    [SerializeField] string itemName;
    [SerializeField] GameObject storedItem;
    private bool addTolist = false;

    public void TryToAddItemToInventory()
    {
        if (InventoryUI.IsMouseOverUI())
        {
            int currentSlotIndex = InventoryUI.GetCurrentMouseOverSlotIndex();

            //Inventory.Instance.TryToRemoveItem(currentSlotIndex);

            //Inventory.Instance.AddItemToSlot(gameObject, currentSlotIndex);
            
            //Inventory.Instance.AddItem(storedItem);

            

            //ItemInfo i = GetComponent<ItemInfo>();
            //Inventory.Instance.AddItemInventory(ref i);

            if(Input.GetKey(KeyCode.LeftControl))
            {
                if (Equipment.Instance.SelectedEQ == Equipment.Selected.Primary)
                {
                    Equipment.Instance.Primary = currentSlotIndex;
                }
                else
                {
                    Equipment.Instance.Secondary = currentSlotIndex;
                }
            }
        }
        else
        {
            //Inventory.Instance.AddItem(storedItem);
        }

        if(gameObject.layer == LayerMask.NameToLayer("Loot"))
        {
            gameObject.layer = 0;
           // Destroy(this);
        }
        else
        {
           //Destroy(this.gameObject);
        }

        //storedItem = null;
    }

    public void StoreItem(GameObject newItem)
    {
        storedItem = newItem;
    }
    public GameObject GetItem()
    {
        return storedItem;
    }
}
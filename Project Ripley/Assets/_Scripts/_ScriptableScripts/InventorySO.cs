using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

[CreateAssetMenu]
public class InventorySO : ScriptableObject
{
    public List<GameObject> myInventory = new List<GameObject>();
    public GameObject primary, secondary;
    public int currentWeapon;
    //public Image primaryImage;
    //public Image secondaryImage;
    public int availableSlot = 0; 
    public int nextAvailableSlot;
    public GameObject pickUpObject;
    public bool availableToDrop = false;
    public bool mouseOverInvetory = false;
    public int mouseOverIndex;
    public bool inventoryFull = false;
    public string currentItemTag;
    public List<GameObject> allItems = new List<GameObject>();

    [SerializeField] bool looting = false;

    public Sprite newGunSprite;

    public Sprite greenGrid;
    public Sprite greyGrid;
    public Sprite blueGrid;

    public Vector2 pickUpBoxSize;
    public Vector2 pickUpBoxOffset;

    public int primaryIndex = -1, secondaryIndex = -1;
    WeaponCreator weaponCreator;
    public WeaponCreator WeaponCreator
    {
        get
        {
            return weaponCreator;
        }
    }

    public void AddItem(int index)
    {

    }

    public string GetItemInfoText(int id)
    {
        return myInventory[id].GetComponent<ItemInfo>().GetItemInfo();
    }

    public void SetLootingMode(bool mode)
    {
        looting = mode;
    }

    public bool GetLootingMode()
    {
        return looting;
    }

    public void MouseOverInvetoryInfo(int myMouseOverIndex)
    {
        mouseOverIndex = myMouseOverIndex;
    }

    public void UpdateSlots()
    {
        inventoryFull = true;
        for (int i = 0; i < myInventory.Count; i++)
        {
            if (myInventory[i] == null)
            {
                inventoryFull = false;
                if (i > 0)
                {
                    if (myInventory[i - 1] != null)
                    {
                        nextAvailableSlot = i;

                        if (myInventory[availableSlot] != null)
                        {
                            availableSlot = nextAvailableSlot;
                        }
                        else if (myInventory[availableSlot] == null)
                        {
                            if (availableSlot < nextAvailableSlot)
                            {
                                nextAvailableSlot = availableSlot;
                            }
                            else if (nextAvailableSlot < availableSlot)
                            {
                                availableSlot = nextAvailableSlot;
                            }
                        }
                    }
                }
                else
                {
                    availableSlot = 0;
                    nextAvailableSlot = 0;
                }
            }
        }
    }

    public void AvailableToDrop(Transform myTransform, LayerMask notIgnoreLayers)
    {
        if (myInventory[mouseOverIndex] != null && myInventory[mouseOverIndex] != null)
        {
            Collider2D[] hit = Physics2D.OverlapBoxAll(myTransform.position,
                myInventory[mouseOverIndex].GetComponent<ItemInfo>().GetCollisionBoxSize(), 0f, notIgnoreLayers);

            if (hit.Length == 0)
            {
                availableToDrop = true;
            }
            else
            {
                availableToDrop = false;
                //for (int i = 0; i < hit.Length; i++)
                //{
                    //availableToDrop = false;
                    //if (hit[i].transform.tag != "PickUpBoxes")
                    //{
                    //    Debug.Log("AVAIBLABLE");
                    //    availableToDrop = true;
                    //}
                    //else if (hit[i].transform.tag == "PickUpBoxes")
                    //{
                    //    Debug.Log("NOT AVAIBLABLE");
                    //    availableToDrop = false;
                    //}
                    //Debug.DrawRay(myTransform.position, hit[i].transform.position);
                //}
            }
        }
    }

    public void MoveToToolbar(int primaryNotPrimary, int currentIndex)
    {
        if (primaryNotPrimary == 1)
        {
            if (primaryIndex != currentIndex)
            {
                if (myInventory[currentIndex] == null)
                {
                    
                    primary = null;
                    primaryIndex = currentIndex;
                }
                else //If A Object Exists In That Invetory Spot
                {
                    if (secondary == myInventory[currentIndex].gameObject && secondaryIndex == currentIndex)
                    {
                        secondary = null;
                        secondaryIndex = -1;
                    }
                    primary = myInventory[currentIndex].gameObject;
                    primaryIndex = currentIndex;
                    currentItemTag = primary.tag;
                }
            }
        }
        else if (primaryNotPrimary == 2)
        {
            if (secondaryIndex != currentIndex)
            {
                if (myInventory[currentIndex] == null)
                {
                    secondary = null;
                    secondaryIndex = currentIndex;
                }
                else
                {
                    if (primary == myInventory[currentIndex].gameObject && primaryIndex == currentIndex)
                    {
                        primary = null;
                        primaryIndex = -1;
                    }
                    secondary = myInventory[currentIndex].gameObject;
                    secondaryIndex = currentIndex;
                    currentItemTag = secondary.tag;
                }
            }
        }
    }

    public void RemoveItem(int inventoryBarIndex)
    {
        if (primaryIndex == inventoryBarIndex)
        {
            //if (mouseOverInvetory == true && primaryIndex == inventoryBarIndex)
            //{
            //    primaryIndex = -1;
            //    primary = null;
            //    //currentWeapon = 1;
            //}
            //else
            //{
            //    primaryIndex = -1;
            //    primary = null;
            //    //currentWeapon = 1;
            //}
            primaryIndex = -1;
            primary = null;
        }
        else if (secondaryIndex == inventoryBarIndex)
        {
            //if(mouseOverInvetory == true && secondaryIndex == inventoryBarIndex)
            //{
            //    secondaryIndex = -1;
            //    secondary = null;
            //    //currentWeapon = 1;
            //}
            //else if(mouseOverInvetory == true && secondaryIndex != inventoryBarIndex)
            //{

            //}
            //else
            //{
            //    secondaryIndex = -1;
            //    secondary = null;
            //    //currentWeapon = 1;
            //}
            secondaryIndex = -1;
            secondary = null;
        }

        if (myInventory[inventoryBarIndex] != null)
        {
            GameObject newPickUp = (Instantiate(pickUpObject, GameObject.Find("Player").transform.position + new Vector3(0f, 0f), Quaternion.identity) as GameObject);

            //newPickUp.GetComponent<PickUpItem>().pickItem = myInventory[inventoryBarIndex];
            newPickUp.GetComponent<InteractionGiver>().AddItem(myInventory[inventoryBarIndex]);

            newPickUp.GetComponent<BoxCollider2D>().size = myInventory[inventoryBarIndex].GetComponent<ItemInfo>().GetCollisionBoxSize();
            newPickUp.GetComponentInChildren<BoxCollider2D>().size = myInventory[inventoryBarIndex].GetComponent<ItemInfo>().GetPickUpBoxSize();

            newPickUp.GetComponent<SpriteRenderer>().sprite = myInventory[inventoryBarIndex].GetComponent<ItemInfo>().GetUISprite();
        }
        myInventory[inventoryBarIndex] = null;
    }

    public void DeleteItem(int inventoryBarIndex)
    {
        if (primaryIndex == inventoryBarIndex)
        {
            primaryIndex = -1;
            primary = null;
            //currentWeapon = 1;
        }
        else if (secondaryIndex == inventoryBarIndex)
        {
            secondaryIndex = -1;
            secondary = null;
            //currentWeapon = 2;
        }
        if (myInventory[inventoryBarIndex] != null)
        {
            myInventory[inventoryBarIndex] = null;
        }
    }
}
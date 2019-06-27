using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class NewPlayerInvetory : MonoBehaviour
{
    public InventorySO invetorySO;
    GameObject currentWeapon;
    GameObject myPrimary, mySecondary;
    GameObject parentPrimary, parentSecondary;
    int myPrimaryIndex, mySecondaryIndex;
    [SerializeField] LayerMask notIgnoreLayers;
    PlayerMovement playerM;
    Animator anim;
    InteractionHandler iHandler;

    void Awake()
    {
        GameData.OnSavePlayer += OnSave;
        GameData.OnLoadPlayer += OnLoad;

        anim = GetComponent<Animator>();
        playerM = GetComponent<PlayerMovement>();
        parentPrimary = transform.Find("Primary").gameObject;
        parentSecondary = transform.Find("Secondary").gameObject;
        iHandler = GetComponent<InteractionHandler>();
    }

    void Update()
    {
        if(invetorySO.currentWeapon == 1 && invetorySO.primary != null)
        {
            anim.SetFloat("ItemAttackID", invetorySO.myInventory[invetorySO.primaryIndex].GetComponent<ItemInfo>().GetAnimationID());
        }
        else if(invetorySO.currentWeapon == 2 && invetorySO.secondary != null)
        {
            anim.SetFloat("ItemAttackID", invetorySO.myInventory[invetorySO.secondaryIndex].GetComponent<ItemInfo>().GetAnimationID());
        }
        else
        {
            anim.SetFloat("ItemAttackID", 0);
        }

        UpdatePrimaries(myPrimary, myPrimaryIndex, invetorySO.primaryIndex, invetorySO.primary, parentPrimary, mySecondary);

        UpdatePrimaries(mySecondary, mySecondaryIndex, invetorySO.secondaryIndex, invetorySO.secondary, parentSecondary, myPrimary);


        if (invetorySO.currentWeapon == 1)
        {
            parentPrimary.SetActive(true);
            parentSecondary.SetActive(false);
        }
        else if (invetorySO.currentWeapon == 2) //If The Current Item Holding Is On The Second Hand
        {
            parentPrimary.SetActive(false);
            parentSecondary.SetActive(true);
        }
        else
        {
            parentPrimary.SetActive(false);
            parentSecondary.SetActive(false);
        }


        if (Input.GetKeyDown(KeyCode.Alpha1) && anim.GetBool("Click Attack") == false && anim.GetBool("Hold Attack") == false)
        {
            invetorySO.currentWeapon = 1;
        }
        else if (Input.GetKeyDown(KeyCode.Alpha2) && anim.GetBool("Click Attack") == false && anim.GetBool("Hold Attack") == false)
        {
            invetorySO.currentWeapon = 2;
        }
        
        invetorySO.AvailableToDrop(transform, notIgnoreLayers); 

        if(!invetorySO.GetLootingMode())
        {
            if (Input.GetKeyDown(KeyCode.Q) && invetorySO.availableToDrop == true && invetorySO.mouseOverInvetory == false)
            {
                if (invetorySO.currentWeapon == 1 && invetorySO.primaryIndex != -1)
                {
                    invetorySO.RemoveItem(invetorySO.primaryIndex);
                }
                else if (invetorySO.currentWeapon == 2 && invetorySO.secondaryIndex != -1)
                {
                    invetorySO.RemoveItem(invetorySO.secondaryIndex);
                }
            }
        }
    }

    public void AddSingleItem(ref GameObject item, int currentIcon = -1)
    {
        invetorySO.UpdateSlots();

        if (invetorySO.mouseOverInvetory == true) //If Mouse Hovering Over Invetory
        {
            if (currentIcon > -1) //If Clicked On Loot UI
            {
                if (invetorySO.myInventory[invetorySO.mouseOverIndex] != null) //If Invetory Slot Already Containts A Item.
                {
                    GameObject newObjcet = invetorySO.myInventory[invetorySO.mouseOverIndex];

                    invetorySO.myInventory[invetorySO.mouseOverIndex] = item;
                    
                    item = newObjcet;
                }
                else //If The Invetory Slot Is Free
                {
                    invetorySO.myInventory[invetorySO.mouseOverIndex] = item;
                    item = null;
                }
            }
            else //If Not Clicked On Loot UI
            {
                invetorySO.RemoveItem(invetorySO.mouseOverIndex);

                invetorySO.myInventory[invetorySO.mouseOverIndex] = item;

                item = null;
            }

            if (invetorySO.primaryIndex == invetorySO.mouseOverIndex && invetorySO.myInventory[invetorySO.mouseOverIndex] != null)
            {
                invetorySO.primary = invetorySO.myInventory[invetorySO.mouseOverIndex];
            }
            else if (invetorySO.secondaryIndex == invetorySO.mouseOverIndex && invetorySO.myInventory[invetorySO.mouseOverIndex] != null)
            {
                invetorySO.secondary = invetorySO.myInventory[invetorySO.mouseOverIndex];
            }
        }
        else
        {
            if (invetorySO.inventoryFull == false)
            {
                invetorySO.myInventory[invetorySO.availableSlot] = item;
                item = null;
            }
        }
    }

    void UpdatePrimaries(GameObject primary, int index, int invetoryIndex, GameObject invetoryPrimary, GameObject parentNewPrimary, GameObject otherPrimary)
    {
        if (invetoryPrimary == null)
        {
            Destroy(primary);
            //myInventory.currentWeapon = 0;
        }

        if (primary != null && invetoryPrimary == null)
        {
            if (parentNewPrimary.transform.Find(invetoryPrimary.name) == false)
            {
                GameObject newPrimary = Instantiate(invetoryPrimary, parentNewPrimary.transform) as GameObject;
                newPrimary.name = invetoryPrimary.name;
                primary = newPrimary;
                index = invetoryIndex;
            }
        }

        if (primary != null)
        {
            if (primary.transform.tag == "Consumable")
            {
                if (primary.GetComponent<ConsumableItemManager>().consumed == true)
                {
                    invetorySO.DeleteItem(index);
                }
                else if (primary.GetComponent<MeleeWeaponManager>() == true)
                {
                    primary.GetComponent<MeleeWeaponManager>().canAttack = !anim.GetBool("Click Attack");
                }
            }

            if (invetoryPrimary == null)
            {
                Destroy(otherPrimary);
                otherPrimary = null;
            }
            else if (primary.name != invetoryPrimary.name)
            {
                Destroy(primary);
                primary = null;
            }
        }
    }

    public void OnSave()
    {
        GameData.aData.pData.inventorySO = invetorySO;
    }
    public void OnLoad()
    {
        invetorySO = GameData.aData.pData.inventorySO;
    }
}
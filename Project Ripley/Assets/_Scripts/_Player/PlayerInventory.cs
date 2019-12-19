using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class PlayerInventory : MonoBehaviour
{
    public InventorySO myInventory;
    public GameObject currentWeapon;
    public GameObject myPrimary, mySecondary;
    private GameObject parentPrimary, parentSecondary;
    public Transform myRightHand;
    public int myPrimaryIndex;
    public int mySecondaryIndex;
    PlayerMovement playerM;
    public Text pickUp;
    public bool inventoryFull = false;
    public Transform toolBar;
    public LayerMask notIgnoreLayers;
    Animator anim;
    [SerializeField]
    private string pickUpTagCollision;

    public string PickUpTagCollision
    {
        get { return pickUpTagCollision; }
    }

    void Start()
    {
        anim = GetComponent<Animator>();
        playerM = GetComponent<PlayerMovement>();
        parentPrimary = transform.Find("Primary").gameObject;
        parentSecondary = transform.Find("Secondary").gameObject;
    }
    public bool IfInvetoryFull()
    {
        return inventoryFull;
    }

    void Update()
    {
        myInventory.inventoryFull = true;
        for (int i = 0; i < myInventory.myInventory.Count; i++)
        {
            if (myInventory.myInventory[i] == null)
            {
                myInventory.inventoryFull = false;
                if (i > 0)
                {
                    if (myInventory.myInventory[i - 1] != null)
                    {
                        myInventory.nextAvailableSlot = i;

                        if (myInventory.myInventory[myInventory.availableSlot] != null)
                        {
                            myInventory.availableSlot = myInventory.nextAvailableSlot;
                        }
                        else if (myInventory.myInventory[myInventory.availableSlot] == null)
                        {
                            if (myInventory.availableSlot < myInventory.nextAvailableSlot)
                            {
                                myInventory.nextAvailableSlot = myInventory.availableSlot;
                            }
                            else if (myInventory.nextAvailableSlot < myInventory.availableSlot)
                            {
                                myInventory.availableSlot = myInventory.nextAvailableSlot;
                            }
                        }
                    }
                }
                else
                {
                    myInventory.availableSlot = 0;
                    myInventory.nextAvailableSlot = 0;
                }
            }
        }

        if (myInventory.currentWeapon == 1 && myInventory.primary != null)
        {
            anim.SetFloat("ItemAttackID", myInventory.myInventory[myInventory.primaryIndex].GetComponent<ItemInfo>().GetAnimationID());
        }
        else if (myInventory.currentWeapon == 2 && myInventory.secondary != null)
        {
            anim.SetFloat("ItemAttackID", myInventory.myInventory[myInventory.secondaryIndex].GetComponent<ItemInfo>().GetAnimationID());
        }
        else
        {
            anim.SetFloat("ItemAttackID", 0);
        }

        if (myInventory.primary == null)
        {
            Destroy(myPrimary);
            //myInventory.currentWeapon = 0;
        }
        if (myInventory.secondary == null)
        {
            Destroy(mySecondary);
            //myInventory.currentWeapon = 0;
        }

        if (myInventory.primary != null && myPrimary == null)
        {
            if (parentPrimary.transform.Find(myInventory.primary.name) == false)
            {
                GameObject newPrimary = Instantiate(myInventory.primary, parentPrimary.transform) as GameObject;
                newPrimary.name = myInventory.primary.name;
                myPrimary = newPrimary;
                myPrimaryIndex = myInventory.primaryIndex;
            }
        }

        if (myPrimary != null)
        {
            if (myPrimary.transform.tag == "Consumable")
            {
                if (myPrimary.GetComponent<ConsumableItemManager>().consumed == true)
                {
                    myInventory.DeleteItem(myPrimaryIndex);
                }
                else if(myPrimary.GetComponent<MeleeWeaponManager>() == true)
                {
                    myPrimary.GetComponent<MeleeWeaponManager>().canAttack = !anim.GetBool("Click Attack");
                }
            }
            if (myInventory.primary == null)
            {
                Destroy(mySecondary);
                mySecondary = null;
            }
            else if (myPrimary.name != myInventory.primary.name)
            {
                Destroy(myPrimary);
                myPrimary = null;
            }
        }

        if (myInventory.secondary != null && mySecondary == null)
        {
            if (parentSecondary.transform.Find(myInventory.secondary.name) == false)
            {
                GameObject newSecondary = Instantiate(myInventory.secondary, parentSecondary.transform) as GameObject;
                newSecondary.name = myInventory.secondary.name;
                mySecondary = newSecondary;
                mySecondaryIndex = myInventory.secondaryIndex;
            }
        }

        if (mySecondary != null)
        {
            if (mySecondary.transform.tag == "Consumable")
            {
                if (mySecondary.GetComponent<ConsumableItemManager>().consumed == true)
                {
                    myInventory.DeleteItem(mySecondaryIndex);
                }
            }
            if(myInventory.secondary == null)
            {
                Destroy(mySecondary);
                mySecondary = null;
            }
            else if (mySecondary.name != myInventory.secondary.name)
            {
                Destroy(mySecondary);
                mySecondary = null;
            }
        }

        if (myInventory.currentWeapon == 1)
        {
            parentPrimary.SetActive(true);
            parentSecondary.SetActive(false);
        }
        else if(myInventory.currentWeapon == 2) //If The Current Item Holding Is On The Second Hand
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
            myInventory.currentWeapon = 1;
        }
        else if (Input.GetKeyDown(KeyCode.Alpha2) && anim.GetBool("Click Attack") == false && anim.GetBool("Hold Attack") == false)
        {
            myInventory.currentWeapon = 2;
        }

        if(Input.GetKeyDown(KeyCode.Q) && myInventory.availableToDrop == true && myInventory.mouseOverInvetory == false)
        {
            if(myInventory.currentWeapon == 1 && myInventory.primaryIndex != -1)
            {
                myInventory.RemoveItem(myInventory.primaryIndex);
            }
            else if(myInventory.currentWeapon == 2 && myInventory.secondaryIndex != -1)
            {
                myInventory.RemoveItem(myInventory.secondaryIndex);
            }
        }

        if (myPrimary != null)
        {
            //myPrimary.GetComponent<SpriteRenderer>().sortingOrder = transform.GetComponent<SpriteRenderer>().sortingOrder + 1;
        }
        if (mySecondary != null)
        {
            //mySecondary.GetComponent<SpriteRenderer>().sortingOrder = transform.GetComponent<SpriteRenderer>().sortingOrder + 1;
        }

        //if(myInventory.myInventory[myInventory.mouseOverIndex] != null)
        //{
        //    RaycastHit2D[] hit = Physics2D.BoxCastAll(transform.position,
        //        myInventory.myInventory[myInventory.mouseOverIndex].GetComponent<ItemInfo>().GetCollisionBoxSize(), 0f, Vector2.zero, notIgnoreLayers);
        //    for (int i = 0; i < hit.Length; i++)
        //    {
        //        if(hit[i].transform.tag != "PickUpBoxes")
        //        {
        //            myInventory.availableToDrop = true;
        //        }
        //        else if(hit[i].transform.tag == "PickUpBoxes")
        //        {
        //            myInventory.availableToDrop = false;
        //        }
        //        Debug.DrawRay(transform.position, hit[i].transform.position);
        //    }
        //}
    }

    public void TriggerStay(GameObject other, GameObject sender, List<GameObject> others, bool destroy)
    {
        // pickUp.gameObject.SetActive(true);
        for (int i = 0; i < others.Count; i++)
        {
            Available();
            if (myInventory.mouseOverInvetory == true && others.Count == 1)
            {
                myInventory.RemoveItem(myInventory.mouseOverIndex);

                //myInventory.myInventory[myInventory.mouseOverIndex] = other.transform.parent.GetComponent<PickUpItem>().pickItem;
                myInventory.myInventory[myInventory.mouseOverIndex] = others[i];

                if (myInventory.primaryIndex == myInventory.mouseOverIndex && myInventory.myInventory[myInventory.mouseOverIndex] != null)
                {
                    myInventory.primary = myInventory.myInventory[myInventory.mouseOverIndex];
                }
                else if (myInventory.secondaryIndex == myInventory.mouseOverIndex && myInventory.myInventory[myInventory.mouseOverIndex] != null)
                {
                    myInventory.secondary = myInventory.myInventory[myInventory.mouseOverIndex];
                }
                //others.RemoveAt(i);
                //Destroy(other.transform.parent.gameObject);
                // Destroy(sender);

                //pickUp.GetComponent<Text>().text = "E To Swap With Highlighted Item";
            }
            else
            {
                if (myInventory.inventoryFull == false)
                {
                    //pickUp.GetComponent<Text>().text = "E To Pick Up";
                    /// myInventory.myInventory[myInventory.availableSlot] = other.transform.GetComponent<PickUpItem>().pickItem; //other.transform.parent.GetComponent<PickUpItem>().pickItem;
                    myInventory.myInventory[myInventory.availableSlot] = others[i];
                    others.Remove(others[i]);
                    i = -1;

                    //Destroy(other.transform.parent.gameObject);
                    //Destroy(sender);
                    //pickUp.gameObject.SetActive(false);
                }
                else
                {
                    continue;
                    //pickUp.GetComponent<Text>().text = "Inventory Is Full";
                }
            }
            
            if (i == others.Count && destroy == true)
            {
                if(myInventory.mouseOverInvetory == true || myInventory.inventoryFull == false)
                {
                    Destroy(sender);
                }     
            }
        }

        //if (myInventory.mouseOverInvetory == true)
        //{
        //    myInventory.RemoveItem(myInventory.mouseOverIndex);

        //    //myInventory.myInventory[myInventory.mouseOverIndex] = other.transform.parent.GetComponent<PickUpItem>().pickItem;
        //    myInventory.myInventory[myInventory.mouseOverIndex] = other;

        //    if (myInventory.primaryIndex == myInventory.mouseOverIndex && myInventory.myInventory[myInventory.mouseOverIndex] != null)
        //    {
        //        myInventory.primary = myInventory.myInventory[myInventory.mouseOverIndex];
        //    }
        //    else if (myInventory.secondaryIndex == myInventory.mouseOverIndex && myInventory.myInventory[myInventory.mouseOverIndex] != null)
        //    {
        //        myInventory.secondary = myInventory.myInventory[myInventory.mouseOverIndex];
        //    }
        //    //Destroy(other.transform.parent.gameObject);
        //    Destroy(sender);

        //    //pickUp.GetComponent<Text>().text = "E To Swap With Highlighted Item";
        //}
        //else
        //{
        //    if (myInventory.inventoryFull == false)
        //    {
        //        //pickUp.GetComponent<Text>().text = "E To Pick Up";
        //        /// myInventory.myInventory[myInventory.availableSlot] = other.transform.GetComponent<PickUpItem>().pickItem; //other.transform.parent.GetComponent<PickUpItem>().pickItem;
        //        myInventory.myInventory[myInventory.availableSlot] = other;

        //        //Destroy(other.transform.parent.gameObject);
        //        Destroy(sender);
        //        //pickUp.gameObject.SetActive(false);
        //    }
        //    else
        //    {
        //        //pickUp.GetComponent<Text>().text = "Inventory Is Full";
        //    }
        //}
        //Destroy(other.transform.parent.gameObject);
        //pickUp.gameObject.SetActive(false);
        //pickUp.gameObject.SetActive(false);
    }
    void Available()
    {
        myInventory.inventoryFull = true;
        for (int i = 0; i < myInventory.myInventory.Count; i++)
        {
            if (myInventory.myInventory[i] == null)
            {
                myInventory.inventoryFull = false;
                if (i > 0)
                {
                    if (myInventory.myInventory[i - 1] != null)
                    {
                        myInventory.nextAvailableSlot = i;

                        if (myInventory.myInventory[myInventory.availableSlot] != null)
                        {
                            myInventory.availableSlot = myInventory.nextAvailableSlot;
                        }
                        else if (myInventory.myInventory[myInventory.availableSlot] == null)
                        {
                            if (myInventory.availableSlot < myInventory.nextAvailableSlot)
                            {
                                myInventory.nextAvailableSlot = myInventory.availableSlot;
                            }
                            else if (myInventory.nextAvailableSlot < myInventory.availableSlot)
                            {
                                myInventory.availableSlot = myInventory.nextAvailableSlot;
                            }
                        }
                    }
                }
                else
                {
                    myInventory.availableSlot = 0;
                    myInventory.nextAvailableSlot = 0;
                }
            }
        }
    }


    public void TriggerExit(Collider2D other)
    {
        //pickUp.gameObject.SetActive(false);
    }
    //void OnTriggerStay2D(Collider2D other)
    //{
    //    if (other.transform.tag == "Pick Ups")
    //    {
    //        pickUp.gameObject.SetActive(true);
    //        if (Input.GetKeyDown(KeyCode.E))
    //        {
    //            if(myInventory.mouseOverInvetory == true)
    //            {
    //                myInventory.RemoveItem(myInventory.mouseOverIndex);
    //                myInventory.myInventory[myInventory.mouseOverIndex] = other.transform.parent.GetComponent<PickUpItem>().pickItem;

    //                if(myInventory.primaryIndex == myInventory.mouseOverIndex && myInventory.myInventory[myInventory.mouseOverIndex] != null)
    //                {
    //                    myInventory.primary = myInventory.myInventory[myInventory.mouseOverIndex];
    //                }
    //                else if(myInventory.secondaryIndex == myInventory.mouseOverIndex && myInventory.myInventory[myInventory.mouseOverIndex] != null)
    //                {
    //                    myInventory.secondary = myInventory.myInventory[myInventory.mouseOverIndex];
    //                }
    //                Destroy(other.transform.parent.gameObject);
    //                //pickUp.GetComponent<Text>().text = "E To Swap With Highlighted Item";
    //            }
    //            else
    //            {
    //                if(myInventory.inventoryFull == false)
    //                {
    //                    //pickUp.GetComponent<Text>().text = "E To Pick Up";
    //                    myInventory.myInventory[myInventory.availableSlot] = other.transform.parent.GetComponent<PickUpItem>().pickItem;

    //                    Destroy(other.transform.parent.gameObject);
    //                    //pickUp.gameObject.SetActive(false);
    //                }
    //                else
    //                {
    //                    //pickUp.GetComponent<Text>().text = "Inventory Is Full";
    //                }
    //            }
    //            //Destroy(other.transform.parent.gameObject);
    //            //pickUp.gameObject.SetActive(false);
    //            pickUp.gameObject.SetActive(false);
    //        }
    //    }
    //}
    //void OnTriggerExit2D(Collider2D other)
    //{
    //    if (other.transform.tag == "Pick Ups")
    //    {
    //        pickUp.gameObject.SetActive(false);
    //    }
    //}
}
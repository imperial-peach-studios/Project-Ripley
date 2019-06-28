using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class InventoryUIManager : MonoBehaviour
{
    public GameObject toolbarParent;
    public GameObject uiParent;
    public InventorySO iSO;
    private Transform bar1, bar2;
    public GameObject UIpanel;

    void Start()
    {
        bar1 = toolbarParent.transform.Find("Bar 1"); //Find And Assign The Bar 1 To The Inventory Toolbar 1
        bar2 = toolbarParent.transform.Find("Bar 2"); //Find And Assign The Bar 2 To The Inventory Toolbar 2
    }

    void Update()
    {
        for (int i = 0; i < iSO.myInventory.Count; i++) //Go Throught The Inventory List
        {
            if (iSO.myInventory[i] != null) //If The Current Inventory Slot Has An Item.    
            {
                iSO.myInventory[i].GetComponent<ItemInfo>().UpdateInfo(); //Update The Inventory Slots Item Info

                uiParent.transform.Find("Bar " + (i + 1)).GetChild(0).GetComponent<Image>().enabled = true; //Enable The Image Component
                uiParent.transform.Find("Bar " + (i + 1)).GetChild(0).GetComponent<Image>().sprite = iSO.myInventory[i].GetComponent<ItemInfo>().GetUISprite();//Get The Correct-
                //-Sprite.
            }
            else //If The Current Inventory Slot Is Empty
            {
                uiParent.transform.Find("Bar " + (i + 1)).GetChild(0).GetComponent<Image>().enabled = false; //Disable The Image Component
                uiParent.transform.Find("Bar " + (i + 1)).GetChild(0).GetComponent<Image>().sprite = null; //Remove The Sprite
            }

            if (iSO.primaryIndex == i && iSO.currentWeapon == 1)
            {
                uiParent.transform.Find("Bar " + (i + 1)).GetComponent<Image>().sprite = iSO.blueGrid;
            }
            if (iSO.secondaryIndex == i && iSO.currentWeapon == 2)
            {
                uiParent.transform.Find("Bar " + (i + 1)).GetComponent<Image>().sprite = iSO.blueGrid;
            }
            else
            {
                //uiParent.transform.Find("Bar " + (i + 1)).transform.localScale = new Vector2(1f, 1f);
            }
        }

        if (iSO.primary != null && iSO.myInventory[iSO.primaryIndex] != null) //If Primary Equipped Item Exists & If Currently Equipped Item
        { //Exists In The Invetory.
            bar1.GetChild(0).GetComponent<Image>().enabled = true;
            bar1.GetChild(0).GetComponent<Image>().sprite = iSO.myInventory[iSO.primaryIndex].GetComponent<ItemInfo>().GetUISprite();
        }
        else
        {
            bar1.GetChild(0).GetComponent<Image>().enabled = false;
            bar1.GetChild(0).GetComponent<Image>().sprite = null;
        }

        if (iSO.secondary != null && iSO.myInventory[iSO.secondaryIndex] != null) //If Secondary Bar Has Something And The invetory Position Exists.
        {
            bar2.GetChild(0).GetComponent<Image>().enabled = true;
            bar2.GetChild(0).GetComponent<Image>().sprite = iSO.myInventory[iSO.secondaryIndex].GetComponent<ItemInfo>().GetUISprite();
        }
        else
        {
            bar2.GetChild(0).GetComponent<Image>().enabled = false;
            bar2.GetChild(0).GetComponent<Image>().sprite = null;
        }

        //if (myInventory.currentWeapon == 1)
        //{
        //    bar1.localScale = new Vector2(1.5f, 1.5f);
        //    bar2.localScale = new Vector2(1f, 1f);

        //    if (toolbarParent.transform.GetChild(1).transform.name != bar1.name)
        //    {
        //        bar1.SetAsLastSibling();
        //    }

        //    bar1.GetComponent<Image>().sprite = myInventory.blueGrid;
        //    bar2.GetComponent<Image>().sprite = myInventory.greyGrid;
        //}
        //else if (myInventory.currentWeapon == 2)
        //{
        //    bar1.localScale = new Vector2(1f, 1f);
        //    bar2.localScale = new Vector2(1.5f, 1.5f);

        //    if (toolbarParent.transform.GetChild(1).transform.name != bar2.name)
        //    {
        //        bar2.SetAsLastSibling();
        //    }

        //    bar1.GetComponent<Image>().sprite = myInventory.greyGrid;
        //    bar2.GetComponent<Image>().sprite = myInventory.blueGrid;
        //}

        ManageBars(bar1, bar2, 1);
        ManageBars(bar2, bar1, 2);
    }

    void ManageBars(Transform firstBar, Transform secondBar, int number)
    {
        if(iSO.currentWeapon == number)
        {
            firstBar.localScale = new Vector2(1.5f, 1.5f);
            secondBar.localScale = new Vector2(1f, 1f);

            if(toolbarParent.transform.GetChild(1).transform.name != firstBar.name)
            {
                firstBar.SetAsLastSibling();
            }

            firstBar.GetComponent<Image>().sprite = iSO.blueGrid;
            secondBar.GetComponent<Image>().sprite = iSO.greyGrid;
        }
    }
}
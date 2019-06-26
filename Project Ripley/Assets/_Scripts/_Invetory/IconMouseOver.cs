using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class IconMouseOver : MonoBehaviour
{
    public int currentIndex;
    public float iconSize;
    bool changed = false;
    InventorySO inventorySO;
    InventoryUIManager invetoryManager;
    Image myImage;
    Text infoText;

    void Awake()
    {
        currentIndex = transform.name[transform.name.Length - 1] - 49;
        if (currentIndex == -1)
        {
            currentIndex = 9;
        }

        inventorySO = transform.parent.parent.parent.GetComponent<InventoryUIManager>().myInventory;
        invetoryManager = transform.parent.parent.parent.GetComponent<InventoryUIManager>();

        myImage = GetComponent<Image>();
        myImage.sprite = inventorySO.greyGrid;

        infoText = invetoryManager.UIpanel.transform.GetChild(1).GetComponent<Text>();
    }

    void Update()
    {
        if(changed == false && inventorySO.mouseOverInvetory == false)
        {
            transform.localScale = new Vector2(1, 1);
            if (currentIndex == inventorySO.primaryIndex || currentIndex == inventorySO.secondaryIndex)
            {
                if (currentIndex == inventorySO.primaryIndex && inventorySO.currentWeapon == 1 
                    || inventorySO.currentWeapon == 2 && currentIndex == inventorySO.secondaryIndex)
                {
                    transform.localScale = new Vector2(1.5f, 1.5f);
                    transform.SetAsLastSibling();
                }
            }
        }
        else
        {
            if(changed == false)
            {
                transform.localScale = new Vector2(1, 1);
            }
        }
        if (currentIndex == inventorySO.primaryIndex || currentIndex == inventorySO.secondaryIndex)
        {
            myImage.sprite = inventorySO.blueGrid;
            //GetComponent<Image>().color = Color.cyan;
        }
        else
        {
            //GetComponent<Image>().color = Color.white;
            if(changed == false)
            {
                myImage.sprite = inventorySO.greyGrid;
            }
        }
    }

    void OnMouseOver()
    {
        if (changed == false)
        {
            Vector3 scale = transform.localScale;
            scale.x = iconSize;
            scale.y = iconSize;
            transform.localScale = scale;

            transform.SetAsLastSibling();
            changed = true;

            invetoryManager.UIpanel.SetActive(true);
            if(inventorySO.myInventory[currentIndex] != null)
            {
                infoText.text = inventorySO.GetItemInfoText(currentIndex);
            }
            else
            {
                infoText.text = "Just Hands, You Use Them To Fight";
            }
        }

        if (Input.GetKeyDown(KeyCode.Alpha1))
        {
            inventorySO.MoveToToolbar(1, currentIndex);
        }
        else if (Input.GetKeyDown(KeyCode.Alpha2))
        {
            inventorySO.MoveToToolbar(2, currentIndex);
        }

        if(currentIndex != inventorySO.primaryIndex && currentIndex != inventorySO.secondaryIndex)
        {
            GetComponent<Image>().sprite = inventorySO.greenGrid;
        }
        inventorySO.MouseOverInvetoryInfo(currentIndex);
        inventorySO.mouseOverInvetory = true;

        
        if (Input.GetKeyDown(KeyCode.Q) && inventorySO.availableToDrop == true && !inventorySO.GetLootingMode())
        {
            inventorySO.RemoveItem(currentIndex);
        }
    }

    void OnMouseExit()
    {
        if (changed == true)
        {
            Vector3 scale = transform.localScale;
            scale.x = 1;
            scale.y = 1;
            transform.localScale = scale;

            transform.SetSiblingIndex(currentIndex);
            changed = false;

            invetoryManager.UIpanel.SetActive(false);
        }

		myImage.sprite = inventorySO.greyGrid;
        inventorySO.mouseOverInvetory = false;
    }
}
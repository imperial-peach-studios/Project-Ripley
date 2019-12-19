using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class IconMouseOver : MonoBehaviour
{
    public int currentIndex;
    public float iconSize;
    bool changed = false;
    InventorySO iSO;
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

        iSO = transform.parent.parent.parent.GetComponent<InventoryUIManager>().iSO;
        invetoryManager = transform.parent.parent.parent.GetComponent<InventoryUIManager>();

        myImage = GetComponent<Image>();
        myImage.sprite = iSO.greyGrid;

        infoText = invetoryManager.UIpanel.transform.GetChild(1).GetComponent<Text>();
    }

    void Update()
    {
        if(changed == false && iSO.mouseOverInvetory == false)
        {
            transform.localScale = new Vector2(1, 1);
            if (currentIndex == iSO.primaryIndex || currentIndex == iSO.secondaryIndex)
            {
                if ((currentIndex == iSO.primaryIndex && iSO.currentWeapon == 1 )
                    || (iSO.currentWeapon == 2 && currentIndex == iSO.secondaryIndex))
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
        if (currentIndex == iSO.primaryIndex || currentIndex == iSO.secondaryIndex)
        {
            myImage.sprite = iSO.blueGrid;
            //GetComponent<Image>().color = Color.cyan;
        }
        else
        {
            //GetComponent<Image>().color = Color.white;
            if(changed == false)
            {
                myImage.sprite = iSO.greyGrid;
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
            if(iSO.myInventory[currentIndex] != null)
            {
                //infoText.text = iSO.GetItemInfoText(currentIndex);
            }
            else
            {
                infoText.text = "Just Hands, You Use Them To Fight";
            }
        }

        if (Input.GetKeyDown(KeyCode.Alpha1))
        {
            iSO.MoveToToolbar(1, currentIndex);
        }
        else if (Input.GetKeyDown(KeyCode.Alpha2))
        {
            iSO.MoveToToolbar(2, currentIndex);
        }

        if(currentIndex != iSO.primaryIndex && currentIndex != iSO.secondaryIndex)
        {
            GetComponent<Image>().sprite = iSO.greenGrid;
        }
        iSO.MouseOverInvetoryInfo(currentIndex);
        iSO.mouseOverInvetory = true;

        
        if (Input.GetKeyDown(KeyCode.Q) && iSO.availableToDrop == true && !iSO.GetLootingMode())
        {
            iSO.RemoveItem(currentIndex);
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

		myImage.sprite = iSO.greyGrid;
        iSO.mouseOverInvetory = false;
    }
}
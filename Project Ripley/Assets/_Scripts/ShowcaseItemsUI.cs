using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class ShowcaseItemsUI : MonoBehaviour
{
    public GameObject linker;
    public List<Image> images = new List<Image>();
    public int currentIcon = -1;
    public Sprite greyIcon, greenIcon;
    InventoryUIManager invetory;
    List<GameObject> items = new List<GameObject>();
    Image myImage;
    LootMouseOver lootMouseOver;

    void Start()
    {
        invetory = transform.parent.GetChild(0).GetComponent<InventoryUIManager>();
        myImage = transform.GetChild(0).GetComponent<Image>();
        lootMouseOver = myImage.GetComponent<LootMouseOver>();

        InteractionHandler.OnShow += OnShow;
        InteractionHandler.OnExit += OnExit;
    }

    public void SetCurrentIconID(int currentID)
    {
        currentIcon = currentID;
        if(linker != null)
        {
            linker.GetComponent<InteractionGiver>().SetIconIndex(currentID);
        }
    }

    public int GetCurrentIconID()
    {
        return currentIcon;
    }

    //public void Enable(Vector3 position, PickUpItem pickUp)
    //{
    //    linker = pickUp.gameObject;
    //    int myCount = pickUp.items.Count;

    //    if (myCount == 0) //If Pick Up Has No Items
    //    {
    //        for (int i = 0; i < images.Count; i++)
    //        {
    //            images[i].gameObject.SetActive(false);
    //            //images[i].transform.GetChild(0).GetComponent<Image>().sprite = null;
    //        }
    //    }
    //    else
    //    {
    //        transform.position = position;
    //        for(int j = 0; j < pickUp.items.Count; j++)
    //        {
    //            pickUp.items[j].GetComponent<ItemInfo>().UpdateInfo();
    //        }

    //        for (int i = 0; i < images.Count; i++)
    //        {
    //            Debug.Log("New " + i);
    //            int positionX = 0;
    //            int positionAdd = 0;

    //            if (myCount == 1)
    //            {
    //                positionX = 0;
    //            }
    //            else if (myCount == 2)
    //            {
    //                positionX = -30;
    //                positionAdd = 60;
    //            }
    //            else if (myCount == 3)
    //            {
    //                positionX = -60;
    //                positionAdd = 60;
    //            }
    //            else if (myCount == 4)
    //            {
    //                positionX = -90;
    //                positionAdd = 60;
    //            }
    //            else if (myCount == 5)
    //            {
    //                positionX = -120;
    //                positionAdd = 60;
    //            }

    //            images[i].transform.localPosition = new Vector2(positionX + positionAdd * i, 0f);


    //            if (i < myCount)
    //            {
    //                images[i].gameObject.SetActive(true);
    //                //Debug.Log("I " + i + " And " + myCount);
    //            }
    //            else if (i >= myCount)
    //            {
    //                images[i].gameObject.SetActive(false);
    //                //Debug.Log("I " + i + " And " + myCount + " deleated");
    //                break;
    //            }

    //            Image child = images[i].transform.GetChild(0).GetComponent<Image>();
    //            if (images[i].GetComponent<LootMouseOver>().currentGameObject == null ||
    //                    images[i].GetComponent<LootMouseOver>().currentGameObject != pickUp.items[i])
    //            {
    //                images[i].GetComponent<LootMouseOver>().SetCurrentObject(pickUp.items[i]);
    //                currentIcon = -1;
    //                images[i].GetComponent<Image>().sprite = greyIcon;
    //            }
    //            else
    //            {
    //                child.sprite = images[i].GetComponent<LootMouseOver>().currentGameObject.GetComponent<ItemInfo>().GetUISprite();
    //            }

    //            if(i == currentIcon)
    //            {
    //                images[i].GetComponent<Image>().sprite = greenIcon;
    //            }
    //            else
    //            {
    //                images[i].GetComponent<Image>().sprite = greyIcon;
    //            }

    //            // images[i].transform.GetChild(0).GetComponent<Image>().sprite = pickUp.items[i].GetComponent<ItemInfo>().GetUISprite();

    //            //Debug.Log(pickUp.items[i].GetComponent<ItemInfo>().GetUISprite());
    //        }
    //    }
    //}

    public void OnShow(Vector3 position, InteractionGiver iGiver)
    {
        if(iGiver.enabled)
        {
            //iGiver.GetItem().GetComponent<ItemInfo>().UpdateInfo();

            linker = iGiver.gameObject;
            transform.position = position;

            myImage.transform.localPosition = new Vector2(0f, 0f);

            Image child = myImage.transform.GetChild(0).GetComponent<Image>();

            if (lootMouseOver.currentGameObject == null ||
                    lootMouseOver.currentGameObject != iGiver.GetItem())
            {
                lootMouseOver.SetCurrentObject(iGiver.GetItem());
                currentIcon = -1;
            }
            //child.sprite = lootMouseOver.currentGameObject.GetComponent<ItemInfo>().GetUISprite();

            if (currentIcon == 0)
            {
                myImage.sprite = greenIcon;
            }
            else
            {
                myImage.sprite = greyIcon;
            }

            myImage.gameObject.SetActive(true);
        }
    }

    //public void Disable(Vector3 position, PickUpItem pickUp)
    //{
    //    for (int i = 0; i < images.Count; i++)
    //    {
    //        images[i].gameObject.SetActive(false);
    //        currentIcon = -1;
    //    }
    //}
    public void OnExit(Vector3 position, InteractionGiver iGiver)
    {
        myImage.gameObject.SetActive(false);
    }
}
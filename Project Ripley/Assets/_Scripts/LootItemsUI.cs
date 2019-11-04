using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class LootItemsUI : MonoBehaviour
{
    private Image barImage, childImage;
    Transform parentTransform;

    List<Sprite> itemSprites = new List<Sprite>();
    
    void Awake()
    {
        barImage = transform.GetChild(0).GetComponent<Image>();
        childImage = barImage.transform.GetChild(0).GetComponent<Image>();

        parentTransform = transform.parent;

        InteractReceiver.OnStartLoot += OnStart;
        InteractReceiver.OnUpdateLoot += OnShow;
        InteractReceiver.OnExitLoot += OnExit;
    }

    void OnStart(GameObject item, Vector3 position)
    {
        var sprite = item.GetComponent<PickUpGiver>().GetItem().GetComponent<ItemInfo>().GetUISprite();

        childImage.sprite = sprite;
        transform.position = position;
        barImage.transform.localPosition = new Vector2(0f, 0f);

        barImage.gameObject.SetActive(true);
    }

    void OnShow(GameObject item, Vector3 position)
    {
        var sprite = item.GetComponent<PickUpGiver>().GetItem();

        if (sprite == null)
        {
            childImage.sprite = null;
            barImage.gameObject.SetActive(false);
            return;
        }
        barImage.gameObject.SetActive(true);
    }
    
    void OnExit(GameObject item, Vector3 position)
    {
        barImage.gameObject.SetActive(false);
    }
}
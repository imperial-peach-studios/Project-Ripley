using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class ItemPopupUI : MonoBehaviour
{
    [SerializeField] Transform myBar;
    [SerializeField] float myOffset;

    void Awake()
    {
        ItemShow.OnItemShowActivated += ShowItem;
        ItemShow.OnItemShowDeactivated += DisableItem;

        myBar.gameObject.SetActive(false);
    }
    void OnDestroy()
    {
        ItemShow.OnItemShowActivated -= ShowItem;
        ItemShow.OnItemShowDeactivated -= DisableItem;
    }

    void ShowItem(Sprite aSprite, Vector3 aPosition)
    {
        transform.position = aPosition + Vector3.up * myOffset;
        myBar.GetChild(0).GetComponent<Image>().sprite = aSprite;
        myBar.gameObject.SetActive(true);
    }
    void DisableItem()
    {
        myBar.gameObject.SetActive(false);
    }
}
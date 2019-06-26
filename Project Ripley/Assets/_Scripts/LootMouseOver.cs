using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class LootMouseOver : MonoBehaviour
{
    public int currentIndex;
    public float iconMaxSize;
    public float iconMinSize;
    bool changed = false;
    public Sprite greyGrid, greenGrid;
    Image myImage;
    bool clicked = false;
    ShowcaseItemsUI showcase;
    public GameObject currentGameObject;

    void Awake()
    {
        currentIndex = transform.name[transform.name.Length - 1] - 49;
        if(currentIndex == -1)
        {
            currentIndex = transform.childCount - 1;
        }
        myImage = GetComponent<Image>();
        myImage.sprite = greyGrid;
        showcase = transform.parent.GetComponent<ShowcaseItemsUI>();
    }

    void Update()
    {
        if(changed == false)
        {
            //transform.localScale = new Vector2(iconMinSize, iconMinSize);
        }

    }

    void OnMouseDown()
    {
        if (showcase.GetCurrentIconID() != currentIndex)
        {
            showcase.SetCurrentIconID(currentIndex);
        }
        else
        {
            showcase.SetCurrentIconID(-1);
        }
    }

    void OnMouseOver()
    {
        if(changed == false)
        {
            Vector3 scale = transform.localScale;
            scale.x = iconMaxSize;
            scale.y = iconMaxSize;
            transform.localScale = scale;

            transform.SetAsLastSibling();
            changed = true;
        }
    }

    void OnDisable()
    {
        showcase.SetCurrentIconID(-1);
    }

    void OnMouseExit()
    {
        if(changed == true)
        {
            Vector3 scale = transform.localScale;
            scale.x = iconMinSize;
            scale.y = iconMinSize;
            transform.localScale = scale;

            transform.SetSiblingIndex(currentIndex);
            changed = false;
        }
    }

    public void SetCurrentObject(GameObject gameobject)
    {
        currentGameObject = gameobject;
    }
}
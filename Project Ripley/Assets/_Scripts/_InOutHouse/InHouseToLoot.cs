using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class InHouseToLoot : MonoBehaviour
{
    [SerializeField] InOutHouse inOutHouse;
    ItemInfo i;
    ItemInfo.TypeOfItem oldType;
    bool once = false;

    void Start()
    {
        i = GetComponent<ItemInfo>();
    }

    void Update()
    {
        if(inOutHouse.inside)
        {
            if(once)
            {
                i.enabled = true;
                //i.typeOfItem = oldType;
                once = false;
            }
        }
        else if (!inOutHouse.inside)
        {
            if (once == false)
            {
                i.enabled = false;
                //oldType = i.typeOfItem;
                //i.typeOfItem = ItemInfo.TypeOfItem.None;
                once = true;
            }
        }
    }

    void OnGUI()
    {
        if(GUILayout.Button("Show ItemInfo"))
        {
            if (once == true)
            {
                i.typeOfItem = oldType;
            }
        }
    }
}
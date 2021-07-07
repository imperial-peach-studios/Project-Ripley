using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEditor;

[CustomEditor(typeof(ItemFactory))]
public class ItemFactoryEditor : Editor
{
    public override void OnInspectorGUI()
    {
        ItemFactory itemFactory = (ItemFactory)target;

        if (itemFactory.myUpdate == true)
        {
            itemFactory.Initialize();
            itemFactory.myUpdate = false;
        }

        DrawDefaultInspector();
    }
}
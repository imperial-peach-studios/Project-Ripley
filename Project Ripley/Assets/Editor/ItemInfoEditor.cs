using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEditor;

[CustomEditor(typeof(ItemInfo))]
public class ItemInfoEditor : Editor
{
    SerializedProperty typeOfItem;

    SerializedProperty melee;
    SerializedProperty range;
    SerializedProperty consumable;
    SerializedProperty looted;

    void OnEnable()
    {
        typeOfItem = serializedObject.FindProperty("typeOfItem");
        melee = serializedObject.FindProperty("meele");
        range = serializedObject.FindProperty("raange");
        consumable = serializedObject.FindProperty("consumable");
        looted = serializedObject.FindProperty("lootItem");
    }

    public override void OnInspectorGUI()
    {
        ItemInfo p = (ItemInfo)target;

        EditorGUILayout.PropertyField(typeOfItem, new GUIContent("Type Of Item"));

        //if (p.typeOfItem != ItemInfo.TypeOfItem.Consumable && p.typeOfItem != ItemInfo.TypeOfItem.None)
        //{
        //    EditorGUILayout.Space();
        //}
        
        if (p.typeOfItem == ItemInfo.TypeOfItem.Melee)
        {
            p.meleeIconIndex = EditorGUILayout.Popup(p.meleeIconIndex, new string[] { p.iconNames[0], p.iconNames[1], p.iconNames[2], p.iconNames[3]});

            EditorGUILayout.IntField(p.meleeIconIndex);

            EditorGUILayout.Space();

            EditorGUILayout.PropertyField(melee, new GUIContent("Melee Class"), true);
        }
        else if (p.typeOfItem == ItemInfo.TypeOfItem.Range)
        {
            p.rangeIconIndex = EditorGUILayout.Popup(p.rangeIconIndex, new string[] { p.iconNames[4], p.iconNames[5], p.iconNames[6], p.iconNames[7]});

            EditorGUILayout.IntField(p.rangeIconIndex + 4);

            EditorGUILayout.Space();

            EditorGUILayout.PropertyField(range, new GUIContent("Range Class"), true);

        }
        else if (p.typeOfItem == ItemInfo.TypeOfItem.Consumable)
        {
            p.consumableIndex = EditorGUILayout.Popup(p.consumableIndex, new string[] { p.iconNames[8], p.iconNames[9], p.iconNames[10], p.iconNames[11]});

            EditorGUILayout.IntField(p.consumableIndex + 8);

            EditorGUILayout.Space();

            EditorGUILayout.PropertyField(consumable, new GUIContent("Consumable Class"), true);
        }

        EditorGUILayout.PropertyField(looted, new GUIContent("Object Loot"));

        serializedObject.ApplyModifiedProperties();
    }
}
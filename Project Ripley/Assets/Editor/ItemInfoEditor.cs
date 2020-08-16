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

        p.UISprite = (Sprite)EditorGUILayout.ObjectField(p.UISprite, typeof(Sprite), true);

        if (p.typeOfItem == ItemInfo.TypeOfItem.Melee)
        {
            EditorGUILayout.Space();

            EditorGUILayout.PropertyField(melee, new GUIContent("Melee Class"), true);
        }
        else if (p.typeOfItem == ItemInfo.TypeOfItem.Range)
        {
            EditorGUILayout.Space();

            EditorGUILayout.PropertyField(range, new GUIContent("Range Class"), true);

        }
        else if (p.typeOfItem == ItemInfo.TypeOfItem.Consumable)
        {
            EditorGUILayout.Space();

            EditorGUILayout.PropertyField(consumable, new GUIContent("Consumable Class"), true);
        }

        EditorGUILayout.PropertyField(looted, new GUIContent("Object Loot"));

        serializedObject.ApplyModifiedProperties();
    }
}
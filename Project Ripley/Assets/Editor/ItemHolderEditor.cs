using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEditor;

[CustomEditor(typeof(ItemHolder))]
[System.Serializable]
public class ItemHolderEditor : Editor
{
    SerializedProperty myItemType;

    SerializedProperty myMelee;
    SerializedProperty myRange;
    SerializedProperty myConsumable;

    void OnEnable()
    {
        myItemType = serializedObject.FindProperty("myItemType");

        myMelee = serializedObject.FindProperty("myMelee");
        myRange = serializedObject.FindProperty("myRange");
        myConsumable = serializedObject.FindProperty("myConsumable");
    }

    public override void OnInspectorGUI()
    {
        ItemHolder p = (ItemHolder)target;

        EditorGUILayout.PropertyField(myItemType, new GUIContent("Item:"));

        if ((int)p.myItemType < 2)
        {
            EditorGUILayout.Space();
            EditorGUILayout.PropertyField(myMelee, new GUIContent("Melee Settings"), true);
        }
        else if ((int)p.myItemType < 4)
        {
            EditorGUILayout.Space();
            EditorGUILayout.PropertyField(myRange, new GUIContent("Range Settings"), true);
        }
        else if ((int)p.myItemType < 5)
        {
            EditorGUILayout.Space();
            EditorGUILayout.PropertyField(myConsumable, new GUIContent("Consumable Settings"), true);
        }

        serializedObject.ApplyModifiedProperties();
    }
}
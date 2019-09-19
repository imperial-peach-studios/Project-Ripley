using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEditor;

[CustomEditor(typeof(ItemInfo))]
public class ItemInfoEditor : Editor
{
    SerializedProperty typeOfItem;
    SerializedProperty m_meleeSO;
    SerializedProperty m_gunSO;

    void OnEnable()
    {
        typeOfItem = serializedObject.FindProperty("typeOfItem");
        m_meleeSO = serializedObject.FindProperty("melee");
        m_gunSO = serializedObject.FindProperty("gun");
    }

    public override void OnInspectorGUI()
    {
        ItemInfo p = (ItemInfo)target;

        EditorGUILayout.PropertyField(typeOfItem, new GUIContent("Type Of Item"));

        if(p.typeOfItem == ItemInfo.TypeOfItem.Melee)
        {
            EditorGUILayout.PropertyField(m_meleeSO, new GUIContent("Melee SO"));

            if (p.gun != null)
            {
                p.gun = null;
            }
        }
        else if(p.typeOfItem == ItemInfo.TypeOfItem.Range)
        {
            EditorGUILayout.PropertyField(m_gunSO, new GUIContent("Gun SO"));

            if (p.melee != null)
            {
                p.melee = null;
            }
        }
        else if(p.typeOfItem == ItemInfo.TypeOfItem.Consumable)
        {

        }
        else if(p.typeOfItem == ItemInfo.TypeOfItem.None)
        {
            if (p.melee != null)
            {
                p.melee = null;
            }
            if(p.gun != null)
            {
                p.gun = null;
            }
        }
        serializedObject.ApplyModifiedProperties();
    }
}
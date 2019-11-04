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

    SerializedProperty uiIcon;
    SerializedProperty animationID;

    SerializedProperty startDurability;
    SerializedProperty durability;
    SerializedProperty durabilityDecrease;
    SerializedProperty damage;
    SerializedProperty knockBack;
    SerializedProperty knockLength;
    SerializedProperty stunLength;

    SerializedProperty bulletObject;
    SerializedProperty bullet;
    SerializedProperty numberOfBulletsFired;
    SerializedProperty firingRate;
    SerializedProperty spreadFactor;

    void OnEnable()
    {
        typeOfItem = serializedObject.FindProperty("typeOfItem");
        m_meleeSO = serializedObject.FindProperty("melee");
        m_gunSO = serializedObject.FindProperty("gun");

        uiIcon = serializedObject.FindProperty("uiIcon");
        animationID = serializedObject.FindProperty("animationID");

        startDurability = serializedObject.FindProperty("startDurability");
        durability = serializedObject.FindProperty("durability");
        durabilityDecrease = serializedObject.FindProperty("durabilityDecrease");
        damage = serializedObject.FindProperty("damage");
        knockBack = serializedObject.FindProperty("knockBack");
        knockLength = serializedObject.FindProperty("knockLength");
        stunLength = serializedObject.FindProperty("stunLength");

        bulletObject = serializedObject.FindProperty("bulletObject");
        bullet = serializedObject.FindProperty("bullet");
        numberOfBulletsFired = serializedObject.FindProperty("numberOfBulletsFired");
        firingRate = serializedObject.FindProperty("firingRate");
        spreadFactor = serializedObject.FindProperty("spreadFactor");
    }

    public override void OnInspectorGUI()
    {
        ItemInfo p = (ItemInfo)target;

        EditorGUILayout.PropertyField(uiIcon, new GUIContent("UI Icon"));
        EditorGUILayout.PropertyField(animationID, new GUIContent("Animation ID"));
        EditorGUILayout.Space();
        EditorGUILayout.PropertyField(typeOfItem, new GUIContent("Type Of Item"));


        if(p.typeOfItem != ItemInfo.TypeOfItem.None)
        {
            EditorGUILayout.PropertyField(startDurability, new GUIContent("Start Durability"));
            EditorGUILayout.PropertyField(durability, new GUIContent("Durability"));
            EditorGUILayout.PropertyField(durabilityDecrease, new GUIContent("Durability Decrease"));
        }

        if (p.typeOfItem != ItemInfo.TypeOfItem.Consumable && p.typeOfItem != ItemInfo.TypeOfItem.None)
        {
            EditorGUILayout.Space();

            EditorGUILayout.PropertyField(damage, new GUIContent("Damage"));
            EditorGUILayout.PropertyField(knockBack, new GUIContent("Knock Back"));
            EditorGUILayout.PropertyField(knockLength, new GUIContent("Knock Length"));
            EditorGUILayout.PropertyField(stunLength, new GUIContent("Stun Length"));

        }

        if (p.typeOfItem == ItemInfo.TypeOfItem.Melee)
        {
            //EditorGUILayout.PropertyField(m_meleeSO, new GUIContent("Melee SO"));

            

            if (p.gun != null)
            {
                p.gun = null;
            }
        }
        else if(p.typeOfItem == ItemInfo.TypeOfItem.Range)
        {
            //EditorGUILayout.PropertyField(m_gunSO, new GUIContent("Gun SO"));
            EditorGUILayout.Space();
            EditorGUILayout.PropertyField(bulletObject, new GUIContent("Bullet Object"));
            EditorGUILayout.PropertyField(bullet, new GUIContent("Bullet"));
            EditorGUILayout.PropertyField(numberOfBulletsFired, new GUIContent("Number Of Bullets Fired"));
            EditorGUILayout.PropertyField(firingRate, new GUIContent("Firing Rate"));
            EditorGUILayout.PropertyField(spreadFactor, new GUIContent("Spread Factor"));

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
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu]
public class MeleeWeaponsSO : ScriptableObject
{
    public string weaponsName;
    [Header("UI Icon For Invetory Icon")]
    public Sprite uiIcon;
    public bool twoHanded = false;
    public int animationID;

    public float attackRate;
    public int damage;
    [Header("A Weapons Durability/How Long It Lasts Before Vanishing")]
    public float durability;
    [Header("How Much To Decrease Durability When Firing")]
    public float durabilityDecrease;
    public float knockBack;
    public float knockLength;
    public float stanLength; //How long they are stanned for

    public Vector2 CollisionBoxSize;
    public Vector2 pickUpBoxSize;

    public Vector2 attackCollisionSize;
    public Vector2 attackCollisionOffset;

    public string info;
}
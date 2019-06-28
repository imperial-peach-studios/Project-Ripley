using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[System.Serializable]
public class InventoryData
{
    [SerializeField] public List<GameObject> myInventory;
    [SerializeField] public GameObject primary, secondary;
    [SerializeField] public int currentWeapon;
}
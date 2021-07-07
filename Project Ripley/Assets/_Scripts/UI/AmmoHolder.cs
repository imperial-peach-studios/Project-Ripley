using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AmmoHolder : MonoBehaviour
{
    [SerializeField] ItemFactory.ItemType myItemAmmo;
    [SerializeField] int myAmmoAmountToAdd;

    public void AddAmmo()
    {
        Player.Instance.inventory.AddAmmo(myItemAmmo, myAmmoAmountToAdd);
        Destroy(gameObject);
    }
}

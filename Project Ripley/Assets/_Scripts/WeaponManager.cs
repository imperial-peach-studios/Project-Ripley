using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(ItemInfo))]
public class WeaponManager : MonoBehaviour
{
    public WeaponCreator weaponCreator;
    public GameObject bulletPoint;
    float firingTimer = 0;
    public float firingRate;
    public float damage;
    public float spreadFactor;

    void Update()
    {
        firingTimer += Time.deltaTime;
        if (Input.GetMouseButton(0) && firingTimer > firingRate)
        {
            weaponCreator.Fire(bulletPoint, spreadFactor, damage);
            firingTimer = 0;
        }
    }
}
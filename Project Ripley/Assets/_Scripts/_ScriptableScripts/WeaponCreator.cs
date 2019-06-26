using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu]
public class WeaponCreator : ScriptableObject
{
    public string weaponsName;
    [Header("UI Icon For Invetory Icon")]
    public Sprite uiIcon;
    [Header("The Weapons Bullet")]
    public GameObject weaponBullet;
    [Header("Weapons GameObject")]
    public GameObject gunObject;
    public bool twoHanded = false;

    [Header("Current Number Of Bullets")]
    public int bullet;
    [Header("Bullets Spawn Point")]
    public Transform bulletPoint;
    [Header("Weapons Max Bullets")]
    public int maxBullets;
    [Header("How Many Bullets To Add At A Time When When Reloading")]
    [Header("Example: Shotgun: Add 1 Bullet At A Time")]
    [Header("Rifle: Add 30 Bullets At Once")]
    public int totalBulletAdd;
    [Header("How Many Bullets Are Fired When Shooting")]
    public int numberOfBulletsFired;
    [Header("A Weapons Durability/How Long It Lasts Before Vanishing")]
    public float durability;
    [Header("How Much To Decrease Durability When Firing")]
    public float durabilityDecrease;

    public Vector2 CollisionBoxSize;
    public Vector2 pickUpBoxSize;

    public string info;

    public void Fire(GameObject bulletPoint, float spreadFactor, float damage)
    {
        durability -= durabilityDecrease;
        bullet -= 1;
        for (int i = 0; i < numberOfBulletsFired; i++) //Spawn Number Of Bullets
        {
            //GameObject newBullet = Instantiate(weapons[id].bullet, weapons[id].bulletPoint.position, weapons[id].bulletPoint.rotation) as GameObject;
            GameObject newBullet = Instantiate(weaponBullet, bulletPoint.transform.position, bulletPoint.transform.rotation) as GameObject;

            newBullet.GetComponent<BulletBehaviour>().SpreadFactor = spreadFactor;
            newBullet.GetComponent<BulletBehaviour>().Damage = damage;

            //BulletBehaviour newBulletBehaviour = newBullet.GetComponent<BulletBehaviour>();

        }
    }

    public void DecreaseBullets()
    {
        bullet -= 1;
    }

    public void AddBullet()
    {

    }
}
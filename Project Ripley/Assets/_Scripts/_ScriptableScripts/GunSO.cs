using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu]
public class GunSO : ScriptableObject
{
    public string weaponsName;
    [Header("UI Icon For Invetory Icon")]
    public Sprite uiIcon;
    [Header("The Weapons Bullet")]
    public GameObject weaponBullet;
    public bool twoHanded = false;
    public int animationID;

    [Header("Current Number Of Bullets")]
    public int bullet;
    [Header("Bullets Spawn Point")]
    public Transform bulletPoint;
    [Header("How Many Bullets Are Fired When Shooting")]
    public int numberOfBulletsFired;
    public float firingRate;
    public float damage;
    public float spreadFactor;
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
            GameObject newBullet = Instantiate(weaponBullet, bulletPoint.transform.position, bulletPoint.transform.rotation) as GameObject;

            newBullet.GetComponent<BulletBehaviour>().SpreadFactor = spreadFactor;
            newBullet.GetComponent<BulletBehaviour>().Damage = damage;
        }
    }

    public void DecreaseBullets()
    {
        bullet -= 1;
    }
}
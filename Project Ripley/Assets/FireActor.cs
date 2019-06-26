using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FireActor : MonoBehaviour
{
    GameObject bullet;
    [SerializeField] Transform bulletPoint;
    float damage;
    float fireRate;
    float fireSpreadFactor;
    float numberOfBulletsFired;

    float fireTimer = 0;

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        fireTimer += Time.deltaTime;

        if (Input.GetButton("Fire1") && fireTimer > fireRate)
        {
            for (int i = 0; i < numberOfBulletsFired; i++) //Spawn Number Of Bullets
            {
                GameObject newBullet = Instantiate(bullet, bulletPoint.transform.position, bulletPoint.transform.rotation) as GameObject;

                newBullet.GetComponent<BulletBehaviour>().SpreadFactor = fireSpreadFactor;
                newBullet.GetComponent<BulletBehaviour>().Damage = damage;
            }
            fireTimer = 0;
        }

        if (Input.GetButtonUp("Fire1"))
        {
            fireTimer = 0;
        }
    }

    public void GetFireInfo(GameObject bullet, float damage, float fireRate, float numberOfBulletsFired, float fireSpread)
    {
        this.bullet = bullet;
        //this.bulletPoint = bulletPoint;
        this.damage = damage;
        this.fireRate = fireRate;
        this.numberOfBulletsFired = numberOfBulletsFired;
        this.fireSpreadFactor = fireSpread;
    }
}
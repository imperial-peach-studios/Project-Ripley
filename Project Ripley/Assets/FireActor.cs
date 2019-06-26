using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FireActor : MonoBehaviour
{
    GameObject bullet;
    [SerializeField] Transform bulletPoint;
    [SerializeField] float yOffset;

    [SerializeField] bool currentlyKnocking;
    [SerializeField] bool currentlyStunning;
    GameObject enemyHit;
    bool giveDamage = false;
    float damage;
    float fireRate;
    float fireSpreadFactor;
    float numberOfBulletsFired;
    float knockBack;
    float knockBackLength;
    float stunLength;
    Animator anim;
    float fireTimer = 0;

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        Vector3 mousePosition = Camera.main.ScreenToWorldPoint(new Vector3(Input.mousePosition.x, Input.mousePosition.y, 10f));

        Vector3 shooPoint = new Vector3(bulletPoint.position.x, bulletPoint.position.y + yOffset, bulletPoint.position.z);

        Vector3 pointest = mousePosition - shooPoint;
        float pointAngle = Mathf.Atan2(pointest.y, pointest.x) * Mathf.Rad2Deg;
        bulletPoint.rotation = Quaternion.AngleAxis(pointAngle, Vector3.forward);

        fireTimer += Time.deltaTime;
        
        if (Input.GetButton("Fire1") || Input.GetButtonDown("Fire1"))
        {
            if (anim != null)
            {
                if (anim.GetCurrentAnimatorStateInfo(0).IsName("Hold & Click"))
                {
                    if (fireTimer > fireRate)
                    {
                        for (int i = 0; i < numberOfBulletsFired; i++) //Spawn Number Of Bullets
                        {
                            GameObject newBullet = Instantiate(bullet, shooPoint, bulletPoint.transform.rotation) as GameObject;

                            newBullet.GetComponent<BulletBehaviour>().SpreadFactor = fireSpreadFactor;
                            newBullet.GetComponent<BulletBehaviour>().Damage = damage;
                            newBullet.GetComponent<BulletBehaviour>().knockBack = currentlyKnocking;
                            newBullet.GetComponent<BulletBehaviour>().stun = currentlyStunning;
                            newBullet.GetComponent<BulletBehaviour>().knockBackPower = knockBack;
                            newBullet.GetComponent<BulletBehaviour>().knockBackLength = knockBackLength;
                            newBullet.GetComponent<BulletBehaviour>().stunLength = stunLength;
                        }
                        fireTimer = 0;
                    }
                }
            }
        }

        if (Input.GetButtonUp("Fire1"))
        {
            fireTimer = 0;
        }
    }

    public void GetFireInfo(GameObject bullet, float damage, float fireRate, float numberOfBulletsFired, float fireSpread, float knockBack, float knockLength, float stunLength, Animator anim)
    {
        this.bullet = bullet;
        //this.bulletPoint = bulletPoint;
        this.damage = damage;
        this.fireRate = fireRate;
        this.numberOfBulletsFired = numberOfBulletsFired;
        this.fireSpreadFactor = fireSpread;
        this.knockBack = knockBack;
        this.knockBackLength = knockLength;
        this.stunLength = stunLength;
        this.anim = anim;
    }
}
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RangeManager : MonoBehaviour
{
    GameObject bullet;
    [SerializeField] Transform myBulletPoint;
    [SerializeField] GameObject myBullet;
    [SerializeField] float yOffset;

    [SerializeField] bool currentlyKnocking;
    [SerializeField] bool currentlyStunning;
    GameObject enemyHit;
    bool giveDamage = false;
    float fireTimer = 0;
    [SerializeField] float fireRate;
    bool hasFired = false;
    private Animator myAnim;

    void Start()
    {
        myAnim = GetComponentInParent<Animator>();
    }

    void Update()
    {
        if (hasFired)
        {
            Range currentItem = Player.Instance.equipment.GetSelectedItem() as Range;

            Vector3 mousePosition = currentItem.GetLastClicked(); //MouseManager.Instance.GetMousePosition()
            Vector3 shooPoint = myBulletPoint.position + Vector3.up * yOffset;

            Vector3 pointest = mousePosition - shooPoint;
            float pointAngle = Mathf.Atan2(pointest.y, pointest.x) * Mathf.Rad2Deg;
            myBulletPoint.rotation = Quaternion.AngleAxis(pointAngle, Vector3.forward);

            for(int i = 0; i < currentItem.GetNumberOfBullets(); i++)
            {
                GameObject newBullet = Instantiate(myBullet, shooPoint, myBulletPoint.rotation) as GameObject;
            }

            hasFired = false;
        }
    }

    public void SetHasFired(bool fired)
    {
        hasFired = fired;
    }
}
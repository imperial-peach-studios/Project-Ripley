using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(ItemInfo))]
public class GunManager : MonoBehaviour
{
    public GunSO gunSO;
    //public GameObject bulletPoint;
    float firingTimer = 0;
    public float positionY;
    public GameObject child;
    //public float firingRate;
    //public float damage;
    //public float spreadFactor;
    
    void Update()
    {
        Vector3 mousePosition = Camera.main.ScreenToWorldPoint(new Vector3(Input.mousePosition.x, Input.mousePosition.y, 10f));
        
        Vector3 pointest = mousePosition - child.transform.position;
        float pointAngle = Mathf.Atan2(pointest.y, pointest.x) * Mathf.Rad2Deg;
        transform.rotation = Quaternion.AngleAxis(pointAngle, Vector3.forward);

        firingTimer += Time.deltaTime;
        if (Input.GetMouseButton(0) && firingTimer > gunSO.firingRate)
        {
            gunSO.Fire(gameObject, gunSO.spreadFactor, gunSO.damage);
            firingTimer = 0;
        } 
    }
}
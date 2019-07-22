using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class InteractReceiver : MonoBehaviour
{
    GameObject closestObject;
    [SerializeField] float radius;
    [SerializeField] LayerMask layersToCollide;
    [SerializeField] KeyCode pickUpKey;

    void Update()
    {
        RaycastHit2D[] circleHit = Physics2D.CircleCastAll(transform.position + new Vector3(0f, 0.5f, 0f), radius, Vector2.zero, 0f, layersToCollide);

        Vector3 newTransform = transform.position;
        float destination = Mathf.Infinity;

        foreach (RaycastHit2D r in circleHit)
        {
            Vector3 diff = r.transform.position - newTransform;
            float newDistance = diff.sqrMagnitude;
            if (destination > newDistance && newDistance >= 0)
            {
                destination = newDistance;
                closestObject = r.transform.gameObject;
            }
        }
        
        if (closestObject != null)
        {
            if (Vector3.Distance(transform.position, closestObject.transform.position) < radius)
            {
                if (Input.GetKeyDown(pickUpKey))
                {
                    closestObject.GetComponent<PickUpGiver>().TryToAddItemToInventory();
                }
            }
        }
    }
}
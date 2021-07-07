using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TypeOfObstacle : MonoBehaviour
{
    [SerializeField] bool fullHide = false;
    [SerializeField] LayerMask collideWithLayer;

    public bool IsPlayerCrouching(Vector2 target)
    {
        Vector2 direction = target - (Vector2)transform.position; //Calculate Direction Of Player
        float distance = Vector2.Distance(target, transform.position);
        Debug.DrawRay(transform.position, direction * distance, Color.yellow); //Draw Direction

        RaycastHit2D hit = (Physics2D.Raycast(transform.position, direction, distance, collideWithLayer));

        if(hit)
        {
            PlayerMovement pMove = hit.transform.gameObject.GetComponent<PlayerMovement>();

            //if(pMove.IsSneaking)
            //    return true;

            return true;
        }

        return false;
    }

    public bool GetIfFullHide()
    {
        return fullHide;
    }
}
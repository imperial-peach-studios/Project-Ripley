using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RaycastLookAt : MonoBehaviour
{
    public Vector2 rayDirection;
    public float rayLength;
    public Color rayColor;
    public LayerMask layerToCollide;
    public List<string> hitTags = new List<string>();
   
    //private InteractionGiver interactionGiver;

    void Start()
    {
        //if(GetComponent<InteractionGiver>() != null)
        //{
        //    interactionGiver = GetComponent<InteractionGiver>();
        //}
    }

    void Update()
    {
        RaycastHit2D[] hit = Physics2D.RaycastAll(transform.position, rayDirection, rayLength, layerToCollide);

        foreach(RaycastHit2D r in hit)
        {
            if(r == true)
            {
                for (int i = 0; i < hitTags.Count; i++)
                {
                    if (r.transform.name == hitTags[i])
                    {
                        //if(interactionGiver != null)
                        //{
                        //    interactionGiver.ActivateBySelf(r.transform.gameObject);
                        //}
                    }
                }
                if(hitTags.Count == 0)
                {
                    //interactionGiver.ActivateBySelf(r.transform.gameObject);
                }
            }
        }
    }
    private void OnDrawGizmos()
    {
        Gizmos.color = rayColor;
        Gizmos.DrawLine(transform.position, (Vector2)transform.position + rayDirection * rayLength);
    }
}
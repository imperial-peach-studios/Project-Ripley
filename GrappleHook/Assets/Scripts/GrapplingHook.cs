using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GrapplingHook : MonoBehaviour
{
    DistanceJoint2D joint;
    Vector3 targetPos;
    RaycastHit2D hit;
    public float maxDistance = 10;
    public LayerMask mask;
    public LineRenderer grapLine;

    private void Start()
    {
        joint = GetComponent<DistanceJoint2D>();
        grapLine.enabled = false;
    }

    private void Update()
    {
        if (Input.GetMouseButtonDown(1))
        {
            targetPos = Camera.main.ScreenToWorldPoint(Input.mousePosition);
            targetPos.z = 0;
            hit = Physics2D.Raycast(transform.position, targetPos - transform.position,maxDistance,mask);

            if (hit.collider != null && hit.collider.gameObject.GetComponent<Rigidbody2D>() != null)
            {
                joint.enabled = true;
                joint.connectedBody = hit.collider.gameObject.GetComponent<Rigidbody2D>();
                joint.distance = Vector2.Distance(transform.position, hit.point);
                joint.connectedAnchor = hit.point - new Vector2(hit.collider.transform.position.x, hit.collider.transform.position.y);

                grapLine.enabled = true;
                grapLine.SetPosition(0, transform.position);
                grapLine.SetPosition(1, hit.point);
                GM.canMove = false;
                GM.canDash = false;
                GM.canJump = false;
            }
        }

        if (Input.GetMouseButton(1))
        {
            grapLine.SetPosition(0, transform.position);
        }

        if (Input.GetMouseButtonUp(1))
        {
            joint.enabled = false;
            grapLine.enabled = false;
            GM.canMove = true;
            GM.canDash = true;
            GM.canJump = true;
        }
    }
}

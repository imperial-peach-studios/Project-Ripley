using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Pathfinding;

public class EnemySight : MonoBehaviour
{
    public float fieldOfViewAngle = 110f;
    public float playerInSightTimer = 0;
    public float playerForgetTimer;
    public float distanceOffset;
    public LayerMask notIgnoreCollision;
    private CircleCollider2D col;
    LineRenderer line;
    private EnemyInfo info;
    //[SerializeField] float offsetDegrees = 0;
    //[SerializeField] float degreesBetweenFieldOfView;
    //[SerializeField] bool previewDirection;
    public Directions directions;

    public enum Directions
    {
        Up,
        Right,
        Left,
        Down,
    }

    Patrol patrol;

    void Awake()
    {
        patrol = GetComponent<Patrol>();
        col = transform.Find("AI Circle Collision").GetComponent<CircleCollider2D>();
        line = transform.Find("Line Renderer").GetComponent<LineRenderer>();
        info = GetComponent<EnemyInfo>();
        info.SetCurrentDirection(new Vector2(0, -1));
    }
    
    void Update()
    {
        //DrawCone();
        //Quaternion leftLine = Quaternion.AngleAxis(fieldOfViewAngle + offsetDegrees, transform.forward);
        //Quaternion rightLine = Quaternion.AngleAxis(-fieldOfViewAngle + offsetDegrees, transform.forward);

        //Vector3 leftPos = transform.position;
        //leftPos = leftLine * leftPos;
        //leftPos = leftPos + transform.position;

        //Vector3 rightPos = transform.position;
        //rightPos = rightLine * rightPos;
        //rightPos = rightPos + transform.position;
        
        //Debug.DrawLine(transform.position, leftPos, Color.green);
        //Debug.DrawLine(transform.position, rightPos, Color.green);

        //degreesBetweenFieldOfView = (360 - (fieldOfViewAngle + fieldOfViewAngle));

        col.radius = GetComponent<EnemyEvent>().distanceOffset;

        //if (previewDirection)
        //{
        //    Vector3 pos;
        //    if(directions == Directions.Up)
        //    {

        //    }
        //    else if (directions == Directions.Up)
        //    {

        //    }
        //    else if (directions == Directions.Up)
        //    {

        //    }
        //    else if (directions == Directions.Up)
        //    {

        //    }
        //}
    }

    void DrawCone()
    {
        Vector3[] positions = new Vector3[3];

        float verticalPosition = 0f;
        float horizontalPosition = 0;
        if (info.GetCurrentDirection().y == 0)
        {
            verticalPosition = 0.8f;
            if (info.GetCurrentDirection().x == 1)
            {
                horizontalPosition = 0.8f;
            }
            else if (info.GetCurrentDirection().x == -1)
            {
                horizontalPosition = -0.8f;
            }
            positions[0] = transform.position + new Vector3(horizontalPosition, verticalPosition, 0f) * distanceOffset;
            positions[1] = transform.position + Vector3.zero;
            positions[2] = transform.position + new Vector3(horizontalPosition, -verticalPosition, 0f) * distanceOffset;
        }
        else if (info.GetCurrentDirection().x == 0)
        {
            horizontalPosition = 0.8f;
            if (info.GetCurrentDirection().y == 1)
            {
                verticalPosition = 0.8f;
            }
            else if (info.GetCurrentDirection().y == -1)
            {
                verticalPosition = -0.8f;
            }
            positions[0] = transform.position + new Vector3(horizontalPosition, verticalPosition, 0f) * distanceOffset;
            positions[1] = transform.position + Vector3.zero;
            positions[2] = transform.position + new Vector3(-horizontalPosition, verticalPosition, 0f) * distanceOffset;
        }
        Vector3 leftDirection = positions[0] - transform.position;
        Vector3 rightDirection = positions[2] - transform.position;
        RaycastHit2D hitLeft = Physics2D.Raycast(transform.position, leftDirection, distanceOffset, notIgnoreCollision);
        RaycastHit2D hitRight = Physics2D.Raycast(transform.position, rightDirection, distanceOffset, notIgnoreCollision);

        if (hitLeft == true)
        {
            if (hitLeft.transform.tag == "Wall")
            {
                positions[0] = hitLeft.point;
            }
        }
        if (hitRight == true)
        {
            if (hitRight.transform.tag == "Wall")
            {
                positions[2] = hitRight.point;
            }
        }

        line.SetPositions(positions);

        if (info.GetCurrentSight())
        {
            line.material.color = Color.red;
        }
        else
        {
            line.material.color = Color.green;
        }
    }

    //void OnTriggerStay2D(Collider2D other)
    //{§
    //    if (other.transform.tag == "Player")
    //    {
    //        playerInSight = false; //Set PlayerInSight To False

    //        Vector2 direction = other.transform.position - transform.position; //Calculate Direction Of Player
    //        Debug.DrawRay(transform.position, direction, Color.yellow); //Draw Direction

    //        float angle = Vector2.Angle(direction, enemyDirection); //Calculate Angle Between Direction and Enemys direction.
    //        if (angle <= fieldOfViewAngle * 0.5f) //Check If Angle Is Smaller Than FieldOfViewAngle
    //        {
    //            RaycastHit2D hit = (Physics2D.Raycast(transform.position, direction, col.radius, notIgnoreCollision));

    //            if (hit == true)
    //            {
    //                if (hit.transform.tag == "Player") //If our raycast hits the player
    //                {
    //                    playerInSight = true; //Set PlayerInSight To True

    //                    personalLastSighting = hit.transform.position; //Set PersonLastSighting To The Player Position.
    //                }
    //            }
    //        }
    //        Debug.DrawRay(transform.position, direction.normalized * col.radius, Color.magenta);
    //    }
    //}

    void OnTriggerStay2D(Collider2D other)
    {
        if (other.transform.tag == "Player")
        {
            bool previous = info.GetCurrentSight();
            info.SetCurrentSight(false); //Set PlayerInSight To False
            
            Vector2 direction = other.transform.position - transform.position; //Calculate Direction Of Player
            Debug.DrawRay(transform.position, direction, Color.yellow); //Draw Direction

            float angle = Vector2.Angle(direction, info.GetCurrentDirection()); //Calculate Angle Between Direction and Enemys direction.
            if (angle <= fieldOfViewAngle * 0.5f) //Check If Angle Is Smaller Than FieldOfViewAngle
            {
                RaycastHit2D hit = (Physics2D.Raycast(transform.position, direction, col.radius, notIgnoreCollision));

                if (hit)
                {
                    if (hit.transform.tag == "Player" && !PlayersMovementData.InsideASafeHouse) //If our raycast hits the player
                    {
                        info.SetSearchMode(false);
                        info.SetCurrentSight(true); //Set PlayerInSight To True

                        info.SetLastSight(hit.transform.position);
                    }
                    else if (hit.transform.gameObject.layer == LayerMask.NameToLayer("Obstacle"))
                    {
                        TypeOfObstacle tOO = hit.transform.gameObject.GetComponent<TypeOfObstacle>();
                        if (!tOO.GetIfFullHide())
                        {
                            bool canSeePlayer = tOO.IsPlayerCrouching(other.transform.position);

                            if (!canSeePlayer)
                            {
                                info.SetSearchMode(false);
                                info.SetCurrentSight(true); //Set PlayerInSight To True

                                info.SetLastSight(hit.transform.position);
                            }
                        }
                    }
                }
            }
            Debug.DrawRay(transform.position, direction.normalized * col.radius, Color.magenta);

            if (!info.GetCurrentSight() && previous == true)
            {
                info.SetSearchMode(true);
            }
        }
    }

    void OnTriggerExit2D(Collider2D other)
    {
        if (other.transform.tag == "Player")
        {
            info.SetCurrentSight(false);
        }
    }

    void OnDrawGizmos()
    {
        Gizmos.color = Color.red;
        if(info != null)
        {
            Gizmos.DrawWireSphere((Vector3)info.GetLastPosition(), 1f);

            if (info.GetCurrentSight() == true)
            {
                Gizmos.color = Color.red;
            }
            else
            {
                Gizmos.color = Color.green;
            }
        }

        float verticalPosition = 0f;
        float horizontalPosition = 0;
        if(info != null)
        {
            if (info.GetCurrentDirection().y == 0)
            {
                verticalPosition = 0.8f;
                if (info.GetCurrentDirection().x == 1)
                {
                    horizontalPosition = 0.8f;
                }
                else if (info.GetCurrentDirection().x == -1)
                {
                    horizontalPosition = -0.8f;
                }
                Gizmos.DrawLine(transform.position, transform.position + new Vector3(horizontalPosition, verticalPosition, 0f) * distanceOffset);
                Gizmos.DrawLine(transform.position, transform.position + new Vector3(horizontalPosition, -verticalPosition, 0f) * distanceOffset);
            }
            else if (info.GetCurrentDirection().x == 0)
            {
                horizontalPosition = 0.8f;
                if (info.GetCurrentDirection().y == 1)
                {
                    verticalPosition = 0.8f;
                }
                else if (info.GetCurrentDirection().y == -1)
                {
                    verticalPosition = -0.8f;
                }
                Gizmos.DrawLine(transform.position, transform.position + new Vector3(horizontalPosition, verticalPosition, 0f) * distanceOffset);
                Gizmos.DrawLine(transform.position, transform.position + new Vector3(-horizontalPosition, verticalPosition, 0f) * distanceOffset);
            }
        }
    }
}
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Pathfinding;

public class EnemyEvent : MonoBehaviour
{
    [SerializeField] EnemyInfo info;
    AIPath path;
    Patrol patrol;
    public float distanceOffset;
    [SerializeField] float waitTimer = 0;
    [SerializeField] float waitAmount;
    float playerInSightTimer = 0;
    [SerializeField] float playerInSightForgetTime;
    [SerializeField] Vector3 startPosition;

    void Awake()
    {
        GameData.OnSavePlayer += OnSave;
        GameData.OnLoadPlayer += OnLoad;
        GameData.BeforeLoadPlayer += OnBeforeLoad;
    }

    void Start()
    {
        path = GetComponent<AIPath>();
        patrol = GetComponent<Patrol>();

        startPosition = transform.position;
    }

    void OnSave()
    {
        if(this != null)
        {
            info.objectName = transform.name;
            if(this.gameObject != null)
            {
                info.SetPosition(startPosition);
            }
            GameData.aData.eData.OnSave(info);
        }
    }

    void OnLoad()
    {
        if(this != null)
        {
            if (!GameData.aData.eData.ExistsInTheSavedList(info))
            {
                Destroy(gameObject);
                return;
            }

            info = GameData.aData.eData.OnLoad(info);

            if(this.gameObject != null)
            {
                transform.position = info.GetPosition();
            }

            patrol.ResetIndex();
            waitTimer = 0;
            playerInSightTimer = 0;

            GameData.data.AllItemsFinishedLoading();
        }
    }

    void OnBeforeLoad()
    {
        if (this != null)
        {
            if (info == null)
            {
                Destroy(gameObject);
                return;
            }

            info.objectName = transform.name;

            GameData.aData.eData.ExistsInDestroyedList(info, true);
        }
    }

    public EnemyInfo GetEnemyInfo()
    {
        return info;
    }

    void Update()
    {
        if(info.GetKnockedDown() || info.GetStunned())
        {
            path.canMove = false;
            //path.Teleport(transform.position, true);
        }
        else if(info.HasAttacked())
        {
            path.canMove = false;
            path.Teleport(transform.position, true);
        }
        if (info.GetCurrentSight()) // If the Player Is Seen
        {
            patrol.enabled = false;
            waitTimer = 0;
            
            path.destination = info.GetLastPosition();
            info.SetCurrentDirection(CalculateDirectionNonDisplay(path.destination));
        }
        else if (info.GetSearchMode())
        {
            patrol.enabled = false;
            
            waitTimer += Time.deltaTime;

            if (waitTimer > waitAmount)
            {
                info.SetSearchMode(false);
                waitTimer = 0;
                //path.destination = info.GetLastPosition();
            }
            else
            {
                info.SetCurrentDirection(CalculateDirectionNonDisplay(info.GetLastPosition()));
            }

            if (info.HasHeardNoise())
            {
                waitTimer = 0;
                info.SetHeardNoise(false);
            }
        }
        else if (!info.GetCurrentSight()) //If The Player Isnt Seen
        {
            patrol.enabled = true;

            if (info.HasHeardNoise())
            {
                info.SetSearchMode(true);
                info.SetHeardNoise(false);
            }

            Vector3 newDirection = new Vector3(path.desiredVelocity.x, path.desiredVelocity.y);

            Vector3 newPosition = transform.position + newDirection;
            if (path.reachedEndOfPath == true)
            {
                info.SetCurrentDirection(CalculateDirectionNonDisplay(transform.position + (Vector3)info.GetCurrentDirection()));
            }
            else
            {
                info.SetCurrentDirection(CalculateDirectionNonDisplay(newPosition));
            }
        }

        if (!info.HasAttacked() && !info.GetStunned() && !info.GetKnockedDown())
        {
            path.canMove = true;
        }
    }

    Vector2 CalculateDirectionDisplay(Vector3 target)
    {
        float x = 0; //Create And Set Variable;
        float y = 0; //Create And Set Variable;

        Vector3 direction = target - transform.position; //Calculate Direction Of Target.
        direction.Normalize(); //Normalize Direction
        float dotProduct = Vector3.Dot(transform.right, direction); //Calculate Dot Product

        if (dotProduct > 0.8f) //If Dot Product Is On The Right Side
        {
            x = 1;
        }
        else if (dotProduct < -0.8f) //If Dot Product Is On The Left Side
        {
            x = -1;
        }
        else if (dotProduct > -0.8f && dotProduct < 0.8f) ////If Dot Product Is Up Or Down
        {
            if (target.y > transform.position.y) //If Dot Product Is On The Up Side
            {
                y = 1;
            }
            else if (target.y < transform.position.y) //If Dot Product Is On The Down Side
            {
                y = -1;
            }
        }
        float verticalPosition = 0f;
        float horizontalPosition = 0;
        if (y == 0)
        {
            verticalPosition = 0.8f;
            if (x == 1)
            {
                horizontalPosition = 0.8f;
            }
            else if (x == -1)
            {
                horizontalPosition = -0.8f;
            }
            Debug.DrawLine(transform.position, transform.position + new Vector3(horizontalPosition, verticalPosition, 0) * distanceOffset);
            Debug.DrawLine(transform.position, transform.position + new Vector3(horizontalPosition, -verticalPosition, 0) * distanceOffset);
        }
        else if (x == 0)
        {
            horizontalPosition = 0.8f;
            if (y == 1)
            {
                verticalPosition = 0.8f;
            }
            else if (y == -1)
            {
                verticalPosition = -0.8f;
            }
            Debug.DrawLine(transform.position, transform.position + new Vector3(horizontalPosition, verticalPosition, 0) * distanceOffset);
            Debug.DrawLine(transform.position, transform.position + new Vector3(-horizontalPosition, verticalPosition, 0) * distanceOffset);
        }
        return new Vector2(x, y);
    }

    Vector2 CalculateDirectionNonDisplay(Vector3 target)
    {
        float x = 0; //Create And Set Variable;
        float y = 0; //Create And Set Variable;

        Vector3 direction = target - transform.position; //Calculate Direction Of Target.
        direction.Normalize(); //Normalize Direction
        float dotProduct = Vector3.Dot(transform.right, direction); //Calculate Dot Product

        if (dotProduct > 0.8f) //If Dot Product Is On The Right Side
        {
            x = 1;
        }
        else if (dotProduct < -0.8f) //If Dot Product Is On The Left Side
        {
            x = -1;
        }
        else if (dotProduct > -0.8f && dotProduct < 0.8f) ////If Dot Product Is Up Or Down
        {
            if (target.y > transform.position.y) //If Dot Product Is On The Up Side
            {
                y = 1;
            }
            else if (target.y < transform.position.y) //If Dot Product Is On The Down Side
            {
                y = -1;
            }
        }

        float verticalPosition = 0f;
        float horizontalPosition = 0;
        if (y == 0)
        {
            verticalPosition = 0.8f;
            if (x == 1)
            {
                horizontalPosition = 0.8f;
            }
            else if (x == -1)
            {
                horizontalPosition = -0.8f;
            }
        }
        else if (x == 0)
        {
            horizontalPosition = 0.8f;
            if (y == 1)
            {
                verticalPosition = 0.8f;
            }
            else if (y == -1)
            {
                verticalPosition = -0.8f;
            }
        }
        return new Vector2(x, y);
    }

}
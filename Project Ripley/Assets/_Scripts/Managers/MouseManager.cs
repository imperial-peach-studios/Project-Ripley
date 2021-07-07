using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MouseManager : MonoBehaviour
{
    public static MouseManager Instance;
    [SerializeField] private Vector3 myMousePosition = Vector2.zero;
    [SerializeField] private float myDirectionSpot = 0.8f;
    private Camera myCamera;

    void Awake()
    {
        if (Instance == null)
        {
            // DontDestroyOnLoad(gameObject);
            Instance = this;
        }
        else if (Instance != this)
        {
            //Destroy(gameObject);
        }

        myCamera = GetComponent<Camera>();
    }

    void Update()
    {
        myMousePosition = myCamera.ScreenToWorldPoint(new Vector3(Input.mousePosition.x, Input.mousePosition.y, Mathf.Abs(myCamera.transform.position.z)));
    }

    public Vector2 GetMousePosition()
    {
        return myMousePosition;
    }

    public Vector2 CalculateDirectionNonDisplay(Transform aTarget, bool aDisplayDirection = false)
    {
        float x = 0; //Create And Set Variable;
        float y = 0; //Create And Set Variable;

        Vector3 position = (aTarget.position + new Vector3(0f, 0f, 0f));
        Vector3 direction = myMousePosition - position; //Calculate Direction Of Target.
        direction.Normalize(); //Normalize Direction

        float dotProduct = Vector3.Dot(aTarget.right, direction); //Calculate Dot Product
        if (dotProduct > myDirectionSpot) //If Dot Product Is On The Right Side
        {
            x = 1;
        }
        else if (dotProduct < -myDirectionSpot) //If Dot Product Is On The Left Side
        {
            x = -1;
        }
        else if (dotProduct > -myDirectionSpot && dotProduct < myDirectionSpot) ////If Dot Product Is Up Or Down
        {
            if (myMousePosition.y > position.y) //If Dot Product Is On The Up Side
            {
                y = 1;
            }
            else if (myMousePosition.y < position.y) //If Dot Product Is On The Down Side
            {
                y = -1;
            }
        }

        if (aDisplayDirection)
        {
            DisplayDirection(position, x, y);
        }


        return new Vector2(x, y);
    }
    public void DisplayDirection(Vector3 position, float x, float y)
    {
        float verticalPosition = 0f;
        float horizontalPosition = 0;
        if (y == 0)
        {
            verticalPosition = myDirectionSpot;
            horizontalPosition = myDirectionSpot * x;

            Debug.DrawLine(position, position + new Vector3(horizontalPosition, verticalPosition, 0) * 2f);
            Debug.DrawLine(position, position + new Vector3(horizontalPosition, -verticalPosition, 0) * 2f);
        }
        else if (x == 0)
        {
            horizontalPosition = myDirectionSpot;
            verticalPosition = myDirectionSpot * y;

            Debug.DrawLine(position, position + new Vector3(horizontalPosition, verticalPosition, 0) * 2f);
            Debug.DrawLine(position, position + new Vector3(-horizontalPosition, verticalPosition, 0) * 2f);
        }
    }
}
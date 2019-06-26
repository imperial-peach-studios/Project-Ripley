using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MouseDatabase
{
    public static Vector3 mousePosition = Vector2.zero;
    private static float changeDirectionValue = 0.8f;

    public static void UpdateMousePosition()
    {
        mousePosition = Camera.main.ScreenToWorldPoint(new Vector3(Input.mousePosition.x, Input.mousePosition.y, 10f));
    }

    public static Vector2 CalculateDirectionNonDisplay(Vector3 target, Transform myTransform)
    {
        float x = 0; //Create And Set Variable;
        float y = 0; //Create And Set Variable;

        Vector3 position = (myTransform.position + new Vector3(0f, 0f, 0f));
        Vector3 direction = target - position; //Calculate Direction Of Target.
        direction.Normalize(); //Normalize Direction

        float dotProduct = Vector3.Dot(myTransform.right, direction); //Calculate Dot Product
        if (dotProduct > changeDirectionValue) //If Dot Product Is On The Right Side
        {
            x = 1;
        }
        else if (dotProduct < -changeDirectionValue) //If Dot Product Is On The Left Side
        {
            x = -1;
        }
        else if (dotProduct > -changeDirectionValue && dotProduct < changeDirectionValue) ////If Dot Product Is Up Or Down
        {
            if (target.y > position.y) //If Dot Product Is On The Up Side
            {
                y = 1;
            }
            else if (target.y < position.y) //If Dot Product Is On The Down Side
            {
                y = -1;
            }
        }
        float verticalPosition = 0f;
        float horizontalPosition = 0;
        if (y == 0)
        {
            verticalPosition = changeDirectionValue;
            if (x == 1)
            {
                horizontalPosition = changeDirectionValue;
            }
            else if (x == -1)
            {
                horizontalPosition = -changeDirectionValue;
            }
            //Debug.DrawLine(position, position + new Vector3(horizontalPosition, verticalPosition, 0) * distanceOffset);
            //Debug.DrawLine(position, position + new Vector3(horizontalPosition, -verticalPosition, 0) * distanceOffset);
        }
        else if (x == 0)
        {
            horizontalPosition = changeDirectionValue;
            if (y == 1)
            {
                verticalPosition = changeDirectionValue;
            }
            else if (y == -1)
            {
                verticalPosition = -changeDirectionValue;
            }
            //Debug.DrawLine(position, position + new Vector3(horizontalPosition, verticalPosition, 0) * distanceOffset);
            //Debug.DrawLine(position, position + new Vector3(-horizontalPosition, verticalPosition, 0) * distanceOffset);
        }

        //else
        //{
        //    if(animHorizontal != 0)
        //    {
        //        x = animHorizontal;
        //    }
        //    else if(animVertical != 0)
        //    {
        //        y = animVertical;
        //    }
        //}
        return new Vector2(x, y);
    }

}
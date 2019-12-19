using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraBoundaryPoint : MonoBehaviour
{
    [SerializeField] bool xAxis, yAxis;

    [SerializeField] XAxisBoundaries xAxisBoundaries;
    [SerializeField] YAxisBoundaries yAxisBoundaries;

    [SerializeField] float offsetFromPointXp, offsetFromPointXn, offsetFromPointYp, offsetFromPointYn;

    public enum YAxisBoundaries
    {
        None,
        Both,
        Up,
        Down,
    }
    public enum XAxisBoundaries
    {
        None,
        Both,
        Left,
        Right,
    }

    public XAxisBoundaries GetXAxisBoundaries()
    {
        if (!xAxis)
        {
            return XAxisBoundaries.None;
        }
        return xAxisBoundaries;
    }
    public YAxisBoundaries GetYAxisBoundaries()
    {
        if (!yAxis)
        {
            return YAxisBoundaries.None;
        }
        return yAxisBoundaries;
    }

    public bool CheckXAxis(Vector3 cameraPosition, ref float minX, ref float maxX, string ax)
    {
        Vector3 pointP = transform.position + Vector3.right * offsetFromPointXp + Vector3.up * offsetFromPointYp;
        Vector3 pointN = transform.position + Vector3.right * offsetFromPointXn + Vector3.up * offsetFromPointYn;

        if (xAxis)
        {
            if(xAxisBoundaries == XAxisBoundaries.Right && ax == "Right")
            {
                if(cameraPosition.x >= pointN.x)
                {
                   maxX = pointN.x;
                   minX = cameraPosition.x - 1000;
                    return true;
                }
            }
            else if(xAxisBoundaries == XAxisBoundaries.Left && ax == "Left")
            {
                if (cameraPosition.x <= pointP.x)
                {
                    minX = pointP.x;
                    maxX = cameraPosition.x + 1000;
                    return true;
                }
            }
        }
        return false;
    }

    public bool CheckYAxis(Vector3 cameraPosition, ref float minY, ref float maxY)
    {
        Vector3 pointP = transform.position + Vector3.right * offsetFromPointXp + Vector3.up * offsetFromPointYp;
        Vector3 pointN = transform.position + Vector3.right * offsetFromPointXn + Vector3.up * offsetFromPointYn;

        if (yAxis)
        {
            if (yAxisBoundaries == YAxisBoundaries.Up)
            {
                if (cameraPosition.y >= pointN.y)
                {
                    maxY = pointN.y;
                    minY = cameraPosition.y - 1000;
                    return true;
                }
            }
            else if (yAxisBoundaries == YAxisBoundaries.Down)
            {
                if (cameraPosition.y <= pointP.y)
                {
                    minY = pointP.y;
                    maxY = cameraPosition.y + 1000;
                    return true;
                }
            }
        }

        return false;
    }
}
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[System.Serializable]
public class PlayerAttackSmall
{
    [SerializeField] string name;
    public string stateName = "";
    public string baseLayerName = "";
    public string perspectiveName = "";
    public string directionName = "";
    public string parameterName = "";
    public int parameterValue = 0;
    [SerializeField] int animationID;
    [SerializeField] Color collisionColor;
    [SerializeField] bool hideCollision;
    public int layerIndex;
    public int stateIndex;
    public int perspectiveIndex;
    public int parameterIndex;
    public int currentDirection = 0;
    public int currentPerspective = 0;
    public Vector2 boxOffset;
    public Vector2 boxSize;
    public Vector2 boxOffsetUp, boxOffsetRight, boxOffsetLeft, boxOffsetDown;
    public Vector2 boxSizeUp, boxSizeRight, boxSizeLeft, boxSizeDown;
    public Color enabledColor;
    public Color disabledColor;
    public float enableCollision;
    public float disableCollision;
    [Range(0, 5)]
    [SerializeField] int attackFrame;
    
    [Space]
    [SerializeField] List<SmallCollisions> comboColls = new List<SmallCollisions>();

    public void SetName(string newName)
    {
        name = newName;
    }

    public List<SmallCollisions> GetComboList()
    {
        return comboColls;
    }

    public Vector2 Offset()
    {
        Vector2 dir = Vector2.zero;

        if(perspectiveName == "Top Down")
        {
            switch(directionName)
            {
                case "Up":
                    dir = boxOffsetUp;
                    break;
                case "Right":
                    dir = boxOffsetRight;
                    break;
                case "Left":
                    dir = boxOffsetLeft;
                    break;
                case "Down":
                    dir = boxOffsetDown;
                    break;
            }
        }
        else
        {
            dir = boxOffset;
        }
        return dir;
    }

    public Vector2 Size()
    {
        Vector2 siz = Vector2.zero;

        if (perspectiveName == "Top Down")
        {
            switch (directionName)
            {
                case "Up":
                    siz = boxSizeUp;
                    break;
                case "Right":
                    siz = boxSizeRight;
                    break;
                case "Left":
                    siz = boxSizeLeft;
                    break;
                case "Down":
                    siz = boxSizeDown;
                    break;
            }
        }
        else
        {
            siz = boxOffset;
        }
        return siz;
    }

    public int GetAttackFrame()
    {
        return attackFrame;
    }

    public bool HideCollision()
    {
        return hideCollision;
    }

    //public Vector2 GetOffset()
    //{
    //    return offset;
    //}

    //public void SetOffset(Vector2 newOffset)
    //{
    //    offset = newOffset;
    //}

    public void SetAnimationID(int id)
    {
        animationID = id;
    }

    public int GetAnimationID()
    {
        return animationID;
    }

    //public Vector2 GetSize()
    //{
    //    return size;
    //}

    //public void SetSize(Vector2 newSize)
    //{
    //    size = newSize;
    //}

    public Color GetColor()
    {
        return collisionColor;
    }

}
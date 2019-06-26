using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[System.Serializable]
public class SmallCollisions
{
    [SerializeField] string name;

    [Range(0f, 1f)] [SerializeField] float collisionEnableAtTime;
    [Range(0f, 1f)] [SerializeField] float collisionDisableAtTime;
    [SerializeField] bool canCauseDamage = false;

    [SerializeField] bool controllOverAll;
    [SerializeField] bool FixOffset_X_BySize_X;
    [SerializeField] bool FixOffset_Y_BySize_Y;

    [SerializeField] CollisionAllInfo infoForAllDirections;
    [SerializeField] CollisionInfoAllDirection specificDirections;


    public void SetName(string newName)
    {
        name = newName;
    }

    public bool GetControlloverAll()
    {
        return controllOverAll;
    }

    public bool GetCanDamage()
    {
        return canCauseDamage;
    }

    public void SetCanDamage(bool v)
    {
        canCauseDamage = v;
    }

    public float GetCollisionEnable()
    {
        return collisionEnableAtTime;
    }
    public float GetCollisionDisable()
    {
        return collisionDisableAtTime;
    }

    public bool FixOffsetXToSizeX()
    {
        return FixOffset_X_BySize_X;
    }

    public bool FixOffsetYToSizeY()
    {
        return FixOffset_Y_BySize_Y;
    }

    public Vector2 GetOffset(Vector2? direction = null)
    {
        if (controllOverAll == true)
        {
            return infoForAllDirections.GetOffset();
        }
        else
        {
            if (direction == null)
            {
                direction = new Vector2(0, 0);
            }
            return specificDirections.GetDirectionOffset(direction.Value);
        }
    }

    public void SetOffset(Vector2 newOffset, Vector2? direction = null)
    {
        if(controllOverAll)
        { 
            infoForAllDirections.SetOffset(newOffset);
        }
        else
        {
            if(direction == null)
            {
                direction = new Vector2(0, 0);
            }
            specificDirections.SetDirectionOffset(newOffset, direction.Value);
        }
    }

    public Vector2 GetSize(Vector2? direction = null)
    {
        if(controllOverAll == true)
        {
            return infoForAllDirections.GetSize();
        }
        else
        {
            if(direction == null)
            {
                direction = new Vector2(0, 0);
            }
            return specificDirections.GetDirectionSize(direction.Value);
        }
    }

    public void SetSize(Vector2 newSize, Vector2? direction = null)
    {
        if (controllOverAll == true)
        {
            infoForAllDirections.SetSize(newSize);
        }
        else
        {
            if(direction == null)
            {
                direction = new Vector2(0, 0);
            }
            specificDirections.SetDirectionSize(newSize, direction.Value);
        }
    }
}

[System.Serializable]
public class CollisionAllInfo
{
    [SerializeField] Vector2 offset;
    [SerializeField] Vector2 size;

    public Vector2 GetOffset()
    {
        return offset;
    }

    public Vector2 GetSize()
    {
        return size;
    }

    public void SetOffset(Vector2 newOffset)
    {
        offset = newOffset;
    }

    public void SetSize(Vector2 newSize)
    {
        size = newSize;
    }
}

[System.Serializable]
public class SpecificCollisionInfo
{
    [SerializeField] Vector2 offset;
    [SerializeField] Vector2 size;

    public Vector2 GetOffset()
    {
        return offset;
    }

    public Vector2 GetSize()
    {
        return size;
    }

    public void SetOffset(Vector2 newOffset)
    {
        offset = newOffset;
    }

    public void SetSize(Vector2 newSize)
    {
        size = newSize;
    }
}

[System.Serializable]
public class CollisionInfoAllDirection
{
    [SerializeField] SpecificCollisionInfo upDirection, rightDirection, leftDirection, downDirection;

    public Vector2 GetDirectionOffset(Vector2 direction)
    {
        Vector2 newOffset = Vector2.zero;
        if(direction == Vector2.right)
        {
            newOffset = rightDirection.GetOffset();
        }
        else if(direction == Vector2.left)
        {
            newOffset = leftDirection.GetOffset();
        }
        else if(direction == Vector2.up)
        {
            newOffset = upDirection.GetOffset();
        }
        else if(direction == Vector2.down)
        {
            newOffset = downDirection.GetOffset();
        }
        return newOffset;
    }

    public Vector2 GetDirectionSize(Vector2 direction)
    {
        Vector2 newOffset = Vector2.zero;
        if (direction == Vector2.right)
        {
            newOffset = rightDirection.GetSize();
        }
        else if (direction == Vector2.left)
        {
            newOffset = leftDirection.GetSize();
        }
        else if (direction == Vector2.up)
        {
            newOffset = upDirection.GetSize();
        }
        else if (direction == Vector2.down)
        {
            newOffset = downDirection.GetSize();
        }
        return newOffset;
    }

    public void SetDirectionOffset(Vector2 newOffset, Vector2 direction)
    {
        if (direction == Vector2.right)
        {
            rightDirection.SetOffset(newOffset);
        }
        else if (direction == Vector2.left)
        {
            leftDirection.SetOffset(newOffset);
        }
        else if (direction == Vector2.up)
        {
            upDirection.SetOffset(newOffset);
        }
        else if (direction == Vector2.down)
        {
            downDirection.SetOffset(newOffset);
        }
    }

    public void SetDirectionSize(Vector2 newSize, Vector2 direction)
    {
        if (direction == Vector2.right)
        {
            rightDirection.SetSize(newSize);
        }
        else if (direction == Vector2.left)
        {
            leftDirection.SetSize(newSize);
        }
        else if (direction == Vector2.up)
        {
            upDirection.SetSize(newSize);
        }
        else if (direction == Vector2.down)
        {
            downDirection.SetSize(newSize);
        }
    }
}
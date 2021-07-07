using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[System.Serializable]
public class Range : Weapon
{
    [System.Serializable]
    struct ComboAttack
    {
        public int myNumberOfBullets;
        public float mySpreadFactor;
    }

    [SerializeField] private int myCurrentComboIndex = 0;
    [SerializeField] ComboAttack myAttack1;
    [SerializeField] ComboAttack myAttack2;
    [SerializeField] Vector3 myLastClickedPosition;

    public int GetNumberOfBullets()
    {
        //return myNumberOfBullets;

        if (myCurrentComboIndex == 1)
        {
            return myAttack1.myNumberOfBullets;
        }
        else if (myCurrentComboIndex == 2)
        {
            return myAttack2.myNumberOfBullets;
        }

        return 0;
    }
    public float GetFiringRate()
    {
        //return myFiringRate;
        return 0;
    }
    public float GetSpreadFactor()
    {
        //return mySpreadFactor;

        if(myCurrentComboIndex == 1)
        {
            return myAttack1.mySpreadFactor;
        }
        else if(myCurrentComboIndex == 2)
        {
            return myAttack2.mySpreadFactor;
        }

        return 0;
    }
    public void SetComboIndex(int aIndex)
    {
        myCurrentComboIndex = aIndex;
    }
    public int GetComboIndex()
    {
        return myCurrentComboIndex;
    }

    public void SetLastClicked(Vector3 aPos)
    {
        myLastClickedPosition = aPos;
    }

    public Vector3 GetLastClicked()
    {
        return myLastClickedPosition;
    }
}
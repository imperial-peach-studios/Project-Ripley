using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[System.Serializable]
public class Consumable : Item
{
    [Space]
    [SerializeField] private int myHealthIncrease;
    [SerializeField] private int myHungerIncrease;
    [SerializeField] private float myConsumeLength;

    public int GetHealthIncrease()
    {
        return myHealthIncrease;
    }
    public void SetHealthIncrease(int aIncrease)
    {
        myHealthIncrease = aIncrease;
    }
    public int GetHungerIncrease()
    {
        return myHungerIncrease;
    }
    public void SetHungerIncrease(int aIncrease)
    {
        myHungerIncrease = aIncrease;
    }
    public float GetConsumeLength()
    {
        return myConsumeLength;
    }
    public void SetConsumeLength(float aLength)
    {
        myConsumeLength = aLength;
    }
}
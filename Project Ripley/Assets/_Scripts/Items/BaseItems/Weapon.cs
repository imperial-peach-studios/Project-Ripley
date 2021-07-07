using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[System.Serializable]
public class Weapon : Item
{
    [Space]
    [SerializeField] private int myDamage;

    [SerializeField] private float myKnockBackAmount;
    [SerializeField] private float myKnockBackLength;
    [SerializeField] private float myStunLength;

    public int GetDamage()
    {
        return myDamage;
    }
    public void SetDamage(int aDamage)
    {
        myDamage = aDamage;
    }
    public float GetKnockBackAmount()
    {
        return myKnockBackAmount;
    }
    public void SetKnockBackAmount(float aAmount)
    {
        myKnockBackAmount = aAmount;
    }
    public float GetKnockBackLength()
    {
        return myKnockBackLength;
    }
    public void SetKnockBackLength(float aLength)
    {
        myKnockBackLength = aLength;
    }
    public float GetStunLength()
    {
        return myStunLength;
    }
    public void SetStunLength(float aLength)
    {
        myStunLength = aLength;
    }
}
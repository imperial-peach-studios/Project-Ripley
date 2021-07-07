using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[System.Serializable]
public class EnemyInfo
{
    [HideInInspector] [SerializeField] public string objectName;
    [HideInInspector] [SerializeField] public float posX, posY;
    [SerializeField] bool playerInSight = false;
    [SerializeField] bool previousPlayerInSight = false;
    [SerializeField] bool heardNoice = false;
    [SerializeField] bool searchMode = false;
    [SerializeField] bool readyToAttack = false;
    [SerializeField] bool attacked = false;
    //[SerializeField] Vector2 personalLastSighting;
    [SerializeField] float personalLastSightingX, personalLastSightingY;
    [SerializeField] float currentDirectionX, currentDirectionY;
    //[SerializeField] Vector2 currentDirection = Vector2.zero;
    [SerializeField] bool knockedDown = false;
    [SerializeField] bool stunned = false;

    public void SetCurrentSight(bool result)
    {
        previousPlayerInSight = playerInSight;
        playerInSight = result;
    }

    public void SetKnockedDown(bool knocked)
    {
        knockedDown = knocked;
    }

    public bool GetKnockedDown()
    {
        return knockedDown;
    }

    public void SetStunned(bool stunned)
    {
        this.stunned = stunned;
    }

    public bool GetStunned()
    {
        return stunned;
    }

    public bool GetCurrentSight()
    {
        return playerInSight;
    }

    public bool HasHeardNoise()
    {
        return heardNoice;
    }

    public bool ReadyToAttack()
    {
        return readyToAttack;
    }

    public bool HasAttacked()
    {
        return attacked;
    }

    public void SetAttacked(bool hasAttacked)
    {
        attacked = hasAttacked;
    }

    public void SetReadyToAttack(bool canValue)
    {
        readyToAttack = canValue;
    }

    public Vector2 GetCurrentDirection()
    {//currentDirection
        return new Vector2(currentDirectionX, currentDirectionY);
    }

    public Vector3 GetPosition()
    {
        return new Vector3(posX, posY, 0);
    }

    public void SetPosition(Vector3 pos)
    {
        posX = pos.x;
        posY = pos.y;
    }

    public void SetLastSight(Vector2 last)
    {
        //personalLastSighting = last;
        personalLastSightingX = last.x;
        personalLastSightingY = last.y;
    }

    public void SetSearchMode(bool mode)
    {
        searchMode = mode;
    }

    public bool GetSearchMode()
    {
        return searchMode;
    }

    public void SetHeardNoise(bool set)
    {
        heardNoice = set;
    }

    public Vector2 GetLastPosition()
    {//personalLastSighting
        return new Vector2(personalLastSightingX, personalLastSightingY);
    }

    public void SetCurrentDirection(Vector2 direction)
    {
        //  currentDirection = direction;
        currentDirectionX = direction.x;
        currentDirectionY = direction.y;
    }
}
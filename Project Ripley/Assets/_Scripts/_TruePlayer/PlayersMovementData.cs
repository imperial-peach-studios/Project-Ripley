using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayersMovementData : MonoBehaviour
{
    public MovementDatabase movementDatabaseSO;
    public static bool InsideASafeHouse = false;

    void Awake()
    {
        GameData.OnSavePlayer += OnSave;
        GameData.OnLoadPlayer += OnLoad;
    }

    public void OnSave()
    {
        GameData.aData.pData.SetPosition(transform.position);
    }

    public void OnLoad()
    {
        transform.position = GameData.aData.pData.GetPosition();
    }
}
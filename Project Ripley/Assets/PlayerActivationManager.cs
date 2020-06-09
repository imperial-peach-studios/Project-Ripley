using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerActivationManager : MonoBehaviour
{
    public static PlayerActivationManager Instance;

    NewPlayerMovement playerM;
    PlayerDash playerD;
    InteractReceiver playerI;
    
    void Awake()
    {
        if (Instance == null)
        {
            //DontDestroyOnLoad(gameObject);
            Instance = this;
        }

        playerM = GetComponent<NewPlayerMovement>();
        playerD = GetComponent<PlayerDash>();
        playerI = GetComponent<InteractReceiver>();
    }
    
    public void SetMovementActive(bool val)
    {
        playerM.enabled = val;
    }

    public void SetDashActive(bool val)
    {
        playerD.enabled = val;
    }

    public void SetInteractionActive(bool val)
    {
        playerI.enabled = val;
    }

    public void SetAllMovementActive(bool val)
    {
        SetMovementActive(val);
        SetDashActive(val);
    }

    public void SetPlayerActive(bool val)
    {
        SetMovementActive(val);
        SetDashActive(val);
        SetInteractionActive(val);
    }
}
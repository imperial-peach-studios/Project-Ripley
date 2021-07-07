using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public enum PlayerState
{
    Idle,
    Walking,
    Dashing,
    Attacking,
    PickingUp,
    Dropping,
    Damaged,
    Dead,
    Count
}

public class Player : MonoBehaviour
{
    public static Player Instance;
    [SerializeField] private PlayerState myPlayerState;

    [HideInInspector] public Inventory inventory;
    [HideInInspector] public Equipment equipment;
    [HideInInspector] public InteractReceiver interact;

    void Awake()
    {
        if (Instance == null)
        {
            //DontDestroyOnLoad(gameObject);
            Instance = this;
        }

        inventory = GetComponent<Inventory>();
        equipment = GetComponent<Equipment>();
        interact = GetComponent<InteractReceiver>();

        //GameData.OnSavePlayer += OnSave;
        //GameData.OnLoadPlayer += OnLoad;
    }

    public void SetPlayerState(PlayerState aPlayerState)
    {
        myPlayerState = aPlayerState;
    }
    public PlayerState GetPlayerState()
    {
        return myPlayerState;
    }

    public bool CanChangeState(PlayerState aState)
    {
        return CheckState(aState);
    }

    public void UpdateStateTo(PlayerState aState)
    {
        bool canUpdate = CheckState(aState);

        if (canUpdate)
        {
            SetPlayerState(aState);
        }
    }

    public PlayerState GetState()
    {
        return myPlayerState;
    }

    bool CheckState(PlayerState aState)
    {
        bool aCan = true;
        switch (aState)
        {
            case PlayerState.Idle:
                if (myPlayerState == PlayerState.Dead)
                {
                    aCan = false;
                }
                break;
            case PlayerState.Walking:
                if (myPlayerState == PlayerState.Dashing || myPlayerState == PlayerState.Attacking || myPlayerState == PlayerState.Damaged || myPlayerState == PlayerState.PickingUp || myPlayerState == PlayerState.Dropping || myPlayerState == PlayerState.Dead)
                {
                    aCan = false;
                }
                break;
            case PlayerState.Dashing:
                if (myPlayerState == PlayerState.Attacking || myPlayerState == PlayerState.Damaged || myPlayerState == PlayerState.PickingUp || myPlayerState == PlayerState.Dropping || myPlayerState == PlayerState.Dead)
                {
                    aCan = false;
                }
                break;
            case PlayerState.Attacking:
                if (myPlayerState == PlayerState.Dashing || myPlayerState == PlayerState.Damaged || myPlayerState == PlayerState.PickingUp || myPlayerState == PlayerState.Dropping || myPlayerState == PlayerState.Dead)
                {
                    aCan = false;
                }
                break;
            case PlayerState.PickingUp:
                if (myPlayerState == PlayerState.Dashing || myPlayerState == PlayerState.Attacking || myPlayerState == PlayerState.Dropping || myPlayerState == PlayerState.Damaged || myPlayerState == PlayerState.Dead)
                {
                    aCan = false;
                }
                break;
            case PlayerState.Dropping:
                if (myPlayerState == PlayerState.Dashing || myPlayerState == PlayerState.Attacking || myPlayerState == PlayerState.PickingUp || myPlayerState == PlayerState.Damaged || myPlayerState == PlayerState.Dead)
                {
                    aCan = false;
                }
                break;
        }

        return aCan;
    }
}
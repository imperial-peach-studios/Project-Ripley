using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Player : MonoBehaviour
{
    public static Player Instance;
    [HideInInspector] public Inventory inventory;
    [HideInInspector] public Equipment equipment;

    NewPlayerMovement playerM;
    PlayerDash playerD;
    InteractReceiver playerI;
    PlayerAttack playerA;

    public static bool InsideASafeHouse = false;
    [SerializeField] private PlayerState myPlayerState;

    public enum PlayerState
    {
        Idle,
        Running,
        Sneaking,
        Dashing,
        Attacking,
        Damaged
    }

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
        playerA = GetComponent<PlayerAttack>();

        inventory = GetComponent<Inventory>();
        equipment = GetComponent<Equipment>();

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

    public void SetPlayerState(PlayerState aPlayerState)
    {
        myPlayerState = aPlayerState;
    }

    public PlayerState GetPlayerState()
    {
        return myPlayerState;
    }

    public void TryToModifyState(PlayerState aPlayerState)
    {
        if(myPlayerState == PlayerState.Attacking || myPlayerState == PlayerState.Damaged || myPlayerState == PlayerState.Dashing)
        {
            return;
        }

        SetPlayerState(aPlayerState);
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

    public void SetAttackActive(bool val)
    {
        playerA.enabled = val;
    }

    public void SetPlayerActive(bool val)
    {
        SetMovementActive(val);
        SetDashActive(val);
        SetInteractionActive(val);
    }

    public bool GetMovementActive()
    {
        return playerM.enabled;
    }

    public bool GetAllMovementActive()
    {
        return playerM.enabled == true && playerD.enabled == true ? true : false;
    }
}
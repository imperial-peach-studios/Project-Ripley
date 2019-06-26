using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerAnimation : MonoBehaviour
{
    Animator anim;
    MovementDatabase movementDatabase;
    PlayerDash pD;
    PlayerMovement pM;

    void Start()
    {
        anim = GetComponent<Animator>();
        pM = GetComponent<PlayerMovement>();
        pD = GetComponent<PlayerDash>();
        movementDatabase = GetComponent<PlayersMovementData>().movementDatabaseSO;
    }

    void Update()
    {
        if (movementDatabase.GetDisableEnableMove() == false)
        {
            AnimInput();
        }
    }
    
    void AnimInput()
    {
        anim.SetFloat("Moving", movementDatabase.GetMoving());
        anim.SetFloat("Horizontal", movementDatabase.GetAnimInput().x);
        anim.SetFloat("Vertical", movementDatabase.GetAnimInput().y);
        anim.SetBool("Dash", pD.HasDashed);
        anim.SetBool("Sneaking", pM.IsSneaking);
    }
}
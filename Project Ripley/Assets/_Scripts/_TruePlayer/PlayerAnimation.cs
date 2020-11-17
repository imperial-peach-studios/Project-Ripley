using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerAnimation : MonoBehaviour
{
    Animator anim;
    MovementDatabase movementDatabase;
    PlayerDash pD;
    PlayerMovement pM;
    PlayerHealth pH;

    private float animHorizontal = 0;
    private float animVertical = 0;

    void Awake()
    {
        anim = GetComponent<Animator>();
        //pM = GetComponentInParent<PlayerMovement>();
        pD = GetComponentInParent<PlayerDash>();
        pH = GetComponentInParent<PlayerHealth>();
        //movementDatabase = GetComponentInParent<PlayersMovementData>().movementDatabaseSO;
    }

    void Update()
    {
        if(Player.Instance.GetAllMovementActive() == true)
        {
            AnimInput();
        }

        if(pH.IsDead())
        {
            anim.SetBool("Dead", true);
        }
        else
        {
            if(anim.GetBool("Dead"))
            {
               anim.SetBool("Dead", false);
            }
        }
    }

    float IsMoving()
    {
        float horizontal = Input.GetAxisRaw("Horizontal");
        float vertical = Input.GetAxisRaw("Vertical");

        if(Mathf.Abs(horizontal) != 0 || Mathf.Abs(vertical) != 0)
        {
            if(Player.Instance.GetPlayerState() == Player.PlayerState.Running)
            {
                return 2;
            }
            else if(Player.Instance.GetPlayerState() == Player.PlayerState.Sneaking)
            {
                return 1;
            }
            
        }

        return 0;
    }
    
    void AnimInput()
    {
        anim.SetFloat("Moving", IsMoving());
        anim.SetFloat("Horizontal", Input.GetAxisRaw("Horizontal"));
        anim.SetFloat("Vertical", Input.GetAxisRaw("Vertical"));
        //anim.SetBool("Dash", pD.HasDashed);
        //anim.SetBool("Sneaking", pM.IsSneaking);
    }
}
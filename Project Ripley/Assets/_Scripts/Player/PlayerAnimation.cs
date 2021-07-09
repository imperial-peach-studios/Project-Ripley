﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerAnimation : MonoBehaviour
{
    Animator myAnim;

    float myHorizontalInput;
    float myVerticalInput;
    bool myIsOver = false;
    bool myPickedUp = false;
    [HideInInspector]
    public bool myIsDead = false;
    bool myFinishedPlayingDead = false;
    [HideInInspector]
    public bool myFinishedPlayingHealing = false;
    private bool myIsHealing = false;
 
    // Start is called before the first frame update
    void Start()
    {
        myAnim = GetComponent<Animator>();
        myHorizontalInput = 0;
        myVerticalInput = -1;
    }

    // Update is called once per frame
    void Update()
    {
        var currentState = myAnim.GetCurrentAnimatorStateInfo(0);

        switch (Player.Instance.GetState())
        {
            case PlayerState.Idle:
                myAnim.SetFloat("Moving", 0f);
                myAnim.SetBool("Dash", false);
                break;
            case PlayerState.Walking:
                myAnim.SetFloat("Moving", 2f);
                float horizontal = Input.GetAxisRaw("Horizontal");
                float vertical = Input.GetAxisRaw("Vertical");
                if(vertical != 0)
                {
                    myVerticalInput = vertical;
                    myHorizontalInput = 0;
                }
                if(horizontal != 0)
                {
                    myHorizontalInput = horizontal;
                    myVerticalInput = 0;
                }
                myAnim.SetFloat("Horizontal", myHorizontalInput);
                myAnim.SetFloat("Vertical", myVerticalInput);
                break;
            case PlayerState.Dashing:
                myAnim.SetBool("Dash", true);
                break;
            case PlayerState.Attacking:

                break;
            case PlayerState.Healing:
                if(myIsHealing == false)
                {
                    myAnim.Play("Consuming");
                    myIsHealing = true;
                }
                break;
            case PlayerState.PickingUp:
                if(!currentState.IsName("PickUp"))
                {
                    if (myPickedUp == false)
                    {
                        myPickedUp = true;
                        myAnim.Play("PickUp");
                    }
                    else
                    {
                        Player.Instance.interact.FinishedPickingUp();
                        myPickedUp = false;
                    }
                }
                break;
            case PlayerState.Damaged:
                break;
            case PlayerState.Dead:
                if (myIsDead == false)
                {
                    myAnim.SetBool("Dead", true);
                    myAnim.Play("Death");
                    myIsDead = true;
                }
                else if(myFinishedPlayingDead)
                {
                    GameManager.Instance.FadeOut();
                    myFinishedPlayingDead = false;
                }
                break;
        }
    }

    public void FinishedPickingUp()
    {
        //Player.Instance.UpdateStateTo(PlayerState.Idle);
    }

    public void FinishedPlayingDeath()
    {
        myFinishedPlayingDead = true;
    }

    public void FinishedPlayingHealing()
    {
        myFinishedPlayingHealing = true;
    }

    public void ResetHealing()
    {
        myFinishedPlayingHealing = false;
        myIsHealing = false;
    }
}
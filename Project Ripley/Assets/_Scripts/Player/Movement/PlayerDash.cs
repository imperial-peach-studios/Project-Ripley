using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerDash : MonoBehaviour
{
    [SerializeField] string myDashInputName;

    [SerializeField, Space] float myDashSpeed;
    [SerializeField] float myDashCooldown;

    [SerializeField] bool myHasDashed = false;

    public bool HasDashed
    {
        get { return myHasDashed; }
    }

    private Rigidbody2D myRB;
    private Vector2 myInputDir;
    private float myDashTimer = 0;

    void Start()
    {
        myRB = GetComponent<Rigidbody2D>();
    }

    void Update()
    {
        if(Player.Instance.CanChangeState(PlayerState.Dashing))
        {
            HandleDash();
        }
    }

    void HandleDash()
    {
        if(myHasDashed == false)
        {
            if (Input.GetButtonDown(myDashInputName))
            {
                Vector2 input = myRB.velocity.normalized;
                if (Mathf.Abs(input.x) > 0 || Mathf.Abs(input.y) > 0)
                {
                    myHasDashed = true;
                    myInputDir = input;
                    Player.Instance.UpdateStateTo(PlayerState.Dashing);
                }
            }
        }
        else
        {
            myDashTimer += Time.deltaTime;

            myRB.AddForce(myInputDir * myDashSpeed, ForceMode2D.Impulse);

            if(myDashTimer > myDashCooldown)
            {
                myHasDashed = false;
                myDashTimer = 0;
                Player.Instance.UpdateStateTo(PlayerState.Idle);
            }
        }
    }
}
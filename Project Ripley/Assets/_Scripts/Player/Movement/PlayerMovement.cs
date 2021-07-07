using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerMovement : MonoBehaviour
{
    [Header("Movement Settings:")]
    [SerializeField] float myWalkingSpeed;
    [SerializeField] float mySneakingSpeed;
    [SerializeField] bool myIsSneaking = false;
    [Space]
    [SerializeField] float myAcceleration;
    [SerializeField] float myDeAcceleration;
    private bool myOnStop = false;

    private Rigidbody2D myRB;
    private Vector2 myInput;

    void Start()
    {
        myRB = GetComponent<Rigidbody2D>();
    }

    void OnDisable()
    {
        if(myRB != null)
        {
            myRB.velocity = Vector2.zero;
        }
    }

    void Update()
    {
        if (Player.Instance.CanChangeState(PlayerState.Walking))
        {
            if(myOnStop == true)
            {
                myOnStop = false;
            }

            HandleInput();
        }
        else
        {
            if(myOnStop == false)
            {
                myRB.velocity = Vector2.zero;
                myOnStop = true;
            }
        }
    }

    void FixedUpdate()
    {
        if(Player.Instance.CanChangeState(PlayerState.Walking))
        {
            HandlePhysics();
        }
    }

    void HandleInput()
    {
        float horizontal = Input.GetAxisRaw("Horizontal");
        float vertical = Input.GetAxisRaw("Vertical");

        Vector2 newInput = new Vector3(horizontal, vertical);

        myInput = AccelerateInput(newInput, myInput);

        if (Mathf.Abs(myInput.x) > 0 || Mathf.Abs(myInput.y) > 0)
        {
            Player.Instance.UpdateStateTo(PlayerState.Walking);
        }
        else
        {
            Player.Instance.UpdateStateTo(PlayerState.Idle);
        }
    }

    void HandlePhysics()
    {
        float speed = SneakOrRun();
        Vector3 moveDirection = myInput.normalized * speed;
        myRB.velocity = moveDirection;
    }

    Vector2 AccelerateInput(Vector2 newInp, Vector2 oldInp)
    {
        newInp.x = GetAcceleratedInput(newInp.x, oldInp.x);
        newInp.y = GetAcceleratedInput(newInp.y, oldInp.y);

        return newInp;

        float GetAcceleratedInput(float newInput, float oldInput)
        {
            float start = oldInput;
            float end = 0;

            float acceleration = myDeAcceleration;

            if (Mathf.Abs(newInput) > 0)
            {
                end = newInput;
                acceleration = myAcceleration;
            }

            return Mathf.MoveTowards(start, end, acceleration * Time.deltaTime);
        }
    }
    float SneakOrRun()
    {
        if (Input.GetKeyDown(KeyCode.LeftShift))
        {
            myIsSneaking = !myIsSneaking;
        }

        return myIsSneaking == true ? mySneakingSpeed : myWalkingSpeed;
    }
}
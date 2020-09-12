using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerMovement : MonoBehaviour
{
    [SerializeField] float runningSpeed; //Player's Running Speed
    [SerializeField] float sneakingSpeed; //Player's Sneaking Speed
    [SerializeField] bool isSneaking = false; //If The Player Is Sneaking
    [SerializeField] bool stopMoving = false;
    [SerializeField] bool disableMove = false;
    MovementDatabase movementDatabase;
    private float movementSpeed = 0;
    public bool IsSneaking
    {
        get { return isSneaking; }
    }
    public bool StopMoving
    {
        get { return stopMoving; }
        set { stopMoving = value; }
    }
    public bool DisableMove
    {
        get { return disableMove; }
        set { disableMove = value; }
    }

    Rigidbody2D rb;
    [SerializeField] Animator anim;

    void Start()
    {
        rb = GetComponent<Rigidbody2D>();
       // anim = GetComponent<Animator>();
        movementDatabase = GetComponent<PlayersMovementData>().movementDatabaseSO;
    }

    void Update()
    {
        //MouseDatabase.UpdateMousePosition();

        if(disableMove != true)
        {
            SneakOrRun();
            Move();

            movementDatabase.EnableMove();
        }
        else
        {
            movementDatabase.SetAnim(new Vector2(anim.GetFloat("Horizontal"), anim.GetFloat("Vertical")));
            movementDatabase.SetMoving(anim.GetFloat("Moving"));
            movementDatabase.DisableMove();
        }
    }

    void SneakOrRun()
    {
        if (Input.GetKeyDown(KeyCode.LeftShift))
        {
            isSneaking = !isSneaking;
        }

        if (isSneaking == true)
        {
            movementSpeed = sneakingSpeed;
        }
        else
        {
            movementSpeed = runningSpeed;
        }
    }

    void Move()
    {
        float horizontalInput = movementDatabase.GetRawInput().x;
        float verticalInput = movementDatabase.GetRawInput().y;

        SetInput(KeyCode.W, KeyCode.S, new Vector2(0f, verticalInput), 0);
        SetInput(KeyCode.A, KeyCode.D, new Vector2(horizontalInput, 0f), 1);

        ApplyVelocity();
    }

    void SetInput(KeyCode first, KeyCode second, Vector2 input, int direction)
    {
        if (Input.GetKey(first) || Input.GetKey(second))
        {
            if(direction == 1)
            {
                movementDatabase.SetHorizontalInput(input.x);
            }
            else if(direction == 0)
            {
                movementDatabase.SetVerticalInput(input.y);
            }
           
            if (stopMoving == false)
            {
                movementDatabase.SetAnim(input);
            }
        }
        else
        {
            if (direction == 1)
            {
                movementDatabase.SetHorizontalInput(0);
            }
            else if (direction == 0)
            {
                movementDatabase.SetVerticalInput(0);
            }
        }
    }

    void ApplyVelocity()
    {
        if (movementDatabase.GetInput().x == 0 && movementDatabase.GetInput().y == 0)
        {
            movementDatabase.SetMoving(0);
        }
        else
        {
            movementDatabase.SetMoving(1);
        }

        Vector3 velocity = new Vector2(movementDatabase.GetRawInput().x, movementDatabase.GetRawInput().y) * Time.deltaTime;
        rb.velocity = velocity.normalized * movementSpeed;

        if (stopMoving == true)
        {
            rb.velocity = Vector2.zero;
        }
    }
}
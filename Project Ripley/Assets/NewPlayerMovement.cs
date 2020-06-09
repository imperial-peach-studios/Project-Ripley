using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class NewPlayerMovement : MonoBehaviour
{
    [Header("Movement Settings:")]
    [SerializeField] float movementSpeed;
    [SerializeField] float sneakingSpeed; //Player's Sneaking Speed
    [SerializeField] bool isSneaking = false; //If The Player Is Sneaking
    [SerializeField] float acceleration, deAcceleration;

    [Header("Wall Check:")]
    [SerializeField] LayerMask wallMask;

    [SerializeField] float distanceToWall;
    [SerializeField] float startY, endY;

    Vector3 input;

    Rigidbody2D rb;


    void Start()
    {
        rb = GetComponent<Rigidbody2D>();
    }

    void Update()
    {
        FinalMovement();
    }

    void FinalMovement()
    {
        float horizontal = Input.GetAxisRaw("Horizontal");
        float vertical = Input.GetAxisRaw("Vertical");

        Vector2 newInput = new Vector3(horizontal, vertical);
        
        WallCollision(ref newInput);
        //pA.SetInput(beforeInput);
        
        input = AccelerateInput(newInput, input);

        float speed = SneakOrRun();
        Vector3 moveDirection = input.normalized * speed;

        rb.velocity = moveDirection;
    }

    float SneakOrRun()
    {
        if (Input.GetKeyDown(KeyCode.LeftShift))
        {
            isSneaking = !isSneaking;
        }

        return isSneaking == true ? sneakingSpeed : movementSpeed;
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

            float acceleration = deAcceleration;

            if (Mathf.Abs(newInput) > 0)
            {
                end = newInput;
                acceleration = this.acceleration;
            }

            return Mathf.MoveTowards(start, end, acceleration * Time.deltaTime);
        }
    }

    int SetDirection(float value, float input)
    {
        int direction = 0; //Make A Variable That Will Store The Direction Of The Player, Depending On Where They Press
        if (value > 0) //If The Player Pressed "Right"
        {
            direction = 1;
        }
        else if (value < 0) //If The Player Pressed "Left"
        {
            direction = -1;
        }

        return direction;
    }

    void WallCollision(ref Vector2 inp)
    {
        for (float y = -0.5f; y < 0; y += 0.1f)
        {
            if (Physics.Raycast(transform.position + Vector3.up * y, Vector2.right * inp.x, wallMask))
            {
                inp.x = 0;
                break;
            }
        }

        for (float x = -0.5f; x < 0; x += 0.1f)
        {
            if (Physics.Raycast(transform.position + Vector3.right * x, Vector2.up * inp.y, wallMask))
            {
                inp.y = 0;
                break;
            }
        }
    }

    void OnDisable()
    {
        if (rb != null)
        {
            rb.velocity = Vector3.zero;
        }
    }
}
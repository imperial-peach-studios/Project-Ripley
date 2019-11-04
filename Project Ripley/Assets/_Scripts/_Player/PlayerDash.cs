using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerDash : MonoBehaviour
{
    [SerializeField] float dashSpeed;
    [SerializeField] float dashCooldown;
    [SerializeField] bool hasDashed = false;
    MovementDatabase movementDatabase;
    public bool HasDashed
    {
        get { return hasDashed; }
    }
    
    float dashTimer = 0;
    Rigidbody2D rb;

    void Awake()
    {
        rb = GetComponent<Rigidbody2D>();
        movementDatabase = GetComponent<PlayersMovementData>().movementDatabaseSO;
    }

    void Update()
    {
        Dash();
    }

    void Dash()
    {
        if (Input.GetKeyDown(KeyCode.Space) && hasDashed == false && Mathf.Abs(movementDatabase.GetInput().x) > 0 ||
            Input.GetKeyDown(KeyCode.Space) && hasDashed == false && Mathf.Abs(movementDatabase.GetInput().y) > 0)
        {
            hasDashed = true;
            GetComponent<PlayerMovement>().enabled = false;
        }

        if (hasDashed)
        {
            dashTimer += Time.deltaTime;

            rb.AddForce(movementDatabase.GetInput() * dashSpeed, ForceMode2D.Impulse);

            if (dashTimer > dashCooldown)
            {
                hasDashed = false;
                dashTimer = 0;
                GetComponent<PlayerMovement>().enabled = true;
            }
        }
    }

    void OnDrawGizmos()
    {
        if(Input.GetKey(KeyCode.G))
        {
            Gizmos.DrawWireCube(movementDatabase.GetInput() * dashSpeed, new Vector3(0.5f, 0.5f, 0));
        }
    }
}
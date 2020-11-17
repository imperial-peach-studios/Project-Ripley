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
       // movementDatabase = GetComponent<PlayersMovementData>().movementDatabaseSO;
    }

    void Update()
    {
        Dash();
    }

    void Dash()
    {
        Vector2 input = Vector2.right * Input.GetAxisRaw("Horizontal") + Vector2.up * Input.GetAxisRaw("Vertical");

        if (Input.GetKeyDown(KeyCode.Space) && hasDashed == false && Mathf.Abs(input.x) > 0 ||
            Input.GetKeyDown(KeyCode.Space) && hasDashed == false && Mathf.Abs(input.y) > 0)
        {
            Player.Instance.SetPlayerState(Player.PlayerState.Dashing);
            hasDashed = true;
            Player.Instance.SetMovementActive(false);
            Player.Instance.SetAttackActive(false);
            //GetComponent<PlayerMovement>().enabled = false;
            //PlayerActivationManager.Instance.SetMovementActive()
        }

        if (hasDashed)
        {
            dashTimer += Time.deltaTime;

            rb.AddForce(input * dashSpeed, ForceMode2D.Impulse);

            if (dashTimer > dashCooldown)
            {
                Player.Instance.SetPlayerState(Player.PlayerState.Idle);
                hasDashed = false;
                dashTimer = 0;
                //GetComponent<PlayerMovement>().enabled = true;
                Player.Instance.SetMovementActive(true);    
                Player.Instance.SetAttackActive(true);
            }
        }
    }

    void OnDrawGizmos()
    {
        Vector2 input = Vector2.right * Input.GetAxisRaw("Horizontal") + Vector2.up * Input.GetAxisRaw("Vertical");

        if (Input.GetKey(KeyCode.G))
        {
            Gizmos.DrawWireCube(input * dashSpeed, new Vector3(0.5f, 0.5f, 0));
        }
    }
}
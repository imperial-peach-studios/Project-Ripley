using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
public class PlayerMovement : MonoBehaviour
{
    private Rigidbody2D rb;
    public float speed,jumpForce,dashSpeed,startDashTime,maxDist;
    private float dir,jumpCharge,dashTime;
    private bool isGrounded, inAir, isDashing, flipped=false,killedEnemy=false;
    public GameObject dashParticle;
    public Slider dashSlider;

    public Transform groundcheck;
    public float CheckRadius;
    public LayerMask groundLayer,dashLayer;

    private void Start()
    {
        rb = GetComponent<Rigidbody2D>();
        dashTime = 2;
    }

    private void Update()
    {
        dashSlider.value = dashTime;
        dir=Input.GetAxis("Horizontal");

        if (Input.GetKeyDown(KeyCode.Space) && isGrounded==true && GM.canJump==true)
        {
            rb.velocity = Vector2.up * jumpForce;
        }

        Vector3 characterScale = transform.localScale;
        if(dir < 0)
        {
            characterScale.x = -1;
            flipped = true;
        }

        if (dir > 0)
        {
            characterScale.x = 1;
            flipped = false;
        }
        transform.localScale = characterScale;

        if (Input.GetKeyDown(KeyCode.Space) && inAir==true && jumpCharge == 1 && GM.canJump == true)
        {
            rb.velocity = Vector2.up * jumpForce;
            jumpCharge = 0;
        }

        if (isGrounded == false)
        {
            inAir = true;
        }
        else
        {
            inAir = false;
            jumpCharge = 1;
        }

        if (dashTime < 2)
        {
            dashTime += 1 * Time.deltaTime;
        }

        if (Input.GetKeyDown(KeyCode.W) && dashTime >=2 && GM.canDash==true )
        {
            Instantiate(dashParticle, transform.position, Quaternion.identity);

            if (flipped == false)
            {
                RaycastHit2D enemyHit = Physics2D.Raycast(transform.position, transform.right, maxDist);
                
                if (enemyHit.collider != null && enemyHit.collider.gameObject.GetComponent<Enemy>() == true)
                {
                    enemyHit.collider.gameObject.GetComponent<Enemy>().Destroy();
                    killedEnemy = true;
                }
            }
            else if (flipped == true)
            {
                RaycastHit2D enemyHit = Physics2D.Raycast(transform.position, transform.right*-1,maxDist);
                if (enemyHit.collider != null && enemyHit.collider.gameObject.GetComponent<Enemy>() == true)
                {
                    enemyHit.collider.gameObject.GetComponent<Enemy>().Destroy();
                    killedEnemy = true;
                }
            }
           
            isDashing = true;
        }
        else
        {
            isDashing = false;
        }
    }

    private void FixedUpdate()
    {
        if (GM.canMove == true)
        {
            rb.velocity = new Vector2(dir * speed, rb.velocity.y);
        }

        if (GM.canMove == false)
        {
            rb.AddForce(new Vector2(dir * speed, rb.velocity.y));
        }
       
        isGrounded = Physics2D.OverlapCircle(groundcheck.position, CheckRadius, groundLayer);

        if (isDashing == true)
        {
            rb.velocity = new Vector2(dir * dashSpeed,rb.velocity.y);
            dashTime = 0;
            if (killedEnemy == true)
            {
                dashTime = 2;
                killedEnemy = false;
            }
        }
    }

    
}

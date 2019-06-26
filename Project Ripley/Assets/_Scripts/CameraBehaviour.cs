using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraBehaviour : MonoBehaviour
{
    public float smoothSpeed = 0.125f;
    public Transform player, enemy;

    public Vector3 offset = new Vector3(0f,0f, -10f);
    public float afterPlayerSpeed;

    //private PlayerBehaviour playerBehaviour;
    private Rigidbody2D targetRB;
    public Transform maxX, minX;
    public float maxDistanceFromPlayer;
    Vector3 velocity;
    public bool followBoth = false;
    public bool followPlayer = true;
    bool disable = false;

    void Start()
    {
        player = GameObject.Find("Player").transform;
        //playerBehaviour = player.GetComponent<PlayerBehaviour>();
        targetRB = player.GetComponent<Rigidbody2D>();
    }
    void Update()
    {
        if (player == null && GameObject.Find("Player") != null)
        {
            //playerBehaviour = player.GetComponent<PlayerBehaviour>();
            targetRB = player.GetComponent<Rigidbody2D>();
        }
    }
    private void FixedUpdate()
    {
        if (Input.GetKeyDown(KeyCode.K) && enemy != null)
        {
            followBoth = !followBoth;
        }
        if (Input.GetKeyDown(KeyCode.J))
        {
            followPlayer = !followPlayer;
        }

        if(disable == false)
        {
            if (followBoth == true)
            {
                CameraFollowPlayerAndEnemy(player.position, enemy.position);
            }
            else
            {
                if (followPlayer == true)
                {
                    CameraFollow(player.position);
                }
                else
                {
                    CameraFollow(enemy.position);
                }
            }
        }
    }

    public void Disable()
    {
        disable = true;
    }
    public void Enable()
    {
        disable = false;
    }

    void CameraFollow(Vector3 target)
    {
        Vector3 desiredPosition = target + offset;

        Vector3 smoothedPosition = Vector3.SmoothDamp(transform.position, desiredPosition, ref velocity, smoothSpeed);
        transform.position = smoothedPosition;
    }
    void CameraFollowPlayerAndEnemy(Vector3 current, Vector3 target)
    {

        Vector3 desiredPosition = (target + current) / 2 + offset;


        Vector3 smoothedPosition = Vector3.SmoothDamp(transform.position, desiredPosition, ref velocity, smoothSpeed);
        transform.position = smoothedPosition;
    }
    void CameraFollowY()
    {
        Vector3 desiredPosition = new Vector3(transform.position.x, player.position.y + offset.y, offset.z);
        if (maxX != null)
        {
            if (desiredPosition.y > maxX.position.y)
            {
                desiredPosition.y = transform.position.y + (maxX.position.y - transform.position.y);
            }
        }
        Vector3 smoothedPosition = Vector3.SmoothDamp(transform.position, desiredPosition, ref velocity, smoothSpeed);
        if (player.position.y > transform.position.y)
        {
            transform.position = smoothedPosition;
        }
    }
    public IEnumerator CameraShake(float duration, float magnitude)
    {
        Vector3 originalPos = transform.localPosition;

        float elapsed = 0.0f;

        while (elapsed < duration)
        {
            float x = Random.Range(-1f, 1f) * magnitude;
            float y = Random.Range(-1f, 1f) * magnitude;

            transform.localPosition = new Vector3(x, y, originalPos.z);

            elapsed += Time.deltaTime;

            yield return null;
        }
        transform.localPosition = originalPos;
    }
}
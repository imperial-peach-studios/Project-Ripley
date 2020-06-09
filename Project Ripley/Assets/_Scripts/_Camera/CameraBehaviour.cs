using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class CameraBehaviour : MonoBehaviour
{
    public float smoothSpeed = 0.125f;
    public Transform player, enemy;
    [SerializeField] Vector3 playerPoint;
    bool xAxisBoundaryActive = false;
    bool yAxisBoundaryActive = false;
    float boundariesXmax, boundariesXmin;
    float boundariesYmax, boundariesYmin;

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

    [SerializeField] Image fadeImage;
    bool fade = false;
    [SerializeField] float fadeDuration;
    [SerializeField] LayerMask boundariesLayerMask;
    Vector3 atLeftScreenPos;
    Vector3 AtRightScreenPos;
    Vector3 atUpScreenPos;
    Vector3 atDownScreenPos;
    Vector3 screenSize;
    [SerializeField] Vector3 collisionSize;
    
    void Start()
    {
        player = GameObject.Find("Player_0_2").transform;
        //playerBehaviour = player.GetComponent<PlayerBehaviour>();
        targetRB = player.GetComponent<Rigidbody2D>();
        GetComponent<Camera>().transparencySortMode = TransparencySortMode.CustomAxis;
        GetComponent<Camera>().transparencySortAxis = Vector3.up;
    }

    void Update()
    {
        if (player == null && GameObject.Find("Player_0_2") != null)
        {
            //playerBehaviour = player.GetComponent<PlayerBehaviour>();
            targetRB = player.GetComponent<Rigidbody2D>();
        }

        atLeftScreenPos = Camera.main.ScreenToWorldPoint(new Vector3(Screen.width - Screen.width, Screen.height / 2, 10));
        AtRightScreenPos = Camera.main.ScreenToWorldPoint(new Vector3(Screen.width, Screen.height / 2, 10));
        atUpScreenPos = Camera.main.ScreenToWorldPoint(new Vector3(Screen.width / 2, Screen.height, 10));
        atDownScreenPos = Camera.main.ScreenToWorldPoint(new Vector3(Screen.width / 2, Screen.height - Screen.height, 10));

        screenSize = Camera.main.ScreenToWorldPoint(new Vector3(Screen.width, Screen.height)) - Camera.main.ScreenToWorldPoint(new Vector3(Screen.width - Screen.width, Screen.height - Screen.height));

        CheckForBoundaries(AtRightScreenPos, new Vector3(collisionSize.x, screenSize.y), "Right");
        CheckForBoundaries(atUpScreenPos, new Vector3(screenSize.x, collisionSize.y), "Up");
        CheckForBoundaries(atLeftScreenPos, new Vector3(collisionSize.x, screenSize.y), "Left");
        CheckForBoundaries(atDownScreenPos, new Vector3(screenSize.x, collisionSize.y), "Down");

        if (followPlayer)
        {
            playerPoint = player.position;

            if (xAxisBoundaryActive)
            {
                playerPoint.x = Mathf.Clamp(playerPoint.x, boundariesXmin, boundariesXmax);
            }
            if (yAxisBoundaryActive)
            {
                playerPoint.y = Mathf.Clamp(playerPoint.y, boundariesYmin, boundariesYmax);
            }
        }

        if (Input.GetKeyDown(KeyCode.L))
        {
            fade = !fade;
        }
        if (fade)
        {
            fadeImage.CrossFadeAlpha(1, fadeDuration, true);
        }
        else
        {
            //fadeImage.CrossFadeAlpha(0, fadeDuration, false);
        }

        Vector3 screePoint = Camera.main.ScreenToWorldPoint(new Vector3(Screen.width, Screen.height - Screen.height, 10));

        //Debug.Log(screePoint);
        Debug.DrawLine(transform.position, screePoint);
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
                    CameraFollow(playerPoint);
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

    public void StartFade()
    {
        fade = true;
        fadeImage.CrossFadeAlpha(0, 0, true);
    }

    public void StopFade()
    {
        fade = false;
    }

    void CheckForBoundaries(Vector3 cameraPositionOffset, Vector3 size, string axis)
    {
        Collider2D[] hits = Physics2D.OverlapBoxAll(cameraPositionOffset, size, 0f, boundariesLayerMask);

        for (int i = 0; i < hits.Length; i++)
        {
            if (hits[i])
            {
                if (axis == "Right" || axis == "Left")
                {
                    if (hits[i].GetComponent<CameraBoundaryPoint>().CheckXAxis(cameraPositionOffset, ref boundariesXmin, ref boundariesXmax, axis))
                    {
                        xAxisBoundaryActive = true;
                        float distance = Mathf.Abs(cameraPositionOffset.x - transform.position.x);
                        if(axis == "Right")
                        {
                            boundariesXmax = boundariesXmax - distance;
                        }
                        else
                        {
                            boundariesXmin = boundariesXmin + distance;
                        }
                    }
                    else
                    {
                        xAxisBoundaryActive = false;
                    }
                }
                else if(axis == "Up" || axis == "Down")
                {
                    if (hits[i].GetComponent<CameraBoundaryPoint>().CheckYAxis(cameraPositionOffset, ref boundariesYmin, ref boundariesYmax))
                    {
                        yAxisBoundaryActive = true;
                        float offset = Mathf.Abs(cameraPositionOffset.y - transform.position.y);
                        //boundariesYmax = boundariesYmax - offset;

                        if (axis == "Up")
                        {
                            boundariesYmax = boundariesYmax - offset;
                        }
                        else
                        {
                            boundariesYmin = boundariesYmin + offset;
                        }
                    }
                    else
                    {
                        yAxisBoundaryActive = false;
                    }
                }
            }
        }

        if(hits.Length == 0)
        {
            if (axis == "Right")
            {
                xAxisBoundaryActive = false;
            }
            else if(axis == "Up")
            {
                yAxisBoundaryActive = false;
            }
        }
    }

    void OnDrawGizmos()
    {
        Gizmos.color = Color.blue;
        Gizmos.DrawWireCube(atLeftScreenPos, new Vector3(collisionSize.x, screenSize.y));
        Gizmos.color = Color.red;
        Gizmos.DrawWireCube(AtRightScreenPos, new Vector3(collisionSize.x, screenSize.y));
        Gizmos.color = Color.yellow;
        Gizmos.DrawWireCube(atUpScreenPos, new Vector3(screenSize.x, collisionSize.y));
        Gizmos.color = Color.magenta;
        Gizmos.DrawWireCube(atDownScreenPos, new Vector3(screenSize.x, collisionSize.y));
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
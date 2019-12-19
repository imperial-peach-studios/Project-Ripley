using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class InOutHouse : MonoBehaviour
{
    public float speed;
    public SpriteRenderer background;
    public SpriteRenderer roof;
    SpriteRenderer mySpriteR;
    public Color roofColor = Color.white;
    public Color backgroundColor = Color.black;
    SpriteMaskInteraction interaction;
    public GameObject spriteMask;
    public bool inside = false;
    int previousLayerOrder;
    public bool insideOtherHouse = false;
    string sortingLayer;
    [SerializeField] Transform targetPos;
    [SerializeField] Transform backPos;
    [SerializeField] Transform newCameraPos;
    [SerializeField] Transform oldCameraPos;
    [SerializeField] Transform point;
    [SerializeField] Transform right, left, down, up;
    [SerializeField] ReceavingEnd re;

    bool teleported = false;

    void Start()
    {
        mySpriteR = GetComponent<SpriteRenderer>();

        spriteMask = transform.GetChild(0).gameObject;

        backgroundColor.a = 0;
        roofColor.a = 1;
        //previousLayerOrder = GetComponent<SpriteRenderer>().sortingOrder;

    }

    void Update()
    {
        if(inside == true)
        {
            spriteMask.SetActive(true);
            mySpriteR.maskInteraction = SpriteMaskInteraction.VisibleInsideMask;
            background.gameObject.SetActive(true);
            roofColor.a = Mathf.Lerp(roofColor.a, 0, Time.deltaTime * speed);
            backgroundColor.a = Mathf.MoveTowards(backgroundColor.a, 1, Time.deltaTime * speed);
            //background.sortingOrder = previousLayerOrder + 11;
            //GetComponent<SpriteRenderer>().sortingOrder = previousLayerOrder + 12;
            background.enabled = true;
        }
        else
        {
            roofColor.a = Mathf.Lerp(roofColor.a, 1, Time.deltaTime * speed);
            backgroundColor.a = Mathf.MoveTowards(backgroundColor.a, 0, Time.deltaTime * speed);
           // background.sortingOrder = previousLayerOrder - 1;
           // GetComponent<SpriteRenderer>().sortingOrder = previousLayerOrder;

            if(backgroundColor.a == 0)
            {
                background.gameObject.SetActive(false);
                background.enabled = false;
                mySpriteR.maskInteraction = SpriteMaskInteraction.None;
                spriteMask.SetActive(false);
            }
        }

        background.color = backgroundColor;
        //roof.color = roofColor;
    }

    void OnTriggerStay2D(Collider2D collision)
    {
        if (collision.transform.tag == "Player")
        {
            inside = true;
            //collision.transform.transform.GetComponent<SpriteRenderer>().sortingLayerName = "Inhouse Layer 1";

            //collision.transform.Find("Sprite & Animations").GetComponent<SpriteRenderer>().sortingLayerName = "Inhouse Layer 1";

            if (teleported == false && collision.transform.position.y >= point.transform.position.y)
            {
                Vector3 pos = collision.transform.position;

                if (pos.x >= right.position.x)
                {
                    pos.x = right.position.x;
                }
                else if (pos.x <= left.position.x)
                {
                    pos.x = left.position.x;
                }

                if (pos.y >= up.position.y)
                {
                    pos.y = up.position.y;
                }
                else if (pos.y <= down.position.y)
                {
                    pos.y = down.position.y;
                }

                Camera.main.GetComponent<CameraBehaviour>().StartFade();

                point.transform.position = pos;
                Vector3 dir = point.transform.position - transform.parent.position;
                Vector3 cam = Camera.main.transform.position - transform.parent.position;
                re.SetPoints(dir, collision.gameObject, cam, transform.parent);
                teleported = true;
            }
        }
    }
    void OnTriggerExit2D(Collider2D collision)
    {
        if (collision.transform.tag == "Player")
        {
            if(collision.transform.position.y < point.transform.position.y)
            {
                teleported = false;
                inside = false;
            }
            //collision.transform.transform.GetComponent<SpriteRenderer>().sortingLayerName = "Player";

            //collision.transform.Find("Sprite & Animations").GetComponent<SpriteRenderer>().sortingLayerName = "Player";

            //collision.transform.position = backPos.position;
            //Camera.main.transform.position = oldCameraPos.position;
            //Camera.main.GetComponent<CameraBehaviour>().enabled = true;

        }
    }
}

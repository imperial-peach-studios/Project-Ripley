using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class InOutHouse : MonoBehaviour
{
    public float speed;
    public SpriteRenderer background;
    public SpriteRenderer roof;
    public Color roofColor = Color.white;
    public Color backgroundColor = Color.black;
    SpriteMaskInteraction interaction;
    public GameObject spriteMask;
    public bool inside = false;
    int previousLayerOrder;
    public bool insideOtherHouse = false;
    string sortingLayer;

    void Start()
    {
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
            GetComponent<SpriteRenderer>().maskInteraction = SpriteMaskInteraction.VisibleInsideMask;
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
                GetComponent<SpriteRenderer>().maskInteraction = SpriteMaskInteraction.None;
                spriteMask.SetActive(false);
            }
        }

        background.color = backgroundColor;
        roof.color = roofColor;
    }

    void OnTriggerStay2D(Collider2D collision)
    {
        if (collision.transform.tag == "Player")
        {
            inside = true;
            collision.transform.transform.GetComponent<SpriteRenderer>().sortingLayerName = "Inhouse Layer 1";
        }
    }
    void OnTriggerExit2D(Collider2D collision)
    {
        if (collision.transform.tag == "Player")
        {
            inside = false;
            collision.transform.transform.GetComponent<SpriteRenderer>().sortingLayerName = "Player";
        }
    }
}

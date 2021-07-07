using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ItemShow : MonoBehaviour
{
    public delegate void OnItemShow(Sprite aSprite, Vector3 aPosition);
    public static OnItemShow OnItemShowActivated;
    public delegate void OnItemDisable();
    public static OnItemDisable OnItemShowDeactivated;

    ItemHolder myItemHolder;

    void Start()
    {
        myItemHolder = GetComponent<ItemHolder>();
    }

    public void OnActive()
    {
        OnItemShowActivated.Invoke(myItemHolder.GetComponent<SpriteRenderer>().sprite, transform.position);
    }

    public void OnExit()
    {
        OnItemShowDeactivated.Invoke();
    }

    void OnTriggerEnter2D(Collider2D collision)
    {
        //OnItemShowActivated.Invoke(myItemHolder.GetComponent<SpriteRenderer>().sprite, transform.position);
    }

    void OnTriggerExit2D(Collider2D collision)
    {
        //OnItemShowDeactivated.Invoke();
    }
}
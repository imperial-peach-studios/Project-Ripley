using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraFollow : MonoBehaviour
{
    [SerializeField] float mySpeed;
    [SerializeField] Vector2 myOffset;
    private Transform myTarget;

    void Start()
    {
        myTarget = Player.Instance.transform;
    }

    void LateUpdate()
    {
        transform.position = Vector3.MoveTowards(transform.position, new Vector3(myTarget.position.x + myOffset.x, myTarget.position.y + myOffset.y, -10), Time.deltaTime * mySpeed);    
    }
}
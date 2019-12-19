using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ReceavingEnd : MonoBehaviour
{
    [SerializeField] Transform point;
    [SerializeField] Transform right, left, down, up;
    GameObject players;
    Vector3 previousPoint;
    Vector3 previousCameraPoint;
    Transform previousParent;
    
    bool isTrue = false;

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        if (isTrue)
        {
            if(players.transform.position.y < point.transform.position.y)
            {
                Vector3 rnew = point.transform.position - transform.parent.position;
                Vector3 newCamera = Camera.main.transform.position - transform.parent.position;


                players.transform.position = previousParent.position + rnew;
                Camera.main.transform.position = previousParent.position + newCamera;
                isTrue = false;
            }
        }
    }

    public void SetPoints(Vector3 pointPos, GameObject player, Vector3 camer, Transform parent)
    {
        isTrue = true;

        point.transform.position = transform.parent.position + pointPos;
        player.transform.position = point.transform.position;

        Camera.main.transform.position = transform.parent.position + camer;

        players = player;
        previousParent = parent;

        Camera.main.GetComponent<CameraBehaviour>().StopFade();
    }
}

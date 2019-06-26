using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GhostEffect : MonoBehaviour
{
    [SerializeField] float ghostDelay = 0.1f;
    [SerializeField] float destroyDelay = 1f;
    private float ghostDelaySeconds;
    [SerializeField] GameObject ghost;

    void Start()
    {
        ghostDelaySeconds = ghostDelay;
    }

    void Update()
    {
        if(ghostDelaySeconds > 0)
        {
            ghostDelaySeconds -= Time.deltaTime;
        }
        else
        {
            //Generate Ghost
            GameObject currentGhost = Instantiate(ghost, transform.position, transform.rotation);
            Sprite currentSprite = GetComponent<SpriteRenderer>().sprite;
            currentGhost.transform.localScale = transform.localScale;
            currentGhost.GetComponent<SpriteRenderer>().sprite = currentSprite;

            ghostDelaySeconds = ghostDelay;
            Destroy(currentGhost, destroyDelay);
        }
    }
}
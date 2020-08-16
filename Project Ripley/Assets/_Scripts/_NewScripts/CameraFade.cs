using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraFade : MonoBehaviour
{
    SpriteRenderer spriteRender;

    [SerializeField] float fadeInSpeed;
    [SerializeField] float fadeOutSpeed;
    [SerializeField] bool fadeIn = false;
    [SerializeField] bool fadeOut = false;
    string sceneToLoad;

    void Awake()
    {
        //LevelProgress.PrepareFadeScreen += FadeIn;
    }

    void Start()
    {
        Player.Instance.SetPlayerActive(false);

        spriteRender = GetComponent<SpriteRenderer>();

        spriteRender.enabled = true;
        spriteRender.color = Color.black;


        fadeOut = true;
    }

    void Update()
    {
        if (fadeIn)
        {
            Color c = spriteRender.color;

            c.a = Mathf.MoveTowards(c.a, 1f, fadeInSpeed);
            spriteRender.color = c;

            if (spriteRender.color.a >= 1f)
            {
                fadeIn = false;
                //LevelProgress.instance.LoadScene();
            }
        }
        else if (fadeOut)
        {
            Color c = spriteRender.color;

            c.a = Mathf.MoveTowards(c.a, 0f, fadeOutSpeed);
            spriteRender.color = c;

            if (spriteRender.color.a == 0f)
            {
                fadeOut = false;
                Player.Instance.SetPlayerActive(true);
                //LevelProgress.instance.FinishedLoadingScene();
            }
        }
    }

    void FadeIn()
    {
        fadeIn = true;
    }
}
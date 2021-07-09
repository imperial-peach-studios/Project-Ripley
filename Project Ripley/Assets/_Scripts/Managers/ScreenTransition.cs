using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ScreenTransition : MonoBehaviour
{
    SpriteRenderer mySpriteRenderer;
    bool myFadeIn = false;
    bool myFadeOut = false;
    float myFade = 1;

    public delegate void OnScreenTransition();
    public static OnScreenTransition OnScreenFadedOut;
    public static OnScreenTransition OnScreenFadedIn;

    void Awake()
    {
        myFade = 1;

        mySpriteRenderer = GetComponent<SpriteRenderer>();
        Color color = mySpriteRenderer.color;
        color.a = myFade;
        mySpriteRenderer.color = color;

        GameManager.OnScreenActive += FadeIn;
        GameManager.OnScreenDeActive += FadeOut;
    }
    void OnDestroy()
    {
        GameManager.OnScreenActive -= FadeIn;
        GameManager.OnScreenDeActive -= FadeOut;
    }

    void Update()
    {
        if(myFadeIn)
        {
            myFade = Mathf.MoveTowards(myFade, 0, Time.deltaTime * 0.5f);

            Color color = mySpriteRenderer.color;
            color.a = myFade;
            mySpriteRenderer.color = color;

            if(myFade == 0)
            {
                //OnScreenFadedIn.Invoke();
                myFadeIn = false;
            }
        }
        else if(myFadeOut)
        {
            myFade = Mathf.MoveTowards(myFade, 1, Time.deltaTime * 0.5f);

            Color color = mySpriteRenderer.color;
            color.a = myFade;
            mySpriteRenderer.color = color;

            if (myFade == 1)
            {
                OnScreenFadedOut.Invoke();
                myFadeOut = false;
            }
        }
    }

    void FadeIn()
    {
        myFadeIn = true;
    }

    void FadeOut()
    {
        myFadeOut = true;
    }
}
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameManager : MonoBehaviour
{
    public static GameManager Instance;

    public delegate void OnScreenTransition();
    public static OnScreenTransition OnScreenActive;
    public static OnScreenTransition OnScreenDeActive;

    //Player myPlayer;

    void Awake()
    {
        if (Instance == null)
        {
            //DontDestroyOnLoad(gameObject);
            Instance = this;
        }

        //GameData.OnSavePlayer += OnSave;
        //GameData.OnLoadPlayer += OnLoad;

        
    }

    void Start()
    {
        //myPlayer = GameObject.Find("Player").GetComponent<Player>();

        FadeIn();
    }

    void Update()
    {
        
    }

    public void FadeIn()
    {
        OnScreenActive.Invoke();
    }

    public void FadeOut()
    {
        OnScreenDeActive.Invoke();
    }

    //public Player GetPlayer()
    //{
    //    return myPlayer;
    //}
}
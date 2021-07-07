using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class LevelProgress : MonoBehaviour
{
    public static LevelProgress instance;

    public delegate void OnScene();
    public static event OnScene OnPreLoad;
    public static event OnScene OnLoadScene;
    public static event OnScene OnSceneLoaded;

    string levelToLoadTo = "";
    string previousRoom;

    void Awake()
    {
        if (instance == null)
        {
            DontDestroyOnLoad(gameObject);
            instance = this;
        }
        else if (instance != this)
        {
            Destroy(gameObject);
        }

        
    }

    public void PreLoadScene(string sceneToLoad)
    {
        previousRoom = levelToLoadTo;

        if (previousRoom == "")
        {
            previousRoom = SceneManager.GetActiveScene().name;
        }

        levelToLoadTo = sceneToLoad;

        Debug.Log(sceneToLoad);
        OnPreLoad.Invoke();
    }

    public void LoadScene()
    {
        //SceneManager.LoadScene(levelToLoadTo, LoadSceneMode.Single);
        //OnLoadScene.Invoke();
        StartCoroutine(ChangeScenes());
    }

    IEnumerator ChangeScenes()
    {
        yield return new WaitForSeconds(1f);
        SceneManager.LoadScene(levelToLoadTo, LoadSceneMode.Single);
    }
}
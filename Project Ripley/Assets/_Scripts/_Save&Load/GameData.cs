using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;
using System.Runtime.Serialization.Formatters.Binary;
using System.IO;

public class GameData : MonoBehaviour
{
    public delegate void OnPlayerData(); // 0 = Player, 1 = Enemy
    public static event OnPlayerData OnSavePlayer;
    public static event OnPlayerData OnLoadPlayer;

    public static GameData data;
    public static AllData aData;

    void Awake()
    {
        if(data == null)
        {
            DontDestroyOnLoad(gameObject);
            data = this;
        }
        else if(data != this)
        {
            Destroy(gameObject);
        }
    }
    
    public void Save()
    {
        

        BinaryFormatter bf = new BinaryFormatter();
        FileStream file = File.Create(Application.persistentDataPath + "/gameData.dat");

        //Save Data Here
        OnSavePlayer.Invoke();

        bf.Serialize(file, aData);
        file.Close();
    }
    public void Load()
    {
        if(File.Exists(Application.persistentDataPath + "/gameData.dat"))
        {
            BinaryFormatter bf = new BinaryFormatter();
            FileStream file = File.Open(Application.persistentDataPath + "/gameData.dat", FileMode.Open);

            aData = (AllData)bf.Deserialize(file);
            file.Close();

            //Load Local Data Here
            OnLoadPlayer.Invoke();
        }
    }
}

[Serializable]
public class AllData
{
    public PlayerData pData;
    public EnemyData eData;
}

[Serializable]
public class PlayerData
{
    public Vector3 position;
    public int health;
    public InventorySO inventorySO;

}

public class EnemyData
{

}
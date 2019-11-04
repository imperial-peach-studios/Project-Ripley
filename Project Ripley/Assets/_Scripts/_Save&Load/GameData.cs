using System;
using System.Collections.Generic;
using System.IO;
using System.Runtime.Serialization.Formatters.Binary;
using UnityEngine;
using UnityEngine.UI;
using UnityEditor;

public class GameData : MonoBehaviour
{
    public delegate void OnPlayerData();
    public static event OnPlayerData OnSavePlayer;
    public static event OnPlayerData OnLoadPlayer;

    public static GameData data;
    public static AllData aData;
    bool load = false;
    float waitAfterDeathTimer = 0;
    [SerializeField] float waitAfterDeathLength;
    [SerializeField] float fadeSpeed;
    [SerializeField] Image blackScreen;

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

        //aData = new AllData();
        //aData.pData = new PlayerData();
       // aData.eData = new EnemyData();
    }

    void Update()
    {
        if(load)
        {
            waitAfterDeathTimer += Time.deltaTime;
            blackScreen.CrossFadeAlpha(1f, fadeSpeed, false);

            if(waitAfterDeathTimer > waitAfterDeathLength)
            {
                OnLoadPlayer.Invoke();
                load = false;
                waitAfterDeathTimer = 0;
            } 
        }
        else
        {
            if(blackScreen.canvasRenderer.GetColor().a != 0)
            {
                blackScreen.enabled = false;
                blackScreen.CrossFadeAlpha(0f, fadeSpeed, false);
            }
        }
    }
    
    public void Save()
    {
        BinaryFormatter bf = new BinaryFormatter();
        FileStream file = File.Create(Application.persistentDataPath + "/gameData.dat");

        //Save Data Here
        OnSavePlayer.Invoke();
        Debug.Log("Save Started");

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

            blackScreen.enabled = true;
            blackScreen.canvasRenderer.SetAlpha(0f);
            load = true;
            Debug.Log("Loaded Started");
        }
    }
}

[Serializable]
public struct AllData
{
    public PlayerData pData;
    //public EnemyData eData;

}

[Serializable]
public struct PlayerData
{
    [SerializeField] float x, y, z;
    [SerializeField] public int health;
    [SerializeField] public List<string> inventoryIndex;
    [SerializeField] public List<float> durabilities;
    [SerializeField] public int primary, secondary, selected;
    int primaryIndex;
    int secondaryIndex;
    int currentWeapon;

    public void SetPosition(Vector3 position)
    {
        this.x = position.x;
        this.y = position.y;
        this.z = position.z;
    }
    public Vector3 GetPosition()
    {
        return new Vector3(x, y, z);
    }

    public void SaveInvetoryData(List<string> inventoryNames, int primaryIndex, int secondaryIndex, int currentWeapon, List<float> durability)
    {
        //this.inventoryIndex = inventoryNames;
        //this.primaryIndex = primaryIndex;
        //this.secondaryIndex = secondaryIndex;
        //this.currentWeapon = currentWeapon;
        //this.durabilities = durability;
    }
    public void SaveInventoriesData(List<string> itemNamesList)
    {
        inventoryIndex = itemNamesList;
    }

    public void SaveEquipmentData(int p, int s, int se)
    {
        primary = p;
        secondary = s;
        selected = se;
    }
    public void LoadInventoryData(ref List<GameObject> inventory, ref int newPrimary, ref int newSecondary, ref int newCurrentWeapon, List<GameObject> allItems)
    {
        //for(int i = 0; i < inventory.Count; i++)
        //{
        //    if(inventory[i] != null)
        //    {
        //        inventory[i] = (GameObject)AssetDatabase.LoadAssetAtPath("Assets/_Prefabs/_InventoryObjects/" + inventoryIndex[i], typeof(GameObject));
        //        for(int x = 0; x < allItems.Count; x++)
        //        {
        //            if(allItems[x].ToString() == inventoryIndex[i])
        //            {
        //                inventory[i] = allItems[x];
        //                //inventory[i].GetComponent<ItemSettings>().SetDurability(durabilities[i]);
        //            }
        //        }
        //    }
        //}

        //newPrimary = primaryIndex;
        //newSecondary = secondaryIndex;
        //newCurrentWeapon = this.currentWeapon;
    }

    public void LoadInventoriesData(ref GameObject[] inventory, List<GameObject> allItems)
    {
        for(int i = 0; i < inventory.Length; i++)
        {
            inventory[i] = null;

            for(int x = 0; x < allItems.Count; x++)
            {
                if(allItems[x].name == inventoryIndex[i])
                {
                    inventory[i] = allItems[x];
                }
            }
        }
    }

    public void LoadEquipmentData(ref int p, ref int s, ref int se)
    {
        p = primary;
        s = secondary;
        se = selected;
    }
}

[Serializable]
public class EnemyData
{
    //int count = 0;


    //public void AddEnemy()
    //{

    //}
}
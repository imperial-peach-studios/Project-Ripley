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
    public static event OnPlayerData BeforeLoadPlayer;

    public static GameData data;
    public static AllData aData;

    bool load = false;
    float waitAfterDeathTimer = 0;
    [SerializeField] float waitAfterDeathLength;
    [SerializeField] float fadeSpeed;
    [SerializeField] Image blackScreen;
    [SerializeField] GameObject dropItemPrefab;

    bool invokeLoad = false;
    int totalAmountOfItemsToLoad = 0;
    int amountOfItemsLoaded = 0;

    [SerializeField] public static List<Items> destroyed = new List<Items>();

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
        aData.iData = new ItemsData();
        aData.eData = new EnemyData();
    }

    void Update()
    {
        if (load)
        {
            waitAfterDeathTimer += Time.deltaTime;
            blackScreen.CrossFadeAlpha(1f, fadeSpeed, false);

            if(waitAfterDeathTimer > waitAfterDeathLength)
            {
                if (!invokeLoad)
                {
                    OnLoadPlayer.Invoke();
                    invokeLoad = true;
                }
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
        Debug.Log("Save Started");
        BinaryFormatter bf = new BinaryFormatter();
        FileStream file = File.Create(Application.persistentDataPath + "/gameData.dat");

        //GameData.aData.eData.destroyedEnemies.Clear();
        GameData.aData.iData.destroyedItems.Clear();
        //destroyed.Clear();

        //Save Data Here
        OnSavePlayer.Invoke();

        bf.Serialize(file, aData);
        file.Close();

        Debug.Log("Save Finished");
    }

    public void Load()
    {
        invokeLoad = false;

        if (File.Exists(Application.persistentDataPath + "/gameData.dat"))
        {
            BinaryFormatter bf = new BinaryFormatter();
            FileStream file = File.Open(Application.persistentDataPath + "/gameData.dat", FileMode.Open);

            Debug.Log("Before Load");
            BeforeLoadPlayer.Invoke();

            EnemyData enemyData = aData.eData;
            for(int k = 0; k  < enemyData.destroyedEnemies.Count; k++)
            {
                Debug.Log("HEJ");
                GameObject newE = GameObject.Find(enemyData.destroyedEnemies[k].objectName);
                Debug.Log("Found " + newE.name != null);

                Debug.Log("Inside Destroyed Enemies");
                EnemyInfo eI = newE.GetComponent<EnemyEvent>().GetEnemyInfo();
                //enemyData.RespawnDestroyesEnemies(eI, newE);

                newE.SetActive(true);
                newE.GetComponent<EnemyHealth>().ResetHealth();
                enemyData.RespawnDestroyesEnemies(ref eI, k);

                Debug.Log("Reset Enemy");

                //if (enemyData.ExistsInDestroyedList(eI, true))
                //{
                //    newE.SetActive(true);
                //    newE.GetComponent<EnemyHealth>().ResetHealth();

                //    Debug.Log("Reset Enemy");
                //}
            }

            ItemsData itemData = aData.iData;
            for (int j = 0; j < itemData.destroyedItems.Count; j++)
            {
                GameObject newI = null;
                //Items iI = null;

                if (itemData.destroyedItems[j].lootedItem)
                {
                    newI = GameObject.Find(itemData.destroyedItems[j].objectName);
                    Debug.Log("Found " + newI.name);
                }
                else
                {
                    newI = Instantiate(dropItemPrefab, itemData.destroyedItems[j].GetPosition(), Quaternion.identity);
                    Debug.Log("Created New " + itemData.destroyedItems[j].objectName);
                }

                //iI = newI.GetComponent<ItemInfo>().I;
                ItemInfo iT = newI.GetComponent<ItemInfo>();

                itemData.RespawnDestroyesItems(ref iT, j);
            }

            Debug.Log("Before Load Finished");

            aData = (AllData)bf.Deserialize(file);
            file.Close();

            totalAmountOfItemsToLoad = enemyData.savedEnemies.Count + itemData.savedItems.Count;
            amountOfItemsLoaded = 0;

            blackScreen.enabled = true;
            blackScreen.canvasRenderer.SetAlpha(0f);
            load = true;
            Debug.Log("Loaded Started");
        }
    }

    public void AllItemsFinishedLoading()
    {
        amountOfItemsLoaded += 1;

        if(amountOfItemsLoaded >= totalAmountOfItemsToLoad)
        {
            amountOfItemsLoaded = totalAmountOfItemsToLoad;
            load = false;
            waitAfterDeathTimer = 0;
            Debug.Log("Finished Loading");
        }
    }
}

[Serializable]
public struct AllData
{
    public PlayerData pData;
    public EnemyData eData;
    public ItemsData iData; 
}

[Serializable]
public struct PlayerData
{
    [SerializeField] float x, y, z;
    [SerializeField] public int health;
    [SerializeField] public List<string> inventoryIndex;
    [SerializeField] public Items[] itemInvetory;
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
    public void SaveInventoriesData(Items[] iI)
    {
        itemInvetory = iI;
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

    public void LoadInventoriesData(ref Items[] iI)
    {
        iI = itemInvetory;
    }

    public void LoadEquipmentData(ref int p, ref int s, ref int se)
    {
        p = primary;
        s = secondary;
        se = selected;
    }
}

[Serializable]
public class ItemsData
{
    [SerializeField] public List<bool> enabledItem = new List<bool>();

    [SerializeField] public List<Items> savedItems = new List<Items>();
    [SerializeField] public List<Items> destroyedItems = new List<Items>();

    [SerializeField] public List<Items> allItemItems = new List<Items>();
    [SerializeField] public List<Items> destroyedList = new List<Items>();

    public int OnNewSave(Items iT)
    {
        if (!savedItems.Contains(iT))
        {
            savedItems.Add(iT);
            Debug.Log("Added " + iT.objectName + " To The Saved List");
        }

        return savedItems.IndexOf(iT); //return index
    }

    public Items NewOnLoad(Items iT)
    {
        for(int i = 0; i < savedItems.Count; i++)
        {
            if(savedItems[i].objectName == iT.objectName)
            {
                Debug.Log("Loaded " + iT.objectName + " From The Saved List");
                return savedItems[i];
            }
        }
        return null;
    }

    public bool ExistsNewInSavedList(Items iT)
    {
        if(iT == null)
        {
            Debug.Log("NULL");
            return false;
        }

        for (int i = 0; i < savedItems.Count; i++)
        {
            if (savedItems[i].objectName == iT.objectName)
            {
                Debug.Log(iT.objectName + " Exists In The Saved List");
                return true;
            }
        }
        Debug.Log("Item Doesnt Exists In The Saved List");
        return false;
    }

    public void AddDestroyedItem(Items iT)
    {
        if (!destroyedItems.Contains(iT))
        {
            Debug.Log("Added " + iT.objectName + " To The Destroyed List");
            destroyedItems.Add(iT);
            Debug.Log(destroyedItems.Count);
        }
    }

    public void ItemRemoveFromDestroyed(Items iT)
    {
        for (int i = 0; i < destroyedItems.Count; i++)
        {
            if (destroyedItems[i].objectName == iT.objectName)
            {
                Debug.Log("Removed From Destroyed List " + iT.objectName);
                destroyedItems.RemoveAt(i);
                return;
            }
        }
    }

    public bool ExistsInDestroyedList(Items iT, bool destroy = false)
    {
        Debug.Log(destroyedItems.Count);
        for (int i = 0; i < destroyedItems.Count; i++)
        {
            if (destroyedItems[i].objectName == iT.objectName)
            {
                Debug.Log("DSJ)");
                for (int j = 0; j < savedItems.Count; j++)
                {
                    if (savedItems[j].objectName == destroyedItems[i].objectName)
                    {
                        float d = savedItems[j].durability;
                        destroyedItems[i].durability = d;
                        savedItems[j] = destroyedItems[i];

                        Debug.Log(savedItems[j].objectName + " Exists In The Destroyed List");

                        if (destroy)
                        {
                            Debug.Log("Removed " + destroyedItems[i].objectName + " From Destroyed List");
                            destroyedItems.RemoveAt(i);
                        }
                        return true;
                    }
                }
            }
        }
        return false;
    }

    public void RespawnDestroyesItems(ref ItemInfo iT, int inter)
    {
        for(int i = 0; i < savedItems.Count; i++)
        {
            if(destroyedItems[inter].objectName == savedItems[i].objectName)
            {
                iT.LoadItem(savedItems[i]);
                Debug.Log("Destroyed Items = Saved Items");
                destroyedItems.RemoveAt(inter);
                break;
            }
        }
    }
}

[Serializable]
public class EnemyData
{
    [SerializeField] public List<EnemyInfo> savedEnemies = new List<EnemyInfo>();
    [SerializeField] public List<EnemyInfo> destroyedEnemies = new List<EnemyInfo>();

    public int OnSave(EnemyInfo eI)
    {
        if(!savedEnemies.Contains(eI))
        {
            savedEnemies.Add(eI);
            Debug.Log("Added  " + eI.objectName);
        }
        return savedEnemies.IndexOf(eI);
    }

    public EnemyInfo OnLoad(EnemyInfo eI)
    {
        for(int i = 0; i < savedEnemies.Count; i++)
        {
            if(savedEnemies[i].objectName == eI.objectName)
            {
                Debug.Log("Loaded " + eI.objectName + " From The Saved List");
                return savedEnemies[i];
            }
        }
        return null;
    }

    public bool ExistsInTheSavedList(EnemyInfo eI)
    {
        if(eI == null)
        {
            return false;
        }

        for(int i = 0; i < savedEnemies.Count; i++)
        {
            if(savedEnemies[i].objectName == eI.objectName)
            {
                Debug.Log(eI.objectName + " Exists In The Saved List");
                return true;
            }
        }
        Debug.Log("Item Doesn't Exists In The Saved List");
        return false;
    }

    public void AddDestroyedEnemy(EnemyInfo eI)
    {
        if (!destroyedEnemies.Contains(eI))
        {
            Debug.Log("Added " + eI.objectName + " To The Destroyed List");
            destroyedEnemies.Add(eI);
        }
    }

    public void EnemyRemovedFromDestroyed(EnemyInfo eI)
    {
        for (int i = 0; i < destroyedEnemies.Count; i++)
        {
            if (destroyedEnemies[i].objectName == eI.objectName)
            {
                destroyedEnemies.RemoveAt(i);
                return;
            }
        }
    }

    public bool ExistsInDestroyedList(EnemyInfo eI, bool destroy = false)
    {
        for (int i = 0; i < destroyedEnemies.Count; i++)
        {
            if (eI.Equals(destroyedEnemies[i]))
            {
                for (int j = 0; j < savedEnemies.Count; j++)
                {
                    if (savedEnemies[j].objectName == destroyedEnemies[i].objectName)
                    {
                        savedEnemies[j] = destroyedEnemies[i];

                        Debug.Log(savedEnemies[j].objectName + " Exists In The Destroyed List");

                        if (destroy)
                        {
                            Debug.Log("Removed " + destroyedEnemies[i].objectName + " From Destroyed List");
                            destroyedEnemies.RemoveAt(i);
                        }
                        return true;
                    }
                }
            }
        }
        return false;
    }

    public void RespawnDestroyesEnemies(ref EnemyInfo eI, int inter)
    {
        for (int i = 0; i < savedEnemies.Count; i++)
        {
            if (savedEnemies[i].objectName == destroyedEnemies[inter].objectName)
            {
                eI = savedEnemies[i];

                destroyedEnemies.RemoveAt(inter);
                break;
            }
        }
    }
}
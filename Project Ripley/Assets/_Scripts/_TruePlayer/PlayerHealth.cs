using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerHealth : MonoBehaviour
{
    [SerializeField] PlayerHP playerHP;
    bool isDead = false;
    bool secondaryDead = false;
    float waitBeforeDeathTimer = 0;
    [SerializeField] float waitBeforeDeathLength;
    
    void Awake()
    {
        GameData.OnSavePlayer += OnSave;
        GameData.OnLoadPlayer += OnLoad;
        //currentHealth = playerHP.Health;
    }

    void Update()
    {
        if(!isDead)
        {
            if(playerHP.Health <= 0)
            {
                playerHP.EqualHP(0);
                isDead = true;
                secondaryDead = true;
            }
            else if(playerHP.Health >= playerHP.MaxHealth)
            {
                playerHP.EqualHP(playerHP.MaxHealth);
            }
        }
        else
        {
            Player.Instance.SetPlayerActive(false);

            waitBeforeDeathTimer += Time.deltaTime;

            if (waitBeforeDeathTimer > waitBeforeDeathLength)
            {
                GameData data = GameData.data;
                data.Load();
                waitBeforeDeathTimer = 0;
            }
        }
    }

    public void DecreaseHealthWith(int value)
    {
        playerHP.LoseHp(value);
    }

    public bool IsDead()
    {
        bool oldDead = secondaryDead;
        secondaryDead = false;
        return oldDead;
    }

    public void OnSave()
    {
        GameData.aData.pData.health = playerHP.Health;
    }

    public void OnLoad()
    {
        playerHP.EqualHP(GameData.aData.pData.health);
        isDead = false;
        secondaryDead = false;

        Player.Instance.SetPlayerActive(true);
    }
}
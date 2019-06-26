using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu]
public class PlayerHP : ScriptableObject
{
    public List<float> playerHP = new List<float>();
    
    [SerializeField]
    int health = 4;
    public int Health
    {
        get
        {
            return health;
        }
    }
    public int currentHPCount = 3;
    public bool canBeBiten = false;
    public bool consumingItem = false;
    private float myConsumeTimer;
    public float MyConsumeTimer {
        get { return myConsumeTimer; }
    }
    private float myConsumeRate;
    public float MyConsumeRate {
        get { return myConsumeRate; }
    }

    [SerializeField]
    int maxHealth = 4;
    public int MaxHealth
    {
        get
        {
            return maxHealth;
        }
    }

    public void Consume(float timer, float rate)
    {
        myConsumeTimer = timer;
        myConsumeRate = rate;
    }

    public void AddHp(int healthAmount)
    {
        health += healthAmount;
        
        if(health > MaxHealth)
        {
            health = maxHealth;
        }
        
        myConsumeTimer = 0;
        myConsumeRate = 0;
    }
    public void LoseHp(int loseHpAmount)
    {
        health -= loseHpAmount;

        if (health < 0)
        {
            health = 0;
        }
    }
}
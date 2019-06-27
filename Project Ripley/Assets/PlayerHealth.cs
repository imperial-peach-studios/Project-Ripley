using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerHealth : MonoBehaviour
{
    [SerializeField] PlayerHP playerHP;
    bool isDead = false;
    bool secondaryDead = false;
    PlayerMovement pM;
    PlayerDash pD;
    PlayerAttack pA;
    NewPlayerInvetory pI;
    InteractionHandler iH;

    void Awake()
    {
        //currentHealth = playerHP.Health;

        pM = GetComponent<PlayerMovement>();
        pD = GetComponent<PlayerDash>();
        pA = GetComponent<PlayerAttack>();
        pI = GetComponent<NewPlayerInvetory>();
        iH = GetComponent<InteractionHandler>();
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
            pM.enabled = false;
            pD.enabled = false;
            pA.enabled = false;
            pI.enabled = false;
            iH.enabled = false;
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
}
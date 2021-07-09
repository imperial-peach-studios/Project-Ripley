using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerHealing : MonoBehaviour
{
    [SerializeField] string myHealingInputName;
    private bool myIsHealing = false;
    private PlayerAnimation myPlayerAnim;

    void Start()
    {
        myPlayerAnim = GetComponentInChildren<PlayerAnimation>();
    }

    void Update()
    {
        if(Player.Instance.CanChangeState(PlayerState.Healing))
        {
            if(myIsHealing == false)
            {
                if (Input.GetButtonDown(myHealingInputName))
                {
                    Player.Instance.UpdateStateTo(PlayerState.Healing);
                    
                    myIsHealing = true;
                }
            }
            else
            {
                if(myPlayerAnim.myFinishedPlayingHealing)
                {
                    Player.Instance.AddHealingHealth();
                    Player.Instance.UpdateStateTo(PlayerState.Idle);
                    myPlayerAnim.ResetHealing();
                    myIsHealing = false;
                }
            }
        }
    }
}
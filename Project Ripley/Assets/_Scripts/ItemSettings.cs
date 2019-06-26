using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class ItemSettings : MonoBehaviour
{
    public MeleeWeaponsSO meleeOS;
    public GunSO gunOS;
    [SerializeField] private float durability;
    [SerializeField] private float maxDurability;
    [SerializeField] private float durabilityDecrease;

    public void UpdateStats()
    {
        if(meleeOS != null)
        {
            maxDurability = meleeOS.durability;
            durabilityDecrease = meleeOS.durabilityDecrease;
        }
        else if(gunOS != null)
        {
            maxDurability = gunOS.durability;
            durabilityDecrease = gunOS.durabilityDecrease;
        }
        durability = maxDurability;
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void Decrease()
    {
        durability -= durabilityDecrease;
    }
}

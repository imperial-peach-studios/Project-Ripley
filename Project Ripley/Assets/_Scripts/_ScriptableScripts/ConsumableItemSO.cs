using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu]
public class ConsumableItemSO : ScriptableObject
{
    public Sprite UIIcon;
    public PlayerHP playerHP;
    public string info;
    public int animationID;

    public Vector2 CollisionBoxSize;
    public Vector2 pickUpBoxSize;

    public bool isConsumableCure = false;

    public void Consume(int hpIncrease, bool myConsumed)
    {
        playerHP.AddHp(hpIncrease);
        myConsumed = true;
    }
}
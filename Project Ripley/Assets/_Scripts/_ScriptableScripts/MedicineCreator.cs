using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

[CreateAssetMenu]
public class MedicineCreator : ScriptableObject
{
    public Sprite UIIcon;
    public PlayerHP playerHP;
    public string info;

    public Vector2 CollisionBoxSize;
    public Vector2 pickUpBoxSize;


    public void Consume(int hpIncrease, bool myConsumed)
    {
        playerHP.AddHp(hpIncrease);
        myConsumed = true;
    }
}
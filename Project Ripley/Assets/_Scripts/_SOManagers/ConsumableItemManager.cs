using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(ItemInfo))]
public class ConsumableItemManager : MonoBehaviour
{
    public ConsumableItemSO consumableItem;
    public PlayerHP playerHP;
    public float consumeTimeRate;
    public bool consuming = false;
    public bool consumed = false;
    public int HPIncrease;
    float consumeTimer = 0;
    float holdTimer = 0;
    float holdTimerRate = 0.1f;

    void Update()
    {
        if (Input.GetMouseButton(0) && consumed == false)
        {
            holdTimer += Time.deltaTime;
            if(holdTimer > holdTimerRate)
            {
                consumeTimer += Time.deltaTime;
                playerHP.consumingItem = true;
                playerHP.Consume(consumeTimer, consumeTimeRate);

                if (consumeTimer > consumeTimeRate)
                {
                    playerHP.AddHp(HPIncrease);
                    consumeTimer = 0;
                    consumed = true;
                    playerHP.consumingItem = false;
                    holdTimer = 0;
                }
            }
        }
        else
        {
            consumeTimer = 0;
            playerHP.consumingItem = false;
            holdTimer = 0;
        }
    }
}
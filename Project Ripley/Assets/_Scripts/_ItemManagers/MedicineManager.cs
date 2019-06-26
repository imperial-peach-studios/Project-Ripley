using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MedicineManager : MonoBehaviour
{
    public MedicineCreator medicineCreator;
    public PlayerHP playerHP;
    public int consumeTimeRate;
    public bool consuming = false;
    public bool consumed = false;
    public int HPIncrease;
    float consumeTimer = 0;

    void Update()
    {
       if(Input.GetMouseButton(0) && consumed == false)
        {
            consumeTimer += Time.deltaTime;
            playerHP.consumingItem = true;
            playerHP.Consume(consumeTimer, consumeTimeRate);

            if(consumeTimer > consumeTimeRate)
            {
                playerHP.AddHp(HPIncrease);
                consumeTimer = 0;
                consumed = true;
                playerHP.consumingItem = false;
            }
        }
        else
        {
            consumeTimer = 0;
            playerHP.consumingItem = false;
        }
    }
}
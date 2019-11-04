using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class PlayerHPManager : MonoBehaviour
{
    public PlayerHP hp;
    public List<GameObject> myChilds = new List<GameObject>();
	public Slider myHPSlider;
    public Image hpImage;
	
	void Awake ()
    {
		//for(int i = 0; i < transform.childCount; i++)
  //      {
  //          if(myChilds.Contains(transform.GetChild(i).gameObject) != true)
  //          {
  //              myChilds.Add(transform.GetChild(i).gameObject);
  //          }
  //      }
		myHPSlider.maxValue = hp.MaxHealth;
	}
	
	void Update ()
    {
        //myHPSlider.value = hp.Health;
        hpImage.fillAmount = hp.Health * 0.1f;

        if (Input.GetKeyDown(KeyCode.G))
        {
            hp.AddHp(1);
        }
        else if (Input.GetKeyDown(KeyCode.H))
        {
            hp.LoseHp(1);
        }

        //for(int i = 0; i < hp.MaxHealth; i++)
        // {
        ///Image myImage = myChilds[i].transform.GetChild(0).GetComponent<Image>();
        //int objectNumber = myChilds[i].name[3] - 48;

        // if(hp.Health >= objectNumber)
        //{
        //   myImage.enabled = true;
        //}
        //else if(hp.Health < objectNumber)
        //{
        //    myImage.enabled = false;
        // }

        //if(hp.Health > 3)
        //{
        //    myImage.color = Color.green;
        //}
        // else if(hp.Health <= 3 && hp.Health >= 2f)
        // {
        //  myImage.color = Color.yellow;
        /*/ *///}
              // else if(hp.Health < 2)
              // {
              //     myImage.color = Color.red;
              //   }
              // }
    }
}
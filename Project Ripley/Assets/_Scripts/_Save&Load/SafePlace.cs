using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class SafePlace : MonoBehaviour
{
    [SerializeField] Text saveText;
    [SerializeField] Text loadText;
    [SerializeField] Text pressText;
    GameData data;
    bool saving = false;

    float waitTimer = 0;
    [SerializeField] float waitLength = 3f;
    [SerializeField] float fadeSpeed;

    void Start()
    {
        data = GameData.data;

        saveText.gameObject.SetActive(true);
        loadText.gameObject.SetActive(true);
        pressText.gameObject.SetActive(true);

        Color newColor = Color.cyan;
        newColor.a = 0;
        saveText.canvasRenderer.SetColor(newColor);
        loadText.canvasRenderer.SetColor(newColor);
        pressText.canvasRenderer.SetColor(newColor);
    }

    void Update()
    {
        if(saving)
        {
            pressText.CrossFadeAlpha(0f, fadeSpeed, false);
            saveText.CrossFadeAlpha(1f, fadeSpeed, false);

            //Save Here

            waitTimer += Time.deltaTime;

            if (waitTimer > waitLength)
            {
                saving = false;
                waitTimer = 0;
            }
        }

        if(!PlayersMovementData.InsideASafeHouse)
        {
            pressText.CrossFadeAlpha(0f, fadeSpeed, false);
        }
        if (saveText.color.a == 1)
        {
            saveText.CrossFadeAlpha(0f, fadeSpeed, false);
        }
    }

    void OnTriggerStay2D(Collider2D collision)
    {
        if(collision.transform.tag == "Player")
        {
            PlayersMovementData.InsideASafeHouse = true;

            if(!saving)
            {
                pressText.CrossFadeAlpha(1f, fadeSpeed, false);

                if (Input.GetKeyDown(KeyCode.E))
                {
                    saving = true;
                }
            }
        }
    }

    void OnTriggerExit2D(Collider2D collision)
    {
        if (collision.transform.tag == "Player")
        {
            PlayersMovementData.InsideASafeHouse = false;
        }
    }
}
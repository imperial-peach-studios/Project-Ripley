using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class InteractableText : MonoBehaviour
{
    Text myText;

    void Start()
    {
        myText = GetComponent<Text>();
        InteractionHandler.OnText += EnableText;
    }

    void Update()
    {
        
    }

    void EnableText(string text, Vector3 position)
    {
        myText.text = text;
    }
}
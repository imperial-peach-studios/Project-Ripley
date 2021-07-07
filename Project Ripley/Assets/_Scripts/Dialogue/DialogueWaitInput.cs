using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Fungus;

public class DialogueWaitInput : MonoBehaviour
{
    [SerializeField] private Flowchart chart;
    [SerializeField] private string fungusMessage;

    private DialogueStarter dialogueStarter;

    [SerializeField] bool waitForPlayer = false;

    void Start()
    {
        if(GetComponent<DialogueStarter>() != null)
        {
            dialogueStarter = GetComponent<DialogueStarter>();
        }
    }

    public void FungusMessage()
    {
        if (!waitForPlayer)
            chart.SendFungusMessage(fungusMessage);
    }
}
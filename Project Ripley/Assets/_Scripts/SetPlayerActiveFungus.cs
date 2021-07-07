using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Fungus;

public class SetPlayerActiveFungus : MonoBehaviour
{
    Flowchart f;
    bool start = false;
    [SerializeField] bool disableAtStart = false;

    public delegate void OnDialogue();
    public static event OnDialogue OnEnablePlayer;
    public static event OnDialogue OnDisablePlayer;

    [SerializeField] float waitAmount;
    bool updateOnce = true;

    private void Awake()
    {
        f = GetComponent<Flowchart>();
        if (disableAtStart)
        {
           // Player.Instance.SetAllMovementActive(false); //OnDisablePlayer.Invoke();
        }
    }

    void Update()
    {
        if (!start)
        {
            if (f.GetExecutingBlocks().Count > 0 && f.SelectedBlock.GetConnectedBlocks().Count >= 0)
            {
                if (!disableAtStart)
                    //Player.Instance.SetAllMovementActive(false); //OnDisablePlayer.Invoke();
                

                start = true;
                updateOnce = true;
            }

            if (f?.GetExecutingBlocks().Count == 0 && f?.SelectedBlock?.GetConnectedBlocks().Count == 0 && disableAtStart)
            {
                StartCoroutine(Wait());
            }
        }
        else
        {
            if (f?.SelectedBlock.GetConnectedBlocks().Count == 0 && f?.GetExecutingBlocks().Count == 0 && updateOnce)
            {
                //Player.Instance.SetAllMovementActive(true); //OnEnablePlayer.Invoke();
                start = false;
                updateOnce = false;
                disableAtStart = false;
            }

            if (f?.GetExecutingBlocks().Count == 0 && f?.SelectedBlock?.GetConnectedBlocks().Count == 0 && disableAtStart)
            {
                start = false;
            }

            return;
        }
    }

    IEnumerator Wait()
    {
        yield return new WaitForSeconds(waitAmount);
        if (!start)
        {
            start = true;
            updateOnce = true;
        }
    }
}
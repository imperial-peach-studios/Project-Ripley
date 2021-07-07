using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

public class ItemInteractor : MonoBehaviour
{
    public UnityEvent OnInteract;

    void Start()
    {
        if(GetComponent<ItemHolder>() != null && OnInteract.GetPersistentEventCount() == 0)
        {
            OnInteract.AddListener(GetComponent<ItemHolder>().AddItem);
        }
    }

    public void Invoke()
    {
        OnInteract.Invoke();
    }
}
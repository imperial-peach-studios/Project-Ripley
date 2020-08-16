using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

public class InteractionManager : MonoBehaviour
{
    public UnityEvent OnInteract;
    
    void Start()
    {
        if(GetComponent<ItemInfo>() != null && OnInteract.GetPersistentEventCount() == 0)
        {
            OnInteract.AddListener(GetComponent<ItemInfo>().TryToAddItemToInventory);
        }
    }
    
    void Update()
    {
        
    }

    public void Invoke()
    {
        OnInteract.Invoke();
        
    }


}
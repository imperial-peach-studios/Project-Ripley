using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

public class InteractionManager : MonoBehaviour
{
    public UnityEvent OnInteract;
    
    void Start()
    {
        
    }
    
    void Update()
    {
        
    }

    public void Invoke()
    {
        OnInteract.Invoke();
    }
}
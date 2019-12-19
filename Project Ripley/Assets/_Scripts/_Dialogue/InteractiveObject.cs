using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;
using UnityEngine.Events;

public class InteractiveObject : MonoBehaviour
{
    public UnityEvent OnInteract;
    public UnityEvent OnClosest;

    public void Interact()
    {
        if (OnInteract != null)
            OnInteract.Invoke();
    }
}

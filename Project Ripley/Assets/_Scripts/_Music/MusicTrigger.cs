using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using FMOD;
using FMODUnity;

public class MusicTrigger : MonoBehaviour
{
    // Start is called before the first frame update
    public StudioEventEmitter musicEmitter;

    private void Update()
    {
        if (Input.GetKeyDown(KeyCode.F))
        {
            Detection();
        }
    }
    public void Detection()
    {
        musicEmitter.EventInstance.setParameterByName("Combat", 1);
    }
}

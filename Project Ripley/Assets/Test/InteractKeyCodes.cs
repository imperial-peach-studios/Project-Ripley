using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[System.Serializable]
public class InteractKeyCodes
{
    public KeyCode key;
    public int index;
    public List<InteractInfo> iInfos = new List<InteractInfo>();
}
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[System.Serializable]
public class PlaySound
{
    [SerializeField] string idName;
    [SerializeField] int ID;
    [SerializeField] float heardRadius;
    [SerializeField] Color circleColor;
    [SerializeField] bool soundPlayed = false;
    [SerializeField] bool hideCircle;
    [SerializeField] bool ignore;
    
    public bool GetSound()
    {
        bool currentSound = soundPlayed;
        soundPlayed = false;
        return currentSound;
    }

    public Color GetColor()
    {
        return circleColor;
    }

    public float GetRadius()
    {
        return heardRadius;
    }

    public bool HideCircle()
    {
        return hideCircle;
    }

    public bool Ignore()
    {
        return ignore;
    }

    public void ActivateSound()
    {
        soundPlayed = true;
    }

    public void StopSound()
    {
        soundPlayed = false;
    }

    public void S(int id)
    {
        
    }
}
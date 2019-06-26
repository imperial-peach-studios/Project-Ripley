using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlaySoundManager : MonoBehaviour
{
    [SerializeField] bool hideAllCircles;
    [SerializeField] bool ignoreAll;
    [SerializeField] List<PlaySound> soundPlayer = new List<PlaySound>();
    int currentID;
   
    public void ActivateSound(int id)
    {
        soundPlayer[id].ActivateSound();
        currentID = id;
    }

    public void StopSound(int id)
    {
        soundPlayer[id].StopSound();
        currentID = id;
    }

    public bool IsInRange(Vector2 target)
    {
        if(!ignoreAll)
        {
            float length = Vector2.Distance(transform.position, target);

            if (soundPlayer[currentID].GetSound() && length < soundPlayer[currentID].GetRadius() && !soundPlayer[currentID].Ignore())
            {
                return true;
            }
            else
            {
                return false;
            }
        }
        return false;
    }
    
    void OnDrawGizmosSelected()
    {
        if(!hideAllCircles)
        {
            for (int i = 0; i < soundPlayer.Count; i++)
            {
                if (!soundPlayer[i].HideCircle())
                {
                    Gizmos.color = soundPlayer[i].GetColor();
                    Gizmos.DrawWireSphere(transform.position, soundPlayer[i].GetRadius());
                }
            }
        }
    }
}
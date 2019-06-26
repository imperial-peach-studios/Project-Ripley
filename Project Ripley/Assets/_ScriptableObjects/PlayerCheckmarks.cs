using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu]
public class PlayerCheckmarks : ScriptableObject
{
    public enum Checkmarks
    {
        GotBall,
        GotBrick,
    }
    public Checkmarks checkMarks;
}
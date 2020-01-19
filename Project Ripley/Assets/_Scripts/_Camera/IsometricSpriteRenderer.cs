﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[ExecuteInEditMode]
public class IsometricSpriteRenderer : MonoBehaviour
{
	void Update ()
    {
        GetComponent<SpriteRenderer>().sortingOrder = (int)(transform.position.y * -10);
	}
}
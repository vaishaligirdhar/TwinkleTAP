﻿using UnityEngine;

public class Indicator : MonoBehaviour
{
    float scale;

    void Update ()
    {
        scale = 1.0f + (scale - 1.0f) * Mathf.Exp (-5.0f * Time.deltaTime);
        transform.localScale = Vector3.one * scale;
    }

    void OnNoteOn ()
    {
        scale = 1.5f;
    }
}
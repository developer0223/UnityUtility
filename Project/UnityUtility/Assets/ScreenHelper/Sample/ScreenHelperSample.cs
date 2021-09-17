// System
using System;
using System.Collections;
using System.Collections.Generic;

// Unity
using UnityEngine;
using UnityEngine.UI;

// Project
// Alias

public class ScreenHelperSample : MonoBehaviour
{
    private void Update()
    {
        if (Input.GetKeyDown(KeyCode.Alpha1))
        {
            ScreenHelper.FadeIn(1.0f);
        }

        if (Input.GetKeyDown(KeyCode.Alpha2))
        {
            ScreenHelper.FadeOut(1.0f);

        }
    }
}
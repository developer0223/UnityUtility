// System
using System;
using System.Collections;
using System.Collections.Generic;

// Unity
using UnityEngine;
using UnityEngine.UI;

// Project
// Alias

public class ImageHelperSample : MonoBehaviour
{
    public Image targetImage = null;
    public float fadeDuration = 1.0f;

    private void Update()
    {
        if (Input.GetKeyDown(KeyCode.Alpha1))
        {
            ImageHelper.FadeAlpha(targetImage, 1.0f, fadeDuration);
        }

        if (Input.GetKeyDown(KeyCode.Alpha2))
        {
            ImageHelper.FadeAlpha(targetImage, 0.1f, fadeDuration);
        }
    }
}
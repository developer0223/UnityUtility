// System
using System;
using System.Collections;
using System.Collections.Generic;

// Unity
using UnityEngine;
using UnityEngine.UI;

// Project
// Alias

public class LoadingScreenSample : MonoBehaviour
{
    private void Start()
    {
        EnableLoadingScreen();
        Invoke(nameof(DisableLoadingScreen), 1.0f);
    }


    private void EnableLoadingScreen()
    {
        LoadingScreen.Show();
    }

    private void DisableLoadingScreen()
    {
        LoadingScreen.Hide();
    }
}
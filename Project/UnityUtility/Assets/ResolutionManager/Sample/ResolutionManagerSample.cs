// System
using System;
using System.Collections;
using System.Collections.Generic;

// Unity
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

// Project
// Alias

public class ResolutionManagerSample : MonoBehaviour
{
    public Text txt_device_resolution = null;

    private void Awake()
    {
        LogDeviceResolution();
    }

    private void LogDeviceResolution()
    {
        string resolutionText = $"Screen Width : {Screen.width}\nScreen Height : {Screen.height}";
        txt_device_resolution.text = resolutionText;
    }

    public void MoveScene()
    {
        SceneManager.LoadScene("ResolutionManagerSample2");
    }
}
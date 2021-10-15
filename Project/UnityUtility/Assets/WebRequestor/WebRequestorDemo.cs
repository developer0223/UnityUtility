// System
using System;
using System.Collections;
using System.Collections.Generic;

// Unity
using UnityEngine;
using UnityEngine.UI;

// Project
// Alias

public class WebRequestorDemo : MonoBehaviour
{
    private void Start()
    {
        WebRequestor.GetRequest(WebRequestor.URL.Google, (success, result) =>
        {
            Debug.Log($"result : {(success ? "success" : "fail")} | result : {result}");
        });
    }
}
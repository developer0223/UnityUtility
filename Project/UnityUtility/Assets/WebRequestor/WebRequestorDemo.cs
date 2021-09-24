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
    public WebRequestor requestor = null;

    private void Start()
    {
        requestor.GetRequest(WebRequestor.URI.Google, (success, result) =>
        {
            Debug.Log($"result : {(success ? "success" : "fail")} | result : {result}");
        });
    }

}
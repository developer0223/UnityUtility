// System
using System;
using System.Collections;
using System.Collections.Generic;

// Unity
using UnityEngine;
using UnityEngine.UI;

// Project
using developer0223.WebRequestor.V2;

// Alias

public class WebRequestorTest : MonoBehaviour
{
    private void Start()
    {
        WebRequestConfig config = new WebRequestConfig()
        {
            url = "http://192.168.1.142:8081/api/test",
            query = new Dictionary<string, string>(),
            body = null,
            callback = (responseCode, result) =>
            {
                Debug.Log($"Requested finished  with result {responseCode}.\n{result}");
            }
        };

        WebRequestor.CreateGetRequest(config);
    }
}

public class WebRequestConfig
{
    public string url = string.Empty;
    public Dictionary<string, string> query = new Dictionary<string, string>();
    public object body = null;
    public Action<long, string> callback = null;
}
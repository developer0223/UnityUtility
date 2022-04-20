// System
using System;
using System.Collections;
using System.Collections.Generic;

// Unity
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.Networking;

// Project
// Alias

public class HttpRequestorHelper : MonoBehaviour
{
    public HttpRequestor requestor = null;

    public static HttpRequestorHelper Instance = null;

    private void Awake()
    {
        Instance = this;
        requestor = HttpRequestor.GetOrCreate();
    }

    public bool Login(string email, string password)
    {
        UnityWebRequest request = new UnityWebRequest();
        //request.SetRequestHeader("Authorization", "Bearer " + jwtToken)

        return false;
    }
}
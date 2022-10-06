// System
using System;
using System.Text;
using System.Collections;
using System.Collections.Generic;

// Unity
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.Networking;

// Project
using developer0223.WebRequestor;

// Alias

public class AWSRequestSample : MonoBehaviour
{
    private void Start()
    {
        StartCoroutine(Co_Post("http://3.35.16.23:port/create?type=t2.micro", (responseCode, result) =>
        {
            Debug.Log($"ResponseCode: {responseCode}");
            Debug.Log($"result: {result}");
        }));
    }

    private IEnumerator Co_Post(string url, Action<long, string> callback)
    {
        using UnityWebRequest request = new UnityWebRequest(url, "POST"); // .SetHeaders(defaultRequestHeaders);
        request.downloadHandler = (DownloadHandler)new DownloadHandlerBuffer();
        request.timeout = 10;

        yield return request.SendWebRequest();
        callback?.Invoke(request.responseCode, request.downloadHandler.text.Trim());
    }
}
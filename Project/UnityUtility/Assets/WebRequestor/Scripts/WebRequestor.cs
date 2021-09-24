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

public class WebRequestor : MonoBehaviour
{
    public class URI
    {
        public static string Google = "https://www.google.com/";
        public static string DB = "";
    }

    public static readonly string ERROR_CODE = "ERROR";

    public void GetRequest(string uri, Action<bool, string> callback)
    {
        StartCoroutine(Co_GetRequest(uri, (result) =>
        {
            callback?.Invoke(result != ERROR_CODE, result);
        }));
    }

    public IEnumerator Co_GetRequest(string uri, Action<string> callback)
    {
        using (UnityWebRequest webRequest = UnityWebRequest.Get(uri))
        {
            yield return webRequest.SendWebRequest();

            string[] pages = uri.Split('/');
            int page = pages.Length - 1;

            switch (webRequest.result)
            {
                case UnityWebRequest.Result.ConnectionError:
                case UnityWebRequest.Result.DataProcessingError:
                    callback?.Invoke(ERROR_CODE);
                    break;
                case UnityWebRequest.Result.ProtocolError:
                    callback?.Invoke(ERROR_CODE);
                    break;
                case UnityWebRequest.Result.Success:
                    callback?.Invoke(webRequest.downloadHandler.text);
                    break;
            }
        }
    }
}
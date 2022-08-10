namespace developer0223.WebRequestor.V2
{
    // System
    using System;

    using System.Threading;
    using System.Threading.Tasks;

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
        private static int DefaultTimeOutSeconds = 10;

        public static Task CreateGetRequest(string url, Dictionary<string, string> query, object body, Action<long, string> callback)
        {
            return SendGetRequest(new WebRequestConfig()
            {
                url = url,
                query = query,
                body = body,
                callback = callback
            });
        }

        public static Task CreateGetRequest(WebRequestConfig config)
        {
            return SendGetRequest(config);
        }

        public static async Task SendGetRequest(WebRequestConfig config)
        {
            UnityWebRequest request = UnityWebRequest.Get(config.url);
            request.timeout = DefaultTimeOutSeconds;

            UnityWebRequest.Result result = await request.SendWebRequest();

            config.callback?.Invoke(request.responseCode, request.downloadHandler.text);
        }
    }
}
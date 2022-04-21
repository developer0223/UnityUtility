// System
using System.Collections.Generic;

// Unity
using UnityEngine.Networking;

public static class UnityWebRequestExtension
{
    public static UnityWebRequest SetHeaders(this UnityWebRequest request, Dictionary<string, string> headers)
    {
        foreach (KeyValuePair<string, string> header in headers)
        {
            request.SetRequestHeader(header.Key, header.Value);
        }

        return request;
    }
}
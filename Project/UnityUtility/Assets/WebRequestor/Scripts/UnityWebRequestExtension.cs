namespace developer0223.WebRequestor
{
    // System
    using System.Collections.Generic;

    // Unity
    using UnityEngine.Networking;

    /// <summary>
    /// Extension methods for UnityEngine.Networking.UnityWebRequest class.
    /// </summary>
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
}
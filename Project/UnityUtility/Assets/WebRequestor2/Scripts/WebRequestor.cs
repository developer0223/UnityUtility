namespace developer0223.WebRequestor.v2
{
    // System
    using System;
    using System.Collections;
    using System.Collections.Generic;

    // Unity
    using UnityEngine;
    using UnityEngine.UI;

    // Project
    // Alias

    public class WebRequestor : MonoBehaviour
    {
        #region Local Singleton Class
        private static WebRequestor instance = null;
        private static WebRequestor Instance
        {
            get
            {
                return instance;
            }
            set
            {
                if (instance)
                {
                    Debug.LogError($"You are overriding {typeof(WebRequestor)}'s Instance. Destroying automatically previous instance's gameobject and reassign new value. But this should not be happen.");
                    Destroy(instance.gameObject);
                }

                instance = value;
            }
        }

        private static WebRequestor GetOrCreate()
        {
            if (Instance == null)
            {
                GameObject _gameObject = new GameObject(nameof(WebRequestor));
                Instance = _gameObject.AddComponent<WebRequestor>();
            }

            return Instance;
        }
        #endregion

        #region Unity Callbacks
        private void Awake()
        {
            if (Instance)
            {
                Debug.LogWarning($"{nameof(WebRequestor)} already exists. Destroy current one. ({gameObject.name})");
                Destroy(this.gameObject);
                return;
            }

            if (Instance == null)
            {
                Debug.LogWarning($"{nameof(WebRequestor)} instantiated but WebRequestor.Instance is null. Assign value automatically. ({gameObject.name})");
                Instance = this;
                return;
            }
        }

        private void OnDestroy()
        {
            if (Instance != null)
            {
                Instance = null;
            }
        }
        #endregion

        #region JWT (Json-Web-Token)
        private static string accessToken = string.Empty;
        private static string refreshToken = string.Empty;

        public static string GetJwtAccessToken() => accessToken;
        public static void SetJwtAccessToken(string token)
        {
            accessToken = token;
            AddDefaultRequestHeaders("Authorization", token);
            //AddDefaultRequestHeaders("Authorization", "Bearer " + token);
        }

        private static string GetJwtRefreshToken() => refreshToken;
        public static void SetJwtRefreshToken(string token)
        {
            refreshToken = token;
        }
        #endregion

        #region Settings
        private int timeoutSeconds = 10;
        private string baseUrl = string.Empty;
        private Dictionary<string, string> defaultRequestHeaders = new Dictionary<string, string>();

        public static void SetTimeoutSeconds(int seconds)
        {
            WebRequestor requestor = GetOrCreate();
            requestor.timeoutSeconds = seconds;
        }

        public static void SetBaseURL(string url)
        {
            WebRequestor requestor = GetOrCreate();
            requestor.baseUrl = url;
        }

        public static void SetDefaultRequestHeaders(Dictionary<string, string> headers)
        {
            if (headers == null)
                headers = new Dictionary<string, string>();

            WebRequestor requestor = GetOrCreate();
            requestor.defaultRequestHeaders = headers;
        }

        public static void AddDefaultRequestHeaders(string key, string value)
        {
            WebRequestor requestor = GetOrCreate();
            RemoveDefaultRequestHeaders(key);
            requestor.defaultRequestHeaders.Add(key, value);
        }

        public static void RemoveDefaultRequestHeaders(string key)
        {
            WebRequestor requestor = GetOrCreate();
            if (requestor.defaultRequestHeaders.ContainsKey(key))
            {
                requestor.defaultRequestHeaders.Remove(key);
            }
        }
        #endregion

        #region Consts
        private static readonly string METHOD_GET = "GET";
        private static readonly string METHOD_POST = "POST";
        private static readonly string METHOD_PUT = "PUT";
        private static readonly string METHOD_DELETE = "DELETE";
        #endregion
    }
}
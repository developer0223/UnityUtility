namespace developer0223.WebRequestor
{
    // System
    using System;
    using System.IO;
    using System.Linq;
    using System.Text;
    using System.Collections;
    using System.Collections.Generic;

    // Unity
    using UnityEngine;
    using UnityEngine.Networking;

    // Project
    using LitJson;

    // Alias

    public class WebRequestor : MonoBehaviour
    {
        #region Local Singleton
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
                DontDestroyOnLoad(gameObject);
                return;
            }
        }
        #endregion

        #region JWT (Json-Web-Token)
        private string accessToken = string.Empty;
        private string refreshToken = string.Empty;

        public static string GetJwtAccessToken() => GetOrCreate().accessToken;
        public static void SetJwtAccessToken(string token)
        {
            GetOrCreate().accessToken = token;
            //AddDefaultRequestHeaders("Authorization", "Bearer " + token);
            AddDefaultRequestHeaders("Authorization", token);
        }

        private static string GetJwtRefreshToken() => GetOrCreate().refreshToken;
        public static void SetJwtRefreshToken(string token)
        {
            GetOrCreate().refreshToken = token;
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

        #region Get Request
        public static void Get(string url, Dictionary<string, string> queryData, Action<long, string> callback)
        {
            WebRequestor requestor = GetOrCreate();
            string query = DictionaryToHttpQuery(queryData);
            string generatedUrl = CombineUrlWithQuery(url, query);
            requestor.StartCoroutine(requestor.Co_Get(generatedUrl, callback));
        }

        private IEnumerator Co_Get(string url, Action<long, string> callback)
        {
            using UnityWebRequest request = new UnityWebRequest(url, METHOD_GET).SetHeaders(defaultRequestHeaders);
            request.downloadHandler = (DownloadHandler)new DownloadHandlerBuffer();
            request.timeout = timeoutSeconds;

            yield return request.SendWebRequest();
            callback?.Invoke(request.responseCode, request.downloadHandler.text.Trim());
        }
        #endregion

        #region Post Request
        public static void Post(string url, Dictionary<string, string> queryData, Action<long, string> callback)
        {
            WebRequestor requestor = GetOrCreate();
            string query = DictionaryToHttpQuery(queryData);
            string generatedUrl = CombineUrlWithQuery(url, query);
            requestor.StartCoroutine(requestor.Co_Post(generatedUrl, string.Empty, callback));
        }

        public static void Post(string url, string bodyJson, Action<long, string> callback)
        {
            WebRequestor requestor = GetOrCreate();
            requestor.StartCoroutine(requestor.Co_Post(url, bodyJson, callback));
        }

        public static void Post(string url, Dictionary<string, string> queryData, string bodyJson, Action<long, string> callback)
        {
            WebRequestor requestor = GetOrCreate();
            string query = DictionaryToHttpQuery(queryData);
            string generatedUrl = CombineUrlWithQuery(url, query);
            requestor.StartCoroutine(requestor.Co_Post(generatedUrl, bodyJson, callback));
        }

        public static void Post(string url, object unformattedJson, Action<long, string> callback)
        {
            WebRequestor requestor = GetOrCreate();
            string bodyJson = JsonMapper.ToJson(unformattedJson);
            string generatedUrl = CombineUrlWithQuery(url, string.Empty);
            requestor.StartCoroutine(requestor.Co_Post(generatedUrl, bodyJson, callback));
        }

        public static void Post(string url, Dictionary<string, string> queryData, object unformattedJson, Action<long, string> callback)
        {
            WebRequestor requestor = GetOrCreate();
            string query = DictionaryToHttpQuery(queryData);
            string generatedUrl = CombineUrlWithQuery(url, query);
            string bodyJson = JsonMapper.ToJson(unformattedJson);
            requestor.StartCoroutine(requestor.Co_Post(generatedUrl, bodyJson, callback));
        }

        private IEnumerator Co_Post(string url, string bodyJson, Action<long, string> callback)
        {
            using UnityWebRequest request = new UnityWebRequest(url, METHOD_POST).SetHeaders(defaultRequestHeaders);
            byte[] jsonBytes = new UTF8Encoding().GetBytes(bodyJson);

            request.uploadHandler = (UploadHandler)new UploadHandlerRaw(jsonBytes);
            request.downloadHandler = (DownloadHandler)new DownloadHandlerBuffer();
            request.timeout = timeoutSeconds;

            yield return request.SendWebRequest();
            callback?.Invoke(request.responseCode, request.downloadHandler.text.Trim());
        }
        #endregion

        #region Put Request
        public static void Put(string url, string bodyJson, Action<long, string> callback)
        {
            WebRequestor requestor = GetOrCreate();
            string generatedUrl = CombineUrlWithQuery(url, string.Empty);
            requestor.StartCoroutine(requestor.Co_Put(generatedUrl, bodyJson, callback));
        }

        public static void Put(string url, Dictionary<string, string> queryData, string bodyJson, Action<long, string> callback)
        {
            WebRequestor requestor = GetOrCreate();
            string query = DictionaryToHttpQuery(queryData);
            string generatedUrl = CombineUrlWithQuery(url, query);
            requestor.StartCoroutine(requestor.Co_Put(generatedUrl, bodyJson, callback));
        }

        public static void Put(string url, object unformattedJson, Action<long, string> callback)
        {
            WebRequestor requestor = GetOrCreate();
            string bodyJson = JsonMapper.ToJson(unformattedJson);
            string generatedUrl = CombineUrlWithQuery(url, string.Empty);
            requestor.StartCoroutine(requestor.Co_Put(generatedUrl, bodyJson, callback));
        }

        public static void Put(string url, Dictionary<string, string> queryData, object unformattedJson, Action<long, string> callback)
        {
            WebRequestor requestor = GetOrCreate();
            string query = DictionaryToHttpQuery(queryData);
            string generatedUrl = CombineUrlWithQuery(url, query);
            string bodyJson = JsonMapper.ToJson(unformattedJson);
            requestor.StartCoroutine(requestor.Co_Put(generatedUrl, bodyJson, callback));
        }

        private IEnumerator Co_Put(string url, string bodyJson, Action<long, string> callback)
        {
            using UnityWebRequest request = new UnityWebRequest(url, METHOD_PUT).SetHeaders(defaultRequestHeaders);
            byte[] jsonBytes = new UTF8Encoding().GetBytes(bodyJson);

            request.uploadHandler = new UploadHandlerRaw(jsonBytes);
            request.downloadHandler = new DownloadHandlerBuffer();
            request.timeout = timeoutSeconds;

            yield return request.SendWebRequest();
            callback?.Invoke(request.responseCode, request.downloadHandler.text.Trim());
        }
        #endregion

        #region Delete Request
        public static void Delete(string url, string bodyJson, Action<long, string> callback)
        {
            WebRequestor requestor = GetOrCreate();
            string generatedUrl = CombineUrlWithQuery(url, string.Empty);
            requestor.StartCoroutine(requestor.Co_Delete(generatedUrl, bodyJson, callback));
        }

        public static void Delete(string url, Dictionary<string, string> queryData, string bodyJson, Action<long, string> callback)
        {
            WebRequestor requestor = GetOrCreate();
            string query = DictionaryToHttpQuery(queryData);
            string generatedUrl = CombineUrlWithQuery(url, query);
            requestor.StartCoroutine(requestor.Co_Delete(generatedUrl, bodyJson, callback));
        }

        public static void Delete(string url, object unformattedJson, Action<long, string> callback)
        {
            WebRequestor requestor = GetOrCreate();
            string bodyJson = JsonMapper.ToJson(unformattedJson);
            string generatedUrl = CombineUrlWithQuery(url, string.Empty);
            requestor.StartCoroutine(requestor.Co_Delete(generatedUrl, bodyJson, callback));
        }

        public static void Delete(string url, Dictionary<string, string> queryData, object unformattedJson, Action<long, string> callback)
        {
            WebRequestor requestor = GetOrCreate();
            string query = DictionaryToHttpQuery(queryData);
            string generatedUrl = CombineUrlWithQuery(url, query);
            string bodyJson = JsonMapper.ToJson(unformattedJson);
            requestor.StartCoroutine(requestor.Co_Delete(generatedUrl, bodyJson, callback));
        }

        private IEnumerator Co_Delete(string url, string bodyJson, Action<long, string> callback)
        {
            using UnityWebRequest request = new UnityWebRequest(url, METHOD_DELETE).SetHeaders(defaultRequestHeaders);
            byte[] jsonBytes = new UTF8Encoding().GetBytes(bodyJson);

            request.uploadHandler = new UploadHandlerRaw(jsonBytes);
            request.downloadHandler = new DownloadHandlerBuffer();
            request.timeout = timeoutSeconds;

            yield return request.SendWebRequest();
            callback?.Invoke(request.responseCode, request.downloadHandler.text.Trim());
        }
        #endregion

        #region Image
        public static void Image(string url, string fileName, Action<bool, Sprite> callback)
        {
            WebRequestor requestor = GetOrCreate();
            string generatedUrl = CombineUrlWithQuery(url, string.Empty);
            requestor.StartCoroutine(requestor.Co_Image(generatedUrl, fileName, callback));
        }

        private IEnumerator Co_Image(string url, string fileName, Action<bool, Sprite> callback)
        {
            string savePath = Path.Combine(Application.persistentDataPath, fileName);
            if (File.Exists(savePath))
            {
                byte[] imageBytes = File.ReadAllBytes(savePath);

                Texture2D texture = new Texture2D(0, 0);
                texture.LoadImage(imageBytes);
                texture.Apply();

                Rect rect = new Rect(0, 0, texture.width, texture.height);
                Sprite sprite = Sprite.Create(texture, rect, new Vector2(0.5f, 0.5f));

                callback?.Invoke(true, sprite);
                yield break;
            }

            using UnityWebRequest request = UnityWebRequestTexture.GetTexture(url);
            request.timeout = timeoutSeconds;
            request.downloadHandler = new DownloadHandlerTexture();

            yield return request.SendWebRequest();

            if (request.responseCode == ResponseCode.OK)
            {
                Texture texture = ((DownloadHandlerTexture)request.downloadHandler).texture;
                Rect rect = new Rect(0, 0, texture.width, texture.height);
                Sprite sprite = Sprite.Create((Texture2D)texture, rect, new Vector2(0.5f, 0.5f));

                byte[] imageBytes = request.downloadHandler.data;
                File.WriteAllBytes(Path.Combine(Application.persistentDataPath, fileName), imageBytes);

                callback?.Invoke(true, sprite);
                yield break;
            }

            callback?.Invoke(false, null);
        }
        #endregion

        #region Utility
        private static string DictionaryToHttpQuery(Dictionary<string, string> dictionary)
        {
            if (dictionary.Count == 0)
                return string.Empty;

            string result = "?";
            List<KeyValuePair<string, string>> dictionaryList = dictionary.ToList();
            for (int i = 0; i < dictionaryList.Count; i++)
            {
                KeyValuePair<string, string> current = dictionaryList[i];
                result += $"{current.Key}={current.Value}";

                if (i != dictionaryList.Count - 1)
                    result += "&";
            }

            return result;
        }

        private static string CombineUrlWithQuery(string url, string queryData)
        {
            if (!url.Contains("http"))
            {
                if (!url.StartsWith("/"))
                    url = $"/{url}";

                url = GetOrCreate().baseUrl + url;
            }
            return $"{url}{queryData}";
        }
        #endregion
    }
}
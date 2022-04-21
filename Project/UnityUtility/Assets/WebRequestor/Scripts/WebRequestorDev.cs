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

    public class WebRequestorDev : MonoBehaviour
    {
        #region Local Singleton
        private static WebRequestorDev Instance = null;

        private static WebRequestorDev GetOrCreate()
        {
            if (Instance == null)
            {
                GameObject _gameObject = new GameObject(nameof(WebRequestorDev));
                Instance = _gameObject.AddComponent<WebRequestorDev>();
            }

            return Instance;
        }
        #endregion

        #region JWT (Json-Web-Token)
        private string accessToken = string.Empty;
        private string refreshToken = string.Empty;

        public static string GetJwtAccessToken() => GetOrCreate().accessToken;
        public static void SetJwtAccessToken(string token) => GetOrCreate().accessToken = token;

        private static string GetJwtRefreshToken() => GetOrCreate().refreshToken;
        private static void SetJwtRefreshToken(string token) => GetOrCreate().refreshToken = token;
        #endregion

        #region Settings
        private int TIMEOUT_SECONDS = 10;
        private string BASE_URL = string.Empty;
        private Dictionary<string, string> DefaultRequestHeaders = new Dictionary<string, string>();

        public static void SetTimeoutSeconds(int seconds) => GetOrCreate().TIMEOUT_SECONDS = seconds;
        public static void SetBaseURL(string url) => GetOrCreate().BASE_URL = url;
        public static void SetDefaultRequestHeaders(Dictionary<string, string> headers) => GetOrCreate().DefaultRequestHeaders = headers;
        public static void AddDefaultRequestHeaders(string key, string value) => GetOrCreate().DefaultRequestHeaders.Add(key, value);
        public static void RemoveDefaultRequestHeaders(string key) => GetOrCreate().DefaultRequestHeaders.Remove(key);
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
            WebRequestorDev requestor = GetOrCreate();
            string query = DictionaryToHttpQuery(queryData);
            string generatedUrl = CombineUrlWithQuery(url, query);
            requestor.StartCoroutine(requestor.Co_Get(generatedUrl, callback));
        }

        private IEnumerator Co_Get(string url, Action<long, string> callback)
        {
            using UnityWebRequest request = new UnityWebRequest(url, METHOD_GET).SetHeaders(DefaultRequestHeaders);
            request.timeout = TIMEOUT_SECONDS;

            yield return request.SendWebRequest();
            callback?.Invoke(request.responseCode, request.downloadHandler.text);
        }
        #endregion

        #region Post Request
        public static void Post(string url, string bodyJson, Action<long, string> callback)
        {
            WebRequestorDev requestor = GetOrCreate();
            requestor.StartCoroutine(requestor.Co_Post(url, bodyJson, callback));
        }

        public static void Post(string url, Dictionary<string, string> queryData, string bodyJson, Action<long, string> callback)
        {
            WebRequestorDev requestor = GetOrCreate();
            string query = DictionaryToHttpQuery(queryData);
            string generatedUrl = CombineUrlWithQuery(url, query);
            requestor.StartCoroutine(requestor.Co_Post(generatedUrl, bodyJson, callback));
        }

        public static void Post(string url, object unformattedJson, Action<long, string> callback)
        {
            WebRequestorDev requestor = GetOrCreate();
            string bodyJson = JsonMapper.ToJson(unformattedJson);
            requestor.StartCoroutine(requestor.Co_Post(url, bodyJson, callback));
        }

        public static void Post(string url, Dictionary<string, string> queryData, object unformattedJson, Action<long, string> callback)
        {
            WebRequestorDev requestor = GetOrCreate();
            string query = DictionaryToHttpQuery(queryData);
            string generatedUrl = CombineUrlWithQuery(url, query);
            string bodyJson = JsonMapper.ToJson(unformattedJson);
            requestor.StartCoroutine(requestor.Co_Post(generatedUrl, bodyJson, callback));
        }

        private IEnumerator Co_Post(string url, string bodyJson, Action<long, string> callback)
        {
            using UnityWebRequest request = new UnityWebRequest(url, METHOD_POST).SetHeaders(DefaultRequestHeaders);
            byte[] jsonBytes = new UTF8Encoding().GetBytes(bodyJson);

            request.uploadHandler = new UploadHandlerRaw(jsonBytes);
            request.downloadHandler = new DownloadHandlerBuffer();
            request.timeout = TIMEOUT_SECONDS;

            yield return request.SendWebRequest();
            callback?.Invoke(request.responseCode, request.downloadHandler.text);
        }
        #endregion

        #region Put Request
        public static void Put(string url, string bodyJson, Action<long, string> callback)
        {
            WebRequestorDev requestor = GetOrCreate();
            requestor.StartCoroutine(requestor.Co_Put(url, bodyJson, callback));
        }

        public static void Put(string url, Dictionary<string, string> queryData, string bodyJson, Action<long, string> callback)
        {
            WebRequestorDev requestor = GetOrCreate();
            string query = DictionaryToHttpQuery(queryData);
            string generatedUrl = CombineUrlWithQuery(url, query);
            requestor.StartCoroutine(requestor.Co_Put(generatedUrl, bodyJson, callback));
        }

        public static void Put(string url, object unformattedJson, Action<long, string> callback)
        {
            WebRequestorDev requestor = GetOrCreate();
            string bodyJson = JsonMapper.ToJson(unformattedJson);
            requestor.StartCoroutine(requestor.Co_Put(url, bodyJson, callback));
        }

        public static void Put(string url, Dictionary<string, string> queryData, object unformattedJson, Action<long, string> callback)
        {
            WebRequestorDev requestor = GetOrCreate();
            string query = DictionaryToHttpQuery(queryData);
            string generatedUrl = CombineUrlWithQuery(url, query);
            string bodyJson = JsonMapper.ToJson(unformattedJson);
            requestor.StartCoroutine(requestor.Co_Put(generatedUrl, bodyJson, callback));
        }

        private IEnumerator Co_Put(string url, string bodyJson, Action<long, string> callback)
        {
            using UnityWebRequest request = new UnityWebRequest(url, METHOD_PUT).SetHeaders(DefaultRequestHeaders);
            byte[] jsonBytes = new UTF8Encoding().GetBytes(bodyJson);

            request.uploadHandler = new UploadHandlerRaw(jsonBytes);
            request.downloadHandler = new DownloadHandlerBuffer();
            request.timeout = TIMEOUT_SECONDS;

            yield return request.SendWebRequest();
            callback?.Invoke(request.responseCode, request.downloadHandler.text);
        }
        #endregion

        #region Delete Request
        public static void Delete(string url, string bodyJson, Action<long, string> callback)
        {
            WebRequestorDev requestor = GetOrCreate();
            requestor.StartCoroutine(requestor.Co_Delete(url, bodyJson, callback));
        }

        public static void Delete(string url, Dictionary<string, string> queryData, string bodyJson, Action<long, string> callback)
        {
            WebRequestorDev requestor = GetOrCreate();
            string query = DictionaryToHttpQuery(queryData);
            string generatedUrl = CombineUrlWithQuery(url, query);
            requestor.StartCoroutine(requestor.Co_Delete(generatedUrl, bodyJson, callback));
        }

        public static void Delete(string url, object unformattedJson, Action<long, string> callback)
        {
            WebRequestorDev requestor = GetOrCreate();
            string bodyJson = JsonMapper.ToJson(unformattedJson);
            requestor.StartCoroutine(requestor.Co_Delete(url, bodyJson, callback));
        }

        public static void Delete(string url, Dictionary<string, string> queryData, object unformattedJson, Action<long, string> callback)
        {
            WebRequestorDev requestor = GetOrCreate();
            string query = DictionaryToHttpQuery(queryData);
            string generatedUrl = CombineUrlWithQuery(url, query);
            string bodyJson = JsonMapper.ToJson(unformattedJson);
            requestor.StartCoroutine(requestor.Co_Delete(generatedUrl, bodyJson, callback));
        }

        private IEnumerator Co_Delete(string url, string bodyJson, Action<long, string> callback)
        {
            using UnityWebRequest request = new UnityWebRequest(url, METHOD_DELETE).SetHeaders(DefaultRequestHeaders);
            byte[] jsonBytes = new UTF8Encoding().GetBytes(bodyJson);

            request.uploadHandler = new UploadHandlerRaw(jsonBytes);
            request.downloadHandler = new DownloadHandlerBuffer();
            request.timeout = TIMEOUT_SECONDS;

            yield return request.SendWebRequest();
            callback?.Invoke(request.responseCode, request.downloadHandler.text);
        }
        #endregion

        #region Image
        public static void Image(string url, string fileName, Action<bool, Sprite> callback)
        {
            WebRequestorDev requestor = GetOrCreate();
            requestor.StartCoroutine(requestor.Co_Image(url, fileName, callback));
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
            request.timeout = TIMEOUT_SECONDS;
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
            return $"{url}{queryData}";
        }
        #endregion
    }
}
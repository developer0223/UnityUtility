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
        private static WebRequestor Instance = null;

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

        #region JWT (Json-Web-Token)
        private string accessToken = string.Empty;
        private string refreshToken = string.Empty;

        public static string GetJwtAccessToken() => GetOrCreate().accessToken;
        public static void SetJwtAccessToken(string token)
        {
            Debug.Log($"SetJwtAccessToken. New token : {token}");
            GetOrCreate().accessToken = token;
            AddDefaultRequestHeaders("Access_token", token);
            //AddDefaultRequestHeaders("Authorization", "Bearer " + token);
        }

        private static string GetJwtRefreshToken() => GetOrCreate().refreshToken;
        public static void SetJwtRefreshToken(string token)
        {
            Debug.Log($"SetJwtRefreshToken. New token : {token}");
            GetOrCreate().refreshToken = token;
        }
        #endregion

        #region Settings
        private int TIMEOUT_SECONDS = 10;
        private string BASE_URL = string.Empty;
        private Dictionary<string, string> DefaultRequestHeaders = new Dictionary<string, string>();

        public static void SetTimeoutSeconds(int seconds)
        {
            WebRequestor requestor = GetOrCreate();
            requestor.TIMEOUT_SECONDS = seconds;
        }

        public static void SetBaseURL(string url)
        {
            WebRequestor requestor = GetOrCreate();
            requestor.BASE_URL = url;
        }

        public static void SetDefaultRequestHeaders(Dictionary<string, string> headers)
        {
            WebRequestor requestor = GetOrCreate();
            requestor.DefaultRequestHeaders = headers;
        }

        public static void AddDefaultRequestHeaders(string key, string value)
        {
            WebRequestor requestor = GetOrCreate();
            RemoveDefaultRequestHeaders(key);
            requestor.DefaultRequestHeaders.Add(key, value);
        }

        public static void RemoveDefaultRequestHeaders(string key)
        {
            WebRequestor requestor = GetOrCreate();
            if (requestor.DefaultRequestHeaders.ContainsKey(key))
            {
                requestor.DefaultRequestHeaders.Remove(key);
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
            using UnityWebRequest request = new UnityWebRequest(url, METHOD_GET).SetHeaders(DefaultRequestHeaders);
            request.downloadHandler = (DownloadHandler)new DownloadHandlerBuffer();
            request.timeout = TIMEOUT_SECONDS;

            yield return request.SendWebRequest();
            callback?.Invoke(request.responseCode, request.downloadHandler.text);
        }
        #endregion

        #region Post Request
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
            requestor.StartCoroutine(requestor.Co_Post(url, bodyJson, callback));
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
            Debug.Log($"Co_Post. url : {url}");
            Debug.Log($"Co_Post. bodyJson : {bodyJson}");

            using UnityWebRequest request = new UnityWebRequest(url, METHOD_POST).SetHeaders(DefaultRequestHeaders);
            byte[] jsonBytes = new UTF8Encoding().GetBytes(bodyJson);

            request.uploadHandler = (UploadHandler)new UploadHandlerRaw(jsonBytes);
            request.downloadHandler = (DownloadHandler)new DownloadHandlerBuffer();
            request.timeout = TIMEOUT_SECONDS;

            yield return request.SendWebRequest();
            callback?.Invoke(request.responseCode, request.downloadHandler.text);
        }
        #endregion

        #region Put Request
        public static void Put(string url, string bodyJson, Action<long, string> callback)
        {
            WebRequestor requestor = GetOrCreate();
            requestor.StartCoroutine(requestor.Co_Put(url, bodyJson, callback));
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
            requestor.StartCoroutine(requestor.Co_Put(url, bodyJson, callback));
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
            WebRequestor requestor = GetOrCreate();
            requestor.StartCoroutine(requestor.Co_Delete(url, bodyJson, callback));
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
            requestor.StartCoroutine(requestor.Co_Delete(url, bodyJson, callback));
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
            WebRequestor requestor = GetOrCreate();
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
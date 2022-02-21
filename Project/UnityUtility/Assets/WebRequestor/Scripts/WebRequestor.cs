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

    // Alias

    public class WebRequestor : MonoBehaviour
    {
        public static int TIMEOUT_SECONDS = 5;

        private static WebRequestor instance = null;

        public class URL
        {
            public static readonly string GOOGLE = "http://wwww.google.com";
            public static readonly string DB_SERVER = "";
            public static readonly string FILE_SERVER = "";
        }

        public static WebRequestor GetOrCreate()
        {
            if (instance == null)
            {
                GameObject _gameObject = new GameObject(nameof(WebRequestor));
                instance = _gameObject.AddComponent<WebRequestor>();
            }

            return instance;
        }


        #region Get Request
        /// <summary>
        /// Send http get request and get response.
        /// </summary>
        /// <param name="url">Target server url.</param>
        /// <param name="parameters">Query parameters.</param>
        /// <param name="callback">Callback method.</param>
        public static void Get(string url, Dictionary<string, string> parameters, Action<long, string> callback)
        {
            WebRequestor requestor = GetOrCreate();
            string query = requestor.DictionaryToHttpQuery(parameters);
            requestor.StartCoroutine(requestor.Co_Get($"{URL.DB_SERVER}{url}{query}", (responseCode, result) =>
            {
                callback?.Invoke(responseCode, result);
            }));
        }

        private IEnumerator Co_Get(string url, Action<long, string> callback)
        {
            using UnityWebRequest webRequest = UnityWebRequest.Get(url);
            webRequest.timeout = TIMEOUT_SECONDS;

            yield return webRequest.SendWebRequest();
            callback?.Invoke(webRequest.responseCode, webRequest.downloadHandler.text);
        }
        #endregion


        #region Post Request
        /// <summary>
        /// Send http post request and get response.
        /// </summary>
        /// <param name="url">Target server url.</param>
        /// <param name="formData">Body data.</param>
        /// <param name="callback">Target server url.</param>
        public static void Post(string url, string bodyJson, Action<long, string> callback)
        {
            WebRequestor requestor = GetOrCreate();
            requestor.StartCoroutine(requestor.Co_Post($"{URL.DB_SERVER}{url}", bodyJson, (responseCode, result) =>
                {
                    callback?.Invoke(responseCode, result);
                }));
        }

        /// <summary>
        /// Send http post request and get response.
        /// </summary>
        /// <param name="url">Target server url.</param>
        /// <param name="formData">Body data.</param>
        /// <param name="callback">Target server url.</param>
        public static void Post(string url, WWWForm formData, Action<long, string> callback)
        {
            WebRequestor requestor = GetOrCreate();
            requestor.StartCoroutine(requestor.Co_Post($"{URL.DB_SERVER}{url}", formData, (responseCode, result) =>
            {
                callback?.Invoke(responseCode, result);
            }));
        }

        /// <summary>
        /// Send http post request and get response.
        /// </summary>
        /// <param name="url">Target server url.</param>
        /// <param name="formData">Body data.</param>
        /// <param name="callback">Target server url.</param>
        public static void Post(string url, Dictionary<string, string> queryData, WWWForm formData, Action<long, string> callback)
        {
            WebRequestor requestor = GetOrCreate();
            string query = requestor.DictionaryToHttpQuery(queryData);
            requestor.StartCoroutine(requestor.Co_Post($"{URL.DB_SERVER}{url}{query}", formData, (responseCode, result) =>
            {
                callback?.Invoke(responseCode, result);
            }));
        }

        private IEnumerator Co_Post(string url, string bodyJson, Action<long, string> callback)
        {
            UnityWebRequest webRequest = new UnityWebRequest(url, UnityWebRequest.kHttpVerbPOST); // "POST"
            byte[] jsonToSend = new System.Text.UTF8Encoding().GetBytes(bodyJson);

            webRequest.uploadHandler = (UploadHandler)new UploadHandlerRaw(jsonToSend);
            webRequest.downloadHandler = (DownloadHandler)new DownloadHandlerBuffer();
            webRequest.SetRequestHeader("Content-Type", "application/json;");

            yield return webRequest.SendWebRequest();

            callback?.Invoke(webRequest.responseCode, webRequest.downloadHandler.text);
        }

        private IEnumerator Co_Post(string url, WWWForm formData, Action<long, string> callback)
        {
            using UnityWebRequest webRequest = UnityWebRequest.Post(url, formData);
            webRequest.timeout = TIMEOUT_SECONDS;

            yield return webRequest.SendWebRequest();
            callback?.Invoke(webRequest.responseCode, webRequest.downloadHandler.text);
        }
        #endregion


        #region Put Request
        /// <summary>
        /// 
        /// </summary>
        /// <param name="url"></param>
        /// <param name="bodyData"></param>
        /// <param name="callback"></param>
        public static void Put(string url, Dictionary<string, string> bodyData, Action<long, string> callback)
        {
            WebRequestor requestor = GetOrCreate();
            requestor.StartCoroutine(requestor.Co_Put($"{URL.DB_SERVER}{url}", bodyData, (responseCode, result) =>
            {
                callback?.Invoke(responseCode, result);
            }));
        }

        public static void Put(string url, Dictionary<string, string> parameters, Dictionary<string, string> bodyData, Action<long, string> callback)
        {
            WebRequestor requestor = GetOrCreate();
            string query = requestor.DictionaryToHttpQuery(parameters);
            requestor.StartCoroutine(requestor.Co_Put($"{URL.DB_SERVER}{url}{query}", bodyData, (responseCode, result) =>
            {
                callback?.Invoke(responseCode, result);
            }));
        }

        private IEnumerator Co_Put(string url, Dictionary<string, string> bodyData, Action<long, string> callback)
        {
            WebRequestor requestor = GetOrCreate();
            string jsonQuery = requestor.DictionaryToHttpQuery(bodyData);
            byte[] myData = Encoding.UTF8.GetBytes(jsonQuery);

            using UnityWebRequest webRequest = UnityWebRequest.Put(url, myData);
            webRequest.SetRequestHeader("Content-type", "application/json");
            webRequest.timeout = TIMEOUT_SECONDS;

            yield return webRequest.SendWebRequest();
            callback?.Invoke(webRequest.responseCode, webRequest.downloadHandler.text);
        }
        #endregion


        #region Delete Request
        public static void Delete(string url, Dictionary<string, string> parameters, Action<long, string> callback)
        {
            WebRequestor requestor = GetOrCreate();
            string query = requestor.DictionaryToHttpQuery(parameters);
            requestor.StartCoroutine(requestor.Co_Delete($"{URL.DB_SERVER}{url}{query}", (responseCode, result) =>
            {
                callback?.Invoke(responseCode, result);
            }));
        }

        private IEnumerator Co_Delete(string url, Action<long, string> callback)
        {
            using UnityWebRequest webRequest = UnityWebRequest.Delete(url);
            webRequest.timeout = TIMEOUT_SECONDS;

            yield return webRequest.SendWebRequest();
            callback?.Invoke(webRequest.responseCode, webRequest.downloadHandler.text);
        }
        #endregion


        #region Image
        public static void Image(string fileName, Action<bool, Sprite> callback)
        {
            WebRequestor requestor = GetOrCreate();
            requestor.StartCoroutine(requestor.Co_Image(fileName, (success, result) =>
            {
                callback?.Invoke(success, result);
            }));
        }

        private IEnumerator Co_Image(string url, Action<bool, Sprite> callback)
        {
            string[] urlArray = url.Split('\\');
            string fileName = urlArray[urlArray.Length - 1];

            string savePath = Path.Combine(Application.persistentDataPath, fileName);
            if (File.Exists(savePath))
            {
                try
                {
                    byte[] imageBytes = File.ReadAllBytes(savePath);

                    Texture2D texture = new Texture2D(0, 0);
                    texture.LoadImage(imageBytes);
                    texture.Apply();

                    Rect rect = new Rect(0, 0, texture.width, texture.height);
                    Sprite sprite = Sprite.Create(texture, rect, new Vector2(0.5f, 0.5f));

                    callback?.Invoke(true, sprite);
                    //Debug.Log($"Loaded image in cache data. name : {fileName}");
                    yield break;
                }
                catch (Exception e)
                {
                    Debug.LogWarning($"Failed to save data at {Path.Combine(Application.persistentDataPath, fileName)}\nError :\n{e.Message}\n{e.StackTrace}");
                }
            }

            using UnityWebRequest webRequest = UnityWebRequestTexture.GetTexture(url);
            webRequest.timeout = TIMEOUT_SECONDS;

            yield return webRequest.SendWebRequest();

            switch (webRequest.result)
            {
                case UnityWebRequest.Result.ConnectionError:
                    callback?.Invoke(false, null);
                    break;
                case UnityWebRequest.Result.DataProcessingError:
                    callback?.Invoke(false, null);
                    break;
                case UnityWebRequest.Result.ProtocolError:
                    callback?.Invoke(false, null);
                    break;
                case UnityWebRequest.Result.Success:
                    Texture texture = ((DownloadHandlerTexture)webRequest.downloadHandler).texture;
                    Rect rect = new Rect(0, 0, texture.width, texture.height);
                    Sprite sprite = Sprite.Create((Texture2D)texture, rect, new Vector2(0.5f, 0.5f));

                    byte[] imageBytes = webRequest.downloadHandler.data;
                    SaveImage(fileName, imageBytes);

                    //Debug.Log($"Loaded image in file server. name : {fileName}");

                    callback?.Invoke(true, sprite);
                    break;
            }
        }

        private void SaveImage(string fileName, byte[] data)
        {
            try
            {
                File.WriteAllBytes(Path.Combine(Application.persistentDataPath, fileName), data);
                //Debug.Log($"Image cached successfully downaloaded and saved at {Path.Combine(Application.persistentDataPath, fileName)}");
            }
            catch (Exception e)
            {
                Debug.LogWarning($"Failed to save data at {Path.Combine(Application.persistentDataPath, fileName)}\nError :\n{e.Message}\n{e.StackTrace}");
            }
        }
        #endregion

        private string DictionaryToHttpQuery(Dictionary<string, string> dictionary)
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
    }
}
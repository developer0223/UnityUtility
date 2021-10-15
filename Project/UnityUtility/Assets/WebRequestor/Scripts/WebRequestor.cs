// System
using System;
using System.IO;
using System.Collections;

// Unity
using UnityEngine;
using UnityEngine.Networking;

// Project
// Alias

public class WebRequestor : MonoBehaviour
{
    private static readonly string ERROR_CODE = "ERROR";

    public static WebRequestor Instance = null;
    public static int TIMEOUT_SECONDS = 5;

    public class URL
    {
        public static string Google = "https://www.google.com/";
        //public static string DB = "";
        //public static string FileServer = "";
    }

    public static WebRequestor GetOrCreate()
    {
        if (Instance == null)
        {
            GameObject _gameObject = new GameObject(nameof(WebRequestor));
            Instance = _gameObject.AddComponent<WebRequestor>();
        }

        return Instance;
    }

    public static void GetRequest(string uri, Action<bool, string> callback)
    {
        WebRequestor webRequestor = GetOrCreate();
        webRequestor.StartCoroutine(webRequestor.Co_GetRequest(uri, (result) =>
        {
            callback?.Invoke(result != ERROR_CODE, result);
        }));
    }

    public IEnumerator Co_GetRequest(string uri, Action<string> callback)
    {
        using UnityWebRequest webRequest = UnityWebRequest.Get(uri);
        webRequest.timeout = TIMEOUT_SECONDS;

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

    public static void GetSprite(string uri, Action<bool, Sprite> callback, bool cache = true)
    {
        WebRequestor requestor = GetOrCreate();
        requestor.StartCoroutine(requestor.Co_GetSprite(uri, (success, sprite) =>
        {
            callback?.Invoke(success, sprite);
        }, cache));
    }

    public IEnumerator Co_GetSprite(string uri, Action<bool, Sprite> callback, bool cache = true)
    {
        string[] urlArray = uri.Split('\\');
        string fileName = urlArray[urlArray.Length - 1];

        if (cache)
        {
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
                    yield break;
                }
                catch (Exception e)
                {
                    Debug.LogWarning($"Failed to save data at {Path.Combine(Application.persistentDataPath, fileName)}\nError :\n{e.Message}\n{e.StackTrace}");
                }
            }
        }

        using UnityWebRequest webRequest = UnityWebRequestTexture.GetTexture(uri);
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

                // Get Sprite
                Texture2D texture = ((DownloadHandlerTexture)webRequest.downloadHandler).texture;
                Sprite sprite = CreateSpriteWithTexture(texture);

                // Save Image
                byte[] imageBytes = webRequest.downloadHandler.data;
                SaveImage(fileName, imageBytes);

                callback?.Invoke(true, sprite);
                break;
        }
    }

    private Sprite CreateSpriteWithTexture(Texture2D texture)
    {
        Rect rect = new Rect(0, 0, texture.width, texture.height);
        Sprite sprite = Sprite.Create(texture, rect, new Vector2(0.5f, 0.5f));

        return sprite;
    }

    private void SaveImage(string fileName, byte[] data)
    {
        try
        {
            File.WriteAllBytes(Path.Combine(Application.persistentDataPath, fileName), data);
        }
        catch (Exception e)
        {
            Debug.LogWarning($"Failed to save data at {Path.Combine(Application.persistentDataPath, fileName)}\nError :\n{e.Message}\n{e.StackTrace}");
        }
    }
}
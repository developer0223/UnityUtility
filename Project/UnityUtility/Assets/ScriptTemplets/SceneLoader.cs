// System
using System;
using System.Collections;
using System.Collections.Generic;

// Unity
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

// Project
// Alias

public class SceneLoader : MonoBehaviour
{
    // public static readonly variables
    public static readonly string LoadingSceneName = "99.Loading";


    // public static variables
    public static string nextSceneName = string.Empty;


    // public variables
    public AsyncOperation loadSceneOperation = null;


    // ui objects
    [Header("Progress UI")]
    public Image img_progress = null;
    public Text txt_progress = null;


    public static void LoadScene(string nextSceneName)
    {
        SceneManager.LoadScene(nextSceneName);
    }

    public static void LoadSceneAsync(string nextSceneName)
    {
        SceneLoader.nextSceneName = nextSceneName;
        LoadScene(LoadingSceneName);
    }

    private void Start()
    {
        StartCoroutine(Co_LoadSceneAsync());
    }

    private IEnumerator Co_LoadSceneAsync()
    {
        loadSceneOperation = SceneManager.LoadSceneAsync(nextSceneName);
        loadSceneOperation.allowSceneActivation = false;

        float timer = 0.0f;
        float loadingPercent = 0.0f;

        while (!loadSceneOperation.isDone)
        {
            timer += Time.deltaTime;

            if (loadSceneOperation.progress <= 0.9f)
            {
                loadingPercent = Mathf.Lerp(loadingPercent, loadSceneOperation.progress, timer);
                img_progress.fillAmount = loadingPercent;
                txt_progress.text = $"{(int)(loadingPercent * 100)}%";

                if (loadSceneOperation.progress <= loadingPercent)
                    timer = 0.0f;
            }
            else
            {
                loadingPercent = Mathf.Lerp(loadingPercent, 1.0f, timer);
                img_progress.fillAmount = loadingPercent;
                txt_progress.text = $"{(int)(loadingPercent * 100)}%";

                if (loadingPercent == 1.0f)
                    break;
            }

            yield return null;
        }

        loadSceneOperation.allowSceneActivation = true;
    }
}
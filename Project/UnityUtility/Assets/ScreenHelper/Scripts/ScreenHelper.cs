// System
using System;
using System.Collections;

// Unity
using UnityEngine;
using UnityEngine.UI;

// Project
// Alias

public class ScreenHelper : MonoBehaviour
{
    private static readonly string Path = "ScreenHelper";

    public Image img_background = null;

    public static ScreenHelper GetOrCreate()
    {
        ScreenHelper imageHelper = FindObjectOfType<ScreenHelper>();

        if (imageHelper == null)
        {
            GameObject _prefab = Resources.Load(Path) as GameObject;
            GameObject _gameObject = Instantiate(_prefab);
            imageHelper = _gameObject.GetComponent<ScreenHelper>();
        }

        return imageHelper;
    }

    public static void FadeIn(float duration, Action callback = null)
    {
        FadeAlpha(1.0f, 0.0f, duration, () => { callback?.Invoke(); });
    }

    public static void FadeOut(float duration, Action callback = null)
    {
        FadeAlpha(0.0f, 1.0f, duration, () => { callback?.Invoke(); });
    }

    private static void FadeAlpha(float startAlpha, float targetAlpha, float duration, Action callback = null)
    {
        ScreenHelper helper = GetOrCreate();
        helper.StopAllCoroutines();
        helper.StartCoroutine(helper.Co_FadeAlpha(startAlpha, targetAlpha, duration, () => { callback?.Invoke(); }));
    }

    private IEnumerator Co_FadeAlpha(float startAlpha, float targetAlpha, float duration, Action callback = null)
    {
        Color startColor = img_background.color;
        startColor.a = startAlpha;
        img_background.color = startColor;

        float start = img_background.color.a;
        float end = targetAlpha;
        float time = 0.0f;

        while (Mathf.Abs(end - img_background.color.a) >= 0.03)
        {
            time += Time.deltaTime / duration;
            Color newColor = img_background.color;
            newColor.a = Mathf.Lerp(start, end, time);
            img_background.color = newColor;
            yield return null;
        }

        Color lastColor = img_background.color;
        lastColor.a = targetAlpha;
        img_background.color = lastColor;

        callback?.Invoke();
    }
}
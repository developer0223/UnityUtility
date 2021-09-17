// System
using System;
using System.Collections;
using System.Collections.Generic;

// Unity
using UnityEngine;
using UnityEngine.UI;

// Project
// Alias

public class ImageHelper : MonoBehaviour
{
    public static ImageHelper GetOrCreate()
    {
        ImageHelper imageHelper = FindObjectOfType<ImageHelper>();

        if (imageHelper == null)
        {
            GameObject _gameObject = new GameObject(nameof(ImageHelper));
            imageHelper = _gameObject.AddComponent<ImageHelper>();
        }

        return imageHelper;
    }

    public static void FadeAlpha(Image image, float targetAlpha, float duration, Action callback = null)
    {
        ImageHelper helper = GetOrCreate();
        helper.StopAllCoroutines();
        helper.StartCoroutine(helper.Co_FadeAlpha(image, targetAlpha, duration, () => { callback?.Invoke(); }));
    }

    private IEnumerator Co_FadeAlpha(Image image, float targetAlpha, float duration, Action callback = null)
    {
        float start = image.color.a;
        float end = targetAlpha;
        float time = 0.0f;

        while (Mathf.Abs(end - image.color.a) >= 0.03)
        {
            time += Time.deltaTime / duration;
            Color newColor = image.color;
            newColor.a = Mathf.Lerp(start, end, time);
            image.color = newColor;
            yield return null;
        }

        Color lastColor = image.color;
        lastColor.a = targetAlpha;
        image.color = lastColor;

        callback?.Invoke();
    }

    public static void FadeAlpha(SpriteRenderer renderer, float targetAlpha, float duration, Action callback = null)
    {
        ImageHelper helper = GetOrCreate();
        helper.StopAllCoroutines();
        helper.StartCoroutine(helper.Co_FadeAlpha(renderer, targetAlpha, duration, () => { callback?.Invoke(); }));
    }

    private IEnumerator Co_FadeAlpha(SpriteRenderer renderer, float targetAlpha, float duration, Action callback = null)
    {
        float start = renderer.color.a;
        float end = targetAlpha;
        float time = 0.0f;

        while (Mathf.Abs(end - renderer.color.a) >= 0.03)
        {
            time += Time.deltaTime / duration;
            Color newColor = renderer.color;
            newColor.a = Mathf.Lerp(start, end, time);
            renderer.color = newColor;
            yield return null;
        }

        Color lastColor = renderer.color;
        lastColor.a = targetAlpha;
        renderer.color = lastColor;

        callback?.Invoke();
    }

    public static void FadeColor(Image image, Color targetColor, float duration, Action callback = null)
    {
        ImageHelper helper = GetOrCreate();
        helper.StopAllCoroutines();
        helper.StartCoroutine(helper.Co_FadeColor(image, targetColor, duration, () => { callback?.Invoke(); }));
    }

    private IEnumerator Co_FadeColor(Image image, Color targetColor, float duration, Action callback = null)
    {
        Color start = image.color;
        Color end = targetColor;
        float time = 0.0f;

        while (Mathf.Abs(end.r - image.color.r) >= 0.03)
        {
            time += Time.deltaTime / duration;
            image.color = Color.Lerp(start, end, time);
            yield return null;
        }

        image.color = targetColor;

        callback?.Invoke();
    }
}
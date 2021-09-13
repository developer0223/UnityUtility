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
}
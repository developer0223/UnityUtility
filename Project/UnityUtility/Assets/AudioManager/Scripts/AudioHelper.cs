// System
using System;
using System.Collections;
using System.Collections.Generic;

// Unity
using UnityEngine;
using UnityEngine.UI;

// Project
// Alias

public class AudioHelper : MonoBehaviour
{
    public static AudioHelper GetOrCreate()
    {
        AudioHelper soundManager = FindObjectOfType<AudioHelper>();
        if (!soundManager)
        {
            GameObject _soundManager = new GameObject(typeof(AudioHelper).Name);
            soundManager = _soundManager.AddComponent<AudioHelper>();
        }

        return soundManager;
    }

    public static void FadeBGM(float targetVolume, float duration, Action callback = null)
    {
        AudioHelper helper = GetOrCreate();
        helper.StartCoroutine(helper.Co_FadeBGM(targetVolume, duration, () => { callback?.Invoke(); }));
    }

    private IEnumerator Co_FadeBGM(float targetVolume, float duration, Action callback = null)
    {
        AudioManager audioManager = AudioManager.GetOrCreate();

        float start = audioManager.BGMVolume;
        float end = targetVolume;
        float time = 0.0f;

        while (Mathf.Abs(end - audioManager.BGMVolume) >= 0.03)
        {
            time += Time.deltaTime / duration;
            audioManager.SetBGMVolume(Mathf.Lerp(start, end, time));
            yield return null;
        }

        audioManager.BGMVolume = targetVolume;
        callback?.Invoke();
    }

    public static void FadeEffect(float targetVolume, float duration, Action callback = null)
    {
        AudioHelper helper = GetOrCreate();
        helper.StartCoroutine(helper.Co_FadeEffect(targetVolume, duration, () => { callback?.Invoke(); }));
    }

    private IEnumerator Co_FadeEffect(float targetVolume, float duration, Action callback = null)
    {
        AudioManager audioManager = AudioManager.GetOrCreate();

        float start = audioManager.EffectVolume;
        float end = targetVolume;
        float time = 0.0f;

        while (Mathf.Abs(end - audioManager.EffectVolume) >= 0.03)
        {
            time += Time.deltaTime / duration;
            audioManager.SetEffectVolume(Mathf.Lerp(start, end, time));
            yield return null;
        }

        audioManager.EffectVolume = targetVolume;
        callback?.Invoke();
    }
}
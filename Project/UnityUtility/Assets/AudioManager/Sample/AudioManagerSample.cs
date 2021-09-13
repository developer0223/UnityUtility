// System
using System;
using System.Collections;
using System.Collections.Generic;

// Unity
using UnityEngine;
using UnityEngine.UI;

// Project
// Alias

public class AudioManagerSample : MonoBehaviour
{
    [Header("Library Components")]
    public AudioManager audioManager = null;
    public AudioHelper audioHelper = null;

    [Header("Custom AudioSources")]
    public AudioSource songSource = null;
    public AudioSource feverSource = null;

    [Header("Target AudioClips")]
    public AudioClip songClip = null;
    public AudioClip feverClip = null;

    [Header("Swap duration")]
    public float swapDuration = 1.0f;

    [Header("Playing State")]
    public int state = 0;

    [Header("Debug Texts")]
    public Text txt_song_time;
    public Text txt_song_volume;
    public Text txt_fever_time;
    public Text txt_fever_volume;

    private void Awake()
    {
        //audioManager = AudioManager.GetOrCreate();
        //audioManager.bgmSource = songSource;
        //audioManager.effectSource = feverSource;
        // or
        audioManager = AudioManager.GetOrCreate(songSource, feverSource);

        audioManager.SetBGMVolume(0.5f);
        audioManager.SetEffectVolume(0.0f);

        audioManager.SetBGMClip(songClip);
        audioManager.SetEffectClip(feverClip);

        SwapSong(state);
    }

    private void Update()
    {
        txt_song_time.text = audioManager.bgmSource.time.ToString("F2");
        txt_song_volume.text = audioManager.bgmSource.volume.ToString("F2");

        txt_fever_time.text = audioManager.effectSource.time.ToString("F2");
        txt_fever_volume.text = audioManager.effectSource.volume.ToString("F2");

        if (Input.GetKeyDown(KeyCode.Space))
        {
            SwapSong(state);
        }

        if (Input.GetKeyDown(KeyCode.Alpha1))
        {
            audioManager.SetBGMTime(0.0f);
        }

        if (Input.GetKeyDown(KeyCode.Alpha2))
        {
            audioManager.SetEffectTime(0.0f);
        }
    }

    private void SwapSong(int state)
    {
        if (state == 0)
        {
            audioManager.ResumeBGM();
            AudioHelper.FadeBGM(0.5f, swapDuration, () => { audioManager.PauseEffect(); });
            AudioHelper.FadeEffect(0.0f, swapDuration);
            this.state = 1;
        }
        else
        {
            audioManager.ResumeEffect();
            AudioHelper.FadeBGM(0.0f, swapDuration, () => { audioManager.PauseBGM(); });
            AudioHelper.FadeEffect(0.5f, swapDuration);
            this.state = 0;
        }
    }
}
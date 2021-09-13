# AudioManager
Tools to management audio system

</br>

# How to use

### Create

> Get or Create AudioManager Instance
```C#
public void CreateAudioManager()
{
    AudioManager audioManager = AudioManager.GetOrCreate();
    AudioManager audioManager = AudioManager.GetOrCreate(bgmSource);
    AudioManager audioManager = AudioManager.GetOrCreate(bgmSource, effectSource);
}
```

</br>

> Get or Create AudioHelper Instance
```C#
public void CreateAudioHelper()
{
    AudioHelper audioHelper = AudioHelper.GetOrCreate();
}
```

</br>

### Fields
> public fields
```C#
public UnityEngine.AudioSource bgmSource;
public UnityEngine.AudioSource effectSource;
```

</br>

> public properties
```C#
public float BGMVolume;
public float EffectVolume;
```

</br>

### Methods
> BGM source control
```C#
public void SetBGMClip(UnityEngine.AudioClip clip);
public void SetBGMTime(float time);
public void SetBGMVolume(float volume);
public void PlayBGM(UnityEngine.AudioClip clip, bool repeat = true);
public void PlayBGM(UnityEngine.AudioClip clip, float volume, bool repeat = true);
public void PauseBGM();
public void ResumeBGM();
public void StopBGM();
```

</br>

> Effect source control
```C#
public void SetEffectClip(UnityEngine.AudioClip clip);
public void SetEffectTime(float time);
public void SetEffectVolume(float volume);
public void PlayEffect(UnityEngine.AudioClip clip, bool repeat = true);
public void PlayEffect(UnityEngine.AudioClip clip, float volume, bool repeat = true);
public void PauseEffect();
public void ResumeEffect();
public void StopEffect();
```

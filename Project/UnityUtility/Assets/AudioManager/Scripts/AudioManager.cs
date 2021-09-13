using UnityEngine;

public class AudioManager : MonoBehaviour
{
    public AudioSource bgmSource;                       // 배경음악 출력용 오디오소스
    public AudioSource effectSource;                    // 효과음 출력용 오디오소스

    private float bgmVolume = 0.5f;                     // 배경음악 볼륨
    public float BGMVolume                              // 배경음악 볼륨
    {
        get { return bgmVolume; }
        set {
            bgmVolume = value;
            bgmSource.volume = value;
        }
    }

    private float effectVolume = 0.5f;                  // 효과음 볼륨
    public float EffectVolume                           // 효과음 볼륨
    {
        get { return effectVolume; }
        set {
            effectVolume = value;
            effectSource.volume = value;
        }
    }

    /// <summary>
    /// 씬에 존재하는 SoundManager 반환. 없을 시 새로 생성
    /// </summary>
    /// <returns></returns>
    public static AudioManager GetOrCreate(AudioSource bgmSource = null, AudioSource effectSource = null)
    {
        AudioManager audioManager = FindObjectOfType<AudioManager>();
        if (!audioManager)
        {
            GameObject _audioManager = new GameObject(typeof(AudioManager).Name);
            audioManager = _audioManager.AddComponent<AudioManager>();

            if (bgmSource == null)
            {
                GameObject _bgmSource = new GameObject("BGMSource");
                AudioSource _bgmSourceComponent = _bgmSource.AddComponent<AudioSource>();
                _bgmSourceComponent.playOnAwake = false;
                _bgmSource.transform.SetParent(_audioManager.transform);
                audioManager.bgmSource = _bgmSourceComponent;
            }
            else
            {
                bgmSource.loop = false;
                bgmSource.playOnAwake = false;
                audioManager.bgmSource = bgmSource;
            }

            if (effectSource == null)
            {
                GameObject _effectSource = new GameObject("EffectSource");
                AudioSource _effectSourceComponent = _effectSource.AddComponent<AudioSource>();
                _effectSourceComponent.playOnAwake = false;
                _effectSource.transform.SetParent(_audioManager.transform);
                audioManager.effectSource = _effectSourceComponent;
            }
            else
            {
                effectSource.loop = false;
                effectSource.playOnAwake = false;
                audioManager.effectSource = effectSource;
            }
        }

        return audioManager;
    }

    private void Awake()
    {
        DontDestroyOnLoad(this);
    }

    private void Start()
    {
        Initialize();
    }

    /// <summary>
    ///  초기설정
    /// </summary>
    private void Initialize()
    {
        bgmSource.volume = BGMVolume;
        effectSource.volume = EffectVolume;
    }

    /// <summary>
    /// 배경음악 클립 설정
    /// </summary>
    /// <param name="clip"></param>
    public void SetBGMClip(AudioClip clip)
    {
        bgmSource.clip = clip;
    }

    /// <summary>
    /// 배경음악 위치 조절
    /// </summary>
    /// <param name="time"></param>
    public void SetBGMTime(float time)
    {
        bgmSource.time = time;
    }

    /// <summary>
    /// 배경음악 볼륨 조절
    /// </summary>
    /// <param name="volume"></param>
    public void SetBGMVolume(float volume)
    {
        BGMVolume = volume;
    }

    /// <summary>
    /// 효과음 볼륨 조절
    /// </summary>
    /// <param name="volume"></param>
    public void SetEffectVolume(float volume)
    {
        EffectVolume = volume;
    }

    /// <summary>
    /// 배경음악 재생
    /// </summary>
    /// <param name="clip">배경음악</param>
    /// <param name="repeat">반복 여부</param>
    public void PlayBGM(AudioClip clip, bool repeat = true)
    {
        bgmSource.clip = clip;
        bgmSource.loop = repeat;
        bgmSource.Play();
    }

    /// <summary>
    /// 배경음악 재생
    /// </summary>
    /// <param name="clip">배경음악</param>
    /// <param name="volume">볼륨</param>z
    /// <param name="repeat">반복 여부</param>
    public void PlayBGM(AudioClip clip, float volume, bool repeat = true)
    {
        BGMVolume = volume;

        bgmSource.clip = clip;
        bgmSource.loop = repeat;
        bgmSource.Play();
    }

    /// <summary>
    /// BGM 일시정지
    /// </summary>
    public void PauseBGM()
    {
        bgmSource.Pause();
    }

    /// <summary>
    /// BGM 일시정지 해제
    /// </summary>
    public void ResumeBGM()
    {
        if (!bgmSource.isPlaying) bgmSource.Play();
        else bgmSource.UnPause();
    }

    /// <summary>
    /// BGM 정지
    /// </summary>
    public void StopBGM()
    {
        bgmSource.Stop();
    }

    /// <summary>
    /// 효과음 클립 설정
    /// </summary>
    /// <param name="clip"></param>
    public void SetEffectClip(AudioClip clip)
    {
        effectSource.clip = clip;
    }

    /// <summary>
    /// 효과음 위치 조절
    /// </summary>
    /// <param name="time"></param>
    public void SetEffectTime(float time)
    {
        effectSource.time = time;
    }

    /// <summary>
    /// 효과음 재생
    /// </summary>
    /// <param name="clip">효과음</param>
    public void PlayEffectSound(AudioClip clip)
    {
        effectSource.PlayOneShot(clip, EffectVolume);
    }

    /// <summary>
    /// 효과음 재생
    /// </summary>
    /// <param name="clip">효과음</param>
    /// <param name="volume">볼륨</param>
    public void PlayEffectSound(AudioClip clip, float volume)
    {
        effectSource.PlayOneShot(clip, volume);
    }

    /// <summary>
    /// 효과음 일시정지
    /// </summary>
    public void PauseEffect()
    {
        effectSource.Pause();
    }

    /// <summary>
    /// 효과음 일시정지 해제
    /// </summary>
    public void ResumeEffect()
    {
        if (!effectSource.isPlaying) effectSource.Play();
        else effectSource.UnPause();
    }

    /// <summary>
    /// 효과음 정지
    /// </summary>
    public void StopEffect()
    {
        effectSource.Stop();
    }
}
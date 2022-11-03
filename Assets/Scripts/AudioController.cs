using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.UI;

public class AudioController : MonoBehaviour
{
    public static AudioController Instance { get; private set; }

    //音效播放源
    private AudioSource sfxSource;

    //音乐播放源
    private AudioSource musicSource;

    [SerializeField] private AudioClip sfxClip;
    [SerializeField] private AudioClip musicClip;

    //进度条
    [SerializeField] private Slider progressBar;

    //音效音量
    private float SFXVolume
    {
        get
        {
            return PlayerPrefs.GetFloat("SFXVolume", 0.6f);
        }
        set
        {
            sfxSource.volume = value;
            PlayerPrefs.SetFloat("SFXVolume", value);
        }
    }

    //音乐音量
    private float MusicVolume
    {
        get
        {
            return PlayerPrefs.GetFloat("MusicVolume", 0.8f);
        }
        set
        {
            musicSource.volume = value;
            PlayerPrefs.SetFloat("MusicVolume", value);
        }
    }

    private void Awake()
    {
        if (Instance == null)
        {
            Instance = this;
            DontDestroyOnLoad(gameObject);
        }
        else
        {
            Destroy(gameObject);
        }

        //初始化
        musicSource = this.gameObject.AddComponent<AudioSource>();
        musicSource.playOnAwake = false;
        musicSource.loop = true;

        sfxSource = this.gameObject.AddComponent<AudioSource>();
        sfxSource.loop = false;
    }

    #region 播放音乐

    /// <summary>
    /// 播放音乐
    /// </summary>
    /// <param name="name"></param>
    public void PlayMusic()
    {
        //如果玩家设置音量低于0.1f，则完全不播放，节省性能
        if (this.MusicVolume < 0.1f)
            return;

        musicSource.clip = musicClip;
        musicSource.Play();

        progressBar.value = Mathf.Clamp01((musicSource.time) / musicSource.clip.length);
    }

    /// <summary>
    /// 暂停音乐
    /// </summary>
    public void PauseMusic()
    {
        musicSource.Pause();
    }

    /// <summary>
    /// 继续播放音乐
    /// </summary>
    public void UnPauseMusic()
    {
        musicSource.UnPause();
    }

    /// <summary>
    /// 停止播放音乐
    /// </summary>
    public void StopMusic()
    {
        musicSource.Stop();
    }

    #endregion 播放音乐

    #region 播放音效

    /// <summary>
    /// 播放音效
    /// </summary>
    /// <param name="name"></param>
    public void PlaySFX()
    {
        //如果玩家设置音量低于0.1f，则完全不播放，节省性能
        if (this.SFXVolume < 0.1f)
            return;

        //Manager.ResourceManager.LoadAsset(name, extension, AssetType.Sound, (UnityEngine.Object obj) =>
        //{
        //    soundSource.PlayOneShot(obj as AudioClip);
        //});

        sfxSource.PlayOneShot(sfxClip);
    }

    #endregion 播放音效

    /// <summary>
    /// 设置音量
    /// </summary>
    /// <param name="value"></param>
    public void SetMusicVolume(float value)
    {
        this.MusicVolume = value;
    }

    /// <summary>
    /// 设置音量
    /// </summary>
    /// <param name="value"></param>
    public void SetSFXVolume(float value)
    {
        this.SFXVolume = value;
    }

    public void InitProgressBar()
    {
        progressBar.value = 0;
    }
}
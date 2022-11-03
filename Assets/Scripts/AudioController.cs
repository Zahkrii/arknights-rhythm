using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.UI;

public class AudioController : MonoBehaviour
{
    public static AudioController Instance { get; private set; }

    //��Ч����Դ
    private AudioSource sfxSource;

    //���ֲ���Դ
    private AudioSource musicSource;

    [SerializeField] private AudioClip sfxClip;
    [SerializeField] private AudioClip musicClip;

    //������
    [SerializeField] private Slider progressBar;

    //��Ч����
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

    //��������
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

        //��ʼ��
        musicSource = this.gameObject.AddComponent<AudioSource>();
        musicSource.playOnAwake = false;
        musicSource.loop = true;

        sfxSource = this.gameObject.AddComponent<AudioSource>();
        sfxSource.loop = false;
    }

    #region ��������

    /// <summary>
    /// ��������
    /// </summary>
    /// <param name="name"></param>
    public void PlayMusic()
    {
        //������������������0.1f������ȫ�����ţ���ʡ����
        if (this.MusicVolume < 0.1f)
            return;

        musicSource.clip = musicClip;
        musicSource.Play();

        progressBar.value = Mathf.Clamp01((musicSource.time) / musicSource.clip.length);
    }

    /// <summary>
    /// ��ͣ����
    /// </summary>
    public void PauseMusic()
    {
        musicSource.Pause();
    }

    /// <summary>
    /// ������������
    /// </summary>
    public void UnPauseMusic()
    {
        musicSource.UnPause();
    }

    /// <summary>
    /// ֹͣ��������
    /// </summary>
    public void StopMusic()
    {
        musicSource.Stop();
    }

    #endregion ��������

    #region ������Ч

    /// <summary>
    /// ������Ч
    /// </summary>
    /// <param name="name"></param>
    public void PlaySFX()
    {
        //������������������0.1f������ȫ�����ţ���ʡ����
        if (this.SFXVolume < 0.1f)
            return;

        //Manager.ResourceManager.LoadAsset(name, extension, AssetType.Sound, (UnityEngine.Object obj) =>
        //{
        //    soundSource.PlayOneShot(obj as AudioClip);
        //});

        sfxSource.PlayOneShot(sfxClip);
    }

    #endregion ������Ч

    /// <summary>
    /// ��������
    /// </summary>
    /// <param name="value"></param>
    public void SetMusicVolume(float value)
    {
        this.MusicVolume = value;
    }

    /// <summary>
    /// ��������
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
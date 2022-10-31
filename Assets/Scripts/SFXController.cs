using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public class SFXController : MonoBehaviour
{
    public static SFXController Instance { get; private set; }

    private AudioSource sfxSource;

    [SerializeField] private AudioClip sfxClip;

    //��Ч����
    private float SFXVolume
    {
        get
        {
            return PlayerPrefs.GetFloat("SFXVolume", 1.0f);
        }
        set
        {
            sfxSource.volume = value;
            PlayerPrefs.SetFloat("SFXVolume", value);
        }
    }

    private AudioSource musicSource;

    //��������
    private float MusicVolume
    {
        get
        {
            return PlayerPrefs.GetFloat("MusicVolume", 1.0f);
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
    public void PlayMusic(string name, string extension)
    {
        //������������������0.1f������ȫ�����ţ���ʡ����
        if (this.MusicVolume < 0.1f)
            return;

        string previousName = "";
        if (musicSource.clip != null)
            previousName = musicSource.clip.name;

        //��ͬ���ֲ��ظ�������Դ
        if (previousName == name)
        {
            musicSource.Play();
            return;
        }

        //Manager.ResourceManager.LoadAsset(name, extension, AssetType.Music, (UnityEngine.Object obj) =>
        //{
        //    musicSource.clip = obj as AudioClip;
        //    musicSource.Play();
        //});
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
}
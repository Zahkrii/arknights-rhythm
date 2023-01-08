using UnityEngine;

/// <summary>
/// 全局设置存储与读取
/// </summary>
public static class SettingManagerBackUp
{
    /// <summary>
    /// 谱面流速
    /// </summary>
    public static int ChartSpeed
    {
        get
        {
            return PlayerPrefs.GetInt("ChartSpeed", 5);
        }
        set
        {
            PlayerPrefs.SetInt("ChartSpeed", value);
        }
    }

    /// <summary>
    /// 谱面延时
    /// </summary>
    public static int ChartDelay
    {
        get
        {
            return PlayerPrefs.GetInt("ChartDelay", 0);
        }
        set
        {
            PlayerPrefs.SetInt("ChartDelay", value);
        }
    }

    /// <summary>
    /// 音效音量，警告：不要在不存在 AudioManager 的场景进行 set 操作，确保场景中存在 AudioManager
    /// </summary>
    public static float SFXVolume
    {
        get
        {
            return PlayerPrefs.GetFloat("SFXVolume", 0.6f);
        }
        set
        {
            try
            {
                AudioManager.Instance.SFXVolume = value;
            }
            catch { }
        }
    }

    /// <summary>
    /// 音乐音量，警告：不要在不存在 AudioManager 的场景进行 set 操作，确保场景中存在 AudioManager
    /// </summary>
    public static float MusicVolume
    {
        get
        {
            return PlayerPrefs.GetFloat("MusicVolume", 0.8f);
        }
        set
        {
            try
            {
                AudioManager.Instance.MusicVolume = value;
            }
            catch { }
        }
    }

    /// <summary>
    /// 当前选择的曲目：用于从选曲场景到谱面场景传参
    /// </summary>
    public static string ChartSelected
    {
        get
        {
            return PlayerPrefs.GetString("ChartSelected");
        }
        set
        {
            PlayerPrefs.SetString("ChartSelected", value);
        }
    }

    /// <summary>
    /// 谱面打击音效
    /// </summary>
    public static string HitSFX
    {
        get
        {
            return PlayerPrefs.GetString("HitSFX", "hit");
        }
        set
        {
            PlayerPrefs.SetString("HitSFX", value);
        }
    }

    /// <summary>
    /// 谱面打击音效音量，警告：不要在不存在 AudioManager 的场景进行 set 操作，确保场景中存在 AudioManager
    /// </summary>
    public static float HitSFXVolume
    {
        get
        {
            return PlayerPrefs.GetFloat("HitSFXVolume", 0.6f);
        }
        set
        {
            try
            {
                AudioManager.Instance.SFXVolume = value;
            }
            catch { }
        }
    }
}
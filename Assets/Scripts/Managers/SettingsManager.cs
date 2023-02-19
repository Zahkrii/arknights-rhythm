using UnityEngine;

/// <summary>
/// ȫ�����ô洢���ȡ
/// </summary>
public static class SettingsManager
{
    /// <summary>
    /// ��������
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
    /// ������ʱ
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
    /// ��Ч���������棺��Ҫ�ڲ����� AudioManager �ĳ������� set ������ȷ�������д��� AudioManager
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
    /// �������������棺��Ҫ�ڲ����� AudioManager �ĳ������� set ������ȷ�������д��� AudioManager
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
    /// ��ǰѡ�����Ŀ�����ڴ�ѡ�����������泡������
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
    /// ��������Ч
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
    /// ��������Ч���������棺��Ҫ�ڲ����� AudioManager �ĳ������� set ������ȷ�������д��� AudioManager
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
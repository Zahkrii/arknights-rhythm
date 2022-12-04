using System;
using System.Collections;
using System.Collections.Generic;
using System.IO;
using System.Numerics;
using UnityEditor;
using UnityEngine;
using UnityEngine.Networking;

public class Chart
{
    public string name;
    public string difficulty;
    public short level;
    public int count;
    public List<Note> notes;
}

[Serializable]
public class Note
{
    public int id;
    public int type;
    public float pos;
    public float time;
    public float holdTime;
}

/// <summary>
/// 谱面数据存储与读取
/// </summary>
public class ChartManager : MonoSingleton<ChartManager>
{
    //存储所有谱面资源
    public Dictionary<string, ChartAsset> chartAssets = new Dictionary<string, ChartAsset>();

    [Header("谱面资源列表")]
    [SerializeField] private List<SerializableKeyValuePair> chartAssetList = new List<SerializableKeyValuePair>();

    // Tap 判定序列
    [Header("谱面数据")]
    public List<TapScript> tapPaddingList = new List<TapScript>();

    //Drag 判定序列
    public List<DragScript> dragPaddingList = new List<DragScript>();

    //Hold 头部判定序列
    public List<HoldScript> holdHeadPaddingList = new List<HoldScript>();

    //Hold 判定序列
    public List<HoldScript> holdingPaddingList = new List<HoldScript>();

    //public float holdTime = 0f;

    [Serializable]
    private struct SerializableKeyValuePair
    {
        public string Key;
        public ChartAsset Value;

        public SerializableKeyValuePair(string key, ChartAsset value)
        {
            Key = key;
            Value = value;
        }
    }

    /// <summary>
    /// 初始化字典，使用前必须执行
    /// </summary>
    public void InitDictionary()
    {
        foreach (var item in chartAssetList)
        {
            chartAssets.Add(item.Key, item.Value);
        }
    }

    protected override void OnAwake()
    {
        base.OnAwake();
        InitDictionary();
    }

    public Chart LoadChart(string key)
    {
        return JsonUtility.FromJson<Chart>(chartAssets[key].chart.text);
    }

    public void LoadChart(string name, Action<Chart> complete = null)
    {
        StartCoroutine(LoadChartAsync(name, complete));
    }

    public IEnumerator LoadChartAsync(string name, Action<Chart> complete = null)
    {
        string chartPath = Path.Combine($"{Application.streamingAssetsPath}/Charts", $"{name}.json");

        UnityWebRequest webRequest = UnityWebRequest.Get(chartPath);
        yield return webRequest.SendWebRequest();

        if (webRequest.result == UnityWebRequest.Result.ProtocolError || webRequest.result == UnityWebRequest.Result.ConnectionError)
        {
            Debug.LogError($"从该地址下载出错：{chartPath}");
            yield break;
            //TODO: 下载重试
        }
        var json = webRequest.downloadHandler.text;
        // 销毁
        webRequest.Dispose();
        var data = JsonUtility.FromJson<Chart>(json);
        complete?.Invoke(data);
    }
}
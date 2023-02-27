using System;
using System.Collections;
using System.Collections.Generic;
using System.IO;
using UnityEngine;
using UnityEngine.Networking;

public class Chart
{
    public ChartID id;
    public string name;
    public Difficulty difficulty;
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
/// �������ݴ洢���ȡ
/// </summary>
public class ChartManager : MonoSingleton<ChartManager>
{
    //�洢����������Դ
    public Dictionary<string, ChartAsset> chartAssets = new Dictionary<string, ChartAsset>();

    [Header("������Դ�б�")]
    [SerializeField] private List<SerializableKeyValuePair> chartAssetList = new List<SerializableKeyValuePair>();

    // Tap �ж�����
    [Header("��������")]
    public List<TapScript> tapPaddingList = new List<TapScript>();

    //Drag �ж�����
    public List<DragScript> dragPaddingList = new List<DragScript>();

    //Hold ͷ���ж�����
    public List<HoldScript> holdHeadPaddingList = new List<HoldScript>();

    //Hold �ж�����
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
    /// ��ʼ���ֵ䣬ʹ��ǰ����ִ��
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

    public Chart LoadChart(string key, Difficulty difficulty)
    {
        switch (difficulty)
        {
            case Difficulty.Easy:
                return JsonUtility.FromJson<Chart>(chartAssets[key].chartEasy.text);

            case Difficulty.Normal:
                return JsonUtility.FromJson<Chart>(chartAssets[key].chartNormal.text);

            case Difficulty.Hard:
                return JsonUtility.FromJson<Chart>(chartAssets[key].chartHard.text);

            case Difficulty.Extra:
                return JsonUtility.FromJson<Chart>(chartAssets[key].chartExtra.text);

            default:
                return JsonUtility.FromJson<Chart>(chartAssets[key].chartNormal.text);
        }
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
            Debug.LogError($"�Ӹõ�ַ���س���{chartPath}");
            yield break;
            //TODO: ��������
        }
        var json = webRequest.downloadHandler.text;
        // ����
        webRequest.Dispose();
        var data = JsonUtility.FromJson<Chart>(json);
        complete?.Invoke(data);
    }
}
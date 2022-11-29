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

public class DataManager : MonoBehaviour
{
    public static DataManager Instance { get; private set; }

    //Tap 判定序列
    public List<TapScript> tapPaddingList = new List<TapScript>();

    //Drag 判定序列
    public List<DragScript> dragPaddingList = new List<DragScript>();

    //Hold 头部判定序列
    public List<HoldScript> holdHeadPaddingList = new List<HoldScript>();

    //Hold 判定序列
    public List<HoldScript> holdingPaddingList = new List<HoldScript>();

    //note下落速度
    public float speed = 1f;

    //public float holdTime = 0f;

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
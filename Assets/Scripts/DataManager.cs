using System;
using System.Collections;
using System.Collections.Generic;
using System.IO;
using UnityEditor;
using UnityEngine;

public class Chart
{
    public string name;
    public string difficulty;
    public short level;
    public int speed;
    public List<Note> notes;
}

[Serializable]
public class Note
{
    public int id;
    public int type;
    public float pos;
    public float size;
    public float time;
}

public class DataManager : MonoBehaviour
{
    public static DataManager Instance { get; private set; }

    //Tap 判定序列
    public List<TapScript> tapPaddingList = new List<TapScript>();

    //Drag 判定序列
    public List<DragScript> dragPaddingList = new List<DragScript>();

    //存储所有谱面（临时方案，后面可能采用 assetsbundle）
    public Dictionary<string, Chart> charts = new Dictionary<string, Chart>();

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

    public void LoadTestChart()
    {
        string[] filePaths = Directory.GetFiles($"{Application.dataPath}/Charts", "*.json", SearchOption.AllDirectories);
        string json = File.ReadAllText(filePaths[0]);
        var data = JsonUtility.FromJson<Chart>(json);
        charts.Add(data.name, data);
    }
}
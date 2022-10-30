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
    public int speed;
    public int count;
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

    //Tap �ж�����
    public List<TapScript> tapPaddingList = new List<TapScript>();

    //Drag �ж�����
    public List<DragScript> dragPaddingList = new List<DragScript>();

    //�洢�������棨��ʱ������������ܲ��� assetsbundle��
    //public Dictionary<string, Chart> charts = new Dictionary<string, Chart>();

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
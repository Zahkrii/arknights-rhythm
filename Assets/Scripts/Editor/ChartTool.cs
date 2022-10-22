using System;
using System.Collections;
using System.Collections.Generic;
using System.IO;
using UnityEditor;
using UnityEngine;

public class ChartOrigin
{
    public int speed;
    public List<NoteOrigin> notes;
    public List<LinkOrigin> links;
}

[Serializable]
public class NoteOrigin
{
    public int id;
    public int type;
    public float pos;
    public float size;
    public float _time;
    public int shift;
    public float time;
}

[Serializable]
public class LinkOrigin
{
    public List<LinkNoteOrigin> notes;
}

[Serializable]
public class LinkNoteOrigin
{
    public int id;
}

public class ChartTool : Editor
{
    [MenuItem("Tools/ConvertChart")]
    public static void ConvertChart()
    {
        List<ChartOrigin> chartOrigins = new List<ChartOrigin>();
        string[] filePaths = Directory.GetFiles($"{Application.dataPath}/Charts", "*.json", SearchOption.AllDirectories);
        foreach (string filePath in filePaths)
        {
            var json = File.ReadAllText(filePath);
            var data = JsonUtility.FromJson<ChartOrigin>(json);
            chartOrigins.Add(data);
        }
    }
}
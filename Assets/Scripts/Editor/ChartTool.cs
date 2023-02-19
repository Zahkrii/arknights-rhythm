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
        string[] filePaths = Directory.GetFiles($"{Application.dataPath}/Charts/origin", "*.json", SearchOption.AllDirectories);
        for (int j = 0; j < filePaths.Length; j++)
        {
            var json = File.ReadAllText(filePaths[j]);
            var data = JsonUtility.FromJson<ChartOrigin>(json);

            int holdStartIndex = -1;

            for (int i = 0; i < data.links.Count; i++) //循环读取links
            {
                foreach (var linknote in data.links[i].notes)//读取其中一个link，循环读取其中的notes
                {
                    for (int k = 0; k < data.notes.Count; k++)
                    {
                        if (data.notes[k].id == linknote.id)
                        {
                            data.notes[k].type = 1;
                        }
                        if (data.notes[k].size == 1.5f)//hold开始
                        {
                            data.notes[k].type = 2;
                            holdStartIndex = k;
                        }
                        if (data.notes[k].size == 0.5f)//hold结束
                        {
                            if (holdStartIndex >= 0)
                            {
                                data.notes[holdStartIndex]._time = data.notes[holdStartIndex].time - data.notes[k].time;
                                data.notes.RemoveAt(k);
                            }
                            holdStartIndex = -1;
                        }
                    }
                }
            }

            for (int k = 0; k < data.notes.Count; k++)
            {
                if (Mathf.Abs(data.notes[k].size - 1.5f) < Mathf.Epsilon)//hold开始
                {
                    data.notes[k].type = 2;
                    holdStartIndex = k;
                    continue;
                }
                if (Mathf.Abs(data.notes[k].size - 0.5f) < Mathf.Epsilon)//hold结束
                {
                    if (holdStartIndex >= 0)
                    {
                        data.notes[holdStartIndex]._time = data.notes[holdStartIndex].time - data.notes[k].time;
                        data.notes.RemoveAt(k);
                    }
                    holdStartIndex = -1;
                }
            }

            Chart newChart = new Chart();
            var filename = filePaths[j].Substring(filePaths[j].LastIndexOf('\\') + 1).Split('.');
            newChart.name = filename[0];
            newChart.difficulty = filename[1];
            newChart.level = short.Parse(filename[2]);
            newChart.notes = new List<Note>(data.notes.ConvertAll(e =>
            {
                return new Note
                {
                    id = e.id,
                    type = e.type,
                    pos = e.pos,
                    //size = e.size,
                    time = e.time,
                    holdTime = e._time
                };
            }));
            //转换完成，准备保存
            var newJson = JsonUtility.ToJson(newChart);
            File.WriteAllText($"{Application.streamingAssetsPath}/Charts/{filename[0]}.json", newJson);
            Debug.Log("Done.");
        }
    }
}
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

            for (int i = 0; i < data.links.Count; i++) //循环读取links
            {
                foreach (var linknote in data.links[i].notes)//读取其中一个link，循环读取其中的notes
                {
                    foreach (var note in data.notes)
                    {
                        if (note.id == linknote.id)
                        {
                            note.type = 1;
                        }
                    }
                }
            }
            Chart newChart = new Chart();
            var filename = filePaths[j].Substring(filePaths[j].LastIndexOf('\\') + 1).Split('.');
            newChart.name = filename[0];
            newChart.difficulty = filename[1];
            newChart.level = short.Parse(filename[2]);
            newChart.speed = data.speed;
            newChart.notes = new List<Note>(data.notes.ConvertAll(e =>
            {
                return new Note
                {
                    id = e.id,
                    type = e.type,
                    pos = e.pos,
                    size = e.size,
                    time = e.time,
                };
            }));
            //转换完成，准备保存
            var newJson = JsonUtility.ToJson(newChart);
            File.WriteAllText($"{Application.streamingAssetsPath}/Charts/{filename[0]}.json", newJson);
        }
    }
}
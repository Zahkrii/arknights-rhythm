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
            var filename = filePaths[j].Substring(filePaths[j].LastIndexOf('\\') + 1).Split('.');

            Debug.Log($"-- 开始转换第 {j + 1} 张谱面 --");
            Debug.Log($"[1/4] 谱面信息：{filename[0]}.{filename[1]}");

            var json = File.ReadAllText(filePaths[j]);
            var data = JsonUtility.FromJson<ChartOrigin>(json);

            //转换 Drag
            for (int i = 0; i < data.links.Count; i++) //循环读取links
            {
                for (int k = 0; k < data.links[i].notes.Count; k++)//读取其中一个link，循环读取其中的notes
                {
                    data.notes.Find(item => item.id == data.links[i].notes[k].id).type = 1;
                }
            }
            Debug.Log("[2/4] 转换 Drag 成功");

            //转换 Hold
            var holdStartList = data.notes.FindAll(item => Mathf.Abs(item.size - 1.5f) < Mathf.Epsilon);
            var holdEndList = data.notes.FindAll(item => Mathf.Abs(item.size - 0.5f) < Mathf.Epsilon);
            for (int i = 0; i < holdStartList.Count; i++)
            {
                data.notes.Find(item => item.id == holdStartList[i].id)._time = holdEndList[i].time - holdStartList[i].time;
                data.notes.Remove(data.notes.Find(item => item.id == holdEndList[i].id));
            }
            Debug.Log("[3/4] 转换 Hold 成功");

            //去除多余数据
            //for (int k = 0; k < data.notes.Count; k++)
            //{
            //    if (data.notes[k].type != 2)
            //    {
            //        data.notes[k]._time = 0;
            //    }
            //}
            var tmpList = data.notes.FindAll(item => item.type != 2);
            for (int i = 0; i < tmpList.Count; i++)
            {
                data.notes.Find(item => item.id == tmpList[i].id)._time = 0;
            }

            Debug.Log("[4/4] 冗余数据去除");

            Chart newChart = new Chart();

            newChart.name = filename[0];
            newChart.difficulty = (Difficulty)int.Parse(filename[1]);
            newChart.level = short.Parse(filename[2]);
            newChart.count = data.notes.Count;
            newChart.notes = new List<Note>(data.notes.ConvertAll(e =>
            {
                return new Note
                {
                    id = e.id,
                    type = e.type,
                    pos = e.pos,
                    time = e.time,
                    holdTime = e._time
                };
            }));
            //转换完成，准备保存
            var newJson = JsonUtility.ToJson(newChart);
            File.WriteAllText($"{Application.streamingAssetsPath}/Charts/{filename[0]}.{filename[1]}.json", newJson);

            Debug.Log("-- 转换成功 --");
        }
    }
}
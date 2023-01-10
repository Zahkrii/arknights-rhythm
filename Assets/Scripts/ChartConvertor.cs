using System;
using System.Collections;
using System.Collections.Generic;
using System.IO;
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

public class ChartConvertor : MonoBehaviour
{
    public static void ConvertChart()
    {
        string[] filePaths = Directory.GetFiles($"{Application.dataPath}/Charts/origin", "*.json", SearchOption.AllDirectories);
        for (int j = 0; j < filePaths.Length; j++)
        {
            Debug.Log($"-- ��ʼת���� {j + 1} ������ --");

            var json = File.ReadAllText(filePaths[j]);
            var data = JsonUtility.FromJson<ChartOrigin>(json);

            //ת�� Drag
            for (int i = 0; i < data.links.Count; i++) //ѭ����ȡlinks
            {
                for (int k = 0; k < data.links[i].notes.Count; k++)//��ȡ����һ��link��ѭ����ȡ���е�notes
                {
                    //for (int k = 0; k < data.notes.Count; k++)
                    //{
                    //    if (data.notes[k].id == linknote.id)//id ��ȱ���Ϊ Drag
                    //    {
                    //        data.notes[k].type = 1;
                    //    }
                    //}
                    data.notes.Find(item => item.id == data.links[i].notes[k].id).type = 1;
                }
                Debug.Log($"[1/4] ת�� Drag ���ȣ�{i / data.links.Count}%");
            }
            Debug.Log("[1/4] ת�� Drag �ɹ�");

            //ת�� Hold
            //for (int k = 0; k < data.notes.Count; k++)
            //{
            //    if (Mathf.Abs(data.notes[k].size - 1.5f) < Mathf.Epsilon)//hold��ʼ
            //    {
            //        data.notes[k].type = 2;
            //        holdStartIndex = k;
            //        continue;
            //    }
            //    if (Mathf.Abs(data.notes[k].size - 0.5f) < Mathf.Epsilon)//hold����
            //    {
            //        if (holdStartIndex >= 0)
            //        {
            //            data.notes[holdStartIndex]._time = data.notes[k].time - data.notes[holdStartIndex].time;
            //            data.notes.RemoveAt(k);
            //        }
            //        holdStartIndex = -1;
            //        k--;
            //    }
            //}
            var holdStartList = data.notes.FindAll(item => Mathf.Abs(item.size - 1.5f) < Mathf.Epsilon);
            var holdEndList = data.notes.FindAll(item => Mathf.Abs(item.size - 0.5f) < Mathf.Epsilon);
            for (int i = 0; i < holdStartList.Count; i++)
            {
                data.notes.Find(item => item.id == holdStartList[i].id)._time = holdEndList[i].time - holdStartList[i].time;
            }
            Debug.Log("[2/4] ת�� Hold �ɹ�");

            //ȥ����������
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

            Debug.Log("[3/4] ��������ȥ��");

            Chart newChart = new Chart();
            var filename = filePaths[j].Substring(filePaths[j].LastIndexOf('\\') + 1).Split('.');
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
            //ת����ɣ�׼������
            Debug.Log("[4/4] ת����ɣ�׼������");
            var newJson = JsonUtility.ToJson(newChart);
            File.WriteAllText($"{Application.streamingAssetsPath}/Charts/{filename[0]}.{filename[0]}.{filename[2]}.json", newJson);

            Debug.Log("-- ת���ɹ� --");
        }
    }
}
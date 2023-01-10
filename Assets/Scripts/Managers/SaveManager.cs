﻿using System;
using System.Collections.Generic;
using System.IO;
using System.IO.Compression;
using System.Linq;
using System.Text;
using UnityEngine;

public class SaveFile
{
    public string playerID; //玩家ID
    public float level = 0; //技术等级
    public List<ChartScore> chartScores; //分数列表
    public List<Opreator> opreators; //已解锁/获得的干员列表
}

[Serializable]
public class Opreator
{
    public OpreatorID id; //干员ID
    public short skinIndex = 0; //干员皮肤Index，从0开始
}

[Serializable]
public class ChartScore
{
    public short id; //曲目ID

    //简单难度（Easy）
    public Score scoreEZ;

    //普通难度（Normal）
    public Score scoreNM;

    //困难难度（Hard）
    public Score scoreHD;

    //额外难度（Extra）
    public Score scoreEX;

    //FC：全连，连击分满分
    //AP：All Perfect，判定分加连击分达到100万

    /// <summary>
    /// 一个方便设置分数的方法
    /// </summary>
    /// <param name="difficulty">曲目难度</param>
    /// <param name="paddingScore">判定分 [0,900000]</param>
    /// <param name="comboScore">连击分 [0,100000]</param>
    public void SetScore(Difficulty difficulty, float padding, float combo, float ranks)
    {
        int paddingScore = Mathf.CeilToInt(Mathf.Clamp(padding, 0, 900000));
        int comboScore = Mathf.CeilToInt(Mathf.Clamp(combo, 0, 100000));
        switch (difficulty)
        {
            case Difficulty.Easy:
                {
                    scoreEZ.padding = paddingScore;
                    scoreEZ.combo = comboScore;
                    scoreEZ.ranks = ranks;
                    break;
                }
            case Difficulty.Normal:
                {
                    scoreNM.padding = paddingScore;
                    scoreNM.combo = comboScore;
                    scoreNM.ranks = ranks;
                    break;
                }
            case Difficulty.Hard:
                {
                    scoreHD.padding = paddingScore;
                    scoreHD.combo = comboScore;
                    scoreHD.ranks = ranks;
                    break;
                }
            case Difficulty.Insane:
                {
                    scoreEX.padding = paddingScore;
                    scoreEX.combo = comboScore;
                    scoreEX.ranks = ranks;
                    break;
                }
        }
    }
}

[Serializable]
public class Score
{
    //判定分数，满分90万
    public int padding = 0;

    //连击分数，满分10万
    public int combo = 0;

    //perfect计数
    public float ranks = 0;
}

public static class SaveManager
{
    //存档名
    private const string FILENAME = "arknights_rhythm_CB05";

    //存档扩展名
    private const string FILENAME_EXTENSION = ".sav";

    //存档特征签名
    private const string FILE_MAGIC_NUMBER = "42";

    public static SaveFile PlayerSave { get; private set; }

    /// <summary>
    /// 初始化存档文件
    /// </summary>
    /// <param name="playerID">玩家ID</param>
    public static void Init(string playerID)
    {
        //新建存档
        SaveFile newSave = new SaveFile();
        newSave.playerID = playerID;
        //初始化曲目
        newSave.chartScores = new List<ChartScore>();
        foreach (ChartID id in Enum.GetValues(typeof(ChartID)))
        {
            newSave.chartScores.Add(new ChartScore { id = (short)id });
        }
        //初始化干员
        newSave.opreators = new List<Opreator>
        {
            new Opreator { id = OpreatorID.Amiya },
            new Opreator { id = OpreatorID.Logos },
            new Opreator { id = OpreatorID.Mountain }
        };
        //保存
        SaveToFile(newSave, FILENAME);
    }

    /// <summary>
    /// 读取存档
    /// </summary>
    public static void Open()
    {
        PlayerSave = LoadFromFile<SaveFile>(FILENAME);
    }

    /// <summary>
    /// 保存存档
    /// </summary>
    public static void Close()
    {
        if (PlayerSave != null)
            SaveToFile(PlayerSave, FILENAME);
        PlayerSave = null;
    }

    /// <summary>
    /// 判断存档是否存在
    /// </summary>
    /// <returns>存档是否存在</returns>
    public static bool PlayerSaveExists()
    {
        return File.Exists(Path.Combine(Application.persistentDataPath, $"{FILENAME}{FILENAME_EXTENSION}"));
    }

    #region Json操作

    /// <summary>
    /// 保存存档
    /// </summary>
    /// <param name="data">存档数据</param>
    /// <param name="saveFileName">存档名</param>
    private static void SaveToFile(object data, string saveFileName)
    {
        string path = Path.Combine(Application.persistentDataPath, $"{saveFileName}{FILENAME_EXTENSION}");
        string json = JsonUtility.ToJson(data);

        try
        {
            File.WriteAllText(path, GZipCompressString(json));
#if UNITY_EDITOR
            Debug.Log($"Save data succeed! \n{Application.persistentDataPath}");
#endif
        }
        catch (Exception e)
        {
#if UNITY_EDITOR
            Debug.LogError($"Save data failed!\nPath:{Application.persistentDataPath}\n{e}");
#endif
        }
    }

    /// <summary>
    /// 存档加载
    /// </summary>
    /// <typeparam name="T">存档类</typeparam>
    /// <param name="saveFileName">存档名</param>
    /// <returns>存档数据</returns>
    private static T LoadFromFile<T>(string saveFileName)
    {
        string path = Path.Combine(Application.persistentDataPath, $"{saveFileName}{FILENAME_EXTENSION}");

        try
        {
            string zippedJson = File.ReadAllText(path);
            if (zippedJson != null && zippedJson.Substring(0, 2).Equals(FILE_MAGIC_NUMBER))
            {
                string json = GZipDecompressString(zippedJson);
                var data = JsonUtility.FromJson<T>(json);
#if UNITY_EDITOR
                Debug.Log($"Load data succeed! \n{data}");
#endif
                return data;
            }
            return default;
        }
        catch (Exception e)
        {
#if UNITY_EDITOR
            Debug.LogError($"Load data failed!\nPath:{Application.persistentDataPath}\n{e}");
#endif
            return default;
        }
    }

    #endregion Json操作

    #region GZip解压缩

    /// <summary>
    /// GZip压缩+Base64+添加特征签名
    /// </summary>
    /// <param name="rawString">原始字符串</param>
    /// <returns>可存储的字符串</returns>
    private static string GZipCompressString(string rawString)
    {
        if (string.IsNullOrEmpty(rawString) || rawString.Length == 0)
        {
            return "";
        }
        else
        {
            byte[] rawData = Encoding.UTF8.GetBytes(rawString);
            return $"{FILE_MAGIC_NUMBER}{Convert.ToBase64String(Compress(rawData))}";
        }
    }

    /// <summary>
    /// 去除特征签名+Base64+GZip解压
    /// </summary>
    /// <param name="zippedString">存储的字符串</param>
    /// <returns>原始字符串</returns>
    private static string GZipDecompressString(string zippedString)
    {
        if (string.IsNullOrEmpty(zippedString) || zippedString.Length == 0)
        {
            return "";
        }
        else
        {
            byte[] zippedData = Convert.FromBase64String(zippedString.Remove(0, 2));
            return Encoding.UTF8.GetString(Decompress(zippedData));
        }
    }

    /// <summary>
    /// GZip压缩
    /// </summary>
    /// <param name="rawData">需要压缩的字符串</param>
    /// <returns>压缩后的字符串</returns>
    private static byte[] Compress(byte[] rawData)
    {
        MemoryStream ms = new MemoryStream();
        GZipStream compressedzipStream = new GZipStream(ms, CompressionMode.Compress, true);
        compressedzipStream.Write(rawData, 0, rawData.Length);
        compressedzipStream.Close();
        return ms.ToArray();
    }

    /// <summary>
    /// Gzip解压
    /// </summary>
    /// <param name="zippedData">压缩后的字符串</param>
    /// <returns>解压后的字符串</returns>
    private static byte[] Decompress(byte[] zippedData)
    {
        MemoryStream ms = new MemoryStream(zippedData);
        GZipStream compressedzipStream = new GZipStream(ms, CompressionMode.Decompress);
        MemoryStream outBuffer = new MemoryStream();
        byte[] block = new byte[1024];
        while (true)
        {
            int bytesRead = compressedzipStream.Read(block, 0, block.Length);
            if (bytesRead <= 0)
                break;
            else
                outBuffer.Write(block, 0, bytesRead);
        }
        compressedzipStream.Close();
        return outBuffer.ToArray();
    }

    #endregion GZip解压缩
}
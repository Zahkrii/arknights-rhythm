using System;
using System.Collections.Generic;
using UnityEngine;

// ������Դ�����˵����ļ���ΪfileName���˵�����ΪmenuName
[CreateAssetMenu(fileName = "NewAudioAsset", menuName = "Audio Asset")]
public class AudioAsset : ScriptableObject //����һ����Ч�б���Դ�����������չ�Լ�����
{
    //��Ч��Դ�б�
    public Dictionary<string, AudioClip> audioClip = new Dictionary<string, AudioClip>();

    [Header("��Ч��Դ�б�")]
    [SerializeField] private List<SerializableKeyValuePair> audioList = new List<SerializableKeyValuePair>();

    [Serializable]
    private struct SerializableKeyValuePair
    {
        public string Key;
        public AudioClip Value;

        public SerializableKeyValuePair(string key, AudioClip value)
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
        foreach (var item in audioList)
        {
            audioClip.Add(item.Key, item.Value);
        }
    }
}
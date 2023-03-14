using System;
using System.Collections.Generic;
using UnityEngine;

[Serializable]
public class AssistantData
{
    [Header("��ԱID")]
    public OpreatorID opreatorID;

    [Header("��������")]
    public Sprite AssistantSprite;

    [Header("����ɫ")]
    public Color accentColor;
}

// ������Դ�����˵����ļ���ΪfileName���˵�����ΪmenuName
[CreateAssetMenu(fileName = "NewPlayerCardData", menuName = "Player Card Data")]
public class PlayerCardDataAsset : ScriptableObject
{
    [Header("�����Ϣ������")]
    public List<AssistantData> assistantData;
}
using System;
using System.Collections.Generic;
using UnityEngine;

[Serializable]
public class AssistantData
{
    [Header("干员ID")]
    public OpreatorID opreatorID;

    [Header("助理立绘")]
    public Sprite AssistantSprite;

    [Header("主题色")]
    public Color accentColor;
}

// 创建资源创建菜单，文件名为fileName，菜单项名为menuName
[CreateAssetMenu(fileName = "NewPlayerCardData", menuName = "Player Card Data")]
public class PlayerCardDataAsset : ScriptableObject
{
    [Header("玩家信息卡数据")]
    public List<AssistantData> assistantData;
}
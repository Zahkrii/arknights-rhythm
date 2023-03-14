using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerCardDataLoader : MonoSingleton<PlayerCardDataLoader>
{
    [SerializeField] private PlayerCardDataAsset playerCardData;

    /// <summary>
    /// 获取所有助理信息
    /// </summary>
    /// <returns>助理信息的列表</returns>
    public List<AssistantData> GetDataList()
    {
        if (playerCardData != null)
            return playerCardData.assistantData;
#if UNITY_EDITOR
        Debug.LogError("找不到 PlayerCardDataAsset！请确认资源是否挂载！");
#endif
        return null;
    }

    /// <summary>
    /// 根据干员 ID 查找对应的助理数据
    /// </summary>
    /// <param name="id">干员 ID</param>
    /// <returns>干员 ID 对应的助理数据</returns>
    public AssistantData GetDataFromID(OpreatorID id)
    {
        if (playerCardData != null)
        {
            return playerCardData.assistantData.Find(item => item.opreatorID == id);
        }
#if UNITY_EDITOR
        Debug.LogError("找不到 PlayerCardDataAsset！请确认资源是否挂载！");
#endif
        return null;
    }

    /// <summary>
    /// 根据干员 ID 查找对应的助理立绘
    /// </summary>
    /// <param name="id">干员 ID</param>
    /// <returns>干员 ID 对应的助理立绘</returns>
    public Sprite GetSpriteFromID(OpreatorID id)
    {
        if (playerCardData != null)
        {
            return playerCardData.assistantData.Find(item => item.opreatorID == id).AssistantSprite;
        }
#if UNITY_EDITOR
        Debug.LogError("找不到 PlayerCardDataAsset！请确认资源是否挂载！");
#endif
        return null;
    }

    /// <summary>
    /// 根据干员 ID 查找对应的主题色
    /// </summary>
    /// <param name="id">干员 ID</param>
    /// <returns>干员 ID 对应的主题色</returns>
    public Color GetAccentColorFromID(OpreatorID id)
    {
        if (playerCardData != null)
        {
            return playerCardData.assistantData.Find(item => item.opreatorID == id).accentColor;
        }
#if UNITY_EDITOR
        Debug.LogError("找不到 PlayerCardDataAsset！请确认资源是否挂载！");
#endif
        return Color.white;
    }
}
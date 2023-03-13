using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerCardDataLoader : MonoSingleton<PlayerCardDataLoader>
{
    [SerializeField] private PlayerCardDataAsset playerCardData;

    /// <summary>
    /// ��ȡ����������Ϣ
    /// </summary>
    /// <returns>������Ϣ���б�</returns>
    public List<AssistantData> GetDataList()
    {
        if (playerCardData != null)
            return playerCardData.assistantData;
#if UNITY_EDITOR
        Debug.LogError("�Ҳ��� PlayerCardDataAsset����ȷ����Դ�Ƿ���أ�");
#endif
        return null;
    }

    /// <summary>
    /// ���ݸ�Ա ID ���Ҷ�Ӧ����������
    /// </summary>
    /// <param name="id">��Ա ID</param>
    /// <returns>��Ա ID ��Ӧ����������</returns>
    public AssistantData GetDataFromID(OpreatorID id)
    {
        if (playerCardData != null)
        {
            return playerCardData.assistantData.Find(item => item.opreatorID == id);
        }
#if UNITY_EDITOR
        Debug.LogError("�Ҳ��� PlayerCardDataAsset����ȷ����Դ�Ƿ���أ�");
#endif
        return null;
    }

    /// <summary>
    /// ���ݸ�Ա ID ���Ҷ�Ӧ����������
    /// </summary>
    /// <param name="id">��Ա ID</param>
    /// <returns>��Ա ID ��Ӧ����������</returns>
    public Sprite GetSpriteFromID(OpreatorID id)
    {
        if (playerCardData != null)
        {
            return playerCardData.assistantData.Find(item => item.opreatorID == id).AssistantSprite;
        }
#if UNITY_EDITOR
        Debug.LogError("�Ҳ��� PlayerCardDataAsset����ȷ����Դ�Ƿ���أ�");
#endif
        return null;
    }

    /// <summary>
    /// ���ݸ�Ա ID ���Ҷ�Ӧ������ɫ
    /// </summary>
    /// <param name="id">��Ա ID</param>
    /// <returns>��Ա ID ��Ӧ������ɫ</returns>
    public Color GetAccentColorFromID(OpreatorID id)
    {
        if (playerCardData != null)
        {
            return playerCardData.assistantData.Find(item => item.opreatorID == id).accentColor;
        }
#if UNITY_EDITOR
        Debug.LogError("�Ҳ��� PlayerCardDataAsset����ȷ����Դ�Ƿ���أ�");
#endif
        return Color.white;
    }
}
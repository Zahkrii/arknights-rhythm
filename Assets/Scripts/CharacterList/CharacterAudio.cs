using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CharacterAudio : MonoBehaviour
{
    [Header("语音类型")]
    public CharacterAudioType audioType;

    [Header("图片")]
    private GameObject selected;
    private GameObject notSelected;

    /// <summary>
    /// Awake is called when the script instance is being loaded.
    /// </summary>
    private void Awake()
    {
        selected = transform.Find("Selected " + transform.name).gameObject;
        notSelected = transform.Find("Not Selected " + transform.name).gameObject;
    }

    // 初始化
    public void ResetNotSelectedSprite()
    {
        selected.SetActive(false);
        notSelected.SetActive(true);
    }

    // 点击事件
    public void OnClick()
    {
        selected.SetActive(true);
        notSelected.SetActive(false);
        // 修改语音框
        CharacterDetailManager.instance.SetSpeakingUI(audioType);
    }
}

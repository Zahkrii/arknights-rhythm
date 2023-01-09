using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class CharacterDetailManager : MonoBehaviour
{
    public static CharacterDetailManager instance;

    [Header("Main Canvas")]
    public GameObject mainCanvas;

    [Header("干员数据")]
    [HideInInspector] public Character_SO characterData;
    [Header("干员详情UI")]
    public Image characterDetailSprite;
    public Image characterDetailNameSprite;
    [Header("干员正在播放语音")]
    public TextMeshProUGUI speakingAudioType;
    public TextMeshProUGUI speakingAudioDetail;
    [Header("干员语音列表")]
    public GameObject[] characterAudios;
    public Dictionary<CharacterAudioType, string> audioType = new Dictionary<CharacterAudioType, string>();
    [Header("查看立绘的Button")]
    public Transform checkSpriteButton;

    /// <summary>
    /// Awake is called when the script instance is being loaded.
    /// </summary>
    private void Awake()
    {
        if (instance == null)
            instance = this;
        else
        {
            if (instance != this)
                Destroy(this.gameObject);
        }

        audioType.Add(CharacterAudioType.Login, "登录语音");
        audioType.Add(CharacterAudioType.ChatFirst, "交谈 1");
        audioType.Add(CharacterAudioType.ChatSecond, "交谈 2");
        audioType.Add(CharacterAudioType.ChatThird, "交谈 3");
        audioType.Add(CharacterAudioType.GameFourStar, "获得四星评价");
        audioType.Add(CharacterAudioType.GameThreeStar, "获得三星评价");
        audioType.Add(CharacterAudioType.GameTwoStar, "获得二星评价");
        audioType.Add(CharacterAudioType.GameOneStar, "获得一星评价");
    }

    public void SetCharacterData(Character_SO characterDataSelected)
    {
        characterData = characterDataSelected;

        // 播放打开动画
        mainCanvas.GetComponent<Animator>().Play("OpenCharacterDetail");

        SetCharacterDetail();
    }

    private void SetCharacterDetail()
    {
        // 设置图片
        characterDetailSprite.sprite = characterData.characterSprite;
        characterDetailSprite.SetNativeSize();
        characterDetailNameSprite.sprite = characterData.characterNameSprite;
        characterDetailNameSprite.SetNativeSize();
        // 更改查看立绘按钮的位置
        checkSpriteButton.position = new Vector3(characterDetailNameSprite.transform.position.x + characterDetailNameSprite.GetComponent<RectTransform>().sizeDelta.x, checkSpriteButton.position.y, checkSpriteButton.position.z);
        // 设置正在播放语音
        speakingAudioType.text = "选择语音";
        speakingAudioDetail.text = string.Empty;
        // 初始化对话
        foreach (var characterAudio in characterAudios)
        {
            // 初始化未被选择图片
            characterAudio.GetComponent<CharacterAudio>().ResetNotSelectedSprite();
            // 添加监听事件
            characterAudio.GetComponent<Button>().onClick.AddListener(characterAudio.GetComponent<CharacterAudio>().OnClick);
        }

    }

    public void SetSpeakingUI(CharacterAudioType targetType)
    {
        // 设置UI
        speakingAudioType.text = audioType[targetType];
        // speakingAudioDetail.text = characterData.characterAudios.characterAudios[targetType];
        speakingAudioDetail.text = characterData.characterAudios[targetType];

        // 修改图片
        foreach (var characterAudio in characterAudios)
        {
            if (characterAudio.GetComponent<CharacterAudio>().audioType != targetType)
                characterAudio.GetComponent<CharacterAudio>().ResetNotSelectedSprite();
        }
    }

    # region 按钮事件

    public void CloseCharacterDetail()
    {
        // 播放打开动画
        mainCanvas.GetComponent<Animator>().Play("CloseCharacterDetail");
    }

    public void OpenCharacterSprite()
    {
        // 播放查看立绘动画
        mainCanvas.GetComponent<Animator>().Play("OpenCharacterSprite");
    }

    public void CloseCharacterSprite()
    {
        // 播放关闭立绘动画
        mainCanvas.GetComponent<Animator>().Play("CloseCharacterSprite");
    }

    # endregion
}

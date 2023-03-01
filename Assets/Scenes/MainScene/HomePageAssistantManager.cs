using System.Collections;
using System.Collections.Generic;
using TMPro;
//using UnityEditor.U2D.Animation;
//注意不要使用，以免导致build失败
using UnityEngine;

public class HomePageAssistantManager : MonoBehaviour
{
    [Header("干员数据")]
    [HideInInspector] public Character_SO characterData;


    public TextMeshProUGUI dialogTextTMP;

    private Opreator assistantID = new() { id = OpreatorID.Amiya };
    // Start is called before the first frame update
    void Start()
    {
        string tmp_id="CB05";
        
        //主界面的助手,默认为阿米娅的默认皮肤
        //badcode55：我认为精英化0（初始皮肤）是默认皮肤

        //查询非默认皮肤。
        if (SaveManager.PlayerSave.opreators.Exists(item => item.id == OpreatorID.Amiya)){
            assistantID = new Opreator { id = OpreatorID.Amiya };
            //默认助手：阿米娅 默认皮肤
        }
        else
        {
            Debug.Log($"Error! Assistant {assistantID.id} SkinIndex {assistantID.skinIndex} doesn't exsist!");
        }

        if (SaveManager.PlayerSaveExists())
        {
            SaveManager.Init(playerName: tmp_id);
            //初始化存档
        }


        //待修改为查询助手，这里先用阿米娅

        if (!SaveManager.PlayerSaveExists())
        {
            //第一次进入主界面
            //dialogTextTMP.text = characterData.characterAudios;
            //触发“hello”语音
        }
        else
        {
            SaveManager.Open();
            //读取存档

            //触发干员对话语音
        }
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void SaveAssisant()
    {

        //待补充
    }
}
